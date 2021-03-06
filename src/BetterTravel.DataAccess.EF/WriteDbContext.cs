﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.Domain.Base;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Utils;
using BetterTravel.DataAccess.Abstractions.Entities;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.EF.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class WriteDbContext : DbContext
    {
        private static readonly Type[] EnumerationTypes =
        {
            typeof(Country), 
            typeof(DepartureLocation),
            typeof(Currency)
        };

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHostEnvironment _environment;
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _dbConnectionString;

        public DbSet<HotTour> HotTours { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DepartureLocation> DepartureLocations { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        
        public WriteDbContext(
            IEventDispatcher eventDispatcher,
            IOptions<ConnectionStrings> connectionStringsOptions,
            IHostEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            _eventDispatcher = eventDispatcher;
            _environment = environment;
            _loggerFactory = loggerFactory;
            _dbConnectionString = connectionStringsOptions.Value.BetterTravelDb;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (_environment != null && (_environment.IsDevelopment() || _environment.IsStaging()))
                builder
                    .UseLoggerFactory(_loggerFactory)
                    .EnableSensitiveDataLogging();
            
            builder
                .UseSqlServer(
                    _dbConnectionString,
                    x => x.MigrationsAssembly(typeof(WriteDbContext).Assembly.FullName))
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries()
                .Where(x => EnumerationTypes.Contains(x.Entity.GetType()))
                .ToList()
                .ForEach(entity => entity.State = EntityState.Unchanged);

            var changedEntries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is Entity)
                .Select(x => (Entity) x.Entity)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            await changedEntries
                .OfType<AggregateRoot>()
                .Select(ProcessDomainEvents)
                .WhenAll();

            return result;
        }

        private async Task ProcessDomainEvents(AggregateRoot root)
        {
            await _eventDispatcher.DispatchAsync(root.DomainEvents);
            root.ClearDomainEvents();
        }
    }
}
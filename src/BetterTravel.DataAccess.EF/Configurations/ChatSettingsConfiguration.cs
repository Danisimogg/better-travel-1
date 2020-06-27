﻿using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class ChatSettingsConfiguration : IEntityTypeConfiguration<ChatSettings>
    {
        public void Configure(EntityTypeBuilder<ChatSettings> builder)
        {
            builder.ToTable(Tables.ChatSettings).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ChatSettingsID");

            builder.Property(p => p.SettingsOfChatId).HasColumnName("SettingsOfChatID");
            
            builder
                .HasOne(p => p.Chat)
                .WithOne(p => p.Settings)
                .HasForeignKey<ChatSettings>(p => p.SettingsOfChatId);
        }
    }
}
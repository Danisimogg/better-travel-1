﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetCountries
{
    public class GetCountriesQueryHandler 
        : QueryHandlerBase<GetCountriesQuery, List<GetCountriesViewModel>>
    {
        public override Task<IHandlerResult<List<GetCountriesViewModel>>> Handle(
            GetCountriesQuery request, 
            CancellationToken cancellationToken)
        {
            var countries = Country.AllCountries
                .Select(country => new GetCountriesViewModel
                {
                    Id = country.Id, 
                    Name = request.Localize 
                        ? L.GetValue(country.Name, Culture.Ru)
                        : country.Name
                })
                .ToList();

            return Task.FromResult(Ok(countries));
        }
    }
}
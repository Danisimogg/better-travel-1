﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.Abstractions;

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

            return Task.FromResult(Data(countries));
        }
    }
}
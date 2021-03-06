﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.Application.HotToursFetcher.Requests;
using BetterTravel.Application.HotToursFetcher.Responses;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstractions.Entities;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.DataAccess.Abstractions.ValueObjects;
using Microsoft.Extensions.Options;
using Refit;

namespace BetterTravel.Application.HotToursFetcher
{
    public class HotToursProvider : IHotToursProvider
    {
        private readonly IHotToursProviderApi _api;

        public HotToursProvider(IOptions<ThirdPartyServices> thirdPartyOptions) =>
            _api = RestService.For<IHotToursProviderApi>(thirdPartyOptions.Value.HotToursProviderUrl);

        public async Task<List<HotTour>> GetHotToursAsync(HotToursProviderQuery providerQuery)
        {
            var request = new HotToursRequest(providerQuery);
            var response = await _api.HotTours(request);

            return response.TourListItems
                .Select(MapResponse)
                .ToList();
        }

        private static HotTour MapResponse(TourListItemResponse source) =>
            new HotTour(
                new HotTourInfo(
                    source.TourName,
                    source.ImageUrl,
                    source.TourDetailsUrl),
                GetCategory((short) source.StarsCount.Count),
                new Duration(
                    source.DurationDay,
                    GetDurationType(source.NightsText)),
                Price.FromUah(
                    source.PriceFrom,
                    GetPriceType(source.PriceDescription)),
                GetCountry(source.CountryLinks.Links.FirstOrDefault()?.Text),
                new Resort(source.ResortLinks.Links.FirstOrDefault()?.Text),
                GetDepartureLocation(source.DeparturePointNameGenitive),
                GetDate(source.Date));

        private static Country GetCountry(string source)
        {
            var name = L.GetName(source, Culture.Ru, true);
            return Country.FromName(name);
        }

        private static DepartureLocation GetDepartureLocation(string source)
        {
            var name = L.GetName(source, Culture.Ru, true);
            return DepartureLocation.FromName(name);
        }

        private static PriceType GetPriceType(string source) =>
            source switch
            {
                "за 1 человека" => PriceType.Single,
                _ => PriceType.Unknown
            };

        private static DateTime GetDate(string source)
        {
            const string pattern = "dd.MM.yyyy";
            DateTime.TryParseExact(source, pattern, null, DateTimeStyles.None, out var result);
            return result;
        }

        private static DurationType GetDurationType(string source) =>
            source switch
            {
                "ночей" => DurationType.Nights,
                _ => DurationType.Unknown
            };

        private static HotelCategoryType GetCategory(short starsCount) =>
            Enum.IsDefined(typeof(HotelCategoryType), starsCount)
                ? (HotelCategoryType) starsCount
                : HotelCategoryType.NoCategory;
    }
}
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.SettingsCountries.Keyboard;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Abstractions.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsCountries
{
    public class SettingsCountriesCommandHandler : TelegramCommandHandler<SettingsCountriesCommand>
    {
        private const string KeyboardMessage = "You can choose the countries from which you will receive updates";

        public SettingsCountriesCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(
            SettingsCountriesCommand request,
            CancellationToken ctx)
        {
            Maybe<Chat> maybeChat = await UnitOfWork.ChatWriteRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId);
            
            return await maybeChat
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(() => EditMessageTextAsync(request.ChatId, request.MessageId, KeyboardMessage, ctx))
                .Tap(markup => EditMessageReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());
        }

        private static Result<List<SettingsCountryKeyboardData>> GetKeyboardDataResult(Chat chat) =>
            Result
                .Success(Country.AllCountries
                .Select(country => new SettingsCountryKeyboardData
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsSubscribed = chat.Settings.CountrySubscriptions.Any(cs => cs.Country == country)
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsCountryKeyboardData> data) => 
            Result.Success(new SettingsCountryKeyboard().ConcreteKeyboardMarkup(data));
    }
}
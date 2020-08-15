﻿using System.Collections.Generic;
using System.Linq;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsBack;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountryToggle;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.TelegramUpdate.Function.Keyboards.Factories
{
    public class SettingsCountryKeyboard : KeyboardFactoryBase<List<SettingsCountryKeyboardData>>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsCountryKeyboardData> data)
        {
            var lines = data
                .Select((d, idx) => new {Data = d, Index = idx})
                .GroupBy(t => t.Index / 2)
                .Select(g => g.Select(gi => gi.Data))
                .Select(GetCountryButtonsLines)
                .Append(Line(Button("<< Back", nameof(SettingsBackCommand))))
                .ToArray();
            var markup = Markup(lines);
            return new InlineKeyboardMarkup(markup);
        }

        private InlineKeyboardButton[] GetCountryButtonsLines(
            IEnumerable<SettingsCountryKeyboardData> countriesData) =>
            Line(countriesData.Select(GetCountryButton).ToArray());

        private InlineKeyboardButton GetCountryButton(SettingsCountryKeyboardData data) =>
            Button(
                data.IsSubscribed
                    ? $"\U00002714 {data.Name}"
                    : $"\U00002796 {data.Name}",
                $"{nameof(SettingsCountryToggleCommand)}:{data.Id}");
    }
}
﻿using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommand : ICommand
    {
        public long ChatId { get; set; }
        public bool IsBot { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ChatType Type { get; set; }
    }
}
﻿using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Price : ValueObject<Price>
    {
        protected Price()
        {
        }
        
        public Price(int amount, PriceType type)
        {
            Amount = amount;
            Type = type;
        }

        public int Amount { get; }
        public PriceType Type { get; }
        
        protected override int GetHashCodeCore() => 
            Amount.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Price other) => 
            Amount == other.Amount && Type == other.Type;
    }
}
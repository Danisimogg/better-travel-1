﻿using System;
using System.Linq;
using BetterExtensions.Domain.Base;

namespace BetterTravel.DataAccess.Abstractions.Entities.Enumerations
{
    public class DepartureLocation : Entity
    {
        public static DepartureLocation NoDeparture = new DepartureLocation(9999, nameof(NoDeparture));
        public static DepartureLocation Kyiv = new DepartureLocation(1, nameof(Kyiv));
        public static DepartureLocation Zaporizhzhia = new DepartureLocation(4, nameof(Zaporizhzhia));
        public static DepartureLocation Lviv = new DepartureLocation(6, nameof(Lviv));
        public static DepartureLocation Odessa = new DepartureLocation(8, nameof(Odessa));
        public static DepartureLocation Kharkiv = new DepartureLocation(11, nameof(Kharkiv));
        public static DepartureLocation Kherson = new DepartureLocation(41, nameof(Kherson));
        public static DepartureLocation Chernivtsi = new DepartureLocation(112, nameof(Chernivtsi));
        public static DepartureLocation Dnipro = new DepartureLocation(2, nameof(Dnipro));
        public static DepartureLocation IvanoFrankivsk = new DepartureLocation(85, nameof(IvanoFrankivsk));


        public static readonly DepartureLocation[] AllDepartures =
        {
            NoDeparture, Kyiv, Zaporizhzhia, Lviv,
            Odessa, Kharkiv, Kherson, Chernivtsi,
            Dnipro, IvanoFrankivsk
        };

        protected DepartureLocation()
        {
        }

        private DepartureLocation(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; }

        public static DepartureLocation FromId(int id) =>
            AllDepartures.SingleOrDefault(country => country.Id == id)
            ?? NoDeparture;

        public static DepartureLocation FromName(string name) =>
            AllDepartures.SingleOrDefault(country =>
                string.Equals(country.Name, name, StringComparison.InvariantCultureIgnoreCase))
            ?? NoDeparture;
    }
}
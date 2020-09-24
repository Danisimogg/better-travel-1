using System;
using System.Linq;
using BetterExtensions.Domain.Base;

namespace BetterTravel.DataAccess.Abstractions.Entities.Enumerations
{
    public class Country : Entity
    {
        public static Country NoCountry = new Country(9999, nameof(NoCountry));
        public static Country Egypt = new Country(11, nameof(Egypt));
        public static Country Turkey = new Country(36, nameof(Turkey));
        public static Country Ukraine = new Country(37, nameof(Ukraine));
        public static Country Spain = new Country(51, nameof(Spain));
        public static Country Bulgaria = new Country(54, nameof(Bulgaria));
        public static Country Hungary = new Country(55, nameof(Hungary));
        public static Country Croatia = new Country(57, nameof(Croatia));
        public static Country Tunisia = new Country(61, nameof(Tunisia));
        public static Country Dominican = new Country(62, nameof(Dominican));
        public static Country UAE = new Country(70, nameof(UAE));
        public static Country Greece = new Country(84, nameof(Greece));
        public static Country Montenegro = new Country(155, nameof(Montenegro));
        public static Country Georgia = new Country(169, nameof(Georgia));
        public static Country Australia = new Country(135, nameof(Australia));
        public static Country Austria = new Country(75, nameof(Austria));
        public static Country Azerbaijan = new Country(290, nameof(Azerbaijan));
        public static Country Albania = new Country(591, nameof(Albania));
        public static Country Algeria = new Country(225, nameof(Algeria));
        public static Country Angola = new Country(637, nameof(Angola));
        public static Country Andorra = new Country(93, nameof(Andorra));
        public static Country Antigua = new Country(311, nameof(Antigua));
        public static Country Argentina = new Country(113, nameof(Argentina));
        public static Country Armenia = new Country(601, nameof(Armenia));
        public static Country Aruba = new Country(700, nameof(Aruba));
        public static Country Afghanistan = new Country(605, nameof(Afghanistan));
        public static Country Bahama_islands = new Country(151, nameof(Bahama_islands));
        public static Country Bangladesh = new Country(609, nameof(Bangladesh));
        public static Country Barbados = new Country(121, nameof(Barbados));
        public static Country Bahrain = new Country(698, nameof(Bahrain));
        public static Country Belarus = new Country(164, nameof(Belarus));
        public static Country Belize = new Country(133, nameof(Belize));
        public static Country Belgium = new Country(76, nameof(Belgium));
        public static Country Benin = new Country(645, nameof(Benin));
        public static Country Bolivia = new Country(326, nameof(Bolivia));
        public static Country Bosnia_Herzegovina = new Country(592, nameof(Bosnia_Herzegovina));
        public static Country Botswana = new Country(137, nameof(Botswana));
        public static Country Brazil = new Country(115, nameof(Brazil));
        public static Country Brunei = new Country(610, nameof(Brunei));
        public static Country Burkina_Faso = new Country(661, nameof(Burkina_Faso));
        public static Country Burundi = new Country(658, nameof(Burundi));
        public static Country Bhutan = new Country(608, nameof(Bhutan));
        public static Country Vanuatu = new Country(614, nameof(Vanuatu));
        public static Country Vatican = new Country(162, nameof(Vatican));
        public static Country Great_Britain = new Country(68, nameof(Great_Britain));
        public static Country Venezuela = new Country(143, nameof(Venezuela));
        public static Country British_Virgin_Islands = new Country(574, nameof(British_Virgin_Islands));
        public static Country East_Timor = new Country(656, nameof(East_Timor));
        public static Country Vietnam = new Country(74, nameof(Vietnam));
        public static Country Gabon = new Country(635, nameof(Gabon));
        public static Country Hawaiian_Islands = new Country(702, nameof(Hawaiian_Islands));
        public static Country Haiti = new Country(572, nameof(Haiti));
        public static Country Guyana = new Country(582, nameof(Guyana));
        public static Country Gambia = new Country(654, nameof(Gambia));
        public static Country Ghana = new Country(647, nameof(Ghana));
        public static Country Guatemala = new Country(132, nameof(Guatemala));
        public static Country Guinea = new Country(651, nameof(Guinea));
        public static Country Guinea_Bissau = new Country(652, nameof(Guinea_Bissau));
        public static Country Germany = new Country(78, nameof(Germany));
        public static Country Honduras = new Country(131, nameof(Honduras));
        public static Country Hong_Kong = new Country(713, nameof(Hong_Kong));
        public static Country Grenada = new Country(716, nameof(Grenada));
        public static Country Denmark = new Country(92, nameof(Denmark));
        public static Country Djibouti = new Country(625, nameof(Djibouti));
        public static Country Zambia = new Country(138, nameof(Zambia));
        public static Country West_Sahara = new Country(618, nameof(West_Sahara));
        public static Country Zimbabwe = new Country(139, nameof(Zimbabwe));
        public static Country Israel = new Country(101, nameof(Israel));

       
        public static readonly Country[] AllCountries =
        {
            NoCountry, Egypt, Turkey, Ukraine, Spain, Bulgaria,
            Hungary, Croatia, Tunisia, Dominican, UAE, Greece,
            Montenegro, Georgia, Albania, Australia,
            Austria, Azerbaijan, Angola, Algeria, Andorra,
            Antigua, Argentina, Armenia, Aruba, Afghanistan,
            Bahama_islands, Bangladesh, Bahrain, Belarus, Belize,
            Belgium, Benin, Bolivia, Bosnia_Herzegovina, Botswana,
            Brazil, Brunei, Burkina_Faso, Burundi, Bhutan, Vanuatu, Vatican,
            Great_Britain, Venezuela, British_Virgin_Islands, East_Timor,
            Vietnam, Gabon, Hawaiian_Islands, Haiti, Guyana, Gambia, Ghana, 
            Guatemala, Guinea, Guinea_Bissau, Germany, Honduras, Hong_Kong,
            Grenada, Denmark, Zambia, Djibouti, West_Sahara, Israel, Zimbabwe,
        };

        protected Country()
        {
        }

        private Country(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; }

        public static Country FromId(int id) =>
            AllCountries.SingleOrDefault(country => country.Id == id)
            ?? NoCountry;

        public static Country FromName(string name) =>
            AllCountries.SingleOrDefault(country =>
                string.Equals(country.Name, name, StringComparison.InvariantCultureIgnoreCase))
            ?? NoCountry;
    }
}
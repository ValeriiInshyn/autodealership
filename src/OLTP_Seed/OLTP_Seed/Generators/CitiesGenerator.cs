using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLTP_Seed.Models;

namespace OLTP_Seed.Generators
{
    public static class CitiesGenerator
    {
        public static Dictionary<string, List<string>> GenerateCities()
        {
            Dictionary<string, List<string>> citiesByCountry = new Dictionary<string, List<string>>()
            {
                { "Japan", new List<string> { "Tokyo" } },
                { "USA", new List<string> { "New York City", "Los Angeles", "Chicago", "Washington D.C." } },
                { "UK", new List<string> { "London" } },
                { "France", new List<string> { "Paris" } },
                { "China", new List<string> { "Beijing" } },
                { "Russia", new List<string> { "Moscow", "Saint Petersburg" } },
                { "India", new List<string> { "Mumbai", "Delhi", "Kolkata", "Chennai", "Bengaluru", "Hyderabad" } },
                { "Brazil", new List<string> { "São Paulo", "Rio de Janeiro" } },
                { "Turkey", new List<string> { "Istanbul" } },
                { "Egypt", new List<string> { "Cairo" } },
                // Add other countries and cities here...
                { "Nigeria", new List<string> { "Abuja", "Lagos" } },
                { "Mexico", new List<string> { "Mexico City" } },
                { "Argentina", new List<string> { "Buenos Aires" } },
                { "Pakistan", new List<string> { "Karachi", "Lahore" } },
                { "Philippines", new List<string> { "Manila" } },
                { "South Korea", new List<string> { "Seoul" } },
                { "Indonesia", new List<string> { "Jakarta" } },
                { "Bangladesh", new List<string> { "Dhaka" } },
                { "Iran", new List<string> { "Tehran" } },
                { "Thailand", new List<string> { "Bangkok" } },
                { "Peru", new List<string> { "Lima" } },
                { "Colombia", new List<string> { "Bogotá" } },
                { "Taiwan", new List<string> { "Taipei" } },
                { "Vietnam", new List<string> { "Ho Chi Minh City" } },
                { "South Africa", new List<string> { "Johannesburg", "Cape Town", "Pretoria" } },
                { "Malaysia", new List<string> { "Kuala Lumpur" } },
                { "Spain", new List<string> { "Madrid", "Barcelona" } },
                { "Saudi Arabia", new List<string> { "Riyadh" } },
                { "Singapore", new List<string> { "Singapore" } },
                { "Canada", new List<string> { "Toronto" } },
                { "Australia", new List<string> { "Sydney", "Melbourne" } },
                { "Netherlands", new List<string> { "Amsterdam" } },
                { "Italy", new List<string> { "Rome", "Milan" } },
                { "Germany", new List<string> { "Berlin", "Munich", "Frankfurt", "Hamburg" } },
                { "Austria", new List<string> { "Vienna" } },
                { "Switzerland", new List<string> { "Zurich" } },
                { "Sweden", new List<string> { "Stockholm" } },
                { "Denmark", new List<string> { "Copenhagen" } },
                { "Norway", new List<string> { "Oslo" } },
                { "Finland", new List<string> { "Helsinki" } },
                { "Ireland", new List<string> { "Dublin" } },
                { "Hungary", new List<string> { "Budapest" } },
                { "Czech Republic", new List<string> { "Prague" } },
                { "Poland", new List<string> { "Warsaw" } },
                { "Portugal", new List<string> { "Lisbon" } },
                { "Greece", new List<string> { "Athens" } },
                { "Romania", new List<string> { "Bucharest" } },
                { "Ukraine", new List<string> { "Kyiv", "Lviv", "Kharkiv" } },
                { "Belarus", new List<string> { "Minsk" } },
                { "Bulgaria", new List<string> { "Sofia" } },
                { "Serbia", new List<string> { "Belgrade" } },
                { "Slovakia", new List<string> { "Bratislava" } },
                { "Croatia", new List<string> { "Zagreb" } },
                { "Slovenia", new List<string> { "Ljubljana" } },
                { "Bosnia and Herzegovina", new List<string> { "Sarajevo" } },
                { "North Macedonia", new List<string> { "Skopje" } },
                { "Albania", new List<string> { "Tirana" } },
                { "Montenegro", new List<string> { "Podgorica" } },
                { "Kosovo", new List<string> { "Pristina" } },
                { "Luxembourg", new List<string> { "Luxembourg City" } },
                { "Malta", new List<string> { "Valletta" } },
                { "Andorra", new List<string> { "Andorra la Vella" } },
                { "Iceland", new List<string> { "Reykjavik" } },
                { "San Marino", new List<string> { "San Marino" } },
                { "Liechtenstein", new List<string> { "Vaduz" } },
                { "Monaco", new List<string> { "Monaco" } },
                { "Vatican City", new List<string> { "Vatican City" } },
                { "Djibouti", new List<string> { "Djibouti City" } },
                { "Ethiopia", new List<string> { "Addis Ababa" } },
                { "Ghana", new List<string> { "Accra" } },
                { "Kenya", new List<string> { "Nairobi" } },
                { "Tanzania", new List<string> { "Dar es Salaam", "Dodoma" } },
                { "Uganda", new List<string> { "Kampala" } },
                { "Zambia", new List<string> { "Lusaka" } },
                { "Zimbabwe", new List<string> { "Harare" } },
                { "Madagascar", new List<string> { "Antananarivo" } },
                { "Mauritius", new List<string> { "Port Louis" } },
                { "Mozambique", new List<string> { "Maputo" } },
                { "Namibia", new List<string> { "Windhoek" } },
                { "Malawi", new List<string> { "Lilongwe" } },
                { "Gabon", new List<string> { "Libreville" } },
                { "Congo", new List<string> { "Brazzaville" } },
                { "Democratic Republic of the Congo", new List<string> { "Kinshasa" } }
            };

            return citiesByCountry;
        }
    }
}
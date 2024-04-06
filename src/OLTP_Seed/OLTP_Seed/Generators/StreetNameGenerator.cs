using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class StreetNameGenerator
    {
        private static string[] prefixes =
        {
            "Main", "Oak", "Maple", "Elm", "Cedar", "Pine", "Spruce", "Hill", "Lake", "Sunset", "River", "Park",
            "Grove", "Forest", "Meadow", "Valley"
        };

        private static string[] suffixes =
        {
            "Street", "Avenue", "Lane", "Road", "Boulevard", "Court", "Way", "Drive", "Circle", "Place", "Terrace",
            "Trail"
        };

        private static Random random = new Random();

        public static string GenerateStreetName()
        {
            string prefix = prefixes[random.Next(prefixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];

            return prefix + " " + suffix;
        }
    }
}

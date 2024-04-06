using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class GearboxTypeGenerator
    {
        private static Random random = new Random();

        public static string GenerateGearboxType()
        {
            // Randomly select between automatic and manual
            return random.Next(2) == 0 ? "Automatic" : "Manual";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class EngineTypeGenerator
    {
        private static string[] engineTypes = { "Electric", "Petrol", "Diesel", "Gas" };

        private static Random random = new Random();

        public static List<string> GenerateEngineTypesList()
        {
            return engineTypes.ToList();
        }

        public static string GenerateEngineType()
        {
            return engineTypes[random.Next(engineTypes.Length)];
        }
    }
}

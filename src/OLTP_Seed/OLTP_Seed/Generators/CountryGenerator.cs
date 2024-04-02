using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using OLTP_Seed.Models;

namespace OLTP_Seed.Generators
{
    public static class CountryGenerator
    {
        public static List<string> GenerateCountriesList()
        {
            List<string> CultureList = new();
            CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo culture in cultureInfos)
            {
                RegionInfo regionInfo = new RegionInfo(culture.Name);
                if (!(CultureList.Contains(regionInfo.EnglishName)))
                {
                    CultureList.Add(regionInfo.EnglishName);
                }
            }

            return CultureList;
        }
    }
}

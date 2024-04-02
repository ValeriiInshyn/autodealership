﻿using OLTP_Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class BrandDtoGenerator
    {
        public static List<BrandDto> GenerateBrandDtos()
        {
            List<BrandDto> carBrandDtos = new List<BrandDto>()
        {
            new BrandDto() { Id = 1, Name = "Toyota", CountryName = "Japan" },
            new BrandDto() { Id = 2, Name = "Honda", CountryName = "Japan" },
            new BrandDto() { Id = 3, Name = "Nissan", CountryName = "Japan" },
            new BrandDto() { Id = 4, Name = "Mazda", CountryName = "Japan" },
            new BrandDto() { Id = 5, Name = "Subaru", CountryName = "Japan" },
            new BrandDto() { Id = 6, Name = "Mitsubishi", CountryName = "Japan" },
            new BrandDto() { Id = 7, Name = "Suzuki", CountryName = "Japan" },
            new BrandDto() { Id = 8, Name = "Lexus", CountryName = "Japan" },
            new BrandDto() { Id = 9, Name = "Infiniti", CountryName = "Japan" },
            new BrandDto() { Id = 10, Name = "Acura", CountryName = "Japan" },
            new BrandDto() { Id = 11, Name = "Toyota", CountryName = "Japan" },
            new BrandDto() { Id = 12, Name = "Lamborghini", CountryName = "Italy" },
            new BrandDto() { Id = 13, Name = "Ferrari", CountryName = "Italy" },
            new BrandDto() { Id = 14, Name = "Maserati", CountryName = "Italy" },
            new BrandDto() { Id = 15, Name = "Alfa Romeo", CountryName = "Italy" },
            new BrandDto() { Id = 16, Name = "Fiat", CountryName = "Italy" },
            new BrandDto() { Id = 17, Name = "Lancia", CountryName = "Italy" },
            new BrandDto() { Id = 18, Name = "Pagani", CountryName = "Italy" },
            new BrandDto() { Id = 19, Name = "Bugatti", CountryName = "France" },
            new BrandDto() { Id = 20, Name = "Renault", CountryName = "France" },
            new BrandDto() { Id = 21, Name = "Peugeot", CountryName = "France" },
            new BrandDto() { Id = 22, Name = "Citroën", CountryName = "France" },
            new BrandDto() { Id = 23, Name = "DS Automobiles", CountryName = "France" },
            new BrandDto() { Id = 24, Name = "Dacia", CountryName = "Romania" },
            new BrandDto() { Id = 25, Name = "Skoda", CountryName = "Czech Republic" },
            new BrandDto() { Id = 26, Name = "Volkswagen", CountryName = "Germany" },
            new BrandDto() { Id = 27, Name = "BMW", CountryName = "Germany" },
            new BrandDto() { Id = 28, Name = "Mercedes-Benz", CountryName = "Germany" },
            new BrandDto() { Id = 29, Name = "Audi", CountryName = "Germany" },
            new BrandDto() { Id = 30, Name = "Porsche", CountryName = "Germany" },
            new BrandDto() { Id = 31, Name = "Opel", CountryName = "Germany" },
            new BrandDto() { Id = 32, Name = "Ford", CountryName = "United States" },
            new BrandDto() { Id = 33, Name = "Chevrolet", CountryName = "United States" },
            new BrandDto() { Id = 34, Name = "Dodge", CountryName = "United States" },
            new BrandDto() { Id = 35, Name = "Jeep", CountryName = "United States" },
            new BrandDto() { Id = 36, Name = "Tesla", CountryName = "United States" },
            new BrandDto() { Id = 37, Name = "Cadillac", CountryName = "United States" },
            new BrandDto() { Id = 38, Name = "GMC", CountryName = "United States" },
            new BrandDto() { Id = 39, Name = "Ram", CountryName = "United States" },
            new BrandDto() { Id = 40, Name = "Buick", CountryName = "United States" },
            new BrandDto() { Id = 41, Name = "Lincoln", CountryName = "United States" },
            new BrandDto() { Id = 42, Name = "Chrysler", CountryName = "United States" },
            new BrandDto() { Id = 43, Name = "Bentley", CountryName = "United Kingdom" },
            new BrandDto() { Id = 44, Name = "Rolls-Royce", CountryName = "United Kingdom" },
            new BrandDto() { Id = 45, Name = "Jaguar", CountryName = "United Kingdom" },
            new BrandDto() { Id = 46, Name = "Land Rover", CountryName = "United Kingdom" },
            new BrandDto() { Id = 47, Name = "Mini", CountryName = "United Kingdom" },
            new BrandDto() { Id = 48, Name = "Aston Martin", CountryName = "United Kingdom" },
            new BrandDto() { Id = 49, Name = "Lotus", CountryName = "United Kingdom" },
            new BrandDto() { Id = 50, Name = "McLaren", CountryName = "United Kingdom" },
            new BrandDto() { Id = 51, Name = "Volvo", CountryName = "Sweden" },
            new BrandDto() { Id = 52, Name = "Koenigsegg", CountryName = "Sweden" },
            new BrandDto() { Id = 53, Name = "Saab", CountryName = "Sweden" },
            new BrandDto() { Id = 54, Name = "Geely", CountryName = "China" },
            new BrandDto() { Id = 55, Name = "BYD", CountryName = "China" },
            new BrandDto() { Id = 56, Name = "Chery", CountryName = "China" },
            new BrandDto() { Id = 57, Name = "Great Wall", CountryName = "China" },
            new BrandDto() { Id = 58, Name = "Haval", CountryName = "China" },
            new BrandDto() { Id = 59, Name = "Dongfeng", CountryName = "China" },
            new BrandDto() { Id = 60, Name = "Zotye", CountryName = "China" },
            new BrandDto() { Id = 61, Name = "BAIC", CountryName = "China" },
            new BrandDto() { Id = 62, Name = "FAW", CountryName = "China" },
            new BrandDto() { Id = 63, Name = "SAIC", CountryName = "China" },
            new BrandDto() { Id = 64, Name = "JAC", CountryName = "China" },
            new BrandDto() { Id = 65, Name = "Landwind", CountryName = "China" },
            new BrandDto() { Id = 66, Name = "Chang'an", CountryName = "China" },
            new BrandDto() { Id = 67, Name = "GAC", CountryName = "China" },
            new BrandDto() { Id = 68, Name = "NIO", CountryName = "China" },
            new BrandDto() { Id = 69, Name = "Lynk & Co", CountryName = "China" },
            new BrandDto() { Id = 70, Name = "Polestar", CountryName = "China" },
            new BrandDto() { Id = 71, Name = "Haval", CountryName = "China" },
            new BrandDto() { Id = 72, Name = "Wuling", CountryName = "China" },
            new BrandDto() { Id = 73, Name = "Hongqi", CountryName = "China" },
            new BrandDto() { Id = 74, Name = "Lifan", CountryName = "China" },
            new BrandDto() { Id = 75, Name = "Soueast", CountryName = "China" },
            new BrandDto() { Id = 76, Name = "Xpeng", CountryName = "China" },
            new BrandDto() { Id = 77, Name = "WEY", CountryName = "China" },
            new BrandDto() { Id = 78, Name = "Luxgen", CountryName = "Taiwan" },
            new BrandDto() { Id = 79, Name = "Tata Motors", CountryName = "India" },
            new BrandDto() { Id = 80, Name = "Mahindra", CountryName = "India" },
            new BrandDto() { Id = 81, Name = "Maruti Suzuki", CountryName = "India" },
            new BrandDto() { Id = 82, Name = "Force Motors", CountryName = "India" },
            new BrandDto() { Id = 83, Name = "Premier", CountryName = "India" },
            new BrandDto() { Id = 84, Name = "Kia", CountryName = "South Korea" },
            new BrandDto() { Id = 85, Name = "Hyundai", CountryName = "South Korea" },
            new BrandDto() { Id = 86, Name = "Genesis", CountryName = "South Korea" },
            new BrandDto() { Id = 87, Name = "SsangYong", CountryName = "South Korea" },
            new BrandDto() { Id = 88, Name = "Daewoo", CountryName = "South Korea" },
            new BrandDto() { Id = 89, Name = "Proton", CountryName = "Malaysia" },
            new BrandDto() { Id = 90, Name = "Perodua", CountryName = "Malaysia" },
            new BrandDto() { Id = 91, Name = "VinFast", CountryName = "Vietnam" },
            new BrandDto() { Id = 92, Name = "GAZ", CountryName = "Russia" },
            new BrandDto() { Id = 93, Name = "VAZ", CountryName = "Russia" },
            new BrandDto() { Id = 94, Name = "Lada", CountryName = "Russia" },
            new BrandDto() { Id = 95, Name = "ZAZ", CountryName = "Ukraine" },
            new BrandDto() { Id = 96, Name = "UAZ", CountryName = "Russia" },
            new BrandDto() { Id = 97, Name = "Bogdan", CountryName = "Ukraine" },
            new BrandDto() { Id = 98, Name = "Moskvitch", CountryName = "Russia" },
            new BrandDto() { Id = 99, Name = "GAS", CountryName = "Russia" },
            new BrandDto() { Id = 100, Name = "LuAZ", CountryName = "Ukraine" },
            new BrandDto() { Id = 101, Name = "GAZelle", CountryName = "Russia" },
            new BrandDto() { Id = 102, Name = "ZIL", CountryName = "Russia" },
            new BrandDto() { Id = 103, Name = "Niva", CountryName = "Russia" },
            new BrandDto() { Id = 104, Name = "KAMAZ", CountryName = "Russia" },
            new BrandDto() { Id = 105, Name = "PAZ", CountryName = "Russia" },
            new BrandDto() { Id = 106, Name = "Zaporozhets", CountryName = "Ukraine" },
            new BrandDto() { Id = 107, Name = "Bogdan", CountryName = "Ukraine" },
            new BrandDto() { Id = 108, Name = "KrAZ", CountryName = "Ukraine" },
            new BrandDto() { Id = 109, Name = "Etalon", CountryName = "Ukraine" },
            new BrandDto() { Id = 110, Name = "LaZ", CountryName = "Ukraine" },
            new BrandDto() { Id = 111, Name = "BelAZ", CountryName = "Belarus" },
            new BrandDto() { Id = 112, Name = "MAZ", CountryName = "Belarus" },
            new BrandDto() { Id = 113, Name = "BKM", CountryName = "Belarus" },
            new BrandDto() { Id = 114, Name = "Neman", CountryName = "Belarus" },
            new BrandDto() { Id = 115, Name = "Belkommunmash", CountryName = "Belarus" },
            new BrandDto() { Id = 116, Name = "LiAZ", CountryName = "Russia" },
            new BrandDto() { Id = 117, Name = "GROZ", CountryName = "Russia" },
            new BrandDto() { Id = 118, Name = "Altai", CountryName = "Russia" },
            new BrandDto() { Id = 119, Name = "Foton", CountryName = "China" },
            new BrandDto() { Id = 120, Name = "Shaanxi", CountryName = "China" },
            new BrandDto() { Id = 121, Name = "Higer", CountryName = "China" },
            new BrandDto() { Id = 122, Name = "Huanghai", CountryName = "China" },
            new BrandDto() { Id = 123, Name = "Zhongtong", CountryName = "China" },
            new BrandDto() { Id = 124, Name = "Fengshen", CountryName = "China" },
            new BrandDto() { Id = 125, Name = "Hawtai", CountryName = "China" },
            new BrandDto() { Id = 126, Name = "Maxus", CountryName = "China" },
            new BrandDto() { Id = 127, Name = "Nanjing", CountryName = "China" },
            new BrandDto() { Id = 128, Name = "SMA", CountryName = "China" },
            new BrandDto() { Id = 129, Name = "Chana", CountryName = "China" },
            new BrandDto() { Id = 130, Name = "Xinkai", CountryName = "China" },
            new BrandDto() { Id = 131, Name = "Haima", CountryName = "China" },
            new BrandDto() { Id = 132, Name = "Changhe", CountryName = "China" },
            new BrandDto() { Id = 133, Name = "Zinoro", CountryName = "China" },
            new BrandDto() { Id = 134, Name = "Jinbei", CountryName = "China" },
            new BrandDto() { Id = 135, Name = "JMC", CountryName = "China" },
            new BrandDto() { Id = 136, Name = "Changfeng", CountryName = "China" },
            new BrandDto() { Id = 137, Name = "ZX Auto", CountryName = "China" },
            new BrandDto() { Id = 138, Name = "Huasong", CountryName = "China" },
            new BrandDto() { Id = 139, Name = "Venucia", CountryName = "China" },
            new BrandDto() { Id = 140, Name = "Linktour", CountryName = "China" },
            new BrandDto() { Id = 141, Name = "Dongfeng Yueda Kia", CountryName = "China" },
            new BrandDto() { Id = 142, Name = "Bisu", CountryName = "China" },
            new BrandDto() { Id = 143, Name = "Changfeng Liebao", CountryName = "China" },
            new BrandDto() { Id = 144, Name = "Shuanghuan", CountryName = "China" },
            new BrandDto() { Id = 145, Name = "Soueast", CountryName = "China" },
            new BrandDto() { Id = 146, Name = "Huanghai", CountryName = "China" },
            new BrandDto() { Id = 147, Name = "JMC", CountryName = "China" },
            new BrandDto() { Id = 148, Name = "Dongfeng Sokon", CountryName = "China" },
            new BrandDto() { Id = 149, Name = "Changan Suzuki", CountryName = "China" },
            new BrandDto() { Id = 150, Name = "Jiangnan", CountryName = "China" },
            new BrandDto() { Id = 151, Name = "Jiangling", CountryName = "China" },
            new BrandDto() { Id = 152, Name = "Rely", CountryName = "China" },
            new BrandDto() { Id = 153, Name = "Wanfeng", CountryName = "China" },
            new BrandDto() { Id = 154, Name = "Sichuan Tengzhong", CountryName = "China" },
            new BrandDto() { Id = 155, Name = "Fujian Benz", CountryName = "China" },
            new BrandDto() { Id = 156, Name = "Soueast", CountryName = "China" },
            new BrandDto() { Id = 157, Name = "Xin Kai", CountryName = "China" },
            new BrandDto() { Id = 158, Name = "Wanxiang", CountryName = "China" },
            new BrandDto() { Id = 159, Name = "FAW Jie Fang", CountryName = "China" },
            new BrandDto() { Id = 160, Name = "BYD Auto", CountryName = "China" },
            new BrandDto() { Id = 161, Name = "Geely", CountryName = "China" },
            new BrandDto() { Id = 162, Name = "Haval", CountryName = "China" },
            new BrandDto() { Id = 163, Name = "Wuling", CountryName = "China" },
            new BrandDto() { Id = 164, Name = "Lifan", CountryName = "China" },
            new BrandDto() { Id = 165, Name = "Great Wall", CountryName = "China" },
            new BrandDto() { Id = 166, Name = "Chery", CountryName = "China" },
            new BrandDto() { Id = 167, Name = "Brilliance", CountryName = "China" },
            new BrandDto() { Id = 168, Name = "Dongfeng", CountryName = "China" },
            new BrandDto() { Id = 169, Name = "FAW", CountryName = "China" },
            new BrandDto() { Id = 170, Name = "Chang'an", CountryName = "China" },
            new BrandDto() { Id = 171, Name = "Baojun", CountryName = "China" },
            new BrandDto() { Id = 172, Name = "Roewe", CountryName = "China" },
            new BrandDto() { Id = 173, Name = "SAIC", CountryName = "China" },
            new BrandDto() { Id = 174, Name = "Trumpchi", CountryName = "China" },
            new BrandDto() { Id = 175, Name = "Lynk & Co", CountryName = "China" },
            new BrandDto() { Id = 176, Name = "Hongqi", CountryName = "China" },
            new BrandDto() { Id = 177, Name = "Luxgen", CountryName = "Taiwan" },
            new BrandDto() { Id = 178, Name = "Toyota", CountryName = "Japan" },
            new BrandDto() { Id = 179, Name = "Lexus", CountryName = "Japan" },
            new BrandDto() { Id = 180, Name = "Honda", CountryName = "Japan" },
            new BrandDto() { Id = 181, Name = "Nissan", CountryName = "Japan" },
            new BrandDto() { Id = 182, Name = "Mitsubishi", CountryName = "Japan" },
            new BrandDto() { Id = 183, Name = "Subaru", CountryName = "Japan" },
            new BrandDto() { Id = 184, Name = "Mazda", CountryName = "Japan" },
            new BrandDto() { Id = 185, Name = "Suzuki", CountryName = "Japan" },
            new BrandDto() { Id = 186, Name = "Isuzu", CountryName = "Japan" },
            new BrandDto() { Id = 187, Name = "Daihatsu", CountryName = "Japan" },
            new BrandDto() { Id = 188, Name = "Mitsuoka", CountryName = "Japan" },
            new BrandDto() { Id = 189, Name = "Acura", CountryName = "Japan" },
            new BrandDto() { Id = 190, Name = "Toyota", CountryName = "Japan" },
            new BrandDto() { Id = 191, Name = "Fiat", CountryName = "Italy" },
            new BrandDto() { Id = 192, Name = "Ferrari", CountryName = "Italy" },
            new BrandDto() { Id = 193, Name = "Lamborghini", CountryName = "Italy" },
            new BrandDto() { Id = 194, Name = "Alfa Romeo", CountryName = "Italy" },
            new BrandDto() { Id = 195, Name = "Maserati", CountryName = "Italy" },
            new BrandDto() { Id = 196, Name = "Pagani", CountryName = "Italy" },
            new BrandDto() { Id = 197, Name = "Lancia", CountryName = "Italy" },
            new BrandDto() { Id = 198, Name = "McLaren", CountryName = "United Kingdom" },
            new BrandDto() { Id = 199, Name = "Aston Martin", CountryName = "United Kingdom" },
            new BrandDto() { Id = 200, Name = "Lotus", CountryName = "United Kingdom" }
        };
            return carBrandDtos;
        }
    }

    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
    }
}
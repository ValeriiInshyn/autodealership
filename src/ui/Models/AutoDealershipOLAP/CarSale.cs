using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealershipOLAP
{
    [Table("CarSales", Schema = "dbo")]
    public partial class CarSale
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int AutoDealershipId { get; set; }

        public AutoDealership AutoDealership { get; set; }

        [Required]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        public decimal TotalIncomeLastMonth { get; set; }

        [Required]
        public int StartDateId { get; set; }

        public Date Date1 { get; set; }

        [Required]
        public int EndDateId { get; set; }

        public Date Date { get; set; }

        [Required]
        public decimal TotalIncomeForCurrentMonth { get; set; }

        [Required]
        public int MonthTotalIncomeModifyPercent { get; set; }

        [Required]
        public int SalesCountForLastMonth { get; set; }

        [Required]
        public int SalesCountForCurrentMonth { get; set; }

        [Required]
        public int SalesCountChangeForMonth { get; set; }

    }
}
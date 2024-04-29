using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models.AutoDealershipOLAP;

namespace CourseWork.Data
{
    public partial class AutoDealershipOLAPContext : DbContext
    {
        public AutoDealershipOLAPContext()
        {
        }

        public AutoDealershipOLAPContext(DbContextOptions<AutoDealershipOLAPContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.Car>()
              .HasOne(i => i.Brand)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.BrandId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.CarSale>()
              .HasOne(i => i.AutoDealership)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.AutoDealershipId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.CarSale>()
              .HasOne(i => i.Brand)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.BrandId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.CarSale>()
              .HasOne(i => i.Date)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.EndDateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.CarSale>()
              .HasOne(i => i.Date1)
              .WithMany(i => i.CarSales1)
              .HasForeignKey(i => i.StartDateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.Lease>()
              .HasOne(i => i.Car)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.CarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.Lease>()
              .HasOne(i => i.Date)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.LeaseEndDateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.Lease>()
              .HasOne(i => i.Date1)
              .WithMany(i => i.Leases1)
              .HasForeignKey(i => i.LeaseSignDateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealershipOLAP.Lease>()
              .HasOne(i => i.Date2)
              .WithMany(i => i.Leases2)
              .HasForeignKey(i => i.LeaseStartDateId)
              .HasPrincipalKey(i => i.Id);
            this.OnModelBuilding(builder);
        }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.AutoDealership> AutoDealerships { get; set; }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.Brand> Brands { get; set; }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.Car> Cars { get; set; }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.CarSale> CarSales { get; set; }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.Date> Dates { get; set; }

        public DbSet<CourseWork.Models.AutoDealershipOLAP.Lease> Leases { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models.AutoDealership;

namespace CourseWork.Data
{
    public partial class AutoDealershipContext : DbContext
    {
        public AutoDealershipContext()
        {
        }

        public AutoDealershipContext(DbContextOptions<AutoDealershipContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseWork.Models.AutoDealership.CarComfortOption>().HasKey(table => new {
                table.CarId, table.ComfortOptionId
            });

            builder.Entity<CourseWork.Models.AutoDealership.CarMultimediaOption>().HasKey(table => new {
                table.CarId, table.MultimediaOptionId
            });

            builder.Entity<CourseWork.Models.AutoDealership.CarSafetyOption>().HasKey(table => new {
                table.CarId, table.SafetyOptionId
            });

            builder.Entity<CourseWork.Models.AutoDealership.AutoDealership>()
              .HasOne(i => i.City)
              .WithMany(i => i.AutoDealerships)
              .HasForeignKey(i => i.CityId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Brand>()
              .HasOne(i => i.Country)
              .WithMany(i => i.Brands)
              .HasForeignKey(i => i.CountryId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarComfortOption>()
              .HasOne(i => i.Car)
              .WithMany(i => i.CarComfortOptions)
              .HasForeignKey(i => i.CarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarComfortOption>()
              .HasOne(i => i.ComfortOption)
              .WithMany(i => i.CarComfortOptions)
              .HasForeignKey(i => i.ComfortOptionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarDelivery>()
              .HasOne(i => i.Distributor)
              .WithMany(i => i.CarDeliveries)
              .HasForeignKey(i => i.DistributorId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarDelivery>()
              .HasOne(i => i.CarSale)
              .WithMany(i => i.CarDeliveries)
              .HasForeignKey(i => i.SaleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarMultimediaOption>()
              .HasOne(i => i.Car)
              .WithMany(i => i.CarMultimediaOptions)
              .HasForeignKey(i => i.CarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarMultimediaOption>()
              .HasOne(i => i.MultimediaOption)
              .WithMany(i => i.CarMultimediaOptions)
              .HasForeignKey(i => i.MultimediaOptionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Car>()
              .HasOne(i => i.CarBodyType)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.BodyTypeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Car>()
              .HasOne(i => i.Brand)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.BrandId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Car>()
              .HasOne(i => i.Color)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.ColorId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Car>()
              .HasOne(i => i.Engine)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.EngineId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Car>()
              .HasOne(i => i.GearBoxType)
              .WithMany(i => i.Cars)
              .HasForeignKey(i => i.GearBoxId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSafetyOption>()
              .HasOne(i => i.Car)
              .WithMany(i => i.CarSafetyOptions)
              .HasForeignKey(i => i.CarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSafetyOption>()
              .HasOne(i => i.SafetyOption)
              .WithMany(i => i.CarSafetyOptions)
              .HasForeignKey(i => i.SafetyOptionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSale>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.CustomerId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSale>()
              .HasOne(i => i.DealershipCar)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.DealershipCarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSale>()
              .HasOne(i => i.Employee)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.EmployeeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSale>()
              .HasOne(i => i.PaymentMethod)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.PaymentMethodId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.CarSale>()
              .HasOne(i => i.SaleStatus)
              .WithMany(i => i.CarSales)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.City>()
              .HasOne(i => i.Country)
              .WithMany(i => i.Cities)
              .HasForeignKey(i => i.CountryId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.DealershipCar>()
              .HasOne(i => i.DealershipCarStatus)
              .WithMany(i => i.DealershipCars)
              .HasForeignKey(i => i.CarStatusId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.DealershipCar>()
              .HasOne(i => i.AutoDealership)
              .WithMany(i => i.DealershipCars)
              .HasForeignKey(i => i.DealershipId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Employee>()
              .HasOne(i => i.AutoDealership)
              .WithMany(i => i.Employees)
              .HasForeignKey(i => i.DealershipId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Engine>()
              .HasOne(i => i.Brand)
              .WithMany(i => i.Engines)
              .HasForeignKey(i => i.BrandId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Engine>()
              .HasOne(i => i.EngineType)
              .WithMany(i => i.Engines)
              .HasForeignKey(i => i.EngineTypeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.LeaseProposalCondition>()
              .HasOne(i => i.Condition)
              .WithMany(i => i.LeaseProposalConditions)
              .HasForeignKey(i => i.ConditionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.LeaseProposalCondition>()
              .HasOne(i => i.LeaseProposal)
              .WithMany(i => i.LeaseProposalConditions)
              .HasForeignKey(i => i.LeaseProposalId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.LeaseProposal>()
              .HasOne(i => i.LeaseType)
              .WithMany(i => i.LeaseProposals)
              .HasForeignKey(i => i.LeaseTypeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Lease>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.CustomerId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Lease>()
              .HasOne(i => i.DealershipCar)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.DealershipCarId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Lease>()
              .HasOne(i => i.Employee)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.EmployeeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CourseWork.Models.AutoDealership.Lease>()
              .HasOne(i => i.LeaseProposal)
              .WithMany(i => i.Leases)
              .HasForeignKey(i => i.ProposalId)
              .HasPrincipalKey(i => i.Id);
            this.OnModelBuilding(builder);
        }

        public DbSet<CourseWork.Models.AutoDealership.AutoDealership> AutoDealerships { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Brand> Brands { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarBodyType> CarBodyTypes { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarComfortOption> CarComfortOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarDelivery> CarDeliveries { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarMultimediaOption> CarMultimediaOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Car> Cars { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarSafetyOption> CarSafetyOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarSale> CarSales { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.CarType> CarTypes { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.City> Cities { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Color> Colors { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.ComfortOption> ComfortOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Condition> Conditions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Country> Countries { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Customer> Customers { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.DealershipCar> DealershipCars { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.DealershipCarStatus> DealershipCarStatuses { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Distributor> Distributors { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Employee> Employees { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Engine> Engines { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.EngineType> EngineTypes { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.GearBoxType> GearBoxTypes { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.LeaseProposalCondition> LeaseProposalConditions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.LeaseProposal> LeaseProposals { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.Lease> Leases { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.LeaseType> LeaseTypes { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.MultimediaOption> MultimediaOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.PaymentMethod> PaymentMethods { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.SafetyOption> SafetyOptions { get; set; }

        public DbSet<CourseWork.Models.AutoDealership.SaleStatus> SaleStatuses { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}
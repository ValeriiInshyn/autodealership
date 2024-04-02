﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OLTP_Seed.Models;

public partial class AutoDealershipContext : DbContext
{
    public AutoDealershipContext()
    {
    }

    public AutoDealershipContext(DbContextOptions<AutoDealershipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutoDealership> AutoDealerships { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }

    public virtual DbSet<CarComfortOption> CarComfortOptions { get; set; }

    public virtual DbSet<CarDelivery> CarDeliveries { get; set; }

    public virtual DbSet<CarMultimediaOption> CarMultimediaOptions { get; set; }

    public virtual DbSet<CarSafetyOption> CarSafetyOptions { get; set; }

    public virtual DbSet<CarSale> CarSales { get; set; }

    public virtual DbSet<CarType> CarTypes { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<ComfortOption> ComfortOptions { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DealershipCar> DealershipCars { get; set; }

    public virtual DbSet<DealershipCarStatus> DealershipCarStatuses { get; set; }

    public virtual DbSet<Distributor> Distributors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Engine> Engines { get; set; }

    public virtual DbSet<EngineType> EngineTypes { get; set; }

    public virtual DbSet<GearBoxType> GearBoxTypes { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<LeaseProposal> LeaseProposals { get; set; }

    public virtual DbSet<LeaseProposalCondition> LeaseProposalConditions { get; set; }

    public virtual DbSet<LeaseType> LeaseTypes { get; set; }

    public virtual DbSet<MultimediaOption> MultimediaOptions { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<SafetyOption> SafetyOptions { get; set; }

    public virtual DbSet<SaleStatus> SaleStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutoDealership;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutoDealership>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.City).WithMany(p => p.AutoDealerships)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_AutoDealerships_Cities");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Brands_Countries");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cars__3214EC07FD549B2F");

            entity.Property(e => e.Generation).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.BodyType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BodyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarBodyTypes");

            entity.HasOne(d => d.Brand).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Cars_Brands");

            entity.HasOne(d => d.CarType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarTypes");

            entity.HasOne(d => d.Color).WithMany(p => p.Cars)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Colors");

            entity.HasOne(d => d.Engine).WithMany(p => p.Cars)
                .HasForeignKey(d => d.EngineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__EngineId__32AB8735");

            entity.HasOne(d => d.GearBox).WithMany(p => p.Cars)
                .HasForeignKey(d => d.GearBoxId)
                .HasConstraintName("FK_Cars_GearBoxTypes");
        });

        modelBuilder.Entity<CarBodyType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<CarComfortOption>(entity =>
        {
            entity.HasKey(e => new { e.CarId, e.ComfortOptionId }).HasName("PK__CarComfo__812AFC7B50B373A4");

            entity.HasOne(d => d.Car).WithMany(p => p.CarComfortOptions)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarComfor__CarId__37703C52");

            entity.HasOne(d => d.ComfortOption).WithMany(p => p.CarComfortOptions)
                .HasForeignKey(d => d.ComfortOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarComfor__Comfo__3864608B");
        });

        modelBuilder.Entity<CarDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarDeliv__626D8FCE5A2DD524");

            entity.ToTable("CarDelivery");

            entity.Property(e => e.DeliveryCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Distributor).WithMany(p => p.CarDeliveries)
                .HasForeignKey(d => d.DistributorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarDelive__Distr__5BAD9CC8");

            entity.HasOne(d => d.Sale).WithMany(p => p.CarDeliveries)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarDelive__SaleI__5AB9788F");
        });

        modelBuilder.Entity<CarMultimediaOption>(entity =>
        {
            entity.HasKey(e => new { e.CarId, e.MultimediaOptionId }).HasName("PK__CarMulti__9D58B07C816F4ABB");

            entity.HasOne(d => d.Car).WithMany(p => p.CarMultimediaOptions)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarMultim__CarId__3D2915A8");

            entity.HasOne(d => d.MultimediaOption).WithMany(p => p.CarMultimediaOptions)
                .HasForeignKey(d => d.MultimediaOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarMultim__Multi__3E1D39E1");
        });

        modelBuilder.Entity<CarSafetyOption>(entity =>
        {
            entity.HasKey(e => new { e.CarId, e.SafetyOptionId }).HasName("PK__CarSafet__7F73AA4C10FA322A");

            entity.HasOne(d => d.Car).WithMany(p => p.CarSafetyOptions)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarSafety__CarId__42E1EEFE");

            entity.HasOne(d => d.SafetyOption).WithMany(p => p.CarSafetyOptions)
                .HasForeignKey(d => d.SafetyOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarSafety__Safet__43D61337");
        });

        modelBuilder.Entity<CarSale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarSales__1EE3C3FFD470F4B6");

            entity.HasOne(d => d.Customer).WithMany(p => p.CarSales)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarSales__Custom__55F4C372");

            entity.HasOne(d => d.DealershipCar).WithMany(p => p.CarSales)
                .HasForeignKey(d => d.DealershipCarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarSales_DealershipCars");

            entity.HasOne(d => d.Employee).WithMany(p => p.CarSales)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_CarSales_Employees");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.CarSales)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_CarSales_PaymentMethods");

            entity.HasOne(d => d.Status).WithMany(p => p.CarSales)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_CarSales_SaleStatus");
        });

        modelBuilder.Entity<CarType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Countries");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<ComfortOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ComfortO__3214EC07D7B7C2FA");

            entity.Property(e => e.OptionName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__A4AE64D8E3ED2AA2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DealershipCar>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CarStatus).WithMany(p => p.DealershipCars)
                .HasForeignKey(d => d.CarStatusId)
                .HasConstraintName("FK_DealershipCars_DealershipCarStatuses");

            entity.HasOne(d => d.Dealership).WithMany(p => p.DealershipCars)
                .HasForeignKey(d => d.DealershipId)
                .HasConstraintName("FK_DealershipCars_AutoDealerships");
        });

        modelBuilder.Entity<DealershipCarStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Distributor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distribu__FD1AEB9E754F1A9A");

            entity.Property(e => e.DistributorAddress)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.DistributorIdentifier).HasMaxLength(50);
            entity.Property(e => e.DistributorName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Dealership).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DealershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_AutoDealerships");
        });

        modelBuilder.Entity<Engine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Engines__3214EC0762EC2A83");

            entity.Property(e => e.EngineVolume).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Engines)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Engines_Brands");

            entity.HasOne(d => d.EngineType).WithMany(p => p.Engines)
                .HasForeignKey(d => d.EngineTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Engines_EngineTypes");
        });

        modelBuilder.Entity<EngineType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EngineTy__737584F755900501");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<GearBoxType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lease__21FA58C149035493");

            entity.Property(e => e.LeaseUniqueNumber).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Leases)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lease__CustomerI__489AC854");

            entity.HasOne(d => d.DealershipCar).WithMany(p => p.Leases)
                .HasForeignKey(d => d.DealershipCarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_DealershipCars");

            entity.HasOne(d => d.Employee).WithMany(p => p.Leases)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Leases_Employees");

            entity.HasOne(d => d.Proposal).WithMany(p => p.Leases)
                .HasForeignKey(d => d.ProposalId)
                .HasConstraintName("FK_Leases_LeaseProposals");
        });

        modelBuilder.Entity<LeaseProposal>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MonthlyPayment).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.LeaseType).WithMany(p => p.LeaseProposals)
                .HasForeignKey(d => d.LeaseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaseProposals_LeaseTypes");
        });

        modelBuilder.Entity<LeaseProposalCondition>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Condition).WithMany(p => p.LeaseProposalConditions)
                .HasForeignKey(d => d.ConditionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaseProposalConditions_Conditions1");

            entity.HasOne(d => d.LeaseProposal).WithMany(p => p.LeaseProposalConditions)
                .HasForeignKey(d => d.LeaseProposalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaseProposalConditions_LeaseProposals");
        });

        modelBuilder.Entity<LeaseType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<MultimediaOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Multimed__3214EC0738D865A3");

            entity.Property(e => e.OptionName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<SafetyOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SafetyOp__3214EC0714E9FAC0");

            entity.Property(e => e.OptionName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<SaleStatus>(entity =>
        {
            entity.ToTable("SaleStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
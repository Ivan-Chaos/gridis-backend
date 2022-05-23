using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GridisBackend.Models
{
    public partial class PowerManagementOLTPContext : DbContext
    {
        public PowerManagementOLTPContext()
        {
        }

        public PowerManagementOLTPContext(DbContextOptions<PowerManagementOLTPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<CallOperator> CallOperators { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Engineer> Engineers { get; set; } = null!;
        public virtual DbSet<InstalledMeter> InstalledMeters { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<MeterModel> MeterModels { get; set; } = null!;
        public virtual DbSet<OperatorReading> OperatorReadings { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<ProvidedService> ProvidedServices { get; set; } = null!;
        public virtual DbSet<Reading> Readings { get; set; } = null!;
        public virtual DbSet<Residence> Residences { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; } = null!;
        public virtual DbSet<Street> Streets { get; set; } = null!;
        public virtual DbSet<Tarrif> Tarrifs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ELTON;Database=PowerManagementOLTP;Trusted_Connection=True;");
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).UpdateDate = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BuildingNumber).HasMaxLength(6);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StreetId).HasColumnName("StreetID");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__street___0C85DE4D");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Person");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BillNumber).HasMaxLength(20);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DaySum).HasColumnType("money");

                entity.Property(e => e.GeneratedAt).HasColumnType("datetime");

                entity.Property(e => e.NightSum).HasColumnType("money");

                entity.Property(e => e.ReadingsId).HasColumnName("ReadingsID");

                entity.Property(e => e.ResidenceId).HasColumnName("ResidenceID");

                entity.Property(e => e.TarrifId).HasColumnName("TarrifID");

                entity.Property(e => e.TotalSum).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Readings)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.ReadingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bill__readings_i__160F4887");

                entity.HasOne(d => d.Residence)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.ResidenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Residence");

                entity.HasOne(d => d.Tarrif)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.TarrifId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_UsedTarrif");
            });

            modelBuilder.Entity<CallOperator>(entity =>
            {
                entity.ToTable("CallOperator");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CallOperators)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallOperator_Person");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__District__city_i__7F2BE32F");
            });

            modelBuilder.Entity<Engineer>(entity =>
            {
                entity.ToTable("Engineer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Engineers)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Engineer__distri__09A971A2");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Engineers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Engineer__person__08B54D69");
            });

            modelBuilder.Entity<InstalledMeter>(entity =>
            {
                entity.ToTable("InstalledMeter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InstalationDate).HasColumnType("date");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.SerialNumber).HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.InstalledMeters)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Meter__model_id__1F98B2C1");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MeterModel>(entity =>
            {
                entity.ToTable("MeterModel");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ModelName).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.MeterModels)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeterModel_Manufacturer");
            });

            modelBuilder.Entity<OperatorReading>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

                entity.Property(e => e.ReadingsId).HasColumnName("ReadingsID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.OperatorReadings)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperatorReadings_CallOperator");

                entity.HasOne(d => d.Readings)
                    .WithMany(p => p.OperatorReadings)
                    .HasForeignKey(d => d.ReadingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperatorReadings_Readings");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaidSum).HasColumnType("money");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNumber).HasColumnType("money");

                entity.Property(e => e.ResidenceId).HasColumnName("ResidenceID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Residence)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ResidenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Residence");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProvidedService>(entity =>
            {
                entity.ToTable("ProvidedService");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompletedTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EngineerId).HasColumnName("EngineerID");

                entity.Property(e => e.ServiceRequestId).HasColumnName("ServiceRequestID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Engineer)
                    .WithMany(p => p.ProvidedServices)
                    .HasForeignKey(d => d.EngineerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Service_Engineer");

                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.ProvidedServices)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Service_ServiceRequest");
            });

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataCollectedAt).HasColumnType("datetime");

                entity.Property(e => e.DayReadings).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.InstalledMeterId).HasColumnName("InstalledMeterID");

                entity.Property(e => e.NightReadings).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.InstalledMeter)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.InstalledMeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Readings_InstalledMeter");
            });

            modelBuilder.Entity<Residence>(entity =>
            {
                entity.ToTable("Residence");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InstalledMeterId).HasColumnName("InstalledMeterID");

                entity.Property(e => e.ResidentId).HasColumnName("ResidentID");

                entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Residences)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_House_Address");

                entity.HasOne(d => d.InstalledMeter)
                    .WithMany(p => p.Residences)
                    .HasForeignKey(d => d.InstalledMeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Residence_InstalledMeter");

                entity.HasOne(d => d.Resident)
                    .WithMany(p => p.Residences)
                    .HasForeignKey(d => d.ResidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_House_Person");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.ServiceName).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.ToTable("ServiceRequest");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CallOperatorId).HasColumnName("CallOperatorID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MeterModelId).HasColumnName("MeterModelID");

                entity.Property(e => e.ReceivedAt).HasColumnType("datetime");

                entity.Property(e => e.ResidenceId).HasColumnName("ResidenceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.HasOne(d => d.CallOperator)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.CallOperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequest_CallOperator");

                entity.HasOne(d => d.MeterModel)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.MeterModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequest_MeterModel");

                entity.HasOne(d => d.Residence)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.ResidenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequest_Residence");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequest_Services");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("Street");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Street__district__02084FDA");
            });

            modelBuilder.Entity<Tarrif>(entity =>
            {
                entity.ToTable("Tarrif");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActiveFrom).HasColumnType("date");

                entity.Property(e => e.ActiveTill).HasColumnType("date");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DayTarrifCost).HasColumnType("money");

                entity.Property(e => e.NightTarrifCost).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

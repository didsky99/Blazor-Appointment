using BlazorAppointmentSystem.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppointmentSystem.Server.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        // DbSet properties
        public DbSet<MAppointmentStatus> MAppointmentStatuses { get; set; }
        public DbSet<MGender> MGenders { get; set; }
        public DbSet<MHolidayType> MHolidayTypes { get; set; }
        public DbSet<MSettings> MSettings { get; set; }
        public DbSet<TbCompany> TbCompanies { get; set; }
        public DbSet<TbCustomer> TbCustomers { get; set; }
        public DbSet<TbHolidays> TbHolidays { get; set; }
        public DbSet<TxAppointment> TxAppointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------------------------
            // MAppointmentStatus
            // ---------------------------
            modelBuilder.Entity<MAppointmentStatus>(entity =>
            {
                entity.ToTable("m_appointment_status");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.StatusId)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.StatusName)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            // ---------------------------
            // MGender
            // ---------------------------
            modelBuilder.Entity<MGender>(entity =>
            {
                entity.ToTable("m_gender");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            // ---------------------------
            // MHolidayType
            // ---------------------------
            modelBuilder.Entity<MHolidayType>(entity =>
            {
                entity.ToTable("m_holiday_type");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            // ---------------------------
            // MSettings
            // ---------------------------
            modelBuilder.Entity<MSettings>(entity =>
            {
                entity.ToTable("m_settings");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.SettingName)
                    .HasMaxLength(20);

                entity.Property(e => e.Value)
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            // ---------------------------
            // TbCompany
            // ---------------------------
            modelBuilder.Entity<TbCompany>(entity =>
            {
                entity.ToTable("tb_company");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            // ---------------------------
            // TbCustomer
            // ---------------------------
            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.ToTable("tb_customer");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(10);

                entity.Property(e => e.GenderId)
                    .IsRequired();

                entity.Property(e => e.GenderText)
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();

                // // Foreign keys
                // entity.HasOne(e => e.GenderId)
                //     .WithMany()
                //     .HasForeignKey(e => e.GenderId)
                //     .HasConstraintName("FK_tb_customer_m_gender");

                // entity.HasOne(e => e.Company)
                //     .WithMany()
                //     .HasForeignKey(e => e.CompanyId)
                //     .HasConstraintName("FK_tb_customer_tb_company");
            });

            // ---------------------------
            // TbHolidays
            // ---------------------------
            modelBuilder.Entity<TbHolidays>(entity =>
            {
                entity.ToTable("tb_holidays");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.HolidayName)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(50);

                entity.Property(e => e.HolidayTypeId)
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();

                // // Foreign key
                // entity.HasOne(e => e.HolidayType)
                //     .WithMany()
                //     .HasForeignKey(e => e.HolidayTypeId)
                //     .HasConstraintName("FK_tb_holidays_m_holidayType");
            });

            // ---------------------------
            // TxAppointment
            // ---------------------------
            modelBuilder.Entity<TxAppointment>(entity =>
            {
                entity.ToTable("tx_appointment");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(100);

                entity.Property(e => e.CustId)
                    .HasMaxLength(10);

                entity.Property(e => e.CustName)
                    .HasMaxLength(50);

                entity.Property(e => e.CustCompany)
                    .HasMaxLength(50);

                entity.Property(e => e.Token)
                    .HasMaxLength(4)
                    .IsRequired();

                entity.Property(e => e.Duration)
                    .HasColumnType("time");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime");

                entity.Property(e => e.AppointmentStatus)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}

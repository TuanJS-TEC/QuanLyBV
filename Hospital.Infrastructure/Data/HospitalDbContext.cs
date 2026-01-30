using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Entities;

namespace Hospital.Infrastructure.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options){}
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            });
            
            modelBuilder.Entity<Department>().ToTable("DEPARTMENT");
            modelBuilder.Entity<Doctor>().ToTable("DOCTOR");
            modelBuilder.Entity<Appointment>().ToTable("APPOINTMENT");
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany()
                .HasForeignKey(d => d.DepartmentID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientID); // [cite: 58]

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorID);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Appointment)
                .WithOne() 
                .HasForeignKey<MedicalRecord>(m => m.AppointmentID)
                .IsRequired(false);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.ConsultationFee)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.Property(m => m.Temperature).HasColumnType("decimal(4, 1)");
                entity.Property(m => m.Weight).HasColumnType("decimal(5, 2)");
                entity.Property(m => m.Height).HasColumnType("decimal(5, 2)");
            });
            modelBuilder.Entity<Medication>().ToTable("MEDICATION");
            modelBuilder.Entity<Prescription>().ToTable("PRESCRIPTION");
            modelBuilder.Entity<PrescriptionDetail>().ToTable("PRESCRIPTION_DETAIL");
            modelBuilder.Entity<Medication>()
                .Property(m => m.UnitPrice)
                .HasColumnType("decimal(18, 2)");
        }
    
    }
};


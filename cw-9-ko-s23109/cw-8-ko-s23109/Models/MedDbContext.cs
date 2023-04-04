using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace cw_8_ko_s23109.Models
{
    public class MedDbContext : DbContext
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }


        public MedDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    IdDoctor=1,
                    FirstName ="Adam",
                    LastName ="Kowal",
                    Email="temp@temp.com"
                },
                 new Doctor{
                    IdDoctor=2,
                    FirstName ="Jarek",
                    LastName ="NieKowal",
                    Email="tempp@tempp.com"
                }
            };

            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(e => e.IdDoctor);
                e.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                e.Property(e => e.Email).HasMaxLength(100).IsRequired();


                e.HasData(doctors);
                e.ToTable("Doctor");
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(e=>e.IdPatient);
                e.Property(e=>e.FirstName).HasMaxLength(100).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                e.Property(e=> e.Birthdate).IsRequired();

                e.ToTable("Patient");
            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(e=>e.IdMedicament);
                e.Property(e => e.Name).HasMaxLength(100).IsRequired();
                e.Property(e => e.Description).HasMaxLength(100).IsRequired();
                e.Property(e=> e.Type).HasMaxLength(100).IsRequired();

                e.ToTable("Medicament");

            });

            modelBuilder.Entity<Prescription>(e =>  {
                e.HasKey(e => e.IdPrescription);
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.Duedate).IsRequired();

                e.HasOne(e => e.Doctor)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Patient)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);

                e.ToTable("Prescription");
            });

            modelBuilder.Entity<Prescription_Medicament>(e => {
                //pola
                e.HasKey(e => e.IdPrescription);
                e.HasKey(e => e.IdMedicament);
                e.Property(e => e.Dose).IsRequired();
                e.Property(e => e.Details).HasMaxLength(100).IsRequired();

                //złączenia
                e.HasOne(e => e.Medicament)
                .WithMany(e => e.Prescription_Medicament)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Prescription)
                .WithMany(e => e.Prescription_Medicament)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);
                
                //wymuszenie nazwy tabeli (i tak pobiera z nazwy modelu, ale może zawsze coś wywalić? )
                e.ToTable("Prescription_Medicament");
            });
        }
    }
}

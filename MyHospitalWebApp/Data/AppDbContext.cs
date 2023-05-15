using HospitalWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Models;

namespace MyHospitalWebApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var symptoms_diseases = modelBuilder.Entity<Symptom_Disease>();

            modelBuilder.Entity<Patient>()
                .HasOne(e => e.User)
                .WithMany().HasForeignKey(p => p.userId)
                .OnDelete(DeleteBehavior.NoAction);

                       modelBuilder.Entity<Doctor>()
                .HasOne(e => e.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);


            //FK Symptom_Disease -> Disease
            symptoms_diseases
                .HasOne(sd => sd.Disease)
                .WithMany(d => d.Symptoms_Diseases)
                .HasForeignKey(sd => sd.DiseaseId)
                .OnDelete(DeleteBehavior.Cascade);

            //FK Symptom_Disease -> Symptom
            symptoms_diseases
                .HasOne(sd => sd.Symptom)
                .WithMany(s => s.Symptoms_Diseases)
                .HasForeignKey(sd => sd.SymptomId)
                .OnDelete(DeleteBehavior.Cascade);

            var diagnostics = modelBuilder.Entity<Diagnostic>();

            //FK Diagnostics -> Disease
            diagnostics
                .HasOne(diag => diag.Disease)
                .WithMany(d => d.Diagnostics)
                .HasForeignKey(diag => diag.diseaseId)
                .OnDelete(DeleteBehavior.Cascade);

            //FK Diagnostics -> Patient
            diagnostics
                .HasOne(diag => diag.Patient)
                .WithMany(p => p.Diagnostics)
                .HasForeignKey(diag => diag.patientId)
                .OnDelete(DeleteBehavior.Cascade);


            var doctors_specialities = modelBuilder.Entity<Doctor_Speciality>();

            //FK Doctor_Speciality -> Doctor
            doctors_specialities
                .HasOne(ds => ds.Doctor)
                .WithMany(p => p.Doctors_Specialities)
                .HasForeignKey(ds => ds.doctorId)
                .OnDelete(DeleteBehavior.Cascade);

            //FK Doctor_Speciality -> Speciality
            doctors_specialities
                .HasOne(ds => ds.Speciality)
                .WithMany(p => p.Doctors_Specialities)
                .HasForeignKey(ds => ds.specialityId)
                .OnDelete(DeleteBehavior.Cascade);

            var appointments = modelBuilder.Entity<Appointment>();

            appointments.HasIndex(a => new { a.doctorId, a.AppointedTime }).IsUnique();
            appointments.HasIndex(a => new { a.patientId, a.AppointedTime }).IsUnique();
            appointments.HasIndex(a => new { a.RoomNr, a.AppointedTime }).IsUnique();


            modelBuilder.Entity<Patient>(entity =>
        entity.HasCheckConstraint("CK_DOB_Patient_before_present", "[DOB] < GETDATE()"));

            modelBuilder.Entity<Doctor>().HasIndex(d => d.PIC).IsUnique();
            modelBuilder.Entity<Patient>().HasIndex(p => p.PIC).IsUnique();



        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Symptom_Disease> Symptoms_Diseases { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Doctor_Speciality> Doctors_Specialities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
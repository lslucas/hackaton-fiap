using ConsultasMedicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsultasMedicas.Data
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

/*
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(claim => claim.UserId);

            modelBuilder.Entity<Patient>().HasMany(p => p.Appointments).WithOne(x => x.Patient).HasPrincipalKey(o => o.Id).HasForeignKey("PatientId");

    //.HasQueryFilter(p => !p.IsDeleted);
*/
            //modelBuilder.Entity<Appointment>().HasQueryFilter(a => !a.IsCancelled);
            modelBuilder.Entity<Appointment>().HasOne(p => p.Doctor).WithMany(x => x.Appointments).HasPrincipalKey(o => o.Id).HasForeignKey("DoctorId");
            modelBuilder.Entity<Doctor>().HasMany(p => p.Appointments).WithOne(x => x.Doctor).HasPrincipalKey(o => o.Id).HasForeignKey("DoctorId");

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });
        }
    }
}

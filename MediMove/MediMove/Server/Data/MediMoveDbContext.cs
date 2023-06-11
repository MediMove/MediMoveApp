using MediMove.Server.Application.Models;
using MediMove.Server.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data
{
    public class MediMoveDbContext:DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<Paramedic> Paramedics { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MediMoveDbContext(DbContextOptions<MediMoveDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Transport.TeamId is a nullable foreign key to Team.Id and is set to null when Team is deleted
            modelBuilder.Entity<Transport>()
                .HasOne(t => t.Team)
                .WithMany(t => t.Transports)
                .HasForeignKey(t => t.TeamId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Paramedic)
                .WithMany()
                .HasForeignKey(t => t.ParamedicId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Team>()
            //    .HasOne(t => t.Paramedic)
            //    .WithMany(p => p.Teams)
            //    .HasForeignKey(t => t.DriverId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            var seeders = new List<IDbSeeder>
            {
                new PersonalInformationsSeeder(),
                new PatientsSeeder(),
                new ParamedicsSeeder(),
                new RatesSeeder(),
                new AvailabilitiesSeeder(),
                new TeamsSeeder(),
                new DispatchersSeeder(),
                new SalariesSeeder(),
                new BillingsSeeder(),
                new TransportsSeeder(),
                new RoleSeeder(),
            };

            foreach (var seeder in seeders)
                seeder.Seed(modelBuilder);
        }
    }
}

using MediMove.Server.Data.Seeders;
using MediMove.Server.Models;
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

        public MediMoveDbContext(DbContextOptions<MediMoveDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            };

            foreach (var seeder in seeders)
                seeder.Seed(modelBuilder);
        }
    }
}

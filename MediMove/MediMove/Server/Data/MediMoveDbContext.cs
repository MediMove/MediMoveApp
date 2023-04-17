using MediMove.Server.Entities;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data
{
    public class MediMoveDbContext:DbContext
    {
        public MediMoveDbContext(DbContextOptions<MediMoveDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                PersonalInfoId = 1,
                Weight = 40
            }
            );
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Dispatcher> Dispatchers { get; set; }

        public DbSet<Paramedic> Paramedics { get; set; }

        public DbSet<PersonalInformation> PersonalInformations { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Transport> Transports { get; set; }


    }
}

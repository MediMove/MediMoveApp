using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class BillingsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            // Warining: No store type was specified for the decimal property 'Cost' on entity type 'Billing'
            modelBuilder.Entity<Billing>() 
                .Property(e => e.Cost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Billing>().HasData(new List<Billing>
            {
                new Billing
                {
                    Id = 1,
                    InvoiceNumber = DateTime.Today.ToString("1/dd/MM/yyyy"),
                    InvoiceDate = DateTime.Now.AddDays(-2),
                    BankAccountNumber = "342301332136124",
                    Cost = 200,
                    PersonalInformationId = 10,
                },
                new Billing
                {
                    Id = 2,
                    InvoiceNumber = DateTime.Today.ToString("2/dd/MM/yyyy"),
                    InvoiceDate = DateTime.Today.AddDays(-1).AddMinutes(-1),
                    BankAccountNumber = "343301232434821",
                    Cost = 90,
                    PersonalInformationId = 11,
                },
                new Billing
                {
                    Id = 3,
                    InvoiceNumber = DateTime.Today.ToString("3/dd/MM/yyyy"),
                    InvoiceDate = DateTime.Today.AddDays(-1).AddMinutes(1),
                    BankAccountNumber = "543322635238421",
                    Cost = 50,
                    PersonalInformationId = 12,
                },
            });
        }
    }
}

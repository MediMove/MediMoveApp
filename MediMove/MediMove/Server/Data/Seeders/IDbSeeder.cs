using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public interface IDbSeeder
    {
        public abstract void Seed(ModelBuilder modelBuilder);
    }
}

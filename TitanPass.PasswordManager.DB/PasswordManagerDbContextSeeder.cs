using TitanPass.PasswordManager.DB.Entities;

namespace TitanPass.PasswordManager.DB
{
    public class PasswordManagerDbContextSeeder : IPasswordManagerDbContextSeeder
    {
        private readonly PasswordManagerDbContext _ctx;

        public PasswordManagerDbContextSeeder(PasswordManagerDbContext context)
        {
            _ctx = context;
        }
        
        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        public void SeedProduction()
        {
            throw new System.NotImplementedException();
        }
    }
}
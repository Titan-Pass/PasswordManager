using Microsoft.EntityFrameworkCore;
using TitanPass.PasswordManager.DB.Entities;

namespace TitanPass.PasswordManager.DB
{
    public class PasswordManagerDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        
        public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options) : base(options) { }
    }
}
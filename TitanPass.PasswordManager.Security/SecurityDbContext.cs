using Microsoft.EntityFrameworkCore;
using TitanPass.PasswordManager.Security.Entities;

namespace TitanPass.PasswordManager.Security
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }
        
        public DbSet<LoginCustomerEntity> LoginCustomers { get; set; }
    }
}
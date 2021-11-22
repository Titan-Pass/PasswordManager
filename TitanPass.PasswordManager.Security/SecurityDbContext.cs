using Microsoft.EntityFrameworkCore;

namespace TitanPass.PasswordManager.Security
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }
        
        
    }
}
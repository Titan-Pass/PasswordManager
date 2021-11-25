namespace TitanPass.PasswordManager.Security
{
    public interface ISecurityDbContextSeeder
    {
        void SeedDevelopment();

        void SeedProduction();
    }
}
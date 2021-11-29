namespace TitanPass.PasswordManager.DB
{
    public interface IPasswordManagerDbContextSeeder
    {
        void SeedDevelopment();
        void SeedProduction();
    }
}
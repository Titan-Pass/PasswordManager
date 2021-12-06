namespace TitanPass.PasswordManager.Security.IServices
{
    public interface IEncryptionService
    {
        string EncryptPassword(string password, string masterKey);

        string DecryptPassword(string encryptedPassword, string masterKey);
    }
}
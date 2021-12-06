using System;
using System.Security.Cryptography;
using System.Text;
using TitanPass.PasswordManager.Security.IServices;

namespace TitanPass.PasswordManager.Security.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly ILoginCustomerService _service;

        public EncryptionService(ILoginCustomerService service)
        {
            _service = service;
        }
        
        public string EncryptPassword(string password, string masterKey)
        {
            string securityKey = masterKey;

            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(password);

            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] securityKeyArray = md5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            
            md5CryptoServiceProvider.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objTripleDESCryptoService.Key = securityKeyArray;

            objTripleDESCryptoService.Mode = CipherMode.ECB;

            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptoTransform = objTripleDESCryptoService.CreateEncryptor();

            byte[] resultArray = objCryptoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            
            objTripleDESCryptoService.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DecryptPassword(string encryptedPassword, string masterKey)
        {
            string securityKey = masterKey;

            byte[] toEncryptArray = Convert.FromBase64String(encryptedPassword);

            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] securityKeyArray = md5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            
            md5CryptoServiceProvider.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objTripleDESCryptoService.Key = securityKeyArray;

            objTripleDESCryptoService.Mode = CipherMode.ECB;

            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptopTransform = objTripleDESCryptoService.CreateDecryptor();

            byte[] resultArray = objCryptopTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            
            objTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
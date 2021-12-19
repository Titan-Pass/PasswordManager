using System;
using System.Security.Cryptography;
using System.Text;
using TitanPass.PasswordManager.Security.IServices;

namespace TitanPass.PasswordManager.Security.Services
{
    public class EncryptionService : IEncryptionService
    {
        //https://qawithexperts.com/article/c-sharp/encrypt-password-decrypt-it-c-console-application-example/169
        
        private readonly ILoginCustomerService _service;

        public EncryptionService(ILoginCustomerService service)
        {
            _service = service;
        }

        public string EncryptPassword(string password, string masterKey)
        {
            string securityKey = masterKey;

            // Getting the bytes of Input String.
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(password);

            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            
            //Getting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = md5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            
            //De-allocating the memory after doing the Job.
            md5CryptoServiceProvider.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptoTransform = objTripleDESCryptoService.CreateEncryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCryptoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            
            objTripleDESCryptoService.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DecryptPassword(string encryptedPassword, string masterKey)
        {
            string securityKey = masterKey;

            byte[] toEncryptArray = Convert.FromBase64String(encryptedPassword);

            //Getting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            
            byte[] securityKeyArray = md5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            
            md5CryptoServiceProvider.Clear();
            
            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptopTransform = objTripleDESCryptoService.CreateDecryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCryptopTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
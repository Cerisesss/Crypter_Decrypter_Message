using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypter_Decrypter_Message
{
    public class Message
    {
        string message;
        string key;

        public Message(string message, string key)
        {
            this.message = message;
            this.key = key;
        }

        /*public string Crypt(string message, string key)
        {
            Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] plainBytes = Encoding.UTF8.GetBytes(message);


            return Convert.ToBase64String(encryptor);
        }*/

        public string Crypt(string message, string key)
        {
            Aes aesAlg = Aes.Create();
            //aesAlg.Padding = PaddingMode.PKCS7;

            //aesAlg.Key = Encoding.UTF8.GetBytes(key);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] plainBytes = Encoding.UTF8.GetBytes(message);

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            csEncrypt.Write(plainBytes, 0, plainBytes.Length);
            csEncrypt.FlushFinalBlock();
                
            return Convert.ToBase64String(msEncrypt.ToArray());
        }


        public string Decrypt(string msgCoder, string key)
        {
            Aes aesAlg = Aes.Create();

            byte[] cipherBytes = Convert.FromBase64String(msgCoder);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            aesAlg.Padding = PaddingMode.PKCS7;

            MemoryStream msDecrypt = new MemoryStream(cipherBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            string plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}

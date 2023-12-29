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
        string iv;

        public Message(string message, string key)
        {
            this.message = message;
            this.key = key;
        }

        public string Crypt()
        {
            Aes aesAlg = Aes.Create();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 32); // Ajustez la taille de la clé si nécessaire (32 octets pour 256 bits)

            aesAlg.Key = keyBytes;
            byte[] plainBytes = Encoding.UTF8.GetBytes(message);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            this.iv = Convert.ToBase64String(aesAlg.IV);

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            csEncrypt.Write(plainBytes, 0, plainBytes.Length);
            csEncrypt.FlushFinalBlock();
                
            return Convert.ToBase64String(msEncrypt.ToArray());
        }


        public string Decrypt(string msgCoder, string key)
        {
            Aes aesAlg = Aes.Create();

            aesAlg.IV = Convert.FromBase64String(this.iv);

            byte[] cipherBytes = Convert.FromBase64String(msgCoder);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 32); // Ajustez la taille de la clé si nécessaire (32 octets pour 256 bits)

            aesAlg.Key = keyBytes;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            MemoryStream msDecrypt = new MemoryStream(cipherBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            string plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}

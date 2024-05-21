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
        string ivString;

        public Message(string message, string key) : this(message, key, "")
        {
        }

        public Message(string message, string key, string ivString)
        {
            this.message = message;
            this.key = key;
            this.ivString = ivString;
        }

        public string GetMessage()
        {
            return this.message;
        }

        public override string ToString()
        {
            return this.GetMessage();
        }

        public string GetKey()
        {
            return this.key;
        }

        public string Crypt(string message, string key)
        {
            Aes aesAlg = Aes.Create();
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 32); // Ajustez la taille de la clé si nécessaire (32 octets pour 256 bits)

            aesAlg.Key = keyBytes;
            byte[] plainBytes = Encoding.UTF8.GetBytes(message);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            csEncrypt.Write(plainBytes, 0, plainBytes.Length);
            csEncrypt.FlushFinalBlock();

            string msgCoder = Convert.ToBase64String(msEncrypt.ToArray());
            string ivString = Convert.ToBase64String(aesAlg.IV);

            string combinedMessage = msgCoder + "::" + ivString;

            return combinedMessage;
        }


        public string Decrypt(string message, string key)
        {
            string[] parts = message.Split(new string[] { "::" }, StringSplitOptions.None);

            string encryptedMessage = parts[0];
            string ivString = parts[1];

            Aes aesAlg = Aes.Create();
            aesAlg.Padding = PaddingMode.PKCS7;

            aesAlg.IV = Convert.FromBase64String(ivString);

            byte[] cipherBytes = Convert.FromBase64String(encryptedMessage);
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

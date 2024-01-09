using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypter_Decrypter_Message
{
    public class main
    {
        public static void Main(string[] args)
        {
            //string message = "ygZjrA9Zp7oKr4dj3LnRZTYyaGEEFTL1U2NZl07j8qEbZHK/c04lIRT5ZMn7kbXK/GjouARVapOH7KOpaIrdHA==";
            string message = "sniff";
            string key = "JeNeSaisPas";

            Message msg = new Message(message, key);

            string msgCoder = msg.Crypt(message, key);
            Console.WriteLine(msgCoder);

            string msgDecoder = msg.Decrypt(msgCoder, key);
            Console.WriteLine(msgDecoder);

            Console.ReadKey();
        }
    }
}

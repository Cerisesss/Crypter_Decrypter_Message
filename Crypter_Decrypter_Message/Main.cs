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
            string message = "Hello World!";
            string key = "je t'aime boubou";

            Message msg = new Message(message, key);

            string msgCoder = msg.Crypt();
            Console.WriteLine(msgCoder);

            string msgDecoder = msg.Decrypt(msgCoder, key);
            Console.WriteLine(msgDecoder);

            Console.ReadKey();
        }
    }
}

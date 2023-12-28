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
            string key = "boubouMocheEtMechant";

            Message msg = new Message(message, key);

            string msgCoder = msg.Crypt(message, key);
            Console.WriteLine(msgCoder);

            //string msgDecoder = msg.Decrypt(msgCoder, key);
            //Console.WriteLine(msgDecoder);

            Console.ReadKey();
        }
    }
}

namespace TestCrypterDecrypter
{
    using Crypter_Decrypter_Message;
    using NUnit.Framework;

    public class TestMessage
    {
        private string textData = "TestSniff";
        private string key = "Test123MDP258BOUBOU";
        private Message message;

        [SetUp]
        public void Setup()
        {
            this.message = new Message(textData, key);
        }

        [Test]
        public void TestEncryptMessage()
        {
            string encryptedMessage = this.message.Crypt(textData, key);
            Assert.That(encryptedMessage, Is.Not.Null);
            Assert.That(encryptedMessage, Is.Not.Empty);
            Assert.That(encryptedMessage, Is.Not.Contains(textData));
            Assert.That(encryptedMessage, Is.Not.Contains(key));
            Assert.That(encryptedMessage, Is.Not.Contains(this.message.GetMessage()));
        }

        [Test]
        public void TestDecryptMessage()
        {
            string encryptedMessage = this.message.Crypt(textData, key);
            string decryptedMessage = this.message.Decrypt(encryptedMessage, key);
            Assert.That(decryptedMessage, Is.Not.Null);
            Assert.That(decryptedMessage, Is.Not.Empty);
            Assert.That(decryptedMessage, Is.EqualTo(textData));
        }

        [Test]
        public void TestGetMessage()
        {
            Assert.That(this.message.GetMessage(), Is.EqualTo(textData));
        }

        [Test]
        public void TestGetKey()
        {
            Assert.That(this.message.GetKey(), Is.EqualTo(key));
        }

        [Test]
        public void TestToString()
        {
            Assert.That(this.message.ToString(), Is.EqualTo(textData));
        }

    }
}
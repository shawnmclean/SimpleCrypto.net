using System;
using NUnit.Framework;

namespace SimpleCrypto.Tests
{
    [TestFixture]
    public class PBKDF2Tests
    {

        internal virtual ICryptoService CreateICryptoService()
        {
            ICryptoService target = new PBKDF2();
            return target;
        }

        [Test]
        public void Compute_3_Param_Sets_Properties()
        {
            var service = CreateICryptoService();

            int hashIterations = 20;
            int saltSize = 20;
            string plainText = "Password";

            service.Compute(plainText, saltSize, hashIterations);


            Assert.AreEqual(saltSize, service.SaltSize, "Salt size does not match");
            Assert.AreEqual(plainText, service.PlainText, "Plain text does not match");
            Assert.AreEqual(hashIterations, service.HashIterations, "Hash Iterations does not match");

        }

        [Test]
        public void Compute_2_Param_Sets_Properties()
        {
            var service = CreateICryptoService();


            string salt = "16.randomSalt";
            string plainText = "Password";

            service.Compute(plainText, salt);


            Assert.AreEqual(salt, service.Salt, "Salt does not match");
            Assert.AreEqual(plainText, service.PlainText, "Plain text does not match");
        }
        [Test]
        public void Compute_1_Param_Sets_Properties()
        {
            var service = CreateICryptoService();

            string plainText = "Password";

            service.Compute(plainText);

            Assert.AreEqual(plainText, service.PlainText, "Default Plain text does not match");
        }

        [Test]
        public void PBKDF2_Hashes_To_512_Bits()
        {
            var service = CreateICryptoService();

            int expectedSize = 64;

            var hash = service.Compute("password", 16, 50);

            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hash);

            Assert.AreEqual(expectedSize, hashWithSaltBytes.Length);
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Different_Hash_On_Different_PlainText()
        {
            var service = CreateICryptoService();

            string salt = "16.randomSalt";
            var hash1 = service.Compute("paffssword", salt);
            var hash2 = service.Compute("password", salt);

            // Convert base64-encoded hash value into a byte array.
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Same_Hash_On_Same_PlainText_And_Salt()
        {
            var service = CreateICryptoService();

            string salt = "16.randomSalt";
            var hash1 = service.Compute("password", salt);
            var hash2 = service.Compute("password", salt);

            // Convert base64-encoded hash value into a byte array.
            Assert.AreEqual(hash1, hash2);
        }
    }
}

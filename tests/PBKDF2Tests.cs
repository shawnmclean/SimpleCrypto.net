using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleCrypto.Tests
{
    [TestClass]
    public class PBKDF2Tests
    {

        internal virtual ICryptoService CreateICryptoService()
        {
            ICryptoService target = new PBKDF2();
            return target;
        }

        [TestMethod]
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

        [TestMethod]
        public void Compute_2_Param_Sets_Properties()
        {
            var service = CreateICryptoService();

            int hashIterations = 50;
            int saltSize = 20;
            string plainText = "Password";

            service.Compute(plainText, saltSize);


            Assert.AreEqual(saltSize, service.SaltSize, "Salt size does not match");
            Assert.AreEqual(plainText, service.PlainText, "Plain text does not match");
            Assert.AreEqual(hashIterations, service.HashIterations, "Default Hash Iterations does not match");
        }
        [TestMethod]
        public void Compute_1_Param_Sets_Properties()
        {
            var service = CreateICryptoService();

            int hashIterations = 50;
            int saltSize = 16;
            string plainText = "Password";

            service.Compute(plainText);


            Assert.AreEqual(saltSize, service.SaltSize, "Salt size does not match");
            Assert.AreEqual(plainText, service.PlainText, "Default Plain text does not match");
            Assert.AreEqual(hashIterations, service.HashIterations, "Default Hash Iterations does not match");
        }

        [TestMethod]
        public void PBKDF2_Hashes_To_512_Bits()
        {
            var service = CreateICryptoService();

            int expectedSize = 64;

            var hash = service.Compute("password", 16, 50);

            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hash);

            Assert.AreEqual(expectedSize, hashWithSaltBytes.Length);
        }

        [TestMethod]
        public void PBKDF2_Hashes_To_The_Different_Hash_On_Different_PlainText()
        {
            var service = CreateICryptoService();

            service.Salt = "16.randomSalt";
            var hash1 = service.Compute("password");
            var hash2 = service.Compute("padfssword");

            // Convert base64-encoded hash value into a byte array.
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void PBKDF2_Hashes_To_The_Same_Hash_On_Same_PlainText_And_Salt()
        {
            var service = CreateICryptoService();

            service.Salt = "16.randomSalt";
            var hash1 = service.Compute("password");
            var hash2 = service.Compute("password");

            // Convert base64-encoded hash value into a byte array.
            Assert.AreEqual(hash1, hash2);
        }
    }
}

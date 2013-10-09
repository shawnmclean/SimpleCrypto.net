using System;
using System.Diagnostics;
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
        public void PBKDF2_Hashes_To_The_Same_Hash_On_Same_PlainText()
        {
            var service = CreateICryptoService();

            string salt = "16.randomSalt";
            var hash1 = service.Compute("password", salt);
            var hash2 = service.Compute("password", salt);

            // Convert base64-encoded hash value into a byte array.
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Same_Hash_On_Same_PlainText_With_Different_Instance()
        {
            var instance1 = CreateICryptoService();
            var instance2 = CreateICryptoService();

            var hash1 = instance1.Compute("password");
            var salt = instance1.Salt;

            var hash2 = instance2.Compute("password", salt);

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Generate_Salt_In_Correct_Format()
        {
            var crypto = CreateICryptoService();

            var salt = crypto.GenerateSalt();

            //get the position of the . that splits the string
            var i = salt.IndexOf('.');
            int hashIter = 0;
            int.TryParse(salt.Substring(0, i), out hashIter);
            Assert.Greater(hashIter, 0);
        }

        [Test]
        public void Generate_Salt_Has_Correct_Hash_Iterations()
        {
            var crypto = CreateICryptoService();

            var salt = crypto.GenerateSalt();

            //get the position of the . that splits the string
            var i = salt.IndexOf('.');
            int hashIter = 0;
            int.TryParse(salt.Substring(0, i), out hashIter);
            Assert.AreEqual(crypto.HashIterations, hashIter);
        }

        [Test]
        [Ignore]
        public void Generate_Salt_Has_Correct_Salt_Size()
        {
            var crypto = CreateICryptoService();

            var salt = crypto.GenerateSalt();

            //get the position of the . that splits the string
            var i = salt.IndexOf('.');
            string basicSalt = salt.Substring(i+1);
            Assert.AreEqual(crypto.SaltSize, basicSalt.Length, "Salt Size is incorrect");
        }

        [Test]
        public void Generate_Salt_Sets_Salt_Property()
        {
            var crypto = CreateICryptoService();

            var salt = crypto.GenerateSalt();

            Assert.AreEqual(crypto.Salt, salt, "The returned salt is not the salt set as parameter");
        }

        [Test]
        public void Comparison_To_Same_Hash_Returns_True()
        {
            var crypto = CreateICryptoService();

            var hash1 = crypto.Compute("password");
            var salt = crypto.Salt;

            var hash2 = crypto.Compute("password", salt);

            Assert.IsTrue(crypto.Compare(hash1, hash2), "Hash comparison fails");

            var hash3 = crypto.Compute("pasw1rd", salt);
            Assert.IsFalse(crypto.Compare(hash1, hash3), "Hash comparison should fail but didn't");
        }
    }
}

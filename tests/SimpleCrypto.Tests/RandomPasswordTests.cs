using NUnit.Framework;
using System.Text.RegularExpressions;

namespace SimpleCrypto.Tests
{
    [TestFixture]
    public class RandomPasswordTests
    {
        [Test]
        public void RandomPassword_Generates_Correct_String_Length()
        {
            int length = 10;
            string password = RandomPassword.Generate(length);

            Assert.AreEqual(length, password.Length, "Password length is incorrect");
        }

        [Test]
        public void RandomPassword_Generates_Only_Lowercase()
        {
            string password = RandomPassword.Generate(PasswordGroup.Lowercase);

            Assert.IsTrue(Regex.IsMatch(password, "^[a-z]+$"), "Non-lowercase found in in lowercase only");
        }

        [Test]
        public void RandomPassword_Generates_Only_Uppercase()
        {
            string password = RandomPassword.Generate(PasswordGroup.Uppercase);

            Assert.IsTrue(Regex.IsMatch(password, "^[A-Z]+$"), "Non-uppercase found in in uppercase only");
        }

        [Test]
        public void RandomPassword_Generates_Single_Length_Uppercase()
        {
            int length = 15;
            string password = RandomPassword.Generate(length, PasswordGroup.Uppercase);

            Assert.IsTrue(Regex.IsMatch(password, "^[A-Z]+$"), "Non-uppercase found in in uppercase only");
            Assert.AreEqual(length, password.Length, "Password length is incorrect");
        }

        [Test]
        public void RandomPassword_Generates_Both_Lowercase_And_Uppercase()
        {
            string password = RandomPassword.Generate(PasswordGroup.Uppercase, PasswordGroup.Lowercase);

            Assert.IsTrue(Regex.IsMatch(password, "^[A-Za-z]+$"), "Non-uppercase found in in uppercase only");
        }
    }
}

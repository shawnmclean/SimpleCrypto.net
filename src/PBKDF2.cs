using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrypto
{
    /// <summary>
    /// Used for hashing using the PBKDF2 Algorithm (wrapping Rfc2898DeriveBytes)
    /// </summary>
    public class PBKDF2 : ICryptoService
    {
        public PBKDF2()
        {
            //Set default salt size and hashiterations
            HashIterations = 5000;
            SaltSize = 16;
        }

        public int HashIterations
        { get; set; }

        public int SaltSize
        { get; set; }

        public string PlainText
        { get; set; }

        public string HashedText 
        { get; private set; }

        public string Salt 
        { get; private set; }

        public string Compute()
        {
            if (string.IsNullOrEmpty(PlainText)) throw new InvalidOperationException("PlainText cannot be empty");

            //if there is no salt, generate one
            if(string.IsNullOrEmpty(Salt))
                generateSalt();
            
            HashedText = calculateHash(HashIterations);

            return HashedText;
        }

        private string calculateHash(int iteration)
        {
            //convert the salt into a byte array
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(PlainText, saltBytes, iteration))
            {
                var key = pbkdf2.GetBytes(64);
                return Convert.ToBase64String(key);
            }
        }

        public string Compute(string textToHash)
        {
            PlainText = textToHash;
            //generate the salt
            generateSalt();
            //compute the hash
            Compute();
            return HashedText;
        }


        public string Compute(string textToHash, int saltSize, int hashIterations)
        {
            PlainText = textToHash;
            HashIterations = hashIterations;
            SaltSize = saltSize;
            //generate the salt
            generateSalt();
            //compute the hash
            Compute();
            return HashedText;
        }

        public string Compute(string textToHash, string salt)
        {
            PlainText = textToHash;
            Salt = salt;
            //expand the salt
            expandSalt();
            Compute();
            return HashedText;
        }

        private void generateSalt()
        {

            if (SaltSize < 1) throw new InvalidOperationException(string.Format("Cannot generate a salt of size {0}, use a value greater than 1, recommended: 16", SaltSize));

            var rand = RandomNumberGenerator.Create();

            var ret = new byte[SaltSize];

            rand.GetBytes(ret);

            //assign the generated salt in the format of {iterations}.{salt}
            Salt = string.Format("{0}.{1}",HashIterations, Convert.ToBase64String(ret));
        }

        private void expandSalt()
        {
            try
            {

                //get the position of the . that splits the string
                var i = Salt.IndexOf('.');

                //Get the hash iteration from the first index
                HashIterations = int.Parse(Salt.Substring(0, i), System.Globalization.NumberStyles.Number);
                
            }
            catch(Exception)
            {
                throw new FormatException("The salt was not in an expected format of {int}.{string}");
            }
        }

        public int GetElapsedTimeForIteration(int iteration)
        {
            var sw = new Stopwatch();
            sw.Start();
            calculateHash(iteration);
            return (int)sw.ElapsedMilliseconds;
            
        } 
    }
}

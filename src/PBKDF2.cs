using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrypto
{
    public class PBKDF2 : ICryptoService
    {
        public PBKDF2()
        {
            //Set defaults
            HashIterations = 50;
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

        public string Compute(string textToHash)
        {
            PlainText = textToHash;



            return HashedText;
        }

        public string Compute(string textToHash, int saltSize)
        {
            SaltSize = saltSize;
            Compute(textToHash);
            return HashedText;
        }

        public string Compute(string textToHash, int saltSize, int hashIterations)
        {
            HashIterations = hashIterations;
            Compute(textToHash, saltSize);
            return HashedText;
        }
    }
}

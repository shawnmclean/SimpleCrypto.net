using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;


namespace SimpleCrypto
{

    /// <summary>
    /// 
    /// </summary>
    public class PBKDF2 : ICryptoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PBKDF2"/> class.
        /// </summary>
        public PBKDF2()
        {
            //Set default salt size and hashiterations
            HashIterations = 100000;
            SaltSize = 34;
        }

        /// <summary>
        /// Gets or sets the number of iterations the hash will go through
        /// </summary>
        public int HashIterations
        { get; set; }

        /// <summary>
        /// Gets or sets the size of salt that will be generated if no Salt was set
        /// </summary>
        public int SaltSize
        { get; set; }

        /// <summary>
        /// Gets or sets the plain text to be hashed
        /// </summary>
        public string PlainText
        { get; set; }

        /// <summary>
        /// Gets the base 64 encoded string of the hashed PlainText
        /// </summary>
        public string HashedText
        { get; private set; }

        /// <summary>
        /// Gets or sets the salt that will be used in computing the HashedText. This contains both Salt and HashIterations.
        /// </summary>
        public string Salt
        { get; set; }


        /// <summary>
        /// Compute the hash
        /// </summary>
        /// <returns>
        /// the computed hash: HashedText
        /// </returns>
        /// <exception cref="System.InvalidOperationException">PlainText cannot be empty</exception>
        public string Compute()
        {
            if (string.IsNullOrEmpty(PlainText)) throw new InvalidOperationException("PlainText cannot be empty");

            //if there is no salt, generate one
            if (string.IsNullOrEmpty(Salt))
                GenerateSalt();

            HashedText = calculateHash(HashIterations);

            return HashedText;
        }


        /// <summary>
        /// Compute the hash using default generated salt. Will Generate a salt if non was assigned
        /// </summary>
        /// <param name="textToHash"></param>
        /// <returns></returns>
        public string Compute(string textToHash)
        {
            PlainText = textToHash;
            //compute the hash
            Compute();
            return HashedText;
        }


        /// <summary>
        /// Compute the hash that will also generate a salt from parameters
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="saltSize">The size of the salt to be generated</param>
        /// <param name="hashIterations"></param>
        /// <returns>
        /// the computed hash: HashedText
        /// </returns>
        public string Compute(string textToHash, int saltSize, int hashIterations)
        {
            PlainText = textToHash;
            //generate the salt
            GenerateSalt(hashIterations, saltSize);
            //compute the hash
            Compute();
            return HashedText;
        }

        /// <summary>
        /// Compute the hash that will utilize the passed salt
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="salt">The salt to be used in the computation</param>
        /// <returns>
        /// the computed hash: HashedText
        /// </returns>
        public string Compute(string textToHash, string salt)
        {
            PlainText = textToHash;
            Salt = salt;
            //expand the salt
            expandSalt();
            Compute();
            return HashedText;
        }

        /// <summary>
        /// Generates a salt with default salt size and iterations
        /// </summary>
        /// <returns>
        /// the generated salt
        /// </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GenerateSalt()
        {
            if (SaltSize < 1) throw new InvalidOperationException(string.Format("Cannot generate a salt of size {0}, use a value greater than 1, recommended: 16", SaltSize));

            var rand = RandomNumberGenerator.Create();

            var ret = new byte[SaltSize];

            rand.GetBytes(ret);

            //assign the generated salt in the format of {iterations}.{salt}
            Salt = string.Format("{0}.{1}", HashIterations, Convert.ToBase64String(ret));

            return Salt;
        }

        /// <summary>
        /// Generates a salt
        /// </summary>
        /// <param name="hashIterations">the hash iterations to add to the salt</param>
        /// <param name="saltSize">the size of the salt</param>
        /// <returns>
        /// the generated salt
        /// </returns>
        public string GenerateSalt(int hashIterations, int saltSize)
        {
            HashIterations = hashIterations;
            SaltSize = saltSize;
            return GenerateSalt();
        }

        /// <summary>
        /// Get the time in milliseconds it takes to complete the hash for the iterations
        /// </summary>
        /// <param name="iteration"></param>
        /// <returns></returns>
        public int GetElapsedTimeForIteration(int iteration)
        {
            var sw = new Stopwatch();
            sw.Start();
            calculateHash(iteration);
            return (int)sw.ElapsedMilliseconds;
        }


        private string calculateHash(int iteration)
        {
            //convert the salt into a byte array
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);
            var pbkdf2 = new Rfc2898DeriveBytes(PlainText, saltBytes, iteration,HashAlgorithmName.SHA1);
            var key = pbkdf2.GetBytes(64);
            return Convert.ToBase64String(key);
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
            catch (Exception)
            {
                throw new FormatException("The salt was not in an expected format of {int}.{string}");
            }
        }

        /// <summary>
        /// Compare password hashes for equality. Uses a constant time comparison method.
        /// </summary>
        /// <param name="passwordHash1"></param>
        /// <param name="passwordHash2"></param>
        /// <returns></returns>
        public bool Compare(string passwordHash1, string passwordHash2)
        {
            if (passwordHash1 == null || passwordHash2 == null)
                return false;

            int min_length = Math.Min(passwordHash1.Length, passwordHash2.Length);
            int result = 0;

            for (int i = 0; i < min_length; i++)
                result |= passwordHash1[i] ^ passwordHash2[i];

            return 0 == result;
        }
    }
}

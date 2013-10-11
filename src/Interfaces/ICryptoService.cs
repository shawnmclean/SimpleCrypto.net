using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrypto
{
    /// <summary>
    /// Interface for Simple Crypto Service
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Gets or sets the number of iterations the hash will go through
        /// </summary>
        int HashIterations { get; set; }

        /// <summary>
        /// Gets or sets the size of salt that will be generated if no Salt was set
        /// </summary>
        int SaltSize { get; set; }

        /// <summary>
        /// Gets or sets the plain text to be hashed
        /// </summary>
        string PlainText { get; set; }

        /// <summary>
        /// Gets the base 64 encoded string of the hashed PlainText
        /// </summary>
        string HashedText { get; }

        /// <summary>
        /// Gets or sets the salt that will be used in computing the HashedText. This contains both Salt and HashIterations.
        /// </summary>
        string Salt { get; set; }
        
        /// <summary>
        /// Compute the hash
        /// </summary>
        /// <returns>the computed hash: HashedText</returns>
        string Compute();

        /// <summary>
        /// Compute the hash using default generated salt. Will Generate a salt if non was assigned
        /// </summary>
        /// <param name="textToHash"></param>
        /// <returns></returns>
        string Compute(string textToHash);

        /// <summary>
        /// Compute the hash that will also generate a salt from parameters
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="saltSize">The size of the salt to be generated</param>
        /// <param name="hashIterations"></param>
        /// <returns>the computed hash: HashedText</returns>
        string Compute(string textToHash, int saltSize, int hashIterations);

        /// <summary>
        /// Compute the hash that will utilize the passed salt
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="salt">The salt to be used in the computation</param>
        /// <returns>the computed hash: HashedText</returns>
        string Compute(string textToHash, string salt);

        /// <summary>
        /// Generates a salt with default salt size and iterations
        /// </summary>
        /// <returns>the generated salt</returns>
        string GenerateSalt();

        /// <summary>
        /// Generates a salt
        /// </summary>
        /// <param name="hashIterations">the hash iterations to add to the salt</param>
        /// <param name="saltSize">the size of the salt</param>
        /// <returns>the generated salt</returns>
        string GenerateSalt(int hashIterations, int saltSize);

        /// <summary>
        /// Get the time in milliseconds it takes to complete the hash for the iterations
        /// </summary>
        /// <param name="iteration"></param>
        /// <returns></returns>
        int GetElapsedTimeForIteration(int iteration);
        
        /// <summary>
        /// Compare the passwords for equality
        /// <param name="passwordHash1">The first password hash to compare</param>
        /// <param name="passwordHash2">The second password hash to compare</param>
        /// <returns>true: indicating the password hashes are the same, false otherwise.</param>
        bool Compare(string passwordHash1, string passwordHash2);
    }
}

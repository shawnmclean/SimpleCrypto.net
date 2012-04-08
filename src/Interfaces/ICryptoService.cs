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
        /// Gets or sets the salt that will be used in computing the HashedText
        /// </summary>
        string Salt { get; set; }

        /// <summary>
        /// Compute the hash
        /// </summary>
        /// <returns>the computed hash: HashedText</returns>
        string Compute();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="saltSize">The size of the salt to be generated</param>
        /// <param name="hashIterations"></param>
        /// <returns>the computed hash: HashedText</returns>
        string Compute(string textToHash, int saltSize = 16, int hashIterations = 50);
    }
}

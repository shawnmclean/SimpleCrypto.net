using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrypto
{
    public interface ICryptoService
    {
        int HashIterations { get; set; }

        int SaltSize { get; set; }

        string PlainText { get; set; }

        string HashedText { get; }

        string Salt { get; }

        string Compute(string textToHash);

        string Compute(string textToHash, int saltSize);

        string Compute(string textToHash, int saltSize, int hashIterations);
    }
}

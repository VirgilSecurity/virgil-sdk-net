using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.CryptoApi.Storage.Exceptions
{
    public class SecureStorageException : Exception
    {
        public SecureStorageException(string message) : base(message)
        {
        }
    }
}

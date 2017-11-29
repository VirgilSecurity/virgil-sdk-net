using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.CryptoApi.Storage.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key already exists.
    /// </summary>
    public class DuplicateKeySecureStorageException : SecureStorageException
    {
        public DuplicateKeySecureStorageException(string key)
            : base($"Specified key '{key}' is already exists in the secure storage.")
        {
        }
    }
}

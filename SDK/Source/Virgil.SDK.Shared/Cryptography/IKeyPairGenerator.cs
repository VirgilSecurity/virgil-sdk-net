namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// The implementation of <see cref="IKeyPairGenerator"/> interface is used to 
    /// generate pairs of public and private keys.
    /// </summary>
    public interface IKeyPairGenerator
    {
        KeyPair Generate(IDictionary<string, object> parameters);
    }
}
namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The implementation of <see cref="IKeyPairGenerator"/> interface is used to 
    /// generate pairs of public and private keys.
    /// </summary>
    public interface IKeyPairGenerator
    {
        KeyPair Generate(IKeyPairParameters parameters);
        KeyPair Generate();
    }
}
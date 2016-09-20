namespace Virgil.SDK.Client
{
    public interface ISignatureVerifier
    {
        bool Verify(SigningRequest signingRequest, RequestSignature signature);
    }
}
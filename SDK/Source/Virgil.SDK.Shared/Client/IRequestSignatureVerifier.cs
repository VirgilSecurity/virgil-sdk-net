namespace Virgil.SDK.Client
{
    public interface IRequestSignatureVerifier
    {
        bool Verify(CanonicalRequest request, RequestSignature signature);
    }
}
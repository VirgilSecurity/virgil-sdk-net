namespace Virgil.SDK.Client
{
    public interface ICanonicalRequestVerifier
    {
        bool Verify(CanonicalRequest request, RequestSignature signature);
    }
}
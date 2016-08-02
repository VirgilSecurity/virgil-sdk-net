namespace Virgil.SDK.Requests
{
    public class VirgilCardIssueRequest : VirgilCardRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardIssueRequest"/> class.
        /// </summary>
        public VirgilCardIssueRequest()
        {
        }

        public VirgilBuffer Fingerprint { get; }

        public void AddSign(VirgilBuffer sign)
        {
            throw new System.NotImplementedException();
        }
    }
}
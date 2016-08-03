namespace Virgil.SDK.Requests
{
    public class VirgilCardRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardRequest"/> class.
        /// </summary>
        public VirgilCardRequest
        (
            string identity, 
            string identityType, 
            byte[] publicKey,
            VirgilCardScope scope
        )
        {
        }

        private object Export()
        {
            throw new System.NotImplementedException();
        }
    }
}
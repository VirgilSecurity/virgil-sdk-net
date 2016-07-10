namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class VirgilCard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard()
        {
        }

        public Guid Id { get; private set; }
        public string Identity { get; private set; }
        public string IdentityType { get; private set; }
        public string AuthorizedBy { get; private set; }
        public IDictionary<string, string> Data { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        public static Task<IEnumerable<VirgilCard>> Search(string identity, string identityType)
        {
            throw new NotImplementedException();
        }
                
        public static Task<IEnumerable<VirgilCard>> SearchGlobal(string identity, VirgilIdentityType identityType)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Encrypt(VirgilBuffer buffer)
        {
            throw new NotSupportedException();
        }

        public bool Verify(VirgilBuffer data, VirgilBuffer sign)
        {   
            throw new NotSupportedException();
        }
    }
}
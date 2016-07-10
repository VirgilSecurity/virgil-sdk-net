namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VirgilPass : VirgilCard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilPass"/> class.
        /// </summary>
        private VirgilPass()
        {
        }

        public VirgilKey Key { get; private set; }

        public static Task<VirgilPass> Create(VirgilCardTicket ticket, VirgilKey key)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilPass> Import(VirgilBuffer ticket)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer SignThenEncrypt(VirgilBuffer buffer, IEnumerable<VirgilCard> recipients, bool includeMyselfAsRecipient = false)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer SignThenEncrypt(VirgilBuffer buffer, VirgilCard recipient, bool includeMyselfAsRecipient = false)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer DecryptThenVerify(VirgilBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Decrypt(VirgilBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Sign(VirgilBuffer buffer)
        {
            throw new NotImplementedException();
        }
    }
}
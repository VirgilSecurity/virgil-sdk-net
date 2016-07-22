namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The Virgil Card ticket is a data structure that represents user's identity, Public Key and other data. 
    /// The ticket is used to tell the Virgil Cards Service that the user's identity and Public Key are valid, 
    /// this kind of validation can be reached by validating signatures of owner's Private Key and 
    /// application's Private Key.
    /// </summary>
    public sealed class VirgilCardTicket
    {
        private readonly IDictionary<Guid, byte[]> signs;  

        /// <summary>
        /// Initializes a new instance of <see cref="VirgilCardTicket"/> class.
        /// </summary>
        public VirgilCardTicket(
            string identity, 
            string identityType, 
            byte[] publicKey,
            bool isGlobal,
            IDictionary<string, string> data = null)
        {
            this.Id = Guid.NewGuid();
            this.Identity = identity;
            this.IdentityType = identityType;
            this.PublicKey = publicKey;
            this.IsGlobal = isGlobal;

            this.signs = new Dictionary<Guid, byte[]>();

            if (data != null)
            {
                this.Data = new ReadOnlyDictionary<string, string>(data);
            }
        }

        /// <summary>
        /// Gets an unique idenitity of the ticket.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the user's identity value.
        /// </summary>
        public string Identity { get; }

        /// <summary>
        /// Gets the user's identity type.
        /// </summary>
        public string IdentityType { get; }

        /// <summary>
        /// Gets a Public Key value.
        /// </summary>
        public byte[] PublicKey { get; }

        /// <summary>
        /// Gets the canonical form of current <see cref="VirgilCard"/> instance.
        /// </summary>
        public byte[] Fingerprint
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VirgilCard"/> is global.
        /// </summary>
        public bool IsGlobal { get; }

        /// <summary>
        /// Gets the key/value parameters of future <see cref="VirgilCard"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; }

        /// <summary>
        /// Gets the list of digital signatures that was signed the <see cref="Fingerprint"/> of current <see cref="VirgilCardTicket"/>.
        /// </summary>
        public IReadOnlyDictionary<Guid, byte[]> Signs => new ReadOnlyDictionary<Guid, byte[]>(this.signs);

        /// <summary>
        /// Exports a current <see cref="VirgilCardTicket"/> to it's binary representation.
        /// </summary>
        public string Export()
        {
            //var ticketObject = new
            //{
            //    id = Guid.Empty,
            //    identity = this.Identity,
            //    identity_type = this.IdentityType,
            //    public_key = this.PublicKey,
            //    data = this.Data,
            //    is_global = this.IsGlobal,
            //    signs = this.signs
            //};

            //var json = JsonConvert.SerializeObject(ticketObject);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a signature of third party Private Keys.
        /// </summary>
        /// <param name="cardId">The <see cref="VirgilCard"/>'s identifier.</param>
        /// <param name="sign">The </param>
        /// <example>
        /// var keyPair = VirgilKeyPair.Generate(); 
        /// var ticket = new VirgilCardTicket("demo@virgilsecurity.com", "email", keyPair.PublicKey());
        /// 
        /// var ownerSign = CryptoHelper.Sign(ticket.Fingerprint, keyPair.PrivateKey());
        /// var appSign = CryptoHelper.Sign(ticket.Fingerprint, %APP_PRIVATE_KEY%);
        /// 
        /// ticket.AddOwnerSign(ownerSign);
        /// ticket.AddSign(%APP_CARD_ID%, appSign);
        /// </example>
        public void AddSign(Guid cardId, byte[] sign)
        {
            this.signs.Add(cardId, sign);
        }

        /// <summary>
        /// Imports the <see cref="VirgilCardTicket"/> from it's binary representation.
        /// </summary>
        public static VirgilCardTicket Import(string ticket)
        {
            throw new NotImplementedException();
        }
    }
}
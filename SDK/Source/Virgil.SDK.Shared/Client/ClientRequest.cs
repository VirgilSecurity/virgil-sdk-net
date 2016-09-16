namespace Virgil.SDK.Client
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    
    using Newtonsoft.Json;

    public class ClientRequest 
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        protected object Model { get; set; }
        
        /// <summary>
        /// Appends the owner's signature.
        /// </summary>
        public void AppendOwnerSignature()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Appends the signature.
        /// </summary>
        public void AppendSignature()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Calculates the fingerprint.
        /// </summary>
        public virtual string CalculateFingerprint()
        {
            if (this.Model == null)
                throw new NullReferenceException();

            var modelJson = JsonConvert.SerializeObject(this.Model);
            var modelData = Encoding.UTF8.GetBytes(modelJson);

            var sha = SHA256.Create();
            var fingerprint = sha.ComputeHash(modelData);
            var fingerprintHex = BitConverter.ToString(fingerprint);    

            return fingerprintHex;
        }
    }
}
namespace Virgil.SDK.Cryptography
{
    using Virgil.Crypto;

    public class Fingerprint
    {
        private readonly byte[] fingerprint;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        public Fingerprint(byte[] fingerprint)
        {
            this.fingerprint = fingerprint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        public Fingerprint(string fingerprintHex)
        {
            this.fingerprint = VirgilByteArrayUtils.HexToBytes(fingerprintHex);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns></returns>
        public byte[] GetValue()
        {
            return this.fingerprint;
        }

        /// <summary>
        /// To the hexadecimal.
        /// </summary>
        public string ToHEX()
        {
            return VirgilByteArrayUtils.BytesToHex(this.fingerprint);
        }
    }
}
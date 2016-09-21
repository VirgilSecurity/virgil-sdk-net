namespace Virgil.SDK.Cryptography
{
    using Virgil.Crypto;

    public class Fingerprint
    {
        private readonly byte[] fingerprintBytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        public Fingerprint(byte[] fingerprintBytes)
        {
            this.fingerprintBytes = fingerprintBytes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        public Fingerprint(string fingerprintHex)
        {
            this.fingerprintBytes = VirgilByteArrayUtils.HexToBytes(fingerprintHex);
        }

        public override string ToString()
        {
            return VirgilByteArrayUtils.BytesToHex(this.fingerprintBytes);
        }
    }
}
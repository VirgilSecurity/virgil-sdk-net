namespace Virgil.SDK.Cryptography
{
    using System;
    using Virgil.Crypto;

    internal static class VirgilCryptoExtentions
    {
        public static VirgilKeyPair.Type ToVirgilKeyPairType(this KeysType keysType)
        {
            VirgilKeyPair.Type type;

            switch (keysType)
            {
                case KeysType.Default:         type = VirgilKeyPair.Type.FAST_EC_ED25519; break;
                case KeysType.RSA_2048:        type = VirgilKeyPair.Type.RSA_2048; break;
                case KeysType.RSA_3072:        type = VirgilKeyPair.Type.RSA_3072; break;
                case KeysType.RSA_4096:        type = VirgilKeyPair.Type.RSA_4096; break;
                case KeysType.RSA_8192:        type = VirgilKeyPair.Type.RSA_8192; break;
                case KeysType.EC_SECP256R1:    type = VirgilKeyPair.Type.EC_SECP256R1; break;
                case KeysType.EC_SECP384R1:    type = VirgilKeyPair.Type.EC_SECP384R1; break;
                case KeysType.EC_SECP521R1:    type = VirgilKeyPair.Type.EC_SECP521R1; break;
                case KeysType.EC_BP256R1:      type = VirgilKeyPair.Type.EC_BP256R1; break;
                case KeysType.EC_BP384R1:      type = VirgilKeyPair.Type.EC_BP384R1; break;
                case KeysType.EC_BP512R1:      type = VirgilKeyPair.Type.EC_BP512R1; break;
                case KeysType.EC_SECP256K1:    type = VirgilKeyPair.Type.EC_SECP256K1; break;
                case KeysType.EC_CURVE25519:   type = VirgilKeyPair.Type.EC_CURVE25519; break;
                case KeysType.FAST_EC_X25519:  type = VirgilKeyPair.Type.FAST_EC_X25519; break;
                case KeysType.FAST_EC_ED25519: type = VirgilKeyPair.Type.FAST_EC_ED25519; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(keysType), keysType, null);
            }

            return type;
        }
    }
}
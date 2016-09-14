namespace Virgil.SDK.Cryptography
{
    using Virgil.Crypto;

    public enum VirgilKeyType
    {
        Default       = EC_ED25519,
        RSA_2048      = VirgilKeyPair.Type.RSA_2048,
        RSA_3072      = VirgilKeyPair.Type.RSA_3072,
        RSA_4096      = VirgilKeyPair.Type.RSA_4096,
        RSA_8192      = VirgilKeyPair.Type.RSA_8192,
        EC_SECP256R1  = VirgilKeyPair.Type.EC_SECP256R1,
        EC_SECP384R1  = VirgilKeyPair.Type.EC_SECP384R1,
        EC_SECP521R1  = VirgilKeyPair.Type.EC_SECP521R1,
        EC_BP256R1    = VirgilKeyPair.Type.EC_BP256R1,
        EC_BP384R1    = VirgilKeyPair.Type.EC_BP384R1,
        EC_BP512R1    = VirgilKeyPair.Type.EC_BP512R1,
        EC_SECP256K1  = VirgilKeyPair.Type.EC_SECP256K1,
        EC_CURVE25519 = VirgilKeyPair.Type.EC_CURVE25519,
        EC_ED25519    = VirgilKeyPair.Type.EC_ED25519
    }
}
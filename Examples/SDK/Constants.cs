namespace Virgil.Examples.SDK
{
    using System;
    using System.Text;

    public class Constants
    {
        public static readonly string AppToken = "45fd8a505f50243fa8400594ba0b2b29";
        public static readonly string EmailId = "virgil-demo@freeletter.me";
        public static readonly Guid PublicKeyId = Guid.Parse("9d9e4ec3-6473-0232-e22d-c5e96d38caeb");
        public static readonly byte[] PublicKey = Encoding.UTF8.GetBytes(
@"-----BEGIN PUBLIC KEY-----
MIGbMBQGByqGSM49AgEGCSskAwMCCAEBDQOBggAEcyMKg/5S3jmknEBpI8MyG/hT
VmPqjw8nSModca4Izrn6HKnqVr2UgUUSyHOC9G/cmhcNLVuLFQ6SSYzGp7qRPAMF
mpi1yJm6czqr6Q4jYg0D00b9KTH++5DyVPqkBRLt1UYmJn7pZGCk/RTjf0E+FrWD
sOvdP8UEb1XKOEDRS1w=
-----END PUBLIC KEY-----");
        public static readonly byte[] PrivateKey = Encoding.UTF8.GetBytes(
@"-----BEGIN EC PRIVATE KEY-----
MIHbAgEBBEEAjjFfgxHZR0lz/f01KoSA7Q9iZ64+pyM+6tzGTU0HjDUmolPDXZea
gMqKQLbX/PFFI149E5DPbgalyi6u93j16aALBgkrJAMDAggBAQ2hgYUDgYIABHMj
CoP+Ut45pJxAaSPDMhv4U1Zj6o8PJ0jKHXGuCM65+hyp6la9lIFFEshzgvRv3JoX
DS1bixUOkkmMxqe6kTwDBZqYtciZunM6q+kOI2INA9NG/Skx/vuQ8lT6pAUS7dVG
JiZ+6WRgpP0U439BPha1g7Dr3T/FBG9VyjhA0Utc
-----END EC PRIVATE KEY-----");
    }
}
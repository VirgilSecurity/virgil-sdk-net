namespace Virgil.SDK.Domain
{
    using Virgil.SDK.TransferObject;

    internal class PersonalCardStorageDto
    {
        public VirgilCardDto virgil_card { get; set; }
        public byte[] private_key { get; set; }
    }
}
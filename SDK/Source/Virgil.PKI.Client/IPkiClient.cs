namespace Virgil.PKI
{
    using Virgil.PKI.Clients;

    /// <summary>
    /// A Client for the Virgil PKI Client for API v1. You can read more about 
    /// the api here: http://developer.virgilsecurity.com.
    /// </summary>
    public interface IPkiClient
    {
        IAccountsClient Accounts { get; }

        IPublicKeysClient PublicKeys { get; }

        IUserDataClient UserData { get; }
    }
}
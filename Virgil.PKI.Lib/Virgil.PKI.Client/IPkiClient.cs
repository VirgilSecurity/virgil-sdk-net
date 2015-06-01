namespace Virgil.PKI
{
    using Virgil.PKI.Clients;
    using Virgil.PKI.Http;

    /// <summary>
    /// A Client for the Virgil PKI Client for API v1. You can read more about 
    /// the api here: http://developer.virgilsecurity.com.
    /// </summary>
    public interface IPkiClient
    {
        IConnection Connection { get; }

        IAccountsClient Accounts { get; }
        IPublicKeysClient PublicKeys { get; }
        IUserDataClient UserData { get; }
        ISignsClient Signs { get; }
    }
}
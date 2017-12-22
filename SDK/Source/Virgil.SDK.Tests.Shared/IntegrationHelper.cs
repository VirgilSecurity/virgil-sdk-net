using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    class IntegrationHelper
    {
        private static string AppCardId = ConfigurationManager.AppSettings["virgil:AppID"];
        private static string AccounId = ConfigurationManager.AppSettings["virgil:AccountID"];
        private static string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        private static string AppPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AppPrivateKeyBase64"];
        private static string ApiPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:ApiPrivateKeyBase64"];

        private static string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];
        public static VirgilCrypto Crypto = new VirgilCrypto();

        public static CardManager GetManager()
        {
            var apiPrivateKey = Crypto.ImportPrivateKey(
                Bytes.FromString(ApiPrivateKeyBase64, StringEncoding.BASE64));

            Func<Task<string>> obtainToken = async () =>
            {
                // emulate server response
                var data = new Dictionary<string, string>();
                data.Add("username", "anna");
                var builder = new AccessTokenBuilder(AccounId, AppCardId, TimeSpan.FromMinutes(1), data);
                var jwtFromServer = builder.Build(apiPrivateKey, Crypto);

                var jwt = JsonWebToken.From(jwtFromServer);
                return jwt.ToString();
            };

            var manager = new CardManager(new CardsManagerParams()
            {
                Crypto = Crypto,
                ApiUrl = CardsServiceAddress,
                AccessManager = new AccessManager(obtainToken),
            });
            return manager;
        } 
        public static async Task<Card> PublishCard(string identity, string previousCardId=null)
        {
            var keypair = Crypto.GenerateKeys();
            var csr = GetManager().GenerateCSR(new CSRParams
            {
                Identity = identity,
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey,
                PreviousCardId = previousCardId
            });

            var appPrivateKey = Crypto.ImportPrivateKey(
                Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64), AppPrivateKeyPassword);

            GetManager().SignCSR(csr, new SignParams
            {
                SignerCardId = AppCardId,
                SignerType = SignerType.App,
                SignerPrivateKey = appPrivateKey
            });
            return await GetManager().PublishCardAsync(csr);
        }

        public static async Task<Card> GetCard(string cardId)
        {
            return await GetManager().GetCardAsync(cardId);
        }

        internal static Task<IList<Card>> SearchCardsAsync(string aliceName)
        {
            return GetManager().SearchCardsAsync(aliceName);
        }
    }
}

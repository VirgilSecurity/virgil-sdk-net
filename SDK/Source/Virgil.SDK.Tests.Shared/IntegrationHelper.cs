using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Validation;
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
        private static string ServiceCardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"];
        private static string ServicePublicKeyPemBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyPemBase64"];

        private static string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];
        public static VirgilCardManagerCrypto CardManagerCrypto = new VirgilCardManagerCrypto();

        public static CardManager GetManager(string username = null)
        {
            Func<Task<string>> obtainToken = async () =>
            {
                var jwtFromServer = await EmulateServerResponseToBuildTokenRequest(username);

                var jwt = JsonWebToken.From(jwtFromServer);
                return jwt.ToString();
            };


            Func<string, Task<string>> signCallBackFunc = async (csrStr) =>
            {
                return await EmulateServerResponseToSignByAppRequest(csrStr);
            };

            var validator = new ExtendedValidator();
            validator.ChangeServiceCreds(ServiceCardId, ServicePublicKeyPemBase64);
            var manager = new CardManager(new CardsManagerParams()
            {
                CardManagerCrypto = CardManagerCrypto,
                ApiUrl = CardsServiceAddress,
                AccessManager = new AccessManager(obtainToken),
                SignCallBackFunc = signCallBackFunc,
                Validator = null

            });
            return manager;
        }

        private static Task<string> EmulateServerResponseToBuildTokenRequest(string username)
        {
            var serverResponse = Task<string>.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000); // simulation of long-term processing

                    var apiPrivateKey = CardManagerCrypto.ImportPrivateKey(
                        Bytes.FromString(ApiPrivateKeyBase64, StringEncoding.BASE64));

                    var data = new Dictionary<string, string>
                    {
                        {"username", username}
                    };
                    var builder = new AccessTokenBuilder(
                        AccounId,
                        AppCardId,
                        TimeSpan.FromMinutes(10),
                        apiPrivateKey,
                        CardManagerCrypto
                    );
                    var identity = SomeHash(username);
                    return builder.Build(identity, data);
                }
            );
            return serverResponse;
        }
        private static Task<string> EmulateServerResponseToSignByAppRequest(string csrStr)
        {
            var serverResponse = Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(1000); // simulation of long-term processing
                var appPrivateKey = CardManagerCrypto.ImportPrivateKey(
                    Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64), AppPrivateKeyPassword);

                var csr = CSR.Import(CardManagerCrypto, csrStr);
                csr.Sign(CardManagerCrypto, new SignParams
                {
                    SignerCardId = AppCardId,
                    SignerType = SignerType.App,
                    SignerPrivateKey = appPrivateKey
                });
                return csr.Export();
            });
            return serverResponse;
        }

        private static string SomeHash(string username)
        {
            return username;
        }
        public static async Task<Card> PublishCard(string username, string previousCardId = null)
        {
            var keypair = CardManagerCrypto.GenerateKeys();
            return await GetManager(username).PublishCardAsync(keypair.PrivateKey, previousCardId);
        }

        public static async Task<Card> GetCard(string cardId)
        {
            return await GetManager().GetCardAsync(cardId);
        }

        internal static Task<IList<Card>> SearchCardsAsync(string identity)
        {
            return GetManager().SearchCardsAsync(identity);
        }
    }
}

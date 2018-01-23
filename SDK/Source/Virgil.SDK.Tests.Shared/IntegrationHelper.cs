using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Validation;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    class IntegrationHelper
    {
        private static string AppCardId = ConfigurationManager.AppSettings["virgil:AppID"];
        private static string AccounId = ConfigurationManager.AppSettings["virgil:AccountID"];
        private static string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        private static string AccessPublicKeyId = ConfigurationManager.AppSettings["virgil:AccessPublicKeyId"];
        private static string AccessPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AccessPrivateKeyBase64"];
        private static string ServiceCardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"];
        private static string ServicePublicKeyPemBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyPemBase64"];
        private static string ServicePublicKeyDerBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyDerBase64"];

        private static string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];
        public static VirgilCardCrypto CardCrypto = new VirgilCardCrypto();
        public static VirgilCrypto Crypto = new VirgilCrypto();

        public static CardManager GetManager(string username = null)
        {
            Func<Task<string>> obtainToken = async () =>
            {
                var jwtFromServer = await EmulateServerResponseToBuildTokenRequest(username);

                return jwtFromServer;
            };


            Func<RawSignedModel, Task<RawSignedModel>> signCallBackFunc = async (model) =>
            {
                var response = await EmulateServerResponseToSignByAppRequest(model.ExportAsString());
                return RawSignedModel.GenerateFromString(response);
            };

            var validator = new VirgilCardVerifier() { VerifySelfSignature = true, VerifyVirgilSignature = true};
            validator.ChangeServiceCreds(ServiceCardId, ServicePublicKeyDerBase64);
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = CardCrypto,
                ApiUrl = CardsServiceAddress,
                AccessTokenProvider = new CallbackJwtProvider(null),
                SignCallBackFunc = signCallBackFunc,
                Verifier = validator
            });
            return manager;
        }

        private static Task<string> EmulateServerResponseToBuildTokenRequest(string username)
        {
            var serverResponse = Task<string>.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000); // simulation of long-term processing

                    var accessPrivatekey = Crypto.ImportPrivateKey(
                        Bytes.FromString(AccessPrivateKeyBase64, StringEncoding.BASE64));

                    var data = new Dictionary<string, string>
                    {
                        {"username", username}
                    };
                    var builder = new JwtGenerator(
                        AppCardId,
                        accessPrivatekey,
                        AccessPublicKeyId,
                        TimeSpan.FromMinutes(10),
                        new VirgilAccessTokenSigner()
                    );
                    var identity = SomeHash(username);
                    return builder.GenerateToken(identity, data).ToString();
                }
            );

            return serverResponse;
        }
        private static Task<string> EmulateServerResponseToSignByAppRequest(string modelStr)
        {
            var serverResponse = Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(1000); // simulation of long-term processing
                                    // var appPrivateKey = Crypto.ImportVirgilPrivateKey(
                                    //    Bytes.FromString(AccessPublicKeyId, StringEncoding.BASE64));
                var rawSignedModel = RawSignedModel.GenerateFromString(modelStr);
                //var csr = CSR.Import(CardCrypto, csrStr);
                /*csr.Sign(CardCrypto, new SignParams
                {
                    SignerCardId = AppCardId,
                    SignerType = SignerType.App,
                    SignerPrivateKey = appPrivateKey
                });*/
                return rawSignedModel.ExportAsString();
            });
            return serverResponse;
        }

        private static string SomeHash(string username)
        {
            return username;
        }
        public static async Task<Card> PublishCard(string username, string previousCardId = null)
        {
            var keypair = Crypto.GenerateKeys();

            return await GetManager(username).PublishCardAsync(
                new CardParams()
                {
                    PublicKey = keypair.PublicKey,
                    PrivateKey = keypair.PrivateKey,
                    PreviousCardId = previousCardId,
                    Meta = new Dictionary<string, string>
                    {
                        { "some meta key", "some meta val" }
                    }
                });
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

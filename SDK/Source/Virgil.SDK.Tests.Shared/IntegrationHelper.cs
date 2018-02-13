using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Virgil.Crypto;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Validation;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    class IntegrationHelper
    {
        public static string AppId = ConfigurationManager.AppSettings["virgil:AppID"];
        public static string AccounId = ConfigurationManager.AppSettings["virgil:AccountID"];
        public static string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        public static string ApiPublicKeyId = ConfigurationManager.AppSettings["virgil:AccessPublicKeyId"];
        public static string ApiPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AccessPrivateKeyBase64"];
        public static string ServiceCardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"];
        public static string ServicePublicKeyPemBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyPemBase64"];
        public static string ServicePublicKeyDerBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyDerBase64"];
        public static string CryptoCompatibilityDataPath = ConfigurationManager.AppSettings["test:CryptoCompatibilityDataPath"];
        public static string OutputTestDataPath = ConfigurationManager.AppSettings["test:OutputDataPath"];
        public static string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];

        public static string PrivateKeySTC31_1 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_1"];
        public static string PrivateKeySTC31_2 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_2"];
        public static string PublicKeySTC32 = ConfigurationManager.AppSettings["test:PublicKeySTC32"];
        public static string OldKeyStoragePath = ConfigurationManager.AppSettings["test:OldKeyStoragePath"];
        public static string OldKeyAliase = ConfigurationManager.AppSettings["test:OldKeyAliase"];

        public static string ImportedAccessPublicKeyId = ConfigurationManager.AppSettings["test:ImportedAccessPublicKeyId"];
        public static string ImportedAccessPublicKey = ConfigurationManager.AppSettings["test:ImportedAccessPublicKey"];
        public static string ImportedJwt = ConfigurationManager.AppSettings["test:ImportedJwt"];

        public static VirgilCardCrypto CardCrypto = new VirgilCardCrypto();
        public static VirgilCrypto Crypto = new VirgilCrypto();

        public static IPrivateKey ApiPrivateKey()
        {
            return Crypto.ImportPrivateKey(
                Bytes.FromString(ApiPrivateKeyBase64, StringEncoding.BASE64));
        }

        public static CardManager GetManager()
        {
            Func<TokenContext, Task<string>> obtainToken = async (TokenContext tokenContext) =>
            {
                var jwtFromServer = await EmulateServerResponseToBuildTokenRequest(tokenContext);

                return jwtFromServer;
            };


            Func<RawSignedModel, Task<RawSignedModel>> signCallBackFunc = async (model) =>
            {
                var response = await EmulateServerResponseToSignByAppRequest(model.ExportAsString());
                return RawSignedModelUtils.GenerateFromString(response);
            };

            var validator = new VirgilCardVerifier() { VerifySelfSignature = true, VerifyVirgilSignature = true};
            validator.ChangeServiceCreds(ServicePublicKeyDerBase64);
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = CardCrypto,
                ApiUrl = CardsServiceAddress,
                AccessTokenProvider = new CallbackJwtProvider(obtainToken),
                SignCallBack = signCallBackFunc,
                Verifier = validator
            });
            return manager;
        }

        public static Task<string> EmulateServerResponseToBuildTokenRequest(TokenContext tokenContext)
        {
            var serverResponse = Task<string>.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000); // simulation of long-term processing
                    var data = new Dictionary<object, object>
                    {
                        {"username", "my_username"}
                    };
                    var builder = new JwtGenerator(
                        AppId,
                        ApiPrivateKey(),
                        ApiPublicKeyId,
                        TimeSpan.FromMinutes(10),
                        new VirgilAccessTokenSigner()
                    );
                    var identity = SomeHash(tokenContext.Identity);
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
                var rawSignedModel = RawSignedModelUtils.GenerateFromString(modelStr);
               
                return rawSignedModel.ExportAsString();
            });
            return serverResponse;
        }

        private static string SomeHash(string identity)
        {
            return String.IsNullOrWhiteSpace(identity) ? "my_default_identity" : identity;
        }
        public static async Task<Card> PublishCard(string username, string previousCardId = null)
        {
            var keypair = Crypto.GenerateKeys();
            return await GetManager().PublishCardAsync(
                new CardParams()
                {
                    Identity = username,
                    PublicKey = keypair.PublicKey,
                    PrivateKey = keypair.PrivateKey,
                    PreviousCardId = previousCardId,
                    ExtraFields = new Dictionary<string, string>
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
//using PCLAppConfig;
using Virgil.Crypto;
using Virgil.CryptoAPI;
using Virgil.CryptoImpl;
using Virgil.SDK.Common;
using Virgil.SDK.Verification;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    public class IntegrationHelper
    {
        public static VirgilCardCrypto CardCrypto = new VirgilCardCrypto();
        public static VirgilCrypto Crypto = new VirgilCrypto();

        public static IPrivateKey ApiPrivateKey()
        {
            return Crypto.ImportPrivateKey(
                Bytes.FromString(AppSettings.ApiPrivateKeyBase64, StringEncoding.BASE64));
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
            validator.ChangeServiceCreds(AppSettings.ServicePublicKeyDerBase64);
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = CardCrypto,
                ApiUrl = AppSettings.CardsServiceAddress,
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
                        AppSettings.AppId,
                        ApiPrivateKey(),
                        AppSettings.ApiPublicKeyId,
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

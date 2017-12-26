using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
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
        public static VirgilCardManagerCrypto CardManagerCrypto = new VirgilCardManagerCrypto();

        public static CardManager GetManager(string identity=null)
        {


            Func<Task<string>> obtainToken = async () =>
            {
                // <emulate server response from client.PostAsync("https://app-demo/retrieve-token?username=my_username")  />
                var serverResponse = Task<string>.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000); // simulation of long-term processing

                    var apiPrivateKey = CardManagerCrypto.ImportPrivateKey(
                        Bytes.FromString(ApiPrivateKeyBase64, StringEncoding.BASE64));

                    var data = new Dictionary<string, string>
                    {
                        { "username", "my username" }
                    };
                    var builder = new AccessTokenBuilder(
                        AccounId, 
                        AppCardId,
                        TimeSpan.FromMinutes(10), 
                        apiPrivateKey, 
                        CardManagerCrypto
                        );
                    return builder.Build(identity, data);
                }
                );
                var jwtFromServer = await serverResponse;
                // </emulate server response>

                var jwt = JsonWebToken.From(jwtFromServer);
                return jwt.ToString();
            };

            Func<string, Task<string>> signCallBackFunc = async (csrStr) => 
            {
                // <emulate server response from client.PostAsync("https://app-demo/sign"){csr_str: "csr as string"}  />
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

               return await serverResponse;
            };

            var manager = new CardManager(new CardsManagerParams()
            {
                CardManagerCrypto = CardManagerCrypto,
                ApiUrl = CardsServiceAddress,
                AccessManager = new AccessManager(obtainToken),
                SignCallBackFunc = signCallBackFunc

            });
            return manager;
        } 
        public static async Task<Card> PublishCard(string identity, string previousCardId=null)
        {
            var keypair = CardManagerCrypto.GenerateKeys();
           /* var csr = GetManager().GenerateCSR(new CSRParams
            {
                Identity = identity,
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey,
                PreviousCardId = previousCardId
            });*/


            return await GetManager(identity).PublishCardAsync(keypair.PrivateKey, previousCardId);
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

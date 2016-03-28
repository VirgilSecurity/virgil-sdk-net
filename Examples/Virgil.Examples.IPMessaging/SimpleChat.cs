namespace Virgil.Examples.IPMessaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Virgil.Examples.Common;

    using Virgil.Crypto;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class SimpleChat
    {
        private static VirgilHub ServiceHub;

        private readonly IIPMessagingClient client;
        private readonly ChatMember currentMember;

        private IChannel channel;

        /// <summary>   
        /// Initializes a new instance of the <see cref="SimpleChat"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="chatMember">The chat member.</param>
        private SimpleChat(IIPMessagingClient client, ChatMember chatMember)
        {
            this.client = client;
            this.currentMember = chatMember;
        }
        
        /// <summary>
        /// Starts the message loop.
        /// </summary>
        private async Task StartMessaging()
        {
            this.channel = await this.client.JoinChannel("DARKSIDE");
            this.channel.MessageRecived += this.OnMessageRecived;
            
            var keepWorking = true;
            while (keepWorking)
            {
                var message = Console.ReadLine();
                await this.OnMessageSend(message);
            }
        }

        /// <summary>
        /// Fired when a current chat member sent a message.
        /// </summary>
        private async Task OnMessageSend(string message)
        {
            var channelRecipients = await this.GetChannelRecipients();

            var encryptedMessage = CryptoHelper.Encrypt(Encoding.UTF8.GetBytes(message), channelRecipients);
            var signature = CryptoHelper.Sign(encryptedMessage, this.currentMember.PrivateKey);

            var encryptedModel = new EncryptedMessageModel
            {
                Message = encryptedMessage,
                Sign = signature
            };

            var encryptedModelJson = JsonConvert.SerializeObject(encryptedModel);
            await this.channel.SendMessage(encryptedModelJson);
        }

        /// <summary>
        /// Fired when a new Message has been added to the channel on the server.
        /// </summary>
        private async Task OnMessageRecived(string sender, string message)
        {
            var foundCards = await ServiceHub.Cards.Search(sender);
            var senderCard = foundCards.Single();

            var encryptedModel = JsonConvert.DeserializeObject<EncryptedMessageModel>(message);

            var isValid = CryptoHelper.Verify(encryptedModel.Message, 
                encryptedModel.Sign, senderCard.PublicKey.PublicKey);

            if (!isValid)
            {
                throw new Exception("The massage signature is not valid");
            }

            var decryptedMessage = CryptoHelper.Decrypt(encryptedModel.Message, 
                this.currentMember.CardId.ToString(), this.currentMember.PrivateKey);

            Console.WriteLine($"\n{sender} - {Encoding.UTF8.GetString(decryptedMessage)}\n");
        }

        /// <summary>
        /// Gets the channel members with public keys and card IDs.
        /// </summary>
        private async Task<IDictionary<string, byte[]>> GetChannelRecipients()
        {
            var channelMembers = await this.channel.GetMembers();
            channelMembers = channelMembers.Where(it => !string.IsNullOrWhiteSpace(it));
            var cardsTasks = channelMembers.Select(cm => ServiceHub.Cards.Search(cm)).ToList();

            await Task.WhenAll(cardsTasks);

            var recipients = cardsTasks.Select(ct => ct.Result)
                .Where(it => it.Any())
                .Select(it => it.First())
                .ToDictionary(c => c.Id.ToString(), c => c.PublicKey.PublicKey);

            return recipients;
        }

        /// <summary>
        /// Setups the instance of <see cref="SimpleChat"/> and runs the message loop.
        /// </summary>
        public static async Task Launch()
        {
            ServiceHub = VirgilHub.Create(Constants.VirgilSimpleChatAccessToken);

            var emailAddress = Param<string>.Mandatory("Enter Email Address").WaitInput();
            var chatMember = await Authorize(emailAddress);

            var messagingClient = new IPMessagingClient(chatMember.Identity);
            var simpleChat = new SimpleChat(messagingClient, chatMember);

            Console.WriteLine("\nWelcome to DARKSIDE chat. Feel free to post here your DARK thoughts.\n");

            await simpleChat.StartMessaging();
        }
        
        /// <summary>
        /// Authorizes a chat member by email address.
        /// </summary>
        private static async Task<ChatMember> Authorize(string emailAddress)
        {
            // search the card by email identity on Virgil Keys service.
            var foundCards = await ServiceHub.Cards.Search(emailAddress);
            var theCard = foundCards.ToList().SingleOrDefault();

            // The app is verifying whether the user really owns the provided email 
            // address and getting a temporary token for public key registration 
            // (in case that the card is not registered, otherwise this token will be 
            // used to retrive a private key).

            var identityResponce = await ServiceHub.Identity
                .Verify(emailAddress, IdentityType.Email);

            Console.WriteLine("\nThe email with confirmation code has been sent to your email address. Please check it!\n");

            var confirmationCode =
                Param<string>.Mandatory("Enter Code: ").WaitInput();

            var identityToken = await ServiceHub.Identity
                .Confirm(identityResponce.ActionId, confirmationCode);

            if (theCard == null)
            {
                return await Register(identityToken);
            }

            Console.WriteLine("\nLoading member's keys...\n");

            var privateKeyResult = await ServiceHub.PrivateKeys.Get(theCard.Id, identityToken);
            var privateKey = privateKeyResult.PrivateKey;

            return new ChatMember(theCard, privateKey);
        }

        /// <summary>
        /// Generates and Registers a card for specified identity.
        /// </summary>
        private static async Task<ChatMember> Register(IdentityTokenDto identityToken)
        {
            Console.WriteLine("\nGenerating and Publishing the keys...\n");

            // generate a new public/private key pair.

            var keyPair = VirgilKeyPair.Generate();

            // The app is registering a Virgil Card which includes a 
            // public key and an email address identifier. The card will 
            // be used for the public key identification and searching 
            // for it in the Public Keys Service.

            var card = await ServiceHub.Cards
                .Create(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());

            // Private key can be added to Virgil Security storage if you want to
            // easily synchronise yout private key between devices.

            await ServiceHub.PrivateKeys.Stash(card.Id, keyPair.PrivateKey());

            return new ChatMember(card, keyPair.PrivateKey());
        }
    }
}
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
    using Virgil.SDK;
    using Virgil.SDK.Identities;

    public class SimpleChat
    {
        private static ServiceHub serviceHub;

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
            var foundCards = await serviceHub.Cards.Search(sender);
            var senderCard = foundCards.Single();

            var encryptedModel = JsonConvert.DeserializeObject<EncryptedMessageModel>(message);

            var isValid = CryptoHelper.Verify(encryptedModel.Message, 
                encryptedModel.Sign, senderCard.PublicKey.Value);

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
            var cardsTasks = channelMembers
                .Select(cm => serviceHub.Cards.Search(cm, includeUnconfirmed: true))
                .ToList();

            await Task.WhenAll(cardsTasks);

            var recipients = cardsTasks.Select(ct => ct.Result)
                .Where(it => it.Any())
                .Select(it => it.First())
                .ToDictionary(c => c.Id.ToString(), c => c.PublicKey.Value);

            return recipients;
        }

        /// <summary>
        /// Setups the instance of <see cref="SimpleChat"/> and runs the message loop.
        /// </summary>
        public static async Task Launch()
        {
            serviceHub = ServiceHub.Create(Constants.VirgilSimpleChatAccessToken);

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
            var foundCards = await serviceHub.Cards.Search(emailAddress, includeUnconfirmed: true);
            var theCard = foundCards.ToList().SingleOrDefault();

            // The app is verifying whether the user really owns the provided email 
            // address and getting a temporary token for public key registration 
            // (in case that the card is not registered, otherwise this token will be 
            // used to retrive a private key).

            if (theCard == null)
            {
                return await Register(emailAddress);
            }

            var emailVerifier = await serviceHub.Identity.VerifyEmail(emailAddress);

            Console.WriteLine("\nThe email with confirmation code has been sent to your email address. Please check it!\n");

            var confirmationCode =
                Param<string>.Mandatory("Enter Code: ").WaitInput();

            var identityInfo = await emailVerifier.Confirm(confirmationCode);

            Console.WriteLine("\nLoading member's keys...\n");

            var privateKeyResult = await serviceHub.PrivateKeys.Get(theCard.Id, identityInfo);
            var privateKey = privateKeyResult.PrivateKey;

            return new ChatMember(theCard, privateKey);
        }

        /// <summary>
        /// Generates and Registers a card for specified identity.
        /// </summary>
        private static async Task<ChatMember> Register(string email)
        {
            Console.WriteLine("\nGenerating and Publishing the keys...\n");

            // generate a new public/private key pair.

            var keyPair = VirgilKeyPair.Generate();

            // The app is registering a Virgil Card which includes a 
            // public key and an email address identifier. The card will 
            // be used for the public key identification and searching 
            // for it in the Public Keys Service.

            var emailVerifier = await serviceHub.Identity.VerifyEmail(email);

            Console.WriteLine("\nThe email with confirmation code has been sent to your email address. Please check it!\n");

            var confirmationCode =
                Param<string>.Mandatory("Enter Code: ").WaitInput();

            var identity = await emailVerifier.Confirm(confirmationCode);
            
            var card = await serviceHub.Cards.Create(identity, keyPair.PublicKey(), keyPair.PrivateKey());

            // Private key can be added to Virgil Security storage if you want to
            // easily synchronise yout private key between devices.

            await serviceHub.PrivateKeys.Stash(card.Id, keyPair.PrivateKey());

            return new ChatMember(card, keyPair.PrivateKey());
        }
    }
}
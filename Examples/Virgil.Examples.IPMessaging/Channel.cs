namespace Virgil.Examples.IPMessaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// A Channel represents a remote channel of communication between multiple IP Messaging clients. 
    /// Members can be added or invited to join channels. 
    /// </summary>
    public class Channel : IChannel
    {
        private readonly string identityToken;
        private readonly string channelName;
        private HttpClient httpClient;
        private string lastMessageId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="channelName">Name of the user.</param>
        /// <param name="identityToken">Name of the channel.</param>
        public Channel(string channelName, string identityToken)
        {
            this.identityToken = identityToken;
            this.channelName = channelName;
        }

        /// <summary>
        /// Send a message to a channel.
        /// </summary>
        /// <param name="message">
        /// A string message to send to this channel. You can also send structured data by serializing it into a string.
        /// </param>
        public async Task SendMessage(string message)
        {
            var jsonContent = JsonConvert.SerializeObject(new { message });
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await this.httpClient.PostAsync($"{Constants.IPMessagingAPIUrl}/channels/{this.channelName}/messages", content);
        }

        public Task Watch(CancellationToken cancellationToken)
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("X-IDENTITY-TOKEN", this.identityToken);

            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    var lastMessageIdUrlPart = string.IsNullOrWhiteSpace(this.lastMessageId) 
                        ? "" : $"?last_message_id={this.lastMessageId}";

                    var response = this.httpClient
                        .GetAsync($"{Constants.IPMessagingAPIUrl}/channels/{this.channelName}/messages{lastMessageIdUrlPart}", cancellationToken).Result;

                    cancellationToken.ThrowIfCancellationRequested();

                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeAnonymousType(responseString,
                        new[] { new { id = "", created_at = "", sender_identifier = "", message = "" } });

                    if (!responseObject.Any())
                    {
                        continue;
                    }

                    this.lastMessageId = responseObject.Last().id;
                    foreach (var responceItem in responseObject)
                    {
                        this.MessageRecived?.Invoke(responceItem.sender_identifier, responceItem.message);
                    }
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Gets a list of all channel members.
        /// </summary>
        public async Task<IEnumerable<string>> GetMembers()
        {
            var response = await this.httpClient
                .GetAsync($"{Constants.IPMessagingAPIUrl}/channels/{this.channelName}/members");

            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeAnonymousType(responseString, new[] { new { identifier = "" }});

            return responseObject
                .Where(it => !string.IsNullOrWhiteSpace(it.identifier))
                .Select(it => it.identifier).ToList();
        }

        /// <summary>
        /// Fired when a new Message has been added to the Channel on the server.
        /// </summary>
        public Func<string, string, Task> MessageRecived { get; set; }
    }
}
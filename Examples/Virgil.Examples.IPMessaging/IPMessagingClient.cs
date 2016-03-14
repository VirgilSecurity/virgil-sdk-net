namespace Virgil.Examples.IPMessaging
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    /// <summary>
    /// It is very simple implementation of IP messaging client that emulates 
    /// required functionality for current example.
    /// </summary>
    /// <seealso cref="IIPMessagingClient" />
    public class IPMessagingClient : IIPMessagingClient
    {
        private readonly string userName;
        private CancellationTokenSource channelCancellationTokenSource;
        private Task channelTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="IPMessagingClient"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public IPMessagingClient(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Join the Channel.
        /// </summary>
        /// <param name="channelName">Name of the channel.</param>
        /// <returns></returns>
        public async Task<IChannel> JoinChannel(string channelName)
        {
            var url = new Uri($"{Constants.IPMessagingAPIUrl}/channels/{channelName}/join");
            var client = new HttpClient();

            var jsonContent = JsonConvert.SerializeObject(new { identifier = this.userName });
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeAnonymousType(responseString, new { identity_token = ""});

            var channel = new Channel(channelName, responseObject.identity_token);
            this.channelCancellationTokenSource = new CancellationTokenSource();

            this.channelTask = channel.Watch(this.channelCancellationTokenSource.Token);

            return channel;
        }
    }   
}
namespace Virgil.Examples.IPMessaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A Channel represents a remote channel of communication between multiple IP Messaging clients. 
    /// Members can be added or invited to join channels. 
    /// </summary>
    public class Channel : IChannel
    {
        private readonly string userName;
        private readonly string channelName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="channelName">Name of the channel.</param>
        public Channel(string userName, string channelName)
        {
            this.userName = userName;
            this.channelName = channelName;
        }

        /// <summary>
        /// Send a message to a channel.
        /// </summary>
        /// <param name="message">
        /// A string message to send to this channel. You can also send structured data by serializing it into a string.
        /// </param>
        public Task SendMessage(string message)
        {
            return Task.Factory.StartNew(() =>
            {
                Task.Delay(2000);
                this.MessageRecived?.Invoke(this.userName, message);
            });
        }

        /// <summary>
        /// Gets a list of all channel members.
        /// </summary>
        public Task<IEnumerable<string>> GetMembers()
        {
            return Task.FromResult(new [] { this.userName }.AsEnumerable());
        }

        /// <summary>
        /// Fired when a new Message has been added to the Channel on the server.
        /// </summary>
        public Func<string, string, Task> MessageRecived { get; set; }
    }
}
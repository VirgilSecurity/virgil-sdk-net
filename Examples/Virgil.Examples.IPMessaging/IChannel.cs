namespace Virgil.Examples.IPMessaging
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A Channel represents a remote channel of communication between multiple IP Messaging clients. 
    /// Members can be added or invited to join channels. 
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// Send a message to a channel.
        /// </summary>
        /// <param name="message">
        /// A string message to send to this channel. You can also send structured data by serializing it into a string.
        /// </param>
        Task SendMessage(string message);

        /// <summary>
        /// Fired when a new Message has been added to the Channel on the server.
        /// </summary>
        Func<string, string, Task> MessageRecived { get; set; }

        /// <summary>
        /// Gets a list of all channel members.
        /// </summary>
        Task<IEnumerable<string>> GetMembers();
    }
}
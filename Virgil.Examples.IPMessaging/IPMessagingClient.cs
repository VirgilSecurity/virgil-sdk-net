namespace Virgil.Examples.IPMessaging
{
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IIPMessagingClient" />
    public class IPMessagingClient : IIPMessagingClient
    {
        private readonly string userName;

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
        public Task<IChannel> JoinChannel(string channelName)
        {
            return Task.FromResult((IChannel)new Channel(this.userName, channelName));
        }
    }   
}
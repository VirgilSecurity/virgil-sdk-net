namespace Virgil.Examples.IPMessaging
{
    using System.Threading.Tasks;

    public interface IIPMessagingClient
    {
        Task<IChannel> JoinChannel(string channelName);
    }
}
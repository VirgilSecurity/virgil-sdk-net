namespace Virgil.SDK.Keys.Domain
{
    public class ConfirmOptions
    {
        public static readonly ConfirmOptions Default = new ConfirmOptions();

        private ConfirmOptions()
        {
        }

        public ConfirmOptions(int timeToLive, int countToLive)
        {
            TimeToLive = timeToLive;
            CountToLive = countToLive;
        }

        public int TimeToLive { get; } = 3600;
        public int CountToLive { get; } = 1;
    }
}
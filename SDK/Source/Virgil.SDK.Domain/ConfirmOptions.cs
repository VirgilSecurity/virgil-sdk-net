namespace Virgil.SDK.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfirmOptions
    {
        public static readonly ConfirmOptions Default = new ConfirmOptions();

        private ConfirmOptions()
        {
        }

        public ConfirmOptions(int timeToLive, int countToLive)
        {
            this.TimeToLive = timeToLive;
            this.CountToLive = countToLive;
        }

        public int TimeToLive { get; } = 3600;
        public int CountToLive { get; } = 1;
    }
}
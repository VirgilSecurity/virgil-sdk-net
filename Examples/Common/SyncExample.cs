namespace Virgil.Examples.Common
{
    using System.Threading.Tasks;

    public abstract class SyncExample : Example
    {
        public abstract void Execute();

        public override Task Run()
        {
            return Task.Run(() => this.Execute());
        }
    }
}
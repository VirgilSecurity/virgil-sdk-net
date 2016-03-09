namespace Virgil.Examples.Common
{
    using System.Threading.Tasks;

    public abstract class AsyncExample : Example
    {
        public abstract Task Execute();

        public override Task Run()
        {
            return this.Execute();
        }
    }
}
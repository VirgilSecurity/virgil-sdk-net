namespace Virgil.Examples.Common
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public abstract class Example : IExample
    {
        private Stopwatch stopwatch;

        public abstract Task Run();

        public void StopWatch()
        {
            this.stopwatch.Stop();
        }

        public void DisplayElapsedTime()
        {
            Console.WriteLine("Elapsed time: {0}", new TimeSpan(this.stopwatch.ElapsedTicks));
        }

        public void StopAndDisplayElapsedTime()
        {
            this.StopWatch();
            this.DisplayElapsedTime();
        }

        public void StartWatch()
        {
            if (this.stopwatch == null)
            {
                this.stopwatch = new Stopwatch();
            }

            this.stopwatch.Start();
        }
    }
}
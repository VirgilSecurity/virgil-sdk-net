namespace Virgil.Examples.Common
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public abstract class Example : IExample
    {
        private Stopwatch stopwatch;

        public abstract Task Run();

        public void RestartWatch()
        {
            this.stopwatch.Restart();
        }

        public void StopWatch()
        {
            this.stopwatch.Stop();
        }

        public void DisplayElapsedTime()
        {
            this.DisplayElapsedTime("Elapsed time");
        }

        public void DisplayElapsedTime(string message)
        {
            Console.WriteLine("{0}: {1}ms", message, this.stopwatch.ElapsedMilliseconds);
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
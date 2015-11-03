namespace Virgil.Examples.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public class ExampleRunner
    {
        private readonly List<Type> examples;

        public ExampleRunner(Assembly assembly)
        {
            // extract all examples from current assembly

            var exampleType = typeof(IExample);

            this.examples = assembly.GetTypes()
                .Where(type => exampleType.IsAssignableFrom(type) && !type.IsAbstract).ToList();
        }

        public async Task Start()
        {
            var keepWorking = true;
            while (keepWorking)
            {
                this.DisplayExamplesMenu();

                var number = Convert.ToInt32(Console.ReadLine());
                keepWorking = number != 0;

                if (number > 0 && number <= this.examples.Count)
                {
                    var exampleType = this.examples[number - 1];
                    var example = (IExample)Activator.CreateInstance(exampleType);

                    Console.Clear();
                    Console.WriteLine(exampleType.GetExampleDescription() + Environment.NewLine);

                    try
                    {
                        await example.Run();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nError: {0}\n{1}", ex.Message, ex.StackTrace);
                    }

                    Console.ReadKey();
                }
            }
        }

        private void DisplayExamplesMenu()
        {
            Console.Clear();
            Console.WriteLine("Virgil Example Runner [Version {0}]\n(C) Copyright 2015, Virgil Security, Inc\n", 
                typeof(ExampleRunner).Assembly.GetName().Version);

            Console.WriteLine("Examples:{0}", Environment.NewLine);

            // display examples in numbered list.

            for (var index = 0; index < this.examples.Count; index++)
            {
                Console.WriteLine("{0,3} - {1}", index + 1, this.examples[index].GetExampleDescription());
            }

            Console.WriteLine("\n{0,3} - {1}", 0, "Exit");
            Console.Write("\nEnter example number: ");
        }
    }
}
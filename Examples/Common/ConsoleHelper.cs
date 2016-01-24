namespace Virgil.Examples.Common
{
    using System;

    public class ConsoleHelper
    {
        public static TValue ReadValue<TValue>(string message, Func<TValue, bool> validator = null)
        {
            TValue value = default(TValue);
            bool tryAgain;

            do
            {
                tryAgain = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message}: ");
                var textLine = Console.ReadLine();
                Console.ResetColor();
                
                try
                {
                    if (typeof(TValue).IsEnum)
                    {
                        value = (TValue)Enum.Parse(typeof(TValue), textLine);
                    }
                    else
                    {
                        value = (TValue)Convert.ChangeType(textLine, typeof(TValue));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();

                    tryAgain = true;
                }

                if (!tryAgain && validator == null)
                {
                    break;
                }

            } while (tryAgain || !validator(value));
            
            return value;
        }
    }
}
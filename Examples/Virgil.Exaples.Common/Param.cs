namespace Virgil.Examples.Common
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class Param<TValue>
    {
        private bool IsOptional { get; set; }
        private string Message { get; set; }
        private Func<TValue, bool> Validator { get; set; }
        private TValue[] PossibleValues { get; set; }

        public Param<TValue> WithValidator(Func<TValue, bool> validator)
        {
            this.Validator = validator;
            return this;
        }

        public Param<TValue> WithPossibleValues(TValue[] possibleValues)
        {
            this.PossibleValues = possibleValues;
            return this;
        }

        public TValue WaitInput()
        {
            var hasValidator = this.Validator != null;
            var isValueEnum = typeof(TValue).IsEnum;

            var displayMessage = $"{this.Message}{(this.IsOptional ? " (optional)" : "")}: ";

            // Display message
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(displayMessage);
            Console.ResetColor();

            var position = Console.CursorLeft;

            // Read text from Console input
            var textValue = (Console.ReadLine() ?? string.Empty).Trim();

            // Return default value is user skiped the entering 
            // any value and the readed line is empty..
            if (string.IsNullOrWhiteSpace(textValue) && !hasValidator && this.IsOptional)
            {
                // Display the default value
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.CursorTop -= 1;
                Console.CursorLeft = position;

                Console.WriteLine("Default");
                Console.ResetColor();

                return default(TValue);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(textValue) && !this.IsOptional)
                {
                    throw new ArgumentException("This parameter is required. Please enter the value.");
                }

                if (typeof (TValue) == typeof(bool))
                {
                    if (new[] {"yes", "y"}.Contains(textValue, StringComparer.Create(CultureInfo.CurrentCulture, true)))
                    {
                        textValue = "true";
                    }

                    if (new[] { "no", "n" }.Contains(textValue, StringComparer.Create(CultureInfo.CurrentCulture, true)))
                    {
                        textValue = "false";
                    }
                }

                // Try to parse the input value from Console line.
                var value = isValueEnum
                    // In case where value type is Enum
                    ? (TValue)Enum.Parse(typeof(TValue), textValue)
                    // Use common parser for all types.
                    : (TValue)Convert.ChangeType(textValue, typeof(TValue));

                if (hasValidator && !this.Validator(value))
                {
                    throw new ArgumentException("Value is not corrent. Please enter valid one.");
                }

                return value;
            }
            catch (Exception ex)
            {
                // Display the error in Red color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                return this.WaitInput();
            }
        }

        public static Param<TValue> Mandatory(string message)
        {
            return new Param<TValue>
            {
                Message = message,
                IsOptional = false
            };
        }
        public static Param<TValue> Optional(string message)
        {
            return new Param<TValue>
            {
                Message = message,
                IsOptional = true
            };
        }
    }
}
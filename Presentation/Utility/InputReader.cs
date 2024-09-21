using Presentation.Interfaces;
using Application.Interfaces;
using Application.Constants;

namespace Presentation.Utility
{
    public class InputReader : IInputReader
    {
        private readonly IValidationService _ValidationService;

        public InputReader(IValidationService ValidationService)
        {
            _ValidationService = ValidationService;
        }

        public string GetValidInput(string prompt, ValidationType validationType)
        {
            string input;
            bool isValidPattern;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This is a required field!");
                    isValidPattern = false;
                    continue;
                }

                isValidPattern = _ValidationService.IsValid(input, validationType);

                if (!isValidPattern)
                {
                    string formatPrompt = GetFormatPrompt(validationType);
                    Console.WriteLine($"Invalid {validationType} format. Please enter a valid format: {formatPrompt}");
                }

            } while (!isValidPattern);

            return input;
        }


        private string GetFormatPrompt(ValidationType validationType)
        {
            switch (validationType)
            {
                case ValidationType.Name:
                    return "Only alphabetical letters are allowed!";
                case ValidationType.MobileNumber:
                    return "Only numeric values are allowed for phone number, and it must be at least 10 digits long!";
                case ValidationType.Email:
                    return "Valid email format: abc@example.com";
                case ValidationType.Date:
                    return "Date format: YYYY-MM-DD, and cannot be greater than the current date.";
                default:
                    throw new ArgumentException("Invalid validation type.");
            }
        }
    }
}




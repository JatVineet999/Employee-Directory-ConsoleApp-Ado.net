using System.Text.RegularExpressions;
using Application.Constants;
using Application.Interfaces;
namespace Application.Utility
{
    public class ValidationService : IValidationService
    {
        public bool IsValid(string input, ValidationType validationType)
        {
            switch (validationType)
            {
                case ValidationType.Name:
                    // Only alphabetical letters are allowed
                    return Regex.IsMatch(input, @"^[A-Za-z\s]+$");
                case ValidationType.Email:
                    // Valid email format
                    return Regex.IsMatch(input, @"^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+\.[a-zA-Z]{2,}$");
                case ValidationType.MobileNumber:
                    // Only numeric values, at least 10 digits long
                    return Regex.IsMatch(input, @"^[0-9]{10,}$");
                case ValidationType.Date:
                    // Date format: YYYY-MM-DD, and cannot be greater than the current date
                    return DateTime.TryParse(input, out DateTime date) && date <= DateTime.Now;
                default:
                    throw new ArgumentException("Invalid validation type.");
            }
        }

    }
}
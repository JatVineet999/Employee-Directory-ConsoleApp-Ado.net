using Application.Constants;

namespace Application.Interfaces
{
    public interface IValidationService
    {
        bool IsValid(string input, ValidationType validationType);
    }
}

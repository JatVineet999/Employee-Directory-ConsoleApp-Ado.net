using Application.Constants;

namespace Presentation.Interfaces
{
    public interface IInputReader
    {
        string GetValidInput(string prompt, ValidationType validationType);
    }
}

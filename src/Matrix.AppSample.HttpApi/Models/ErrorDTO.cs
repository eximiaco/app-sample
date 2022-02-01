namespace Docway.Nursing.API.Models
{
    public sealed class ErrorDTO
    {
        public ErrorDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}

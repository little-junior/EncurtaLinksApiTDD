namespace EncurtaLinks.API.ErrorHandlers
{
    public record ErrorResponse(int StatusCode, string Error, string Message);
}

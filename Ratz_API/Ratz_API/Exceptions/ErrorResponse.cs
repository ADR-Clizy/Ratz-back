namespace Ratz_API.Models.DataTransferObjects
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        public ErrorResponse(string iError)
        {
            Error = iError;
        }
    }
}

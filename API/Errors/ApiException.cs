namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null ,string detailes = null) : base(statusCode, message)
        {
            Detailes = detailes;
        }

        public string Detailes {get ; set ; }
    }
}
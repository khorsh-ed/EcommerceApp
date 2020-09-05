using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
          return statusCode switch
          {
              400 => "Bad Request you have made",
              401 => "Authorized you are not",
              404 => "Resource Found , It was not",
              500 => "Errors are the path to dark side",
              _   => null
          };
        }

    }
}
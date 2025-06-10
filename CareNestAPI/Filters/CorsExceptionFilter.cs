using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CareNestAPI.Filters
{
    public class CorsExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Add CORS headers to error responses
            var response = context.HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            // Create a standardized error response
            var result = new ObjectResult(new
            {
                message = "An error occurred",
                error = context.Exception.Message
            })
            {
                StatusCode = 500
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
} 
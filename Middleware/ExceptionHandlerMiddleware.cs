using System.Text.Json;
using GraphQLPractise.Entities;

namespace GraphQLPractise.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var error = new ErrorResponse();

            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case KeyNotFoundException e:
                        error = new ErrorResponse()
                        {
                            Message = e.Message.ToString(),
                            StatusCode = StatusCodes.Status404NotFound
                        };
                        httpContext.Response.StatusCode = 404;
                        break;
                    case BadHttpRequestException e:
                        error = new ErrorResponse()
                        {
                            Message = e.Message.ToString(),
                            StatusCode = StatusCodes.Status404NotFound
                        };
                        break;
                }

                var json = JsonSerializer.Serialize(error);
                await httpContext.Response.WriteAsJsonAsync(json);
            }

        }
    }
}


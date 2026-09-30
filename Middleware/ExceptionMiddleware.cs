using System.Net;
using System.Text.Json;
namespace EmployeeManagementAPI.Middleware
{
public class  ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)//runs for every req
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                //sets status code and reurns 500 internal server error
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //reponse in json format
                context.Response.ContentType = "application/json";
                var response = new
                {
                    statusCode = context.Response.StatusCode,
                    Message = ex.Message
                };
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

    }
}
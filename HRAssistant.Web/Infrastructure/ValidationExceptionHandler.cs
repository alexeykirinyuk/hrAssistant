using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LiteGuard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HRAssistant.Web.Infrastructure
{
    public class ValidationExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandler(RequestDelegate next)
        {
            Guard.AgainstNullArgument(nameof(next), next);

            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Guard.AgainstNullArgument(nameof(httpContext), httpContext);

            try
            {
                await _next(httpContext);
            }
            catch (ValidationException exception)
            {
                httpContext.Response.Clear();

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";

                var errors = exception.Errors.Select(e => e.ErrorMessage);
                var errorListJson = JsonConvert.SerializeObject(errors);

                await httpContext.Response.WriteAsync(errorListJson);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidationExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionHandler>();
        }
    }
}

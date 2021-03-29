using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace OMDB_API.Attributes
{
    [AttributeUsage(AttributeTargets.Method)] //use on method
    public class ApiKeyAuth : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "apikey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(ApiKeyHeaderName);

            context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey);
            //check if api key input matches the actual key
            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedObjectResult("apikey was invalid");
                return;
            }
            await next();
        }
    }
}

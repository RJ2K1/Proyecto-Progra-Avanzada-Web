
namespace BackEnd.Middleware
{
    public class ApiKeyManager
    {
        private readonly RequestDelegate _requestDelegate;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyManager(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No contiene API Key");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key Incorrecto");
                return;
            }

            await _requestDelegate(context);
        }
    }




}

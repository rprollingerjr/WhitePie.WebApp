namespace WhitePie.WebApp.Middleware
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;

        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the maintenance mode is enabled
            var maintenanceMode = bool.Parse(Environment.GetEnvironmentVariable("MAINTENANCE_MODE") ?? "false");

            if (maintenanceMode)
            {
                // Check if the request path is not the maintenance page
                if (!context.Request.Path.Value.Contains("/maintenance") && !context.Request.Path.Value.Equals("/") && !IsExternalRequest(context))
                {
                    context.Response.Redirect("/maintenance");
                    return;
                }
            }
            else if (context.Request.Path.Value.Contains("/maintenance"))
            {
                context.Response.Redirect("/");
                return;
            }

            await _next(context);
        }

        private static bool IsExternalRequest(HttpContext context)
        {
            var requestPath = context.Request.Path.Value;
            return !requestPath.StartsWith("/");
        }
    }
}

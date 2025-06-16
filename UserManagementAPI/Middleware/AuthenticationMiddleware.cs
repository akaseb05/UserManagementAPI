namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKey = "12345";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // ✅ Skip authentication for Swagger and static files
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/favicon.ico"))
            {
                await _next(context);
                return;
            }

            // 🔒 Enforce API key check for all other routes
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key) || key != ApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }
}

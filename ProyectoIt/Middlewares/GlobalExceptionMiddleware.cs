using System.Net;
using System.Text.Json;

namespace WebUI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
                await LogToFileAsync(ex, context);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno del servidor",
                Details = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }



        private static async Task LogToFileAsync(Exception exception, HttpContext context)
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logPath);

            var logFile = Path.Combine(logPath, $"log-{DateTime.Now:dd-MM-yyyy}.txt");
            var logEntry = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] {context.Request.Method} {context.Request.Path} - {exception.Message}\n{exception.StackTrace}\n\n";

            await File.AppendAllTextAsync(logFile, logEntry);
        }

    }
}

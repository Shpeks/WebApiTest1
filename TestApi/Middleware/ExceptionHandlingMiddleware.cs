using System.Text.Json;

namespace TestApi.Middleware;

/// <summary>
/// Для глобальной обработки исключений в приложении.
/// Логирует ошибки и возвращает соответствующие HTTP-ответы в формате JSON.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Выполняет обработку запроса и отлавливает все необработанные исключения.
    /// Возвращает клиенту ответ с кодом ошибки и сообщением в формате JSON.
    /// </summary>
    /// <param name="context">Контекст текущего HTTP-запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;

            if (e is ArgumentException or FormatException)
            {
                _logger.LogError($"{StatusCodes.Status400BadRequest}: {e.Message}");
                statusCode = StatusCodes.Status400BadRequest;
                message = e.Message;
            }
            else
            {
                _logger.LogError(e, "Необработанная ошибка");
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Ошибка сервера";
            }
            
            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = message,
            };

            var json = JsonSerializer.Serialize(errorResponse);
            
            await context.Response.WriteAsync(json);
        }
    }
}
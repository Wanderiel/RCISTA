using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Handling;

public class ClobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ClobalExceptionHandler> _logger;

    public ClobalExceptionHandler(ILogger<ClobalExceptionHandler> logger) =>
        _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        (int statusCode, string? title) = exception switch
        {
            MissingUserException =>
                (StatusCodes.Status404NotFound, "Пользователь не найден."),
            MissingWorksationException =>
                (StatusCodes.Status404NotFound, "Рабочая станция не найдена"),
            MissingNonPaperMediaException =>
                (StatusCodes.Status404NotFound, "Диск не найдён."),
            NonPaperMediaAlreadyAssignedException =>
                (StatusCodes.Status409Conflict, "диск занят в другой рабочей станции."),
            _ =>
                (StatusCodes.Status500InternalServerError, "Внутрення ошибка сервера.")
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Произошло исключение. TraceId: {TraceId}", httpContext.TraceIdentifier);
        else
            _logger.LogWarning(exception.Message, "Произошло исключение. TraceId: {TraceId}", httpContext.TraceIdentifier);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Instance = httpContext.Request.Path,
                Extensions = { ["TraceId"] = httpContext.TraceIdentifier }
            },
            cancellationToken);

        return true;
    }
}

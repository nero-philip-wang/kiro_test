using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using AWSomeShop.Application.DTOs;

namespace AWSomeShop.API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, errorCode, message) = GetErrorDetails(exception);

        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            Error = new ErrorDetail
            {
                Code = errorCode,
                Message = message,
                Details = GetValidationDetails(exception)
            }
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        await context.Response.WriteAsync(json);
    }

    private static (int statusCode, string errorCode, string message) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ErrorCodes.UNAUTHORIZED, "未认证或 Token 已过期"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, ErrorCodes.NOT_FOUND, "资源不存在"),
            ArgumentException => (StatusCodes.Status400BadRequest, ErrorCodes.VALIDATION_ERROR, exception.Message),
            InvalidOperationException => (StatusCodes.Status400BadRequest, ErrorCodes.BAD_REQUEST, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, ErrorCodes.INTERNAL_ERROR, "服务器内部错误")
        };
    }

    private static List<FieldError>? GetValidationDetails(Exception exception)
    {
        if (exception is ValidationException validationEx && validationEx.Errors != null)
        {
            return validationEx.Errors.Select(e => new FieldError
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage
            }).ToList();
        }
        return null;
    }
}

public class ValidationException : Exception
{
    public List<ValidationError> Errors { get; }

    public ValidationException(string message) : base(message)
    {
        Errors = new List<ValidationError>();
    }

    public ValidationException(List<ValidationError> errors) : base("Validation failed")
    {
        Errors = errors;
    }
}

public class ValidationError
{
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
using DishFinder.Application.DTOs.Common;
using System.Net;
using System.Text.Json;

namespace DishFinder.Api.Middlewares;
public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context); // Continue to next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ApiError
        {
            Message = "An error occurred while processing your request.",
            Detail = exception.Message
        };

        switch (exception)
        {
            case UnauthorizedAccessException:
                errorResponse.StatusCode = StatusCodes.Status401Unauthorized;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                break;

            case KeyNotFoundException:
                errorResponse.StatusCode = StatusCodes.Status404NotFound;
                response.StatusCode = StatusCodes.Status404NotFound;
                break;

            case ArgumentException:
                errorResponse.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = exception.Message;
                break;

            default:
                errorResponse.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var json = JsonSerializer.Serialize(errorResponse);
        return response.WriteAsync(json);
    }
}


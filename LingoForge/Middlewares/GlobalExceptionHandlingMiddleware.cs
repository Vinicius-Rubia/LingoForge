using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace LingoForge.Middlewares;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, errorResponse) = exception switch
        {
            LingoForgeException ex => ((HttpStatusCode)ex.StatusCode, new BaseResponseErrorDTO(ex.GetErrors())),

            _ => (HttpStatusCode.InternalServerError, new BaseResponseErrorDTO($"Ocorreu um erro inesperado no servidor. {exception}"))
        };

        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

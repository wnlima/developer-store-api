using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Diagnostics;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public ValidationExceptionMiddleware(RequestDelegate next, Serilog.ILogger logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleGenericExceptionAsync(context, ex, StatusCodes.Status401Unauthorized);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleGenericExceptionAsync(context, ex, StatusCodes.Status404NotFound);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request error");
                await HandleGenericExceptionAsync(context, new Exception("Internal Server Error", ex), StatusCodes.Status500InternalServerError);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }

        private Task HandleGenericExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new ApiResponse
            {
                Success = false,
                Message = exception.Message,
            };

            if (Debugger.IsAttached || _env.EnvironmentName == "Test")
                response.Message = exception.ToString();

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}

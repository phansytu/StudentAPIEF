using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentAPIw6.Exceptions;
namespace StudentAPIw6.handler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            if (exception is ValidationException validationException)
            {
                _logger.LogWarning(
                    exception,
                    "Validation thất bại tại {Path}",
                    httpContext.Request.Path
                );

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var problemDetails =
                    new HttpValidationProblemDetails(errors)
                    {
                        Status =
                            StatusCodes.Status400BadRequest,

                        Title = "Dữ liệu không hợp lệ",

                        Detail =
                            "Vui lòng kiểm tra lại dữ liệu gửi lên.",

                        Instance =
                            httpContext.Request.Path
                    };

                httpContext.Response.StatusCode =
                    StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(
                    problemDetails,
                    cancellationToken
                );

                return true;
            }
            var (statusCode, title) = exception switch
            {
                NotFoundException =>
                     (
                         StatusCodes.Status404NotFound,
                         "Không tìm thấy tài nguyên"
                     ),

                BadRequestException =>
                     (
                         StatusCodes.Status400BadRequest,
                         "Yêu cầu không hợp lệ"
                     ),

                _ =>
                    (
                        StatusCodes.Status500InternalServerError,
                        "Lỗi hệ thống"
                    )
            };
            if (statusCode >= 500)
            {
                _logger.LogError(
                    exception,
                    "Lỗi hệ thống tại {Path}",
                    httpContext.Request.Path
                );
            }
            else
            {
                _logger.LogWarning(
                    exception,
                    "Lỗi request tại {Path}: {Message}",
                    httpContext.Request.Path,
                    exception.Message
                );
            }

            var response = new ProblemDetails
            {
                Status = statusCode,

                Title = title,

                Detail = exception.Message,

                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode =
                statusCode;

            await httpContext.Response.WriteAsJsonAsync(
                response,
                cancellationToken
            );

            return true;
        }
    }
}
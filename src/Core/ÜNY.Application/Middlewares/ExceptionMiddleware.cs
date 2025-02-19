using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ÜNY.Application.Exceptions;

namespace ÜNY.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new { Message = exception.Message, StatusCode = 404 };
                    break;
                case ValidationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new { Message = exception.Message, StatusCode = 400 };
                    break;
                case BusinessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    response = new { Message = exception.Message, StatusCode = 409 };
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new { Message = "An unexpected error occurred.", StatusCode = 500 };
                    break;
            }

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}

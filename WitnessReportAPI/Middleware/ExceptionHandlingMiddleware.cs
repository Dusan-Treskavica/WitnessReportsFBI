using Common.Constants;
using Common.Error;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WitnessReportAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
       
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch(Exception ex)
            {
                await this.HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            ErrorModel errorModel = new ErrorModel();
            httpContext.Response.ContentType = "application/json";

            if(exception is RequestProcessingException)
            {
                errorModel = this.HandleRequestProcessingException(httpContext, (RequestProcessingException)exception);
            }
            else if(exception is DatabaseException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorModel.StatusCode = httpContext.Response.StatusCode;
                errorModel.Message = exception.Message;
            }
            else if(exception is Exception)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorModel.StatusCode = httpContext.Response.StatusCode;
                errorModel.Message = exception.Message;
            }

            await httpContext.Response.WriteAsync(errorModel.ToString());
        }

        private ErrorModel HandleRequestProcessingException(HttpContext httpContext, RequestProcessingException exception)
        {
            switch (exception.HttpStatusCode)
            {
                case HTTPResponseCodes.BAD_REQUEST:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case HTTPResponseCodes.UNAUTHORIZED:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case HTTPResponseCodes.NOT_FOUND:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case HTTPResponseCodes.NOT_ALLOWED:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    break;
                case HTTPResponseCodes.CONFLICT_RESOURCES:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case HTTPResponseCodes.INTERNAL_SERVER_ERROR:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return new ErrorModel
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            };
        }
    }
}

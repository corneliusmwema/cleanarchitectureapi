using API.Common;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Handle NotFoundException
            if (exception is NotFoundError)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse<object>(exception.Message, 404);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 404
                };
                context.ExceptionHandled = true;
                return;
            }

            // Handle UnauthorizedException
            if (exception is UnauthorizedAccess)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse<object>(exception.Message, 401);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 401
                };
                context.ExceptionHandled = true;
                return;
            }


            // Handle BadRequestException
            if (exception is BadRequestError)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse<object>(exception.Message, 400);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
                return;
            }

            // Handle ForbiddenException
            if (exception is ForbiddenError)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse<object>(exception.Message, 403);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 403
                };
                context.ExceptionHandled = true;
                return;
            }

            // Handle ValidationException
            if (exception is ValidationException validationException)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse(validationException.Message, 422, validationException.Errors);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 422
                };
                context.ExceptionHandled = true;
                return;
            }


            // Handle InvalidFileExtension

            if (exception is InvalidFileExtension)
            {
                var errorResponse = ApiResponseUtility.CreateErrorResponse<object>(exception.Message, 400);
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
                return;
            }

            // Handle other exceptions
            var genericErrorResponse = ApiResponseUtility.HandleException(exception);
            context.Result = new ObjectResult(genericErrorResponse)
            {
                StatusCode = genericErrorResponse.Status
            };

            context.ExceptionHandled = true; // Mark exception as handled
        }
    }
}
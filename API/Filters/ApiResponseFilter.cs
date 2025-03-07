using API.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing before the action executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is not ApiResponse<object>)
                {
                    var statusCode = objectResult.StatusCode ?? 200;
                    var message = statusCode switch
                    {
                        201 => "Resource created successfully.",
                        204 => "No content.",
                        _ => "Operation completed successfully."
                    };
                    var apiResponse = ApiResponseUtility.CreateSuccessResponse(objectResult.Value, message, statusCode);
                    context.Result = new ObjectResult(apiResponse)
                    {
                        StatusCode = statusCode
                    };
                }
            }
            else if (context.Result is EmptyResult)
            {
                var apiResponse = ApiResponseUtility.CreateSuccessResponse<object>(null);
                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = 200
                };
            }
        }
    }
}
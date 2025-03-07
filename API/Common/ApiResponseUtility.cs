namespace API.Common;

public static class ApiResponseUtility
{
    public static ApiResponse<T> CreateSuccessResponse<T>(T data, string message = "Operation completed successfully.",
        int status = 200)
    {
        return new ApiResponse<T>(true, message, status, data);
    }

    public static ApiResponse<T> CreateErrorResponse<T>(string message, int statusCode = 500, T? errors = default)
    {
        return new ApiResponse<T>(false, message, statusCode, errors);
    }

    public static ApiResponse<object> HandleException(Exception exception)
    {
        var message = exception.Message;
        var statusCode = 500;

        switch (exception)
        {
            case KeyNotFoundException _:
                message = "The requested resource was not found.";
                statusCode = 404;
                break;
            // Add more cases as needed
        }

        return CreateErrorResponse<object>(message, statusCode);
    }
}
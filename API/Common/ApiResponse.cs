namespace API.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, string message, int status, T? data = default)
        {
            Success = success;
            Message = message;
            Status = status;
            Data = data;
        }
    }
}

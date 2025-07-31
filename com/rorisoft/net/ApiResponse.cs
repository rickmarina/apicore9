namespace com.rorisoft.net
{
    public abstract class ApiResponse<T> 
    {
        public bool Success { get; set; } = true;
        public string? Error { get; set; }
        public T? Entity { get; set; }
        public ApiResponse(T? entity = default, string? error = null)
        {
            Entity = entity;
            Error = error;
            Success = error is null;
        }
    }
}

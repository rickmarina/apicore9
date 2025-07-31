namespace com.rorisoft.net
{
    public class ApiErrorResponse<T> : ApiResponse<T>
    {
        public ApiErrorResponse(string msg) : base(default, msg) { }

    }
}

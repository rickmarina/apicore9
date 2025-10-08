namespace com.rorisoft.net
{
    public class ApiErrorResponse : ApiResponse<object>
    {
        public ApiErrorResponse(string msg) : base(default, msg) { }

    }
}

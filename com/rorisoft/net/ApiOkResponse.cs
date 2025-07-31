namespace com.rorisoft.net
{
    public class ApiOkResponse<T> : ApiResponse<T>
    {
        public ApiOkResponse(T entity) : base(entity, null) { }

    }
}

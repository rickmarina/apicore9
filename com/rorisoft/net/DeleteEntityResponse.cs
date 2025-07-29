namespace com.rorisoft.net
{
    public class DeleteEntityResponse<TEntity> : ServiceResponseBase where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}

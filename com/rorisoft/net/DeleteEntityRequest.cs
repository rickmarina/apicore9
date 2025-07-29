namespace com.rorisoft.net
{
    public class DeleteEntityRequest<TEntity> : ServiceRequestBase where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}

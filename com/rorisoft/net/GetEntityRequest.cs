namespace com.rorisoft.net
{
    public class GetEntityRequest<TEntity> : ServiceRequestBase where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}

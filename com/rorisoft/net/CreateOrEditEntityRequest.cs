namespace com.rorisoft.net
{
    public class CreateOrEditEntityRequest<TEntity> : ServiceRequestBase where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}

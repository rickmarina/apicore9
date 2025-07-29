namespace com.rorisoft.net
{
    public class CreateOrEditEntityResponse<TEntity> : ServiceResponseBase where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}

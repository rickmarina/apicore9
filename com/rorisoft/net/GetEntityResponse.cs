namespace com.rorisoft.net
{
    public class GetEntityResponse<TEntity> : ServiceResponseBase where TEntity : class
    {
        private TEntity _entity;

        public TEntity Entity {
            get {
                return _entity;
            }
            set {
                _entity = value;
                StopDelta();
            }
             
        }

        public GetEntityResponse(bool delta = false) : base(delta)
        {

        }
    }
}

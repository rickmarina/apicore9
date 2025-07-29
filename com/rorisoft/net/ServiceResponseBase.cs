using System.Diagnostics;

namespace com.rorisoft.net
{
    public abstract class ServiceResponseBase
    {
        public string Message { get; set; }
        public List<CommentModel> Comments { get; set; }
        //public Exception Exception { get; set; } = null; No se puede serializar System.Type por razones de seguridad 
        public ExceptionInfoModel exceptionInfo { get; set; }
        public bool Success { get; set; }
        public long Delta { get; set; }
        public string redirectUrl { get; set; }

        private Stopwatch sw;

        public ServiceResponseBase(bool delta = false)
        {
            Success = true;
            Comments = [];

            if (delta) {
                StartDelta();
            }

        }

        public void StartDelta()
        {
            sw = new Stopwatch();
            sw.Start();
        }

        public void StopDelta()
        {
            if (sw != null) { 
                sw.Stop();
                Delta = sw.ElapsedMilliseconds;
            }
        }

        public void ProcessException(Exception ex)
        {
            Success = false;
            exceptionInfo = new ExceptionInfoModel() {
                Module = ex.TargetSite?.Module.ToString() ?? string.Empty,
                Method = ex.TargetSite?.Name.ToString() ?? string.Empty,
                Message = ex.Message,
                Target = ex.TargetSite?.ToString() ?? string.Empty,
                InnerException = ex.InnerException?.Message ?? string.Empty
                };
        }

    }
}

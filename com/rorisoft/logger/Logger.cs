namespace com.rorisoft.log2
{
    public class Logger<T> where T : ILogData , new()
    {
        private readonly IEnumerable<ILogBehaviour> _logBehaviours;
        public T logData { get; set; }
        public Logger(IEnumerable<ILogBehaviour> logs, string className, string method) 
        {
            this.logData = new T() { className = className, method = method };
            this._logBehaviours = logs;
        }

        public void setClassName(string cn)
        {
            this.logData.className = cn;
        }

        public void setMethod(string m)
        {
            this.logData.method = m;
        }

        public void log()
        {
            foreach (var b in _logBehaviours)
            {
                b.log(logData);
            }
        }

        public void log(string msg)
        {
            foreach (var b in _logBehaviours)
            {
                b.log(msg, logData);
            }
        }
    }
}

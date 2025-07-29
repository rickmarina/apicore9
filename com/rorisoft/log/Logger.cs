using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.log
{
 
    [Obsolete("Utilizar Logger de com.rorisoft.log2")]
    public abstract class Logger<T> : ILogable<T> where T : ILogData, new()
    {
        public T _info;

        public Logger()
        {
            this._info = new T();
        }
        public Logger(T info)
        {
            this._info = new T();
            this._info = info;
        }

        public Logger(string classname) : base()
        {
            this._info = new T();
            this._info.className = classname;
        }

        public abstract int executeLog();
        public abstract int executeLog(string msg);
        public void setClass(string className) {
            this._info.className = className;
        }

        public void setLogData(T log)
        {
            this._info = log;
        }

        public void setMethod(string methodName)
        {
            this._info.method = methodName;
        }
    }
}

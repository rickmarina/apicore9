using com.rorisoft.log;
using System;
using System.Data;

namespace com.rorisoft.db
{
    public interface IDBContext
    {
        void addParameter(string param, bool val);
        void addParameter(string param, DateTime val);
        void addParameter(string param, DateTime? val);
        void addParameter(string param, DBNull val);
        void addParameter(string param, decimal val);
        void addParameter(string param, double val);
        void addParameter(string param, float val);
        void addParameter(string param, long val);
        void addParameter(string param, string val);
        void addParameterLike(string param, string val);
        void addParameterOptional(string param, int? val);
        void addParamter(string param, int val);
        bool canRead();
        void clearParameters();
        void close();
        void closeAndDispose();
        void closeReader();
        void conectar();
        void Connect();
        void Dispose();
        int ejecutarQuery();
        int ejecutarScalar();
        T? ejecutarScalar<T>();
        T? ejecutarScalarWithIdentity<T>();
        int executeLog();
        int executeLog(string msg);
        ILogable<ILogData>? getLogger();
        IDataReader? getReader();
        IDataReader? getReader(DBFactory.TIPODB tipo);
        bool HasRows();
        bool IsProcedimientoAlmacenado();
        string obtenerTrazaQuery();
        void queryReader();
        string queryResultString();
        DataSet? querySet();
        void setCadenaConexion(string cad);
        void setClass(string className);
        void setComando(string q);
        void setLogData(ILogData log);
        void setMethod(string methodName);
        void setProcedimientoAlmacenado(string sp);
        void setTimeOut(int t);
        void TransformarParametrosMotor(string orig, string dest);
    }
}
using com.rorisoft.log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.rorisoft.db
{
    public interface InterfaceDB : IDisposable
    {


        void setCadenaConexion(string cad);

        void Connect();

        void beginTransaction(String transname);
        void linkTransaction();
        void rollBackTransaction();
        void commitTransaction();

        //long getLastIdentity(InterfaceDB db);

        DataSet Query(String q);
        DataSet Query();

        void setComando(String q);
        void setComando(String q, long limite);
        void setComandoTimeOut(String q, int t);
        void setTimeOut(int t);
        void clearParameters();
        void addParameter(String param, String val);
        void addParameter(String param, DBNull val);
        void addParameter(String param, int val);
        void addParameter(String param, decimal val);
        void addParameter(String param, double val);
        void addParameter(String param, long val);
        void addParameter(String param, bool val);
        void addParameter(String param, DateTime val);
        void addParameter(String param, Nullable<DateTime> val);
        void addParameter(String param, Nullable<Double> val);
        void addParameterLike(String param, String value);

        /// <summary>
        /// Permite transformar los parámetros reemplazando la cadena origen por destino
        /// Por ejemplo en SQL Server los parámetros se pasan siempre con @, sin embargoo mysql no admite el nombre de parámetros con @
        /// se invocaría esta función antes de su ejecución para no tener que cambiar las inclusiones de los parámetros
        /// </summary>
        /// <param name="orig">Cadena a reemplazar</param>
        /// <param name="dest">Cadena nueva</param>
        void TransformarParametrosMotor(string orig, string dest);

        //ejecuciones de consultas
        int ejecutarQueryWithExitParameter();
        int ejecutarQuery();
        int ejecutarQuery(String q);
        int ejecutarScalar();
        double ejecutarScalarDouble();
        decimal ejecutarScalarDecimal();
        T ejecutarScalar<T>();
        T ejecutarScalarWithIdentity<T>();

        String queryResultString();
        void procedimientoAlmacenado(String q);
        bool IsProcedimientoAlmacenado();

        void QueryReader();
        void QueryReader(String q);
        String obtenerQueryConParametros();
        bool Read();
        IDataReader? getReader();
        bool hasRows();

        Dictionary<String, String> crearDiccionario(String q, String campoClave, String campoValor);
        Dictionary<String, String> crearDiccionario(String campoClave, String campoValor);

        void Close();

        void CloseAndDispose();
        //void Dispose();
        void CloseReader();

    }
}

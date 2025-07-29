﻿using com.rorisoft.excepcion;
using com.rorisoft.log;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.rorisoft.db
{
    public class MotorDBMysql : InterfaceDB, IDisposable
    {
        //Variables privadas
        private MySqlDataReader? reader;
        private MySqlCommand? comando;
        private MySqlConnection? conexion;
        private String cadenaConexion;
        private MySqlDataAdapter? adapter;
        private MySqlTransaction? trans;

        public MotorDBMysql(string cadena)
        {
            this.cadenaConexion = cadena;
        }

        public void setCadenaConexion(string cad)
        {
            this.cadenaConexion = cad;
        }

        public void Connect()
        {
            try
            {
                if (this.conexion?.State != ConnectionState.Open) { 
                    this.conexion = new MySqlConnection(this.cadenaConexion);
                    this.conexion.Open();
                }
            }
            catch (MySqlException ex)
            {
                throw new BBDDException(BBDDException.TipoError.Fatal, "Error al conectar a la DB: ", ex);
            }
        }

        //-- Gestión de transacciones --
        #region "Gestión de transacciones"
        public void beginTransaction(String transname) { this.trans = this.conexion.BeginTransaction(IsolationLevel.ReadCommitted); }
        public void linkTransaction() { this.comando.Transaction = this.trans; }
        public void rollBackTransaction() { this.trans.Rollback(); }
        public void commitTransaction() { this.trans.Commit(); }
        #endregion

        //public static long getLastIdentity(InterfaceDB db)
        //{
        //    String query = "SELECT @@identity";
        //    db.setComando(query);
        //    long resultado = db.ejecutarScalar();
        //    return resultado;
        //}

        public DataSet Query(String q)
        {
            try
            {
                if (this.conexion.State == ConnectionState.Closed) { Connect(); }
                this.comando = new MySqlCommand(q, this.conexion);
                this.comando.CommandType = CommandType.Text;
                this.adapter = new MySqlDataAdapter(this.comando);

                DataSet ds = new DataSet();
                this.adapter.Fill(ds);

                return ds;
            }
            catch (NullReferenceException ex) { throw new BBDDException(BBDDException.TipoError.Critica, "Dataset vacío", ex); }
            catch (MySqlException exsql) { throw new BBDDException(BBDDException.TipoError.Fatal, "Error al conectar db", exsql); }
        }

        public DataSet Query()
        {
            try
            {
                if (this.conexion.State == ConnectionState.Closed) { Connect(); }
                this.comando.CommandType = CommandType.Text;
                this.adapter = new MySqlDataAdapter(this.comando);

                DataSet ds = new DataSet();
                this.adapter.Fill(ds);

                return ds;
            }
            catch (NullReferenceException ex) { throw new BBDDException(BBDDException.TipoError.Critica, "Dataset vacío", ex); }
            catch (MySqlException exsql) { throw new BBDDException(BBDDException.TipoError.Fatal, "Error al conectar db", exsql); }
        }

        public void setComando(String q) { this.comando = new MySqlCommand(q, this.conexion); }
        public void setComando(String q, long limite) { setComando(q + " limit " + limite.ToString() + ";"); }
        public void setComandoTimeOut(String q, int t) { this.comando.CommandTimeout = t; setComando(q); }

        public void setTimeOut(int t) { this.comando.CommandTimeout = t; }
        public void clearParameters() { this.comando.Parameters.Clear(); }

        public void addParameter(String param, string? val)
        {
            if (val == null)
                this.comando?.Parameters.AddWithValue(param, DBNull.Value);
            else
                this.comando?.Parameters.AddWithValue(param, val);
        }
        public void addParameter(String param, DBNull val) { this.comando.Parameters.AddWithValue(param, DBNull.Value); }
        public void addParameter(String param, int val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, decimal val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, double val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, long val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, bool val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, DateTime val) { this.comando.Parameters.AddWithValue(param, val); }
        public void addParameter(String param, Nullable<DateTime> val)
        {
            if (val == null)
                this.comando.Parameters.AddWithValue(param, DBNull.Value);
            else
                this.comando.Parameters.AddWithValue(param, val);
        }
        public void addParameter(String param, Nullable<Double> val)
        {
            if (val == null)
                this.comando.Parameters.AddWithValue(param, DBNull.Value);
            else
                this.comando.Parameters.AddWithValue(param, val);
        }
        public void addParameterLike(String param, String value)
        {
            this.comando.Parameters.AddWithValue(param, "%" + value + "%");
        }


        public void TransformarParametrosMotor(string orig, string dest)
        {
            foreach (MySqlParameter param in this.comando.Parameters)
            {
                param.ParameterName = param.ParameterName.Replace(orig, dest);
            }
        }

        //Ejecuciones de consultas

        public int ejecutarQueryWithExitParameter()
        {
            MySqlParameter param = new MySqlParameter("@result", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            this.comando.Parameters.Add(param);
            int r = ejecutarQuery();
            return Convert.ToInt32(param.Value);
        }

        public int ejecutarQuery() { return this.comando.ExecuteNonQuery(); }
        public int ejecutarQuery(String q) { this.setComando(q); return this.comando.ExecuteNonQuery(); }

        public int ejecutarScalar()
        {
            int resultado = 0;
            try
            {
                resultado = Convert.ToInt32(this.comando.ExecuteScalar());
            }
            catch (Exception) { resultado = 0; }
            return resultado;
        }
        public double ejecutarScalarDouble()
        {
            double resultado = 0;
            try
            {
                resultado = Convert.ToDouble(this.comando.ExecuteScalar());
            }
            catch (Exception) { resultado = 0; }
            return resultado;
        }
        public decimal ejecutarScalarDecimal()
        {
            decimal resultado = 0;
            try
            {
                resultado = Convert.ToDecimal(this.comando.ExecuteScalar());
            }
            catch (Exception) { resultado = 0; }
            return resultado;
        }

        public T ejecutarScalar<T>()
        {
            T resultado = default(T);
            try
            {
                object scalarResult = this.comando.ExecuteScalar();
                resultado = (T)(dynamic)scalarResult;
            }
            catch (Exception) { resultado = default(T); }
            return resultado;
        }

        public T ejecutarScalarWithIdentity<T>()
        {
            if (this.comando.CommandText.Trim().Length == 0)
                throw new BBDDException(BBDDException.TipoError.Critica, "No se puede ejecutar un comando que no existe", new Exception("No se puede ejecutar un comando que no existe"));

            if (this.comando.CommandText.Trim()[this.comando.CommandText.Trim().Length-1] == ';') 
                this.comando.CommandText += " SELECT LAST_INSERT_ID();";
            else
                this.comando.CommandText += "; SELECT LAST_INSERT_ID();";

            return ejecutarScalar<T>();
        }

        public void procedimientoAlmacenado(String q)
        {
            setComando(q); this.comando.CommandType = CommandType.StoredProcedure;
        }

        public bool IsProcedimientoAlmacenado()
        {
            return this.comando?.CommandType == CommandType.StoredProcedure;
        }

        public bool Read()
        {
            return this.reader.Read();
        }

        public IDataReader getReader()
        {
            return this.reader;
        }

        public bool hasRows()
        {
            return this.reader?.HasRows ?? false;
        }

        public void QueryReader()
        {
            if (this.conexion.State == ConnectionState.Closed) this.Connect();
            this.reader = this.comando.ExecuteReader();
        }

        public String obtenerQueryConParametros()
        {
            String query = this.comando.CommandText;
            foreach (MySqlParameter p in this.comando.Parameters)
            {
                query = query.Replace(p.ParameterName, p.Value.ToString());
            }
            return query;
        }

        public void QueryReader(String q)
        {
            setComando(q);
            this.comando.CommandType = CommandType.Text;
            this.QueryReader();
            //return this.QueryReader();
        }
        public String queryResultString()
        {
            String r;
            //this.reader = (MySqlDataReader)QueryReader();
            QueryReader();
            if (this.reader.HasRows)
            {
                this.reader.Read();
                r = this.reader[0].ToString();
            }
            else { r = ""; }
            this.reader.Close();
            return r;
        }

        public Dictionary<String, String> crearDiccionario(String q, String campoClave, String campoValor)
        {
            setComando(q);
            return crearDiccionario(campoClave, campoValor);
        }
        public Dictionary<String, String> crearDiccionario(String campoClave, String campoValor)
        {
            Dictionary<String, String> diccionario = new Dictionary<string, string>();
            try
            {
                DataSet datos = Query();
                foreach (DataRow item in datos.Tables[0].Rows)
                {
                    diccionario.Add(item[campoClave].ToString(), item[campoValor].ToString());
                }
            }
            catch (Exception ex)
            {

                throw new BBDDException(BBDDException.TipoError.Critica, "Error diccionario de claves", ex);
            }
            return diccionario;
        }

       
        public void Close()
        {
            if (this.reader != null)
                if (!this.reader.IsClosed)
                    this.reader.Close();
            this.conexion?.Close();
        }
        public void CloseAndDispose()
        {
            Close();
            Dispose();
        }
        public void CloseReader()
        {
            this.reader?.Close();
        }

        public void Dispose()
        {
            this.comando?.Dispose();
            this.reader?.Dispose();
        }
        public int insertarLog(LogVO log)
        {
            int pk = 0;
            if (this.conexion != null)
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO Log (entidad, entidad_id, fecha, usuario, descripcion, prioridad) ");
                query.Append("VALUES (@entidad,@entidad_id,CURRENT_TIMESTAMP,@usuario, @descripcion, @prioridad); select LAST_INSERT_ID(); ");
                //select LAST_INSERT_ID(); SELECT SCOPE_IDENTITY();
                this.Connect();
                this.setComando(query.ToString());

                addParameter("@entidad", log.entidad.ToString());
                addParameter("@entidad_id", log.entidadID);
                addParameter("@usuario", log.usuarioID);
                addParameter("@descripcion", log.descripcion);
                addParameter("@prioridad", log.prioridad.ToString());

                //pk = ejecutarScalar();
                pk = ejecutarQuery();

            }
            return pk;
        }

    }
}
namespace com.rorisoft.db
{
    public class DBFactory //: IFactory
    {
        public enum TIPODB
        {
            aplicacion = 0,
            sql_server = 1,
            mysql = 2,
            postgresql = 3
        }

        public static DB? crear(Enum tipo, String cadenaConexion)
        {
            if (string.IsNullOrEmpty(cadenaConexion))
                throw new ArgumentException("Cadena de conexión obligatoria.");

            if ((TIPODB)tipo == TIPODB.mysql)
            {
                return new DB(new MotorDBMysql(cadenaConexion));
            } else if ((TIPODB)tipo == TIPODB.sql_server)
            {
                return new DB(new MotorDBMSSQL(cadenaConexion));
            }

            return null;
        }


    }
}
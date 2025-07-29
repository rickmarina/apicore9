using com.rorisoft.log;
using System.Diagnostics;

namespace com.rorisoft.excepcion
{
    public class ApiException: Exception
    {
        public enum TipoError
        {
            Fatal = 1,
            Critica = 2,
            Informacion = 3,
            Concurrencia = 4,
            Cache = 5
        }

        public ApiException() { }

        public ApiException(string mensaje) : base(mensaje)
        {

        }
        public ApiException(TipoError te, String mensaje, Exception excepcion) : base(mensaje, excepcion)
        {
            Debug.WriteLine("API Exception: " + excepcion.Message);
            Debug.WriteLine("API Exception Mensaje: " + mensaje);
        }

        public ApiException(TipoError te, String mensaje, Exception excepcion, ILogable<ILogData> logger) : base(mensaje,excepcion)
        {
            logger.setLogData(new LogVO()
            {
                entidad = nameof(LogConstants.ENTIDAD.APLICACION),
                descripcion = "WebExcepcion [" + mensaje + " " + excepcion.Message + "]"
            });

        }

       

    }
}
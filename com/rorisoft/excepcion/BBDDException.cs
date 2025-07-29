namespace com.rorisoft.excepcion
{
    /// <summary>
    /// Excepciones de base de datos
    /// </summary>
    public class BBDDException : Exception
    {
        public enum TipoError
        {
            Fatal = 1,
            Critica = 2,
            Informacion = 3,
            Concurrencia = 4
        }

        public BBDDException() { }

        public BBDDException(string mensaje) : base(mensaje)
        {
        }

        public BBDDException(TipoError te, String mensaje, Exception excepcion) : base(mensaje, excepcion)
        {
        }



    }
}
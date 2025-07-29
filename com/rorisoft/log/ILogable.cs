using static com.rorisoft.log.LogConstants;

namespace com.rorisoft.log
{

    /// <summary>
    /// En el futuro ILogable lo implementarán difentes clases
    /// clase log BBDD 
    /// clase log Fichero 
    /// clase log etc... 
    /// y por dependencía se pasará a los diferentes objetos, en el constructor o en otro método
    /// </summary>
    public interface ILogable<ILogData> 
    {

        void setLogData(ILogData log);
        void setClass(string className);
        void setMethod(string methodName);
        int executeLog();
        int executeLog(string msg); //si únicamente cambia el mensaje

    }

    public interface ILogData
    {
        
        public long id { get; set; }
        public string entidad { get; set; }
        public string entidadID { get; set; }
        public DateTime fecha { get; set; }
        public int usuarioID { get; set; }
        public string? descripcion { get; set; }
        public PRIORIDAD prioridad { get; set; }
        public string? response { get; set; }
        public string? request { get; set; }
        public string? className { get; set; }
        public string? method { get; set; }
        public string? message { get; set; }
    }

    
}

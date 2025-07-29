using static com.rorisoft.log.LogConstants;

namespace com.rorisoft.log
{
    public class LogVO : ILogData
    {
        public long id { get; set; }
        public string entidad { get; set; }
        public string entidadID { get; set; }
        public DateTime fecha { get; set; }
        public int usuarioID { get; set; }
        public string? descripcion { get; set; }
        public PRIORIDAD prioridad { get; set; }
        public string? request { get; set; }
        public string? response { get; set; }
        public string? className { get; set; }
        public string? method { get; set; }
        public string? message { get; set; }

        public LogVO()
        {
            this.fecha = DateTime.Now;
            this.prioridad = PRIORIDAD.BAJA;
            this.entidad = nameof(ENTIDAD.APLICACION);
            this.entidadID = "0";
            this.usuarioID = 0;
        }

        

    }
}

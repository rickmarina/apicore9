using static com.rorisoft.cache.CacheControl;

namespace com.rorisoft.cache
{
    public class DatosCache
    {
        
        public DatosCache()
        {

        }


        public DESTINO ubicacion { get; set; }
        public DateTime? caducidad { get; set; }
        public String? datos_fichero { get; set; } // Ruta con la ubicación del fichero que contiene los datos
        public String? datos_memoria { get; set; } // Datos serializados 

        public override string ToString()
        {
            return $"Ubicacion [{ubicacion.ToString()}] Caducidad [{caducidad}] Fichero [{datos_fichero}] Memoria [{datos_memoria}]";
        }
    }
}

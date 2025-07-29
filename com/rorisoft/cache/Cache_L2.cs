using System.Collections.Concurrent;

/// <summary>
/// Caché de aplicación Wrapper
/// Nivel 2
/// Este caché se almacena únicamente en memoria
/// Relaciona un objecto lógico con un token de caché L1
/// </summary>
namespace com.rorisoft.cache
{
    public abstract class Cache_L2<TLogicCache> : ICacheL2 where TLogicCache : class
    {

        protected CacheControl_L2<TLogicCache> listaCacheL2;

        public Cache_L2()
        {
            this.listaCacheL2 = CacheControl_L2<TLogicCache>.getSingleton;
        }

        /// <summary>
        /// Devuelve un token si como resultado de la lógica ha aplicar hemos encontrado un candidato
        /// Este método virtual será implementado por la instancia de aplicación de Caché Nivel 2
        /// </summary>
        public virtual string estrategiaMejorPrimero()
        {
            string token = "";
            return token;
        }

        // Si tenemos un token, queremos recuperar ese contenido del caché de Nivel 1 
        public List<T>? obtenerDatosTokenL1<T>(string token)
        {
            return Cache_L1.obtenerResultadosCacheParaToken<T>(token);
        }

        // Verificar si es válido un token cache nivel 1 
        public bool esValidoCacheL1Token(string token)
        {
            return Cache_L1.esCacheL1Valido(token);
        }

        public void addCache(TLogicCache datos, string token)
        {
            listaCacheL2.addCache(token, datos);
        }

        public void delCache(string token)
        {
            listaCacheL2.delCache(token);
        }

        public void clearCache()
        {
            listaCacheL2.clearCache();
        }

        public ConcurrentDictionary<string, TLogicCache> obtenerContenidoCache()
        {
            return listaCacheL2.obtenerListaCache();
        }
        

       
    }

    
}

/* Ejemplo de implementación en API de aplicación 
 * 
 * 
    public class ApplicationCache : Cache_L2<CoordVO>
    {

        public CoordVO mi_posicion { get; set; }


        public ApplicationCache(CoordVO pos)
        {
            this.mi_posicion = pos;
        }
        public ApplicationCache()
        {

        }

        public static void test()
        {

        }

        //Recorremos la caché nivel 2 buscando alguna posición que esté a menos de 1 km de mi posición 
        //En caso de encontrarlo, usamos sus resultados del nivel 1 de caché
        public override string estrategiaMejorPrimero()
        {
            string token = string.Empty;

            foreach (var item in this.listaCacheL2.obtenerListaCache())
            {
                
                if (Cache_L1.esCacheL1Valido(item.Key))
                {
                    double distancia = GeoUtils.geoDistanciaKm(mi_posicion.longitud, mi_posicion.latitud, item.Value.longitud, item.Value.latitud);
                    if (distancia <= 1.1) { 
                        token = item.Key;
                        return token;
                    }
                } else
                {
                    //Expiró el caché lo borramos de L2 
                    this.listaCacheL2.delCache(item.Key);
                }

            }
            return token;
        }

        public List<EstacionDatabaseModel> obtenerResultadosBusquedaEstaciones(FiltroEstacionesVO filtro)
        {
            this.mi_posicion = filtro.gps;
            string token = estrategiaMejorPrimero();
            List<EstacionDatabaseModel> resultados;

            if (!string.IsNullOrEmpty(token))
            {
                resultados = obtenerDatosTokenL1<EstacionDatabaseModel>(token);
            }
            else
            {
                Func<FiltroEstacionesVO, List<EstacionDatabaseModel>> callBackResultados = new Func<FiltroEstacionesVO, List<EstacionDatabaseModel>>(EstacionesUseCase.obtenerEESS);
                resultados = Cache_L1.obtenerResultadosCache<FiltroEstacionesVO, EstacionDatabaseModel>(filtro, "", CacheControl.cachePeriodo.HORA3, callBackResultados, CacheControl.DESTINO.DISCO);

                //Incorporar el objeto en el caché L2 
                addCache(filtro.gps, filtro.getToken());
            }

            return resultados;
        }



    }
*/
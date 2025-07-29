using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;

/// <summary>
/// Caché L1 CORE
/// Relacion entre token y conjunto de datos cacheados en disco / memoria
/// </summary>

namespace com.rorisoft.cache
{
    public static class Cache_L1
    {

        public static void establecerRutaCache(string ruta)
        {
            CacheControl.getSingleton.setRutaFisica(ruta);
        }

        public static string obtenerRutaCache()
        {
            return CacheControl.getSingleton.getRutaFisica();
        }
        public static IEnumerable<T>? obtenerResultadosCache<TParam1, T>(ICacheable ocache, string ext, CacheControl.cachePeriodo intervalo, Func<TParam1, IEnumerable<T>> crearResultados, CacheControl.DESTINO destino = CacheControl.DESTINO.MEMORIA)
        {
            IEnumerable<T>? resultados = null;

            String cacheToken = ocache.getToken(); //ocache._cacheName + "_" + ocache.getHash();
            bool isCacheActivo = CacheControl.getSingleton.isCacheActivo(cacheToken);

            Debug.WriteLine($"Cache system. Token [{cacheToken}] isCacheActivo [{isCacheActivo}] ");

            if (isCacheActivo)
            {
                Debug.WriteLine("Cache system. Recuperando cache para: " + ocache._cacheName);
                var cacheData = CacheControl.getSingleton.getCacheData(cacheToken);
                if (cacheData != null) 
                    resultados = JsonSerializer.Deserialize<List<T>>(cacheData);
            }
            else
            {
                Debug.WriteLine("Cache system. No existe caché, regenerando la lista: " + ocache._cacheName);
                resultados = crearResultados((TParam1)ocache);

                if (resultados.Count() > 0)
                {
                    Debug.WriteLine("Cache system. Grabando caché para: " + cacheToken);
                    CacheControl.getSingleton.addCache(cacheToken, JsonSerializer.Serialize(resultados), intervalo, destino);
                }
            }

            return resultados;
        }

        public static List<T>? obtenerResultadosCacheParaToken<T>(string token)
        {
            List<T>? resultados = null;
            bool isCacheActivo = CacheControl.getSingleton.isCacheActivo(token);
            if (isCacheActivo)
            {
                Debug.WriteLine("Cache system. Recuperando cache para: " + token);
                var cacheData = CacheControl.getSingleton.getCacheData(token);
                if (cacheData != null)
                    resultados = JsonSerializer.Deserialize<List<T>>(cacheData);
            }

            return resultados;

        }

        public static bool esCacheL1Valido(string token)
        {
            return CacheControl.getSingleton.isCacheActivo(token);
        }

        public static ConcurrentDictionary<string, DatosCache> obtenerContenidoCache()
        {
            return CacheControl.getSingleton.obtenerDiccionarioCache();
        }

        public static long ObtenerOperacionesProcesadas()
        {
            return CacheControl.getSingleton.ObtenerTotalOperacionesProcesadas();
        }

        public static long ObtenerNumeroTotalRegistros()
        {
            return CacheControl.getSingleton.ObtenerNumeroTotalRegistros();
        }

        public static void clearCache()
        {
            CacheControl.getSingleton.ClearCache();
        }

        public static long ClearCachePrefix(string prefijo)
        {
            return CacheControl.getSingleton.ClearCachePrefix(prefijo);
        }
    }
}

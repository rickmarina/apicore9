using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace com.rorisoft.cache
{

    /// <summary>
    /// Caché de Nivel 2
    /// Únicamente gestiona datos en memoria
    /// Singleton 
    /// Capa por encima del core para almacenar TdatosCache personalizados por aplicación
    /// No explira
    /// </summary>
    /// <typeparam name="TDatosCache">Tipo de datos sobre los que se va a aplicar lógica para buscar un token reutilizable</typeparam>
    public sealed class CacheControl_L2<TDatosCache>
    {
        private static CacheControl_L2<TDatosCache>? instancia = null;
        private static readonly object padlock = new();

        private string? rutaCache = null;
        private static ConcurrentDictionary<String, TDatosCache> listacache = new ConcurrentDictionary<string, TDatosCache>();

        
        CacheControl_L2()
        {

        }

        public static CacheControl_L2<TDatosCache> getSingleton
        {
            get
            {
                if (instancia == null)
                {
                    lock (padlock)
                    {
                        instancia ??= new CacheControl_L2<TDatosCache>();
                    }
                }
                return instancia;
            }
        }

        public ConcurrentDictionary<String, TDatosCache> obtenerListaCache()
        {
            return listacache;
        }

        public List<KeyValuePair<string, TDatosCache>> obtenerListadoCache()
        {
            List<KeyValuePair<string, TDatosCache>> listado = new List<KeyValuePair<string, TDatosCache>>();
            listado.AddRange(listacache);
            return listado;
        }

        public bool isCache(String token)
        {
            return listacache.ContainsKey(token);
        }

        public bool isCacheActivo(String token)
        {
            bool activo = true;
            if (!isCache(token))
            {
                activo = false;
            }

            return activo;
        }
        
        public string addCache(String token, TDatosCache datos)
        {
            if (!isCache(token))
            {
                listacache[token] = datos;
                //listacache.Add(token, datos);
            }
            
            return token;
        }

        public void delCache(string token)
        {
            if (isCache(token))
            {
                TDatosCache? datos;
                _ =listacache.TryRemove(token, out datos);
                //listacache.Remove(token);
            }
        }

        public void clearCache()
        {
            listacache.Clear();
        }

        public TDatosCache getCacheData(String token)
        {
            TDatosCache dc = listacache[token];
            String r = string.Empty;

            return dc;
        }

        //Este método limpia los ficheros de caché que lleven más de un mes sin accederse
        //De este modo vamos limpiando los ficheros que no tienen uso
        public void deleteOldCacheFiles()
        {
            if (!string.IsNullOrEmpty(rutaCache))
            {
                string[] files = Directory.GetFiles(rutaCache);

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastAccessTime < DateTime.Now.AddMonths(-1))
                        fi.Delete();
                }
            }
        }

        private String pathFileData(String token)
        {
            if (string.IsNullOrEmpty(rutaCache))
            {
                throw new NullReferenceException("No existe la ruta en disco para el caché");
            }
            return rutaCache + token + ".dat";
        }



    }
}

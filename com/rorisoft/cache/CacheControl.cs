using com.rorisoft.excepcion;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace com.rorisoft.cache
{

    public sealed class CacheControl
    {
        private static CacheControl? instancia = null;
        private static readonly object padlock = new object();

        private string? rutaCache = null;
        private static ConcurrentDictionary<String, DatosCache> listacache = new ConcurrentDictionary<string, DatosCache>();

        private int totalCacheOps = 0;
        private DateTime? dateLastReset = null;

        public enum cachePeriodo
        {
            DEFECTO = 20,
            HORA = 60,
            HORA2 = 120,
            HORA3 = 180,
            HORA4 = 240,
            DIARIO = 1440
        }

        public enum DESTINO
        {
            DISCO = 1,
            MEMORIA = 2
        }

        CacheControl()
        {

        }

        public static CacheControl getSingleton
        {
            get
            {
                if (instancia == null)
                {
                    lock (padlock)
                    {
                        instancia ??= new CacheControl();
                    }
                }
                return instancia;
            }
        }

        public void setRutaFisica(string ruta)
        {
            if (!ruta.EndsWith(@"\"))
                ruta = ruta+@"\";
            this.rutaCache = ruta;
        }

        public string getRutaFisica()
        {
            return this.rutaCache ?? string.Empty;
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
            else if (listacache[token].caducidad < DateTime.Now)
            {
                activo = false;
            }
            else if ((listacache[token].ubicacion == DESTINO.DISCO) && !File.Exists(pathFileData(token)))
            {
                activo = false;
            }

            if (activo)
                totalCacheOps++;

            return activo;
        }

        public int addCache(String token, String contenido)
        {
            return addCache(token, contenido, cachePeriodo.DEFECTO, DESTINO.DISCO);
        }
        public int addCache(String token, String contenido, DESTINO destino)
        {
            return addCache(token, contenido, cachePeriodo.DEFECTO, destino);
        }
        public int addCache(String token, String contenido, cachePeriodo minutos, DESTINO destino)
        {
            try { 
                if (!isCache(token))
                {
                    DatosCache dc = new DatosCache();
                    dc.caducidad = DateTime.Now.AddMinutes((double)minutos);
                    dc.ubicacion = destino;
                    if (destino == DESTINO.MEMORIA)
                        dc.datos_memoria = contenido;
                    else if (destino == DESTINO.DISCO)
                    {
                        dc.datos_fichero = pathFileData(token);
                        //Crear fichero en ambos casos, si ha caducado puede haber cambiado el contenido 
                        File.WriteAllText(dc.datos_fichero, contenido);
                        Debug.WriteLine("Creando fichero de caché para " + token);
                    }

                    listacache[token] = dc;
                    //listacache.Add(token, dc);
                }
                else
                {
                    //El caché existe
                    //Incrementar caducidad
                    listacache[token].caducidad = DateTime.Now.AddMinutes((double)minutos);
                }
            } catch(Exception ex)
            {
                throw new ApiException(ApiException.TipoError.Cache, $"Error creando caché. Token [{token}] periodo [{minutos.ToString()}] destino [{destino.ToString()}] contenido size [{contenido.Length}]", ex);
            }

            return 1;
        }


        public string? getCacheData(String token)
        {
            DatosCache dc = listacache[token];
            String r = string.Empty;

            if (dc.ubicacion == DESTINO.DISCO)
                r = File.ReadAllText(pathFileData(token));
            else if (dc.ubicacion == DESTINO.MEMORIA)
                r = dc?.datos_memoria ?? string.Empty;

            return r;
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

        public ConcurrentDictionary<string, DatosCache> obtenerDiccionarioCache()
        {
            return listacache;
        }
        public List<KeyValuePair<string, DatosCache>> obtenerListadoCache()
        {
            List<KeyValuePair<string, DatosCache>> listado = new List<KeyValuePair<string, DatosCache>>();
            listado.AddRange(listacache);
            return listado;
        }

        /// <summary>
        /// Número de operaciones que ha podido resolver el caché
        /// </summary>
        /// <returns></returns>
        public long ObtenerTotalOperacionesProcesadas()
        {
            return this.totalCacheOps;
        }

        /// <summary>
        /// Número de operaciones que contiene el caché L1
        /// </summary>
        /// <returns></returns>
        public long ObtenerNumeroTotalRegistros()
        {
            return listacache.Keys.Count;
        }

        private String pathFileData(String token)
        {
            if (string.IsNullOrEmpty(rutaCache))
            {
                throw new NullReferenceException("No existe la ruta en disco para el caché");
            }
            return rutaCache + token + ".dat";
        }

        /// <summary>
        /// Borra todo el caché L1
        /// </summary>
        public void ClearCache()
        {
            listacache.Clear();
            this.dateLastReset = DateTime.UtcNow;
        }

        /// <summary>
        /// Elimina de la caché L1 todos los tokens que comiencen por el prefijo dado
        /// </summary>
        /// <param name="prefijo"></param>
        public long ClearCachePrefix(string prefijo)
        {

            IEnumerable<KeyValuePair<string, DatosCache>> toDelete = listacache.Where(x => x.Key.StartsWith(prefijo + "_"));
            long total = toDelete.Count();

            foreach (KeyValuePair<string, DatosCache> k in toDelete)
            {
                listacache.TryRemove(k);
                //listacache.Remove(k.Key);
            }

            this.dateLastReset = DateTime.UtcNow;

            return total;
        }

        /// <summary>
        /// Borra un token pasado por parámetro de la caché L1
        /// </summary>
        /// <param name="token"></param>
        public void ClearCacheToken(string token)
        {
            _ = listacache.TryRemove(token, out DatosCache? datoscache);
            //listacache.Remove(token);
        }
    }
}

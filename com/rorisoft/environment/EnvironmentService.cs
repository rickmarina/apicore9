using com.rorisoft.cache;
using com.rorisoft.excepcion;
using Microsoft.Extensions.Configuration;

namespace com.rorisoft.environment
{
    public sealed class EnvironmentService
    {

        public enum Environment_Type
        {
            APPSETTINGS_LOCAL = 1,
            AZURE_FUNCTION = 2
        }

        private static EnvironmentService? instancia = null;
        private static readonly object padlock = new object();
        
        public static IConfigurationRoot? root = null;

        //Variables de configuración comunes 
        private static string? CONFIG_ENVIRONMENT = "";
        private static string? CONFIG_CONNECTION = "";
        private static bool CONFIG_LOGBBDD = false;
        private static string? RUTA_CACHE_L1 = "";

        public static EnvironmentService getSingleton(Environment_Type type, IConfigurationRoot? config = null)
        {
            if (instancia == null)
            {
                lock (padlock)
                {
                    instancia ??= new EnvironmentService(type, config);
                }
            }
            return instancia;
        }

        /// <summary>
        /// Constructor para Singleton 
        /// Va parametrizado para recuperar la configuración de fichero, configurationroot, httpget, etc..
        /// </summary>
        /// <param name="type">Tipo de configuración: APPSETTTINGS loca, AZUREFUNCTION (leerá el config)</param>
        /// <param name="config">IConfigurationRoot, por ejemplo para el caso de las funciones Azure</param>
        private EnvironmentService(Environment_Type type, IConfigurationRoot? config)
        {
            try
            {
                if (type == Environment_Type.APPSETTINGS_LOCAL)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                    root = builder.Build();

                    CONFIG_ENVIRONMENT = root.GetSection("Environment").Value;
                    CONFIG_CONNECTION = root.GetConnectionString($"{CONFIG_ENVIRONMENT}Connection");
                    CONFIG_LOGBBDD = Boolean.Parse(root.GetSection("logBBDD").Value ?? "false");

                    //Establecer la ruta de caché
                    RUTA_CACHE_L1 = root.GetSection("cacheL1").GetSection($"{CONFIG_ENVIRONMENT}Path").Value;
                    if (!string.IsNullOrEmpty(RUTA_CACHE_L1))
                        Cache_L1.establecerRutaCache(RUTA_CACHE_L1);

                    

                }
                else if (type == Environment_Type.AZURE_FUNCTION)
                {
                    if (config != null) { 
                        root = config;

                        CONFIG_ENVIRONMENT = root.GetSection("Environment").Value;
                        CONFIG_CONNECTION = root.GetConnectionString($"{CONFIG_ENVIRONMENT}Connection");
                        CONFIG_LOGBBDD = Boolean.Parse(root.GetSection("logBBDD").Value ?? "false");
                        RUTA_CACHE_L1 = root.GetSection("cacheL1").GetSection($"{CONFIG_ENVIRONMENT}Path").Value;
                        if (!string.IsNullOrEmpty(RUTA_CACHE_L1))
                            Cache_L1.establecerRutaCache(RUTA_CACHE_L1);

                    }
                }
            } catch(Exception ex)
            {
                throw new ApiException(ApiException.TipoError.Critica, "Fallo en la precarga de configuración: "+ex.Message, ex);
            }
        }

        public string? GetEnvironment { get { return CONFIG_ENVIRONMENT; } }
        public string? GetConnection { get { return CONFIG_CONNECTION; } }
        public bool GetLogBBDD {  get { return CONFIG_LOGBBDD; } }

        public string? GetOtherConfig(string key)
        {
            return root?.GetSection(key) != null ? root.GetSection(key).Value : "";
        }

        public IConfigurationSection? GetSection(string section)
        {
            return root?.GetSection(section);
        }

        public List<KeyValuePair<string, string>> obtenerConfiguracion()
        {
            try
            {
                return root?.AsEnumerable().ToList()!;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

using com.rorisoft.log2.behaviours;
using com.rorisoft.excepcion;
using System;
using System.Collections.Generic;

namespace com.rorisoft.log2
{
    public class LogFactory
    {

        [Flags]
        public enum BEHAVIOUR
        {
            DEBUG = 1, 
            CONSOLA = 2, 
            FICHERO = 4,
            BASEDATOS = 8
        }

        /// <summary>
        /// Tipo de log será un número que representa la combinación de tipos de logs posibles
        /// De tal manera que 6 indicara que será un log de CONSOLA (2) + FICHERO (4) 
        /// </summary>
        /// <param name="tipo">Ej: BEHAVIOUR.DEBUG | BEHAVIOUR.CONSOLA </param>
        /// <param name="classname">Para clases normales se puede utilizar this.GetType().Name para clases estáticas MethodBase.GetCurrentMethod().DeclaringType.FullName</param>
        /// <param name="method">System.Reflection.MethodInfo.GetCurrentMethod().ToString()</param>
        /// <param name="path">Necesario si el logger va tener un comportamiento de tipo FICHERO (4)</param>
        /// <returns></returns>
        public static Logger<LogData> make(BEHAVIOUR behaviours, string classname, string method, string path = "")
        {

            if ((int)behaviours == 0)
                throw new ApiException("Es necesario especificar 'tipo' al crear el objeto Logger");

            List<ILogBehaviour> logs = new List<ILogBehaviour>();
            foreach (var b in (int[])Enum.GetValues(typeof(BEHAVIOUR)))
            {
                if ((b & (int)behaviours) == b) {  //TODO: esta condición sobra, basta con hacer el and con cada uno de los elementos de la enumeración
                    if (b == (int)BEHAVIOUR.DEBUG)
                    {
                        logs.Add(new LogDebugBehaviour());
                    } else if (b == (int)BEHAVIOUR.FICHERO)
                    {
                        if (string.IsNullOrEmpty(path))
                            throw new ApiException("No es posible instanciar un file logger sin especificar el path");

                        logs.Add(new LogFileBehaviour(path));
                    }
                }

            }

            Logger<LogData> logger = new Logger<LogData>(logs, classname, method);

            return logger;

        }


    }
}

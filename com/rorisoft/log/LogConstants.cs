using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.log
{
    public static class LogConstants
    {
        public enum ENTIDAD
        {
            APLICACION,
            USUARIO,
            EMAIL,
            BBDD
        }
        public enum PRIORIDAD
        {
            BAJA,
            MEDIA,
            ALTA
        }

    }
}

using com.rorisoft.db;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.db
{
    public interface IFactory
    {
        DB crear(Enum tipo, String cadenaConexion);
    }
}

using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace com.rorisoft.utils
{
    public class ParseUtils
    {
        //--- METODOS PARSE
        //Métodos PARSE
        public static decimal parseDecimal(object cad)
        {
            decimal resultado = 0;
            try
            {
                if (cad == DBNull.Value)
                    resultado = 0;
                else if (cad == null)
                    resultado = 0;
                else if (string.IsNullOrEmpty(cad.ToString()))
                    resultado = 0;
                else
                {
                    //posible mejora, puede sobrar el replace
                    resultado = Decimal.Parse(cad.ToString()!.Replace(",", "."), System.Globalization.NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
                    //resultado = Decimal.Parse(cad.ToString().Replace(".", ","));
                }
            }
            catch (Exception)
            {
                resultado = 0;
                Debug.WriteLine("Error parsing Decimal");
            }
            return resultado;
        }

        public static float parseFloat(string cad)
        {
            float resultado = 0;
            if (!String.IsNullOrEmpty(cad))
            {
                try
                {
                    resultado = float.Parse(cad.Replace(".", ","));
                }
                catch (Exception)
                {
                    resultado = 0;
                }
            }
            return resultado;
        }
        public static double parseDouble(String cad)
        {
            double resultado = 0;
            if (!String.IsNullOrEmpty(cad))
            {
                try
                {
                    resultado = Double.Parse(cad.Replace(".", ","));
                }
                catch (Exception)
                {
                    resultado = 0;
                }
            }
            return resultado;
        }

        public static int parseInteger(object cad)
        {
            int resultado = 0;
            try
            {
                resultado = int.Parse(cad?.ToString() ?? "0");
            }
            catch (Exception)
            {
                resultado = 0;
                Debug.WriteLine("Error parsing Integer ");
            }
            return resultado;
        }

        public static long parseLong(object cad)
        {
            long resultado = 0;
            try
            {
                resultado = long.Parse(cad.ToString()!);
            }
            catch (Exception)
            {
                resultado = 0;
            }
            return resultado;
        }

        public static string parseString(object cad)
        {
            String resultado = "";
            try
            {
                resultado = cad.ToString()!;
            }
            catch (Exception)
            {
                resultado = "";
            }
            return resultado;
        }

        public static bool parseBoolean(string cad)
        {
            bool resultado = false;
            try
            {
                if (!String.IsNullOrEmpty(cad))
                {
                    if (cad.Equals("0"))
                        resultado = false;
                    else if (cad.Equals("1"))
                        resultado = true;
                    else
                        resultado = Boolean.Parse(cad);
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }

        public static String parseDateTimeForMysql(string cad)
        {
            string resultado = cad;
            try
            {
                if (cad != null)
                    resultado = String.Format("{0:yyyy/MM/dd hh:mm:ss}", DateTime.Parse(cad));
            }
            catch (Exception)
            {
                resultado = String.Empty;
            }
            return resultado;
        }

        public static DateTime? parseDateTime(string cad)
        {
            DateTime? res = null;
            try
            {
                res = DateTime.Parse(cad);
            } catch (Exception)
            {
                res = null;
            }
            return res;
        }

        //Valores si NULL
        public static String siNullString(string s, string def)
        {
            return s == null ? def : s;
        }

        //Valores si DBNULL
        public static String siNullValue(object o, string def)
        {
            if (o == DBNull.Value)
                return def;
            else
                return o.ToString()!;
        }
        public static bool siNullValue(object o, bool def)
        {
            if (o == DBNull.Value)
                return def;
            else
                return bool.Parse(o.ToString()!);
        }

        public static T IfDBNullDefaultValue<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value)
                return defaultValue;

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type underlyingType = Nullable.GetUnderlyingType(typeof(T))!;
                return (T)Convert.ChangeType(value, underlyingType);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static DataTable ConvertListToDataTable<T>(List<T> lista, string columnName) where T : IConvertible
        {
            DataTable dataTable = new();
            dataTable.Columns.Add(columnName, typeof(T));

            if (lista != null)
            {
                foreach (T item in lista)
                {
                    dataTable.Rows.Add(item);
                }
            }
            return dataTable;
        }

        public static DataTable ConvertTupleListToDataTable<T1, T2>(List<(T1, T2)> lista, string columnName1, string columnName2)
        {
            DataTable dataTable = new();
            dataTable.Columns.Add(columnName1, typeof(T1));
            Type typeT2 = Nullable.GetUnderlyingType(typeof(T2)) ?? typeof(T2);
            dataTable.Columns.Add(columnName2, typeT2);
            if (lista != null)
            {
                foreach ((T1 item1, T2 item2) in lista)
                {
                    dataTable.Rows.Add(item1, item2 == null ? DBNull.Value : (object)item2);
                }
            }
            return dataTable;
        }
    }
}

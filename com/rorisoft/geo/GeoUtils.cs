using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.geo
{
    public static class GeoUtils
    {

        private const int EARTH_RADIUS = 6371; // Radius of the earth in km

        public static double geoDistanciaKm(double lon1, double lat1, double lon2, double lat2)
        {
            //return geoDistanciaKm(utils.ParseUtils.parseDecimal(lon1), utils.ParseUtils.parseDecimal(lat1), utils.ParseUtils.parseDecimal(lon2), utils.ParseUtils.parseDecimal(lat2));
            double dlat = deg2rad(lat2 - lat1);
            double dlon = deg2rad(lon2 - lon1);
            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = EARTH_RADIUS * c;

            return d;
        }

        public static double geoDistanciaKm(decimal lon1, decimal lat1, decimal lon2, decimal lat2)
        {
            //$dLat = deg2rad($lat2 - $lat1);
            double dlat = deg2rad((double)(lat2 - lat1));
            //$dLon = deg2rad($lon2 - $lon1);
            double dlon = deg2rad((double)(lon2 - lon1));

            //$a = sin($dLat / 2) * sin($dLat / 2) + cos(deg2rad($lat1)) * cos(deg2rad($lat2)) * sin($dLon / 2) * sin($dLon / 2);
            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(deg2rad((double)lat1)) * Math.Cos(deg2rad((double)lat2)) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);

            //$c = 2 * atan2(sqrt($a), sqrt(1 -$a));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //$d = $R * $c; // Distance in km
            double d = EARTH_RADIUS * c;

            return d;
        }


        private static double deg2rad(double n) { return Math.PI * n / 180; }
    }
}

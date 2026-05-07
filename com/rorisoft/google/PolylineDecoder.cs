using System;
using System.Collections.Generic;

/// <summary>
/// Decodificador de polilíneas de Google Maps. Basado en el algoritmo oficial descrito en:
/// https://developers.google.com/maps/documentation/utilities/polylinealgorithm
/// Ejemplo de uso:
/// var encoded = "iggvF`ppURPf@^h@b@".AsSpan();
/// var coords = PolylineDecoderFast.Decode(encoded);
/// foreach (var p in coords)
/// {
///     Console.WriteLine($"{p.Lat}, {p.Lng}");
/// }
/// </summary>
public static class PolylineDecoder
{
    public struct Point
    {
        public double Lat;
        public double Lng;

        public Point(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }

    public static List<Point> Decode(ReadOnlySpan<char> encoded)
    {
        int len = encoded.Length;
        var result = new List<Point>(len / 2); // estimación inicial

        int index = 0;
        int lat = 0, lng = 0;

        while (index < len)
        {
            lat += DecodeNext(encoded, ref index);
            lng += DecodeNext(encoded, ref index);

            result.Add(new Point(lat * 1e-5, lng * 1e-5));
        }

        return result;
    }

    private static int DecodeNext(ReadOnlySpan<char> encoded, ref int index)
    {
        int result = 0;
        int shift = 0;
        int b;

        // Loop desenrollado parcialmente para rendimiento
        do
        {
            b = encoded[index++] - 63;
            result |= (b & 0x1F) << shift;
            shift += 5;
        }
        while (b >= 0x20);

        // Branch optimizado
        return (result & 1) != 0
            ? ~(result >> 1)
            : (result >> 1);
    }
}
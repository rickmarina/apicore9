using com.rorisoft.structures.tree.quadtree;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.structures.tree.quadtree
{
    public class GasDataPOI
    {
        public string pais { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string rotulo { get; set; }
        public string direccion { get; set; }
        public decimal precio95 { get; set; }
        public decimal precio98 { get; set; }
        public decimal precioDiesel { get; set; }
        public decimal precioGLP { get; set; }
        public string bandera { get; set; }
        public string horario { get; set; }

        public GasDataPOI()
        {

        }
        public GasDataPOI(string rotulo, decimal precio95, decimal precio98, decimal precioDiesel, decimal precioGLP) 
        {
            this.rotulo = rotulo;
            this.precio95 = precio95;
            this.precio98 = precio98;
            this.precioDiesel = precioDiesel;
            this.precioGLP = precioGLP;
        }

    }
}

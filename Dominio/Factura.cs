using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Factura
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public Venta Venta { get; set; }

        //public int Id;
        //public string NumeroFactura; // F0001, F0002, etc.
        //public DateTime Fecha;
        //public Venta Venta;
    }
}

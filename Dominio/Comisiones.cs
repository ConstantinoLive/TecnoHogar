using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comisiones
    {
        public int IdComision { get; set; }
        public Venta IdVenta { get; set; }
        public Usuario IdUsuario { get; set; }
        public decimal PorcentajeAplicado { get; set; }
        public decimal MontoComision { get; set; }
        public DateTime Fecha { get; set; }

       
    }
}

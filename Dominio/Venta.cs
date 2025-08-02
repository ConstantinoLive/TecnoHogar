using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime? Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public List<VentaDetalle> Detalles { get; set; }
        public decimal Total { get; set; }
    }
}

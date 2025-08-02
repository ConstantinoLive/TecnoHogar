using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class CompraHistorial
    {
        public string RazonSocial { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}

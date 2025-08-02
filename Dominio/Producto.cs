using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string ImagenUrl { get; set; }
        public bool Activo {  get; set; }
        public Marca Marca { get; set; }
        public TipoProducto TipoProducto { get; set; }

       
    }
}

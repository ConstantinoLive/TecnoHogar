using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class TipoProducto
    {
        public int IdTipoProducto { get; set; }
        public string Nombre { get; set; }
        public Categoria categoria { get; set; }
        public bool Activo {  get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}

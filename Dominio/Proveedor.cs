using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT {  get; set; }
        public string Direccion {  get; set; }
        public string Telefono {  get; set; }
        public string Email {  get; set; }
        public bool Activo { get; set; }

    }
}

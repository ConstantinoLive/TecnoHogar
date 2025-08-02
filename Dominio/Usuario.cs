using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public enum TipoUsuario
    {
        Administrador = 1,

        Vendedor = 2
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Admin { get; set; } // Admin - Vendedor
        public bool Activo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }



    }

}

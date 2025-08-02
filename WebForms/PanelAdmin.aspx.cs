using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebForms
{
    public partial class PanelAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            if (!Seguridad.esAdmin((Usuario)Session["Usuario"])){
                Session.Add("Error", "Debes tener permiso de administrador");
                Response.Redirect("Error.aspx", false);
                return;
            }
        }

        protected void lkbProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProductos.aspx", false);
        }

        protected void lkbCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaClientes.aspx", false);
        }

        protected void lkbVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaVenta.aspx", false);
        }

        protected void lkbUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaUsuarios.aspx", false);
        }

        protected void lkbProveedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProveedores.aspx", false);
        }

        protected void lkbCompras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Compras.aspx", false);
        }

        protected void lkbMarcas_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaMarcas.aspx", false);
        }

        protected void lkbTipoProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaTipoProducto.aspx", false);
        }

        protected void lkbCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaCategorias.aspx", false);
        }
    }
}
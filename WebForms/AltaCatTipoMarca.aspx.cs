using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class AltaCatTipoMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            if (!Seguridad.esAdmin((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes tener permiso de administrador");
                Response.Redirect("Error.aspx", false);
                return;
            }
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarTipoProducto_Click(object sender, EventArgs e)
        {

        }
    }
}
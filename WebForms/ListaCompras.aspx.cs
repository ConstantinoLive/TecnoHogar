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
    public partial class ListaCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ComprasNegocio negocio = new ComprasNegocio();
            List<Compra> lista = negocio.Listar();

            GVCompras.DataSource = lista;
            GVCompras.DataBind();
        }

        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {

        }
    }
}
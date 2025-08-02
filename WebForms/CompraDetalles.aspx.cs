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
    public partial class CompraDetalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCompra;
                if (int.TryParse(Request.QueryString["id"], out idCompra))
                {
                    // Cargás los detalles con el ID
                    CargarDetalles(idCompra);
                }
            }
            //CargarDetalles(1);
        }

        private void CargarDetalles(int idCompra)
        {
            CompraDetalleNegocio negocio = new CompraDetalleNegocio();
            List<CompraDetalle> lista = negocio.ObtenerDetallesPorCompra(idCompra);
            GVCompraDetalle.DataSource = lista;
            GVCompraDetalle.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

        }
    }
}
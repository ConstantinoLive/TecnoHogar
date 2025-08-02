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
    public partial class VentaDetalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idVenta;
                if (int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    // Cargás los detalles con el ID
                    CargarDetalles(idVenta);
                }
            }
            //CargarDetalles(1);
        }

        private void CargarDetalles(int idVenta)
        {
            VentaDetalleNegocio negocio = new VentaDetalleNegocio();
            List<VentaDetalle> lista = negocio.ObtenerDetallesPorVenta(idVenta);
            GVVentaDetalle.DataSource = lista;
            GVVentaDetalle.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaVentas.aspx", false);
        }

       
    }
}
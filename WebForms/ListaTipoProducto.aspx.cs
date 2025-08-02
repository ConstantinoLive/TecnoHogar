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
    public partial class ListaTipoProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTP();
            }
        }

        private void CargarTP()
        {
            TipoProductoNegocio negocio = new TipoProductoNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;
            List<TipoProducto> listaTP = mostrarEliminados ?
                negocio.ListarTPEliminados() :
                negocio.ListarTPConSp();
            try
            {
                
    
                Session["listaTipoProductos"] = listaTP;
                GVTP.DataSource = listaTP;
                GVTP.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }

        }
        /*
        protected void btnAgregarTP_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaTipoProducto.aspx", false);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<TipoProducto> listaTP = (List<TipoProducto>)Session["listaTipoProductos"];
            List<TipoProducto> filtrada = listaTP.Where(TP => TP.Nombre.Trim().ToLower().Contains(txtBuscarTP.Text.Trim().ToLower())).ToList();

            GVTP.DataSource = filtrada;
            GVTP.DataBind();
        }
        */
        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarTP();
        }

        protected void GVTP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVTP.PageIndex = e.NewPageIndex;
            CargarTP();
        }

        protected void GVTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GVTP.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaTipoProducto.aspx?id=" + id);
        }

        protected void GVTP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVTP.Rows[rowIndex];
            int idTP = Convert.ToInt32(GVTP.DataKeys[row.RowIndex].Values["IdTipoProducto"]);

            TipoProductoNegocio negocio = new TipoProductoNegocio();

            if (e.CommandName == "Delete")
            {
                negocio.EliminarTP(idTP);
            }
            else if (e.CommandName == "Reactivar")
            {
                negocio.ReactivarTP(idTP);
            }

            CargarTP();
        }

        protected void GVTP_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

        protected void lkbAdregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaTipoProducto.aspx", false);
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            List<TipoProducto> listaTP = (List<TipoProducto>)Session["listaTipoProductos"];
            List<TipoProducto> filtrada = listaTP.Where(TP => TP.Nombre.Trim().ToLower().Contains(txtBuscarTP.Text.Trim().ToLower())).ToList();

            GVTP.DataSource = filtrada;
            GVTP.DataBind();
        }
    }
}
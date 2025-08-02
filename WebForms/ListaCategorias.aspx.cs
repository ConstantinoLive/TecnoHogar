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
    public partial class ListaCategorias : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;

            List<Categoria> lista = mostrarEliminados ?
                negocio.ListarCategoriasEliminadas() :
                negocio.ListarCategorias();
            try
            {
                Session["listaCategoria"] = lista;
                GVCategorias.DataSource = lista;
                GVCategorias.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }

        }
        /*
        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCategoria.aspx", false);
        }*/

        protected void GVCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCategorias.PageIndex = e.NewPageIndex;
            CargarCategorias();
        }

        protected void GVCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GVCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaCategoria.aspx?id=" + id);
        }

        protected void GVCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVCategorias.Rows[rowIndex];
                int idCategoria = Convert.ToInt32(GVCategorias.DataKeys[row.RowIndex].Values["IdCategoria"]);

                CategoriaNegocio negocio = new CategoriaNegocio();

                if (e.CommandName == "Delete")
                {
                    negocio.EliminarCategoria(idCategoria);
                }
                else if (e.CommandName == "Reactivar")
                {
                    negocio.ReactivarCategoria(idCategoria);
                }

                CargarCategorias();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
           

        }

        protected void GVCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

       /* protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Categoria> lista = (List<Categoria>)Session["listaCategoria"];
            List<Categoria> filtrada = lista.Where(C => C.Nombre.Trim().ToLower().Contains(txtBuscarCategoria.Text.Trim().ToLower())).ToList();

            GVCategorias.DataSource = filtrada;
            GVCategorias.DataBind();
        }*/

        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        protected void lkbAdregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCategoria.aspx", false);
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            List<Categoria> lista = (List<Categoria>)Session["listaCategoria"];
            List<Categoria> filtrada = lista.Where(C => C.Nombre.Trim().ToLower().Contains(txtBuscarCategoria.Text.Trim().ToLower())).ToList();

            GVCategorias.DataSource = filtrada;
            GVCategorias.DataBind();
        }
    }
}
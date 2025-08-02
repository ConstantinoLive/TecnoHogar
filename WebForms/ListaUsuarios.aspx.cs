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
    public partial class ListaUsuarios : System.Web.UI.Page
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
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;

            try
            {
                List<Usuario> lista = mostrarEliminados ?
                    negocio.ListarEliminados() :
                    negocio.Listar();

                Session["listaUsuario"] = lista;

                GVUsuarios.DataSource = lista;
                GVUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }

        }
        
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdmin.aspx", false);
        }

        protected void GVUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string id = GVUsuarios.SelectedDataKey.Value.ToString();
            string id = GVUsuarios.SelectedValue.ToString();
            Response.Redirect("AltaUsuario.aspx?Id=" + id);
        }

        protected void GVUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;


        }
        
        protected void GVUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVUsuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        protected void GVUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete" || e.CommandName == "Reactivar")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int idUsuario = Convert.ToInt32(GVUsuarios.DataKeys[row.RowIndex].Values["IdUsuario"]);

                    UsuarioNegocio negocio = new UsuarioNegocio();

                    if (e.CommandName == "Delete")
                    {
                        negocio.EliminarUsuario(idUsuario);
                    }
                    else if (e.CommandName == "Reactivar")
                    {
                        negocio.ReactivarUsuario(idUsuario);
                    }

                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }

        }

        protected void lkbAdregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaUsuario.aspx", false);
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                List<Usuario> lista = (List<Usuario>)Session["listaUsuario"];
                List<Usuario> filtrada = lista.Where(c => c.NombreUsuario.Trim().ToLower().Contains(txtBuscarUsuario.Text.Trim().ToLower()) || c.Apellido.Trim().ToLower().Contains(txtBuscarUsuario.Text.Trim().ToLower()) || c.Nombre.Trim().ToLower().Contains(txtBuscarUsuario.Text.Trim().ToLower())).ToList();
                GVUsuarios.DataSource = filtrada;
                GVUsuarios.DataBind();
                txtBuscarUsuario.Text = "";
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }
    }
}
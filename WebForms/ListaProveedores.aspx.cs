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
    public partial class Proveedores : System.Web.UI.Page
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
                CargarProveedor();
            }
        }

        private void CargarProveedor()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;

            try
            {
                List<Proveedor> lista = mostrarEliminados ?
                negocio.ListarEliminados() :
                negocio.Listar();

                Session["listaProveedor"] = lista;

                GVProveedores.DataSource = lista;
                GVProveedores.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaProveedor.aspx", false);
        }

        protected void GVProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVProveedores.PageIndex = e.NewPageIndex;
            CargarProveedor();
        }
       
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdmin.aspx", false);
        }

        protected void GVProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string id = GVProveedores.SelectedDataKey.Value.ToString();
            string id = GVProveedores.SelectedValue.ToString();
            Response.Redirect("AltaProveedor.aspx?Id=" + id);
        }

        protected void GVProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }
       

        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarProveedor();
        }

        protected void GVProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete" || e.CommandName == "Reactivar")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int idProveedor = Convert.ToInt32(GVProveedores.DataKeys[row.RowIndex].Values["IdProveedor"]);

                    ProveedorNegocio negocio = new ProveedorNegocio();

                    if (e.CommandName == "Delete")
                    {
                        negocio.EliminarProveedor(idProveedor);
                    }
                    else if (e.CommandName == "Reactivar")
                    {
                        negocio.ReactivarProveedor(idProveedor);
                    }

                    CargarProveedor();
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
            Response.Redirect("AltaProveedor.aspx", false);
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            try
            {
                List<Proveedor> lista = (List<Proveedor>)Session["listaProveedor"];
                List<Proveedor> listaFiltrada = lista.Where(c => c.CUIT.Trim().Contains(txtBuscarCuit.Text.Trim()) || c.RazonSocial.Trim().ToLower().Contains(txtBuscarCuit.Text.Trim().ToLower())).ToList();

                GVProveedores.DataSource = listaFiltrada;
                GVProveedores.DataBind();
                txtBuscarCuit.Text = "";

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }
    }
}
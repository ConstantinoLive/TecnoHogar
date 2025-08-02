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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

          

            if (!IsPostBack)
            {

                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;

            try
            {
                List<Cliente> lista = mostrarEliminados ?
                    negocio.ListarEliminados() :
                    negocio.ListarConSp();
                Session["listaCliente"] = lista;
                dgvClientes.DataSource = lista;
                dgvClientes.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }
        /*
        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCliente.aspx", false);
        }
        */
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            /*Logica para redirigir al panel que corresponda egun su perfil*/
            Response.Redirect("Default.aspx", false);
        }

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string id = dgvClientes.SelectedDataKey.Value.ToString();
            string id = dgvClientes.SelectedValue.ToString();
            Response.Redirect("AltaCliente.aspx?id=" + id);
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            CargarClientes();
        }

        protected void dgvClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }
        /*
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Cliente> lista = (List<Cliente>)Session["listaCliente"];
            List<Cliente> filtrada = lista.Where(c => c.Dni.Trim().Contains(txtBuscarDni.Text.Trim())).ToList();

            dgvClientes.DataSource = filtrada;
            dgvClientes.DataBind();

            txtBuscarDni.Text = "";
        }
        */
        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarClientes();
        }

        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete" || e.CommandName == "Reactivar")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int idCliente = Convert.ToInt32(dgvClientes.DataKeys[row.RowIndex].Values["IdCliente"]);

                    ClienteNegocio negocio = new ClienteNegocio();

                    if (e.CommandName == "Delete")
                    {
                        negocio.EliminarCliente(idCliente);
                    }
                    else if (e.CommandName == "Reactivar")
                    {
                        negocio.ReactivarCliente(idCliente);
                    }

                    CargarClientes();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }


        }

        protected void btnimgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            List<Cliente> lista = (List<Cliente>)Session["listaCliente"];
            List<Cliente> filtrada = lista.Where(c => c.Dni.Trim().Contains(txtBuscarDni.Text.Trim())).ToList();

            dgvClientes.DataSource = filtrada;
            dgvClientes.DataBind();

            txtBuscarDni.Text = "";
        }

        protected void lkbAdregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCliente.aspx", false);
        }
    }
}
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace WebForms
{
    public partial class Comisiones : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            Session["PaginaAnterior"] = Request.UrlReferrer?.AbsoluteUri;

            if (!IsPostBack)
            {
                if (((Usuario)Session["Usuario"]).Admin)
                {
                    cargarUsuarios();
                }
                cargarComisiones(); 
            }
            
            if (Session["IdUsuario"] == null && Session["Usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Session["IdUsuario"] = usuario.IdUsuario;
                lblPorcentajeComision.Text = (usuario.PorcentajeComision/100).ToString("P"); 
            }
        }

        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComisiones();
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComisiones();
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio UserNegocio = new UsuarioNegocio();
            List<Usuario> listaUsuarios = UserNegocio.Listar();
            ddlUsuario.DataSource = listaUsuarios;
            ddlUsuario.DataTextField = "NombreUsuario";
            ddlUsuario.DataValueField = "IdUsuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("-- Seleccionar Usuario --", "0"));
        }

        private void cargarComisiones()
        {
            try
            {
                string anio = ddlAnio.SelectedValue;
                int mes = Convert.ToInt32(ddlMes.SelectedValue);
                int idUsuario;

                if (anio == "--" || mes == 0)
                {
                    lblNoData.Visible= true;
                    rptComisiones.DataSource = null;
                    rptComisiones.DataBind();
                    lblTotalComision.Text = "$0,00";
                    return;
                }
                lblNoData.Visible = false;

                ComisionesNegocio comNegocio = new ComisionesNegocio();

                Usuario usuario = (Usuario)Session["Usuario"];
               

                if (!usuario.Admin)
                {
                    idUsuario = usuario.IdUsuario;
                    lblPorcentajeComision.Text = (usuario.PorcentajeComision / 100).ToString("P");
                }
                else
                {
                    idUsuario = ddlUsuario.SelectedValue != "0" ? Convert.ToInt32(ddlUsuario.SelectedValue) : 0;

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    Usuario usuarioSeleccionado = usuarioNegocio.Listar().FirstOrDefault(u => u.IdUsuario == idUsuario);

                    lblPorcentajeComision.Text = (usuarioSeleccionado.PorcentajeComision / 100).ToString("P");
                }

               // lblPorcentajeComision.Text = (usuario.PorcentajeComision / 100).ToString("P"); 

                List<Dominio.Comisiones> lista = comNegocio.listar(anio, mes, idUsuario);
                rptComisiones.DataSource = lista;
                rptComisiones.DataBind();

                decimal total = comNegocio.sumarMontoComision(anio, mes, idUsuario);
                lblTotalComision.Text = total.ToString("C"); // Formato moneda
            }
            catch (Exception ex)
            {
                Session.Add("Error", "Error al cargar las comisiones: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComisiones();
        }
    }
}
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Utils;

namespace WebForms
{
    public partial class AltaTipoProducto : System.Web.UI.Page
    {
        private List<TipoProducto> lista;
        private List<TipoProducto> listaE;
        private WebControl[] CajasDeTexto;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    cargarDropdowns();

                    // Si es edición, llena campos
                    if (Request.QueryString["Id"] != null)
                    {
                        TipoProductoNegocio negocio = new TipoProductoNegocio();
                        lista = negocio.ListarTPConSp();

                        int id = int.Parse(Request.QueryString["Id"]);
                        TipoProducto seleccionado = lista.Find(x => x.IdTipoProducto == id);

                        txtID.Text = seleccionado.IdTipoProducto.ToString();
                        txtNombre.Text = seleccionado.Nombre;
                        //DDLCategorias.SelectedValue = seleccionado.categoria.Nombre.ToString();
                        DDLCategorias.SelectedValue = seleccionado.categoria.IdCategoria.ToString();

                        btnAceptar.Enabled = true;

                    }

                   
                }
                CajasDeTexto= new WebControl[] { txtNombre, DDLCategorias };

                ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }
        }

        private void cargarDropdowns()
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            DDLCategorias.DataSource = catNegocio.ListarCategorias();
            DDLCategorias.DataTextField = "Nombre";
            DDLCategorias.DataValueField = "IdCategoria";
            DDLCategorias.DataBind();
            DDLCategorias.Items.Insert(0, new ListItem("- Seleccione -"));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaTipoProducto.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            

            try
            {
                if (DDLCategorias.SelectedValue == "0" || string.IsNullOrEmpty(DDLCategorias.SelectedValue))
                {
                    lblMensaje.Text = "Debe seleccionar una categoría válida";
                    lblMensaje.Visible = true;
                    return;
                }

                TipoProducto TP = new TipoProducto();
                TipoProductoNegocio negocio = new TipoProductoNegocio();

                TP.Nombre = txtNombre.Text;
                TP.categoria = new Categoria();
                TP.categoria.IdCategoria = int.Parse(DDLCategorias.SelectedValue);

                if (Request.QueryString["Id"] != null)
                {
                    TP.IdTipoProducto = int.Parse(Request.QueryString["Id"]);
                    negocio.ModificarTP(TP);
                    Response.Redirect("ListaTipoProducto.aspx", false);
                }
                else
                {
                    lista = negocio.ListarTPConSp();
                    listaE = negocio.ListarTPEliminados();

                    bool encontrado = lista.Any(x => x.Nombre.Trim().ToLower() == TP.Nombre.Trim().ToLower());
                    bool encontradoEliminados = listaE.Any(y => y.Nombre.Trim().ToLower() == TP.Nombre.Trim().ToLower());

                    if (!encontrado && !encontradoEliminados)
                    {
                        negocio.AgregarTP(TP);
                        Response.Redirect("ListaTipoProducto.aspx", false);
                    }
                    else if (encontrado)
                    {
                        lblMensaje.Text = "El tipo de producto ya se encuentra registrado y activo.";
                        lblMensaje.Visible = true;
                    }
                    else if (encontradoEliminados)
                    {
                        lblMensaje.Text = "El tipo de producto ya existe pero está inactivo. Vuelva a darlo de alta.";
                        lblMensaje.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
            lblMensaje.Text = "";
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
        }
    }   

}
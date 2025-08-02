using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using WebForms.Utils; // Assuming this is the namespace where ValidacionCampo is defined


namespace WebForms
{
    public partial class AltaProveedor : System.Web.UI.Page
    {
        private List<Proveedor> lista;
        private TextBox[] CajasDeTexto = new TextBox[5];
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CajasDeTexto[0] = txtRazonSocial;
                CajasDeTexto[1] = txtCuit;
                CajasDeTexto[2] = txtDireccion;
                CajasDeTexto[3] = txtTelefono;
                CajasDeTexto[4] = txtEmail;

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

                if (Request.QueryString["Id"] != null)
                {
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    lista = negocio.Listar();


                    if (!IsPostBack)
                    {
                        int id = int.Parse(Request.QueryString["Id"]);
                        Proveedor seleccionado = lista.Find(x => x.IdProveedor == id);

                        txtCuit.Text = seleccionado.CUIT;
                        txtDireccion.Text = seleccionado.Direccion;
                        txtEmail.Text = seleccionado.Email;
                        txtId.Text = seleccionado.IdProveedor.ToString();
                        txtRazonSocial.Text = seleccionado.RazonSocial;
                        txtTelefono.Text = seleccionado.Telefono;

                        btnAceptar.Enabled = true;

                    }

                }

                ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);


            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProveedores.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Proveedor nuevo = new Proveedor();
            ProveedorNegocio negocio = new ProveedorNegocio();

            try
            {

                nuevo.Direccion = txtDireccion.Text.Trim();
                nuevo.RazonSocial = txtRazonSocial.Text.Trim();
                if (ValidacionCampo.ValidarCorreo(txtEmail.Text.Trim()))
                {
                    nuevo.Email = txtEmail.Text.Trim();
                }
                else
                {

                    lblEmailMensaje.Text = "Formato invalido";
                }

                nuevo.Telefono = txtTelefono.Text.Trim();
                nuevo.CUIT = txtCuit.Text.Trim();

                if (Request.QueryString["Id"] != null)
                {
                    nuevo.IdProveedor = int.Parse(Request.QueryString["id"]);
                    negocio.ModificarProveedor(nuevo);
                    Response.Redirect("ListaProveedores.aspx", false);
                }
                else
                {
                    lista = negocio.Listar();
                    bool encontrado = lista.Any(x => x.CUIT == nuevo.CUIT);

                    if (encontrado)
                    {
                        lblAviso.Text = "El proveedor ya se encuentra registrado";
                        return;
                    }


                    negocio.AltaPorveedor(nuevo);
                    Response.Redirect("ListaProveedores.aspx", false);

                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }

        
        protected void txtRazonSocial_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
        }

        protected void txtCuit_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);

        }

        protected void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);

        }

        protected void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
            lblEmailMensaje.Text = "";
        }
    }
}
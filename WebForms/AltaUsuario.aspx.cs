using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using WebForms.Utils;

namespace WebForms
{
    public partial class AltaVendedor : System.Web.UI.Page
    {
        private WebControl[] Controles = new WebControl[7]; 

        protected void Page_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            try
            {
                Controles[0] = txtApellido;
                Controles[1] = txtEmail;
                Controles[2] = txtNombre;
                Controles[3] = txtNombreUsuario;
                Controles[4] = txtContrasena;
                Controles[5] = ddlRol;
                Controles[6] = txbComision;

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

                    if (Request.QueryString["Id"] != null)
                    {
                        Usuario usuario = new Usuario();
                        UsuarioNegocio negocio = new UsuarioNegocio();

                        List<Usuario> lista = negocio.Listar();

                        usuario = lista.Find(x => x.IdUsuario == int.Parse((Request.QueryString["Id"])));

                        txtIdUsuario.Text = usuario.IdUsuario.ToString();
                        txtApellido.Text = usuario.Apellido;
                        txtNombre.Text = usuario.Nombre;
                        txtNombreUsuario.Text = usuario.NombreUsuario;
                        txtEmail.Text = usuario.Email;
                        txtContrasena.Text = usuario.Contrasena;
                        txtFechaAlta.Text = usuario.FechaAlta.ToString("yyyy-MM-dd");
                        ddlRol.SelectedValue = usuario.Admin.ToString();
                        txbComision.Text = (usuario.PorcentajeComision/100).ToString("P");

                    }
                    else
                    {

                        txtFechaAlta.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
                ValidacionCampo.TodosCamposCompletos(Controles);
                
            }
            catch (Exception ex)
            {
                Session.Add("Error" , ex.ToString());
                Response.Redirect("Error.aspx", false);

            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaUsuarios.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario nuevo = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                nuevo.Apellido = txtApellido.Text;
                nuevo.NombreUsuario = txtNombreUsuario.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Contrasena = txtContrasena.Text;
                nuevo.FechaAlta = DateTime.Parse(txtFechaAlta.Text);
                nuevo.Admin = Convert.ToBoolean(ddlRol.SelectedValue);
                nuevo.PorcentajeComision = decimal.Parse(txbComision.Text);

                if (Request.QueryString["Id"] != null)
                {
                    nuevo.IdUsuario = int.Parse(Request.QueryString["Id"]);
                    negocio.ModificarUsuario(nuevo);

                }
                else
                {
                    bool encontrado = negocio.Listar().Exists(x => x.Email == nuevo.Email);

                    if (encontrado) { 
                        
                        lblAviso.Text = "El email ingresado ya se encuentra registrado.";
                        return;
                    }
                    negocio.AgregarUsuario(nuevo);

                }

                Response.Redirect("ListaUsuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }


        }

        protected void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);
        }

        protected void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);

        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);

        }

        protected void txtApellido_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);

        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
           
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);
               

        }

        protected void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);

        }

       protected void txbComision_TextChanged(object sender, EventArgs e)
        {
            
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);
            
        }



        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, Controles);
        }
    }
}
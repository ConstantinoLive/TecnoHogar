using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class vendedores : System.Web.UI.MasterPage
    {
        public string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }
            Usuario usuario = (Usuario)Session["Usuario"];

            name = $"{usuario.Nombre} {usuario.Apellido}";
        }
        protected void lnkVenta_Click(object sender, EventArgs e)
        {
         
            Response.Redirect("AltaVenta.aspx", false);
        }

        protected void lnkProductos_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("ListaProductos.aspx", false);
        }

        protected void lnkClientes_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ListaClientes.aspx", false);
        }

        protected void lnkNuevoCliente_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("AltaCliente.aspx", false);
        }

        protected void lnkFacturas_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("ListaVentas.aspx", false);
        }
        
        protected void lkbSalir_Click(object sender, EventArgs e)
        {
            
            Session.Remove("Usuario");
            Response.Redirect("Default.aspx", false);
        }

        private bool ValidarCampos()
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            string contraseñaActual = txtPassActual.Text.Trim();
            string contraseñaNueva = txtPassNueva.Text.Trim();
            string contraseñaNueva2 = txtPassNueva2.Text.Trim();

            if (string.IsNullOrWhiteSpace(contraseñaActual) || string.IsNullOrWhiteSpace(contraseñaNueva) || string.IsNullOrWhiteSpace(contraseñaNueva2))
            {
                lblMensaje.Text = "Todos los campos deben estar completos.";
                return false;
            }

            return true;
        }

        private bool validaContraseña(string contraseña)
        {
            // Expresión regular mejorada que requiere:
            // - Al menos una minúscula
            // - Al menos una mayúscula
            // - Al menos un número
            // - Al menos un carácter especial
            // - Mínimo 8 caracteres
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            // Verificar que la contraseña no sea nula o vacía
            if (string.IsNullOrWhiteSpace(contraseña))
            {
                return false;
            }

            // Validar contra el patrón
            return Regex.IsMatch(contraseña, patron);
        }

        
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    string contraseñaActual = txtPassActual.Text.Trim();
                    string contraseñaNueva = txtPassNueva.Text.Trim();
                    string contraseñaNueva2 = txtPassNueva2.Text.Trim();

                    UsuarioNegocio negocio = new UsuarioNegocio();

                    Usuario usuario = negocio.Listar()
                        .FirstOrDefault(u => u.Contrasena.Equals(txtPassActual.Text.Trim(), StringComparison.OrdinalIgnoreCase) && u.Email.Equals(((Usuario)Session["Usuario"]).Email, StringComparison.OrdinalIgnoreCase));

                    lblMensaje.Text = string.Empty;

                    if (!validaContraseña(txtPassNueva.Text))
                    {
                        lblMensaje.Text = "Formato incorrecto";
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarCambioPass", "mostrarModalCambioPass();", true);
                        return;
                    }


                    if (contraseñaNueva != contraseñaNueva2)
                    {
                        lblMensaje.Text = "Las contraseñas nuevas no coinciden";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarCambioPass", "mostrarModalCambioPass();", true);
                        return;
                    }


                    if (contraseñaActual == contraseñaNueva)
                    {
                        lblMensaje.Text = "La nueva contraseña debe ser diferente a la actual";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarCambioPass", "mostrarModalCambioPass();", true);
                        return;
                    }



                    if(usuario == null)
                    {
                        lblMensaje.Text = "La contraseña actual es incorrecta";
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarCambioPass", "mostrarModalCambioPass();", true);
                        return;
                    }
                    else
                    {
                        usuario.Contrasena = txtPassNueva.Text;

                        negocio.ModificarUsuario(usuario);
                        Session.Remove("Usuario");
                        Response.Redirect("Default.aspx", false);
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarCambioPass", "mostrarModalCambioPass();", true);
                }
                
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //MostrarModalCambioPass = false;
            Response.Redirect("AltaVenta.aspx", false);
        }
    }
}
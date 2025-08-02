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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    EmailService email = new EmailService();

                    //List<Usuario> lista = negocio.Listar();

                    Usuario usuario = negocio.Listar()
                    .FirstOrDefault(u => u.Email.Equals(txtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (usuario == null)
                    {
                        lblMensaje.Text = "Email no registrado en el sistema";
                        return;
                    }

                    Session.Add("EmailUsuario", usuario.Email);
                    Session.Add("NombreUsuario", usuario.Nombre);
                    Session.Add("UsuarioNombre", usuario.NombreUsuario);
                    Session.Add("ContraseñaUsuario", usuario.Contrasena);
                    

                    lblMensaje.Text = "";
                    
                    string cuerpoEmail = $@"
                    <h2 style='color: #2c3e50;'>Recuperación de acceso</h2>
                    
                    <p>Estimado/a <strong>{usuario.NombreUsuario}</strong>,</p>
                    
                    <p>Hemos recibido una solicitud de recuperación de credenciales para tu cuenta.</p>
                    
                    <div style='background-color: #f8f9fa; padding: 15px; border-left: 4px solid #3498db;'>
                        <p><strong>Tus datos de acceso:</strong></p>
                        <ul style='list-style-type: none; padding-left: 0;'>
                            <li>👤 <strong>Usuario:</strong> {usuario.NombreUsuario}</li>
                            <li>🔑 <strong>Contraseña temporal:</strong> {usuario.Contrasena}</li>
                        </ul>
                    </div>
                    
                    <p style='color: #e74c3c; font-weight: bold;'>Por seguridad, te recomendamos:</p>
                    <ol>
                        <li>Cambiar esta contraseña al ingresar al sistema</li>
                        <li>No compartir tus credenciales con nadie</li>
                        <li>Eliminar este email después de usarlo</li>
                    </ol>
                    
                    <p>Si no solicitaste este acceso, por favor contacta a soporte.</p>
                    
                    <hr style='border: 1px solid #ecf0f1;'>
                    
                    <footer style='font-size: 12px; color: #7f8c8d;'>
                        <p>Este es un mensaje automático - Por favor no respondas a este correo</p>
                        <p>© {DateTime.Now.Year} Nombre de tu Sistema. Todos los derechos reservados.</p>
                    </footer>";
                   
                    email.ArmarCorreo(
                        destinatario: usuario.Email,
                        asunto: "Recuperación de cuenta - No responder",
                        cuerpo: cuerpoEmail
                     );

                    email.EnviarEmail();

                    Response.Redirect("Confirmacion.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error.aspx", ex.ToString());
            }

        }

        private bool ValidarCampos()
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;


            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                lblMensaje.Text = "Todos los campos deben estar completos.";
                return false;
            }

            return true;
        }
    }
}
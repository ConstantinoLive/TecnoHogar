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
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmailUsuario"] == null)
            {
                Session.Add("Error", "No se realizo la validacion correctamente");
                Response.Redirect("Error.aspx", false);
            }
            
        }

        protected void btnReenviar_Click(object sender, EventArgs e)
        {
            try
            {

                EmailService email = new EmailService();
                UsuarioNegocio negocio = new UsuarioNegocio();

                List<Usuario> lista = negocio.Listar();

                Usuario usuario = negocio.Listar()
                    .FirstOrDefault(u => u.Email.Equals(
                    Session["EmailUsuario"]?.ToString()?.Trim(),
                    StringComparison.OrdinalIgnoreCase
                    ));

                string cuerpoEmail = $@"
                    <h2 style='color: #2c3e50;'>Recuperación de acceso</h2>
                    
                    <p>Estimado/a <strong>{(Session["NombreUsuario"])}</strong>,</p>
                    
                    <p>Hemos recibido una solicitud de recuperación de credenciales para tu cuenta.</p>
                    
                    <div style='background-color: #f8f9fa; padding: 15px; border-left: 4px solid #3498db;'>
                        <p><strong>Tus datos de acceso:</strong></p>
                        <ul style='list-style-type: none; padding-left: 0;'>
                            <li>👤 <strong>Usuario:</strong> {(Session["UsuarioNombre"])}</li>
                            <li>🔑 <strong>Contraseña temporal:</strong> {(Session["ContraseñaUsuario"])}</li>
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
                    destinatario: Session["EmailUsuario"].ToString(),
                    asunto: "Recuperación de cuenta - No responder",
                    cuerpo: cuerpoEmail
                 );

                email.EnviarEmail();

                Response.Redirect("Confirmacion.aspx", false);
            }

            catch (Exception ex)
            {

                Session.Add("Error.aspx", ex.ToString());
            }



        }
    }
}
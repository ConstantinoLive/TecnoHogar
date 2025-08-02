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
    public partial class AltaMarca : System.Web.UI.Page
    {
        private List<Marca> lista;
        private List<Marca> listaE;
        private TextBox[] CajasDeTexto = new TextBox[1];
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

                CajasDeTexto[0] = txtNombre;


                if (Request.QueryString["Id"] != null)
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    lista = negocio.ListarMarcaConSp();


                    if (!IsPostBack)
                    {
                        int id = int.Parse(Request.QueryString["Id"]);
                        Marca seleccionado = lista.Find(x => x.IdMarca == id);

                        txtID.Text = seleccionado.IdMarca.ToString();
                        txtNombre.Text = seleccionado.Nombre;


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
            Response.Redirect("ListaMarcas.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Marca MRK = new Marca();
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {

                MRK.Nombre = txtNombre.Text;


                if (Request.QueryString["Id"] != null)
                {

                    MRK.IdMarca = int.Parse(Request.QueryString["Id"]);
                    negocio.ModificarMarca(MRK);
                    Response.Redirect("ListaMarcas.aspx", false);
                }
                else
                {
                    lista = negocio.ListarMarcaConSp();
                    listaE = negocio.ListarMarcaEliminadas();
                    bool encontrado = lista.Any(x => x.Nombre.Trim().ToLower() == MRK.Nombre.Trim().ToLower());
                    bool encontradoElimninados = listaE.Any(y => y.Nombre.Trim().ToLower() == MRK.Nombre.Trim().ToLower());

                    if (!encontrado && !encontradoElimninados)
                    {
                        negocio.AgregarMarca(MRK);
                        Response.Redirect("ListaMarcas.aspx", false);

                    }
                    else if (encontrado)
                    {
                        lblMensaje.Text = "La marca que intenta ingresar ya se encuentra registrada y activa";
                        lblMensaje.Visible = true;
                        return;
                    }
                    else if (encontradoElimninados)
                    {
                        lblMensaje.Text = "La marca que intenta ingresar ya se encuentra registrada e inactiva. Vuelva a darla de alta";
                        lblMensaje.Visible = true;
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx");

            }
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ValidacionCampo.ControlAceptar(btnAceptar, CajasDeTexto);
            lblMensaje.Text = "";
        }
    }
}
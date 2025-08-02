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
    public partial class AltaCategoria : System.Web.UI.Page
    {
        private List<Categoria> lista;
        private List<Categoria> listaE;
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
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    lista = negocio.ListarCategorias();


                    if (!IsPostBack)
                    {
                        int id = int.Parse(Request.QueryString["Id"]);
                        Categoria seleccionado = lista.Find(x => x.IdCategoria == id);

                        txtID.Text = seleccionado.IdCategoria.ToString();
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
            Response.Redirect("ListaCategorias.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Categoria CTGR = new Categoria();
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {

                CTGR.Nombre = txtNombre.Text;


                if (Request.QueryString["Id"] != null)
                {

                    CTGR.IdCategoria = int.Parse(Request.QueryString["Id"]);
                    negocio.ModificarCategoria(CTGR);
                    Response.Redirect("ListaCategorias.aspx", false);
                }
                else
                {
                    lista = negocio.ListarCategorias();
                    listaE = negocio.ListarCategoriasEliminadas();
                    bool encontrado = lista.Any(x => x.Nombre.Trim().ToLower() == CTGR.Nombre.Trim().ToLower());
                    bool encontradoElimninados = listaE.Any(y => y.Nombre.Trim().ToLower() == CTGR.Nombre.Trim().ToLower());

                    if (!encontrado && !encontradoElimninados)
                    {
                        negocio.AgregarCategoria(CTGR);
                        Response.Redirect("ListaCategorias.aspx", false);

                    }
                    else if (encontrado)
                    {
                        lblMensaje.Text = "La categoria que intenta ingresar ya se encuentra registrada y activa";
                        lblMensaje.Visible = true;
                        return;
                    }
                    else if (encontradoElimninados)
                    {
                        lblMensaje.Text = "La categoria que intenta ingresar ya se encuentra registrada e inactiva. Vuelva a darla de alta";
                        lblMensaje.Visible = true;
                        return;
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
    }
}
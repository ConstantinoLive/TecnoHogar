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
    public partial class ListaProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            if (Session["Usuario"] != null && !Seguridad.esAdmin((Usuario)Session["Usuario"]))
            {
                

                // OCULTO COLUMNAS AL VENDEDOR
                GVProductos.Columns[5].Visible = false; // PRECIO COMPRA
                GVProductos.Columns[6].Visible = false; // GANANCIA
                GVProductos.Columns[11].Visible = false; // ACCIONES
            }

            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            bool mostrarEliminados = CheckEliminados.Checked;

            try
            {
                List<Producto> lista = mostrarEliminados ?
                negocio.ListarEliminados() :
                negocio.Listar();
                Session["listaProducto"] = lista;
                GVProductos.DataSource = lista;
                GVProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }
      
        protected void GVProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVProductos.PageIndex = e.NewPageIndex;
            //GVProductos.DataBind();
            CargarProductos();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdmin.aspx", false);
        }

        protected void GVProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GVProductos.SelectedValue.ToString();
            Response.Redirect("AltaProducto.aspx?Id=" + id);
        }

        protected void GVProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

        protected void CheckEliminados_CheckedChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void GVProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete" || e.CommandName == "Reactivar")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int id = Convert.ToInt32(GVProductos.DataKeys[row.RowIndex].Values["IdProducto"]);

                    ProductoNegocio negocio = new ProductoNegocio();

                    if (e.CommandName == "Delete")
                    {
                        negocio.EliminarProductoLogico(id);
                    }
                    else if (e.CommandName == "Reactivar")
                    {
                        negocio.ReactivarProducto(id);
                    }

                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }

        }

        public string CalculoPrecioVenta(object precioCompraObj, object porcentajeGananciaObj)
        {
            decimal precioCompra = Convert.ToDecimal(precioCompraObj);
            decimal porcentajeGanancia = Convert.ToDecimal(porcentajeGananciaObj);
            decimal precioVenta = precioCompra * (1 + (porcentajeGanancia / 100));

            return precioVenta.ToString("C2");
        }

        protected void lkbAdregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaProducto.aspx", false);
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                List<Producto> lista = (List<Producto>)Session["listaProducto"];
                string filtro = txtBuscarCuit.Text.Trim().ToLower();

                List<Producto> listaFiltrada = lista.Where(c =>
                    c.Nombre.Trim().ToLower().Contains(filtro) ||
                    c.Descripcion.Trim().ToLower().Contains(filtro)
                ).ToList();

                GVProductos.DataSource = listaFiltrada;
                GVProductos.DataBind();
                txtBuscarCuit.Text = "";

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }
    }
}
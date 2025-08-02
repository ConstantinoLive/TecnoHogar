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
    public partial class FacturaVtaMs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Venta ventaActual = null;
                VentaNegocio negocio = new VentaNegocio();

                // CAPTURO ID DE URL
                if (int.TryParse(Request.QueryString["ID"], out int idVenta))
                {
                    // ME TRAIGO LA VENTA DESDE BD
                    ventaActual = negocio.BuscarVenta(idVenta);
                }
                else
                {
                    // O CAPTURO LA VENTA EN CURSO
                    ventaActual = (Venta)Session["VentaEnCurso"];
                }

                if (ventaActual != null)
                {
                    // ENCABEZADO
                    lblNumeroFactura.Text = ventaActual.IdVenta.ToString("D8"); // Ej: 00000012
                    //lblFecha.Text = ventaActual.Fecha?.ToString("dd/MM/yyyy") ?? DateTime.Now.ToString("dd/MM/yyyy");
                    lblFecha.Text = ventaActual.Fecha?.ToString("dd/MM/yyyy");
                    lblUsuario.Text = ventaActual.Usuario.NombreUsuario + " " + ventaActual.Usuario.Apellido;

                    // CLIENTE
                    lblClienteNombre.Text = ventaActual.Cliente.Nombre + " " + ventaActual.Cliente.Apellido;
                    lblClienteDireccion.Text = ventaActual.Cliente.Direccion;
                    lblClienteDNI.Text = ventaActual.Cliente?.Dni ?? "-";

                    // DETALLES
                    var lista = new List<object>();

                    foreach (var detalle in ventaActual.Detalles)
                    {
                        lista.Add(new
                        {
                            Producto = detalle.Producto,
                            Cantidad = detalle.Cantidad,
                            PrecioVenta = detalle.PrecioVenta,
                            Subtotal = detalle.Cantidad * detalle.PrecioVenta
                        });
                    }
                    rptDetalles.DataSource = lista;
                    rptDetalles.DataBind();

                    // Total
                    lblTotal.Text = $"${ventaActual.Total:N0}";
                }
                else
                {
                    Response.Redirect("ListaVentas.aspx");
                }

                // SOLO IMPRIMIR SI ES TRUE
                if (Request.QueryString["imprimir"] == "true")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "printFactura", "window.print();", true);
                }
            }
        }
    }
}
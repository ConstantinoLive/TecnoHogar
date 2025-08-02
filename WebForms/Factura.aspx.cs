using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Negocio;


namespace WebForms
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Venta ventaActual = null;
                VentaNegocio negocio = new VentaNegocio();

                try
                {
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
                catch (Exception ex)
                {
                    Session["Error"] = "Ocurrió un error al procesar la venta: " + ex.Message;
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        public void DescargarPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Fuente básica
                Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Encabezado
                doc.Add(new Paragraph("Tecno Hogar S.A.", fontTitulo));
                doc.Add(new Paragraph("CUIT: 30-12345678-9", fontNormal));
                doc.Add(new Paragraph("Av. Siempre Viva 123, CABA", fontNormal));
                doc.Add(new Paragraph("Tel: (011) 4567-8900 - contacto@tecnohogar.com.ar", fontNormal));
                doc.Add(new Paragraph(" ", fontNormal));

                // Datos Factura
                doc.Add(new Paragraph($"FACTURA B - Nº: {lblNumeroFactura.Text}", fontSubtitulo));
                doc.Add(new Paragraph($"Fecha: {lblFecha.Text}", fontNormal));
                doc.Add(new Paragraph($"Usuario: {lblUsuario.Text}", fontNormal));
                doc.Add(new Paragraph(" ", fontNormal));

                // Datos Cliente
                doc.Add(new Paragraph("Datos del Cliente", fontSubtitulo));
                doc.Add(new Paragraph($"Nombre: {lblClienteNombre.Text}", fontNormal));
                doc.Add(new Paragraph($"Dirección: {lblClienteDireccion.Text}", fontNormal));
                doc.Add(new Paragraph($"DNI: {lblClienteDNI.Text}", fontNormal));
                doc.Add(new Paragraph(" ", fontNormal));

                // Tabla de productos
                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 4, 1, 2, 2 });

                // Encabezado
                string[] columnas = { "Producto", "Cantidad", "Precio Unitario", "Subtotal" };
                foreach (string col in columnas)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col, fontSubtitulo));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(cell);
                }

                // Items del repeater
                foreach (RepeaterItem item in rptDetalles.Items)
                {
                    var lblProducto = item.FindControl("lblProducto") as Label;
                    var cantidad = DataBinder.Eval(item.DataItem, "Cantidad")?.ToString();
                    var precio = DataBinder.Eval(item.DataItem, "PrecioVenta")?.ToString();
                    var subtotal = DataBinder.Eval(item.DataItem, "Subtotal")?.ToString();

                    tabla.AddCell(new Phrase(DataBinder.Eval(item.DataItem, "Producto.Nombre").ToString(), fontNormal));
                    tabla.AddCell(new Phrase(cantidad, fontNormal));
                    tabla.AddCell(new Phrase($"${precio}", fontNormal));
                    tabla.AddCell(new Phrase($"${subtotal}", fontNormal));
                }

                doc.Add(tabla);

                // Total
                doc.Add(new Paragraph(" ", fontNormal));
                doc.Add(new Paragraph($"Total: {lblTotal.Text}", fontSubtitulo));

                // Observaciones
                doc.Add(new Paragraph(" ", fontNormal));
                doc.Add(new Paragraph("Método de pago: Tarjeta de Crédito (Visa 1 pago)", fontNormal));
                doc.Add(new Paragraph("Observaciones: Garantía oficial de fábrica en todos los productos. Entrega en 48 hs.", fontNormal));

                doc.Close();

                // Descargar
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename=Factura_{lblNumeroFactura.Text}.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
    }
}
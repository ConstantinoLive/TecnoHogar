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
    public partial class Compras : System.Web.UI.Page
    {
        List<CompraDetalle> listaCompraDetalle = new List<CompraDetalle>();//
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                hfSeccionActiva.Value = "OCPendientes";
                cargarDropdowns();
                ActualizarNotificacionStock();
            }
            CargarProductosHistorial();
            MostrarSeccionActiva();
            ComprasNegocio negocio = new ComprasNegocio();
            lblNumOC.Text = negocio.obtenerNumProxCompra().ToString("D8");
        }

        protected void MostrarSeccion(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            hfSeccionActiva.Value = btn.CommandArgument;

            MostrarSeccionActiva();
        }

        private void MostrarSeccionActiva()
        {
            OCPendientes.Style["display"] = "none";
            NuevaOC.Style["display"] = "none";
            HistorialPrecios.Style["display"] = "none";
            Productos.Style["display"] = "none";
            Proveedores.Style["display"] = "none";
            StockCritico.Style["display"] = "none";

            switch (hfSeccionActiva.Value)
            {
                case "OCPendientes":
                    OCPendientes.Style["display"] = "block";
                    if (!IsPostBack) CargarOC();
                    break;
                case "NuevaOC":
                    NuevaOC.Style["display"] = "block";
                    break;
                case "HistorialPrecios":
                    HistorialPrecios.Style["display"] = "block";
                    break;
                case "Productos":
                    Productos.Style["display"] = "block";
                    CargarProductos();
                    break;
                case "Proveedores":
                    Proveedores.Style["display"] = "block";
                    CargarProveedores();
                    break;
                case "StockCritico":
                    StockCritico.Style["display"] = "block";
                    CargarProductosCriticos();
                    break;
            }

            upSecciones.Update();
        }

        /****NuevaOC****/

        private void cargarDropdowns()
        {
            try
            {
                ProveedorNegocio PNegocio = new ProveedorNegocio();
                DDLProveedor.DataSource = PNegocio.Listar();
                DDLProveedor.DataTextField = "RazonSocial";
                DDLProveedor.DataValueField = "IdProveedor";
                DDLProveedor.DataBind();

                DDLProveedor.Items.Insert(0, new ListItem("- Seleccionar proveedor -", "0"));

                ProductoNegocio Negocio = new ProductoNegocio();
                DDLProducto.DataSource = Negocio.ListarConSp();

                DDLProducto.DataTextField = "Nombre";
                DDLProducto.DataValueField = "CodigoArticulo";
                DDLProducto.DataBind();

                DDLProducto.Items.Insert(0, new ListItem("- Seleccionar producto -", "0"));

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar los desplegables: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void DDLProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarAlertas();

            // SI NO SE SELECCIONO UN PRODUCTO
            if (DDLProducto.SelectedIndex == 0)
            {
                // RESET Valores
                TxtBPrecio.Text = "$";
                SeccionCantidadActiva(false);
                txtCantidad.Text = "0";
            }
            else
            {
                // BUSCO PRODUCTO EN LISTA PARA GUARDARME SUS VALORES
                ProductoNegocio negocioProd = new ProductoNegocio();
                string codigoSeleccionado = DDLProducto.SelectedValue;
                Producto producto = negocioProd.ListarConSp()
                                        .FirstOrDefault(p => p.CodigoArticulo == codigoSeleccionado);

                try
                {
                    if (producto != null)
                    {
                        TxtBPrecio.Text = producto.PrecioCompra.ToString("C");

                        // RECUPERO LISTA ACTUAL DE COMPRADETALLE
                        if (Session["ListaCompraDetalle"] == null)
                            Session["ListaCompraDetalle"] = new List<CompraDetalle>();

                        List<CompraDetalle> listaCompraDetalle = (List<CompraDetalle>)Session["ListaCompraDetalle"];

                        // BUSCO SI EL PRODUCTO SELECCIONADO YA SE ENCUENTRA EN LA LISTA ACTUAL DE COMPRADETALLE
                        CompraDetalle detalleExistente = listaCompraDetalle.FirstOrDefault(d => d.Producto.CodigoArticulo == codigoSeleccionado);

                        if (detalleExistente != null)
                        {
                            // SI EL PRODUCTO YA ESTABA EN LA LISTA, PRECARGO LA CANTIDAD QUE LE ASIGNARON LA ANTERIOR VEZ
                            txtCantidad.Text = detalleExistente.Cantidad.ToString();
                        }

                        // SECCION "CANTIDAD" SOLO HABILITADA SI PROVEEDOR Y PRODUCTO YA FUERON SELECCIONADOS
                        if (DDLProveedor.SelectedIndex != 0)
                            SeccionCantidadActiva(true);
                        else
                            SeccionCantidadActiva(false);
                    }

                }
                catch (Exception ex)
                {
                    Session.Add("Error", "Ocurrió un problema al actualizar cantidad: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void BtnPlus_Click(object sender, ImageClickEventArgs e)
        {
            OcultarAlertas();

            try
            {
                if (DDLProducto.SelectedValue == "0" || string.IsNullOrEmpty(DDLProducto.SelectedValue) ||
                    DDLProveedor.SelectedValue == "0" || string.IsNullOrEmpty(DDLProveedor.SelectedValue))
                {
                    lblAlerta2.Text = "Debe ingresar un producto y un proveedor";
                    PanelAleta.Visible = true;
                    return;
                }

                // GUARDO PRODUCTO A AGREGAR PARA LUEGO CARGARLO EN SU CORRESPONDIENTE COMPRADETALLE
                ProductoNegocio negocioProd = new ProductoNegocio();
                string codigoSeleccionado = DDLProducto.SelectedValue;
                Producto producto = negocioProd.ListarConSp()
                                      .FirstOrDefault(p => p.CodigoArticulo == codigoSeleccionado);

                if (producto == null)
                {
                    lblAlerta2.Text = "El producto no existe en la base de datos.";
                    PanelAleta.Visible = true;
                    return;
                }

                // RECUPERO LISTA ACTUAL DE COMPRADETALLE
                if (Session["ListaCompraDetalle"] == null)
                    Session["ListaCompraDetalle"] = new List<CompraDetalle>();

                List<CompraDetalle> listaCompraDetalle = (List<CompraDetalle>)Session["ListaCompraDetalle"];

                // GUARDO LA CANTIDAD SETEADA
                int cantidad = 0;
                int.TryParse(txtCantidad.Text, out cantidad);
                if (cantidad < 1)
                {
                    lblAlerta2.Text = "La cantidad debe ser mayor a 0";
                    PanelAleta.Visible = true;
                    return;
                }

                // COMPRUEBO SI EL PRODUCTO A AGREGAR YA SE ENCUENTRA CARGADO EN EL GRID
                CompraDetalle detalleExistente = listaCompraDetalle.FirstOrDefault(d => d.Producto.CodigoArticulo == codigoSeleccionado);


                if (detalleExistente != null)
                {
                    // SI YA ESTABA AGREGADO LE ACTUALIZO SU CANTIDAD CON LA CANTIDAD NUEVA INGRESADA
                    detalleExistente.Cantidad = cantidad;
                    LblAlertaOK.Text = "Cantidad actualizada correctamente.";
                    PanelAlertaOK.Visible = true;
                }
                else
                {
                    // SI NO ESTABA AGREGADO LO AÑADO A LA LISTA
                    CompraDetalle nuevo = new CompraDetalle();
                    nuevo.Producto = new Producto();
                    nuevo.Producto = producto;
                    nuevo.Producto.IdProducto = negocioProd.GetIdproducto(producto.CodigoArticulo);
                    nuevo.Cantidad = cantidad;
                    nuevo.PrecioUnitario = producto.PrecioCompra;

                    // ORDEN DE LISTA EN GRID OPCIONAL
                    //listaCompraDetalle.Add(nuevo); // ULTIMO AGREGADO AL FINAL DE LISTA
                    listaCompraDetalle.Insert(0, nuevo); // EL ULTIMO AGREGADO AL INICIO DE LA LISTA
                    LblAlertaOK.Text = "Producto agregado correctamente.";
                    PanelAlertaOK.Visible = true;
                    DDLProveedor.Enabled = false;
                }

                // RESGUARDO LA LISTA EN SESSION PARA ACTUALIZARLE CON EL PROXIMO PRODUCTO A AGREGAR
                Session["ListaCompraDetalle"] = listaCompraDetalle;

                // RESET Valor y controles
                txtCantidad.Text = "0";
                txtCantidad.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;

                DDLProducto.SelectedIndex = 0;
                TxtBPrecio.Text = "$";

                // ACTUALIZO
                ActualizarGridDetalle();
                ActualizarTotal();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al agregar producto al grid: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }


        private void ActualizarGridDetalle()
        {
            try
            {
                if (Session["ListaCompraDetalle"] == null)
                    Session["ListaCompraDetalle"] = new List<CompraDetalle>();
                listaCompraDetalle = (List<CompraDetalle>)Session["ListaCompraDetalle"];
                gvDetalleOC.DataSource = listaCompraDetalle;
                gvDetalleOC.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al actualizar grid detalle: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void ActualizarTotal()
        {
            try
            {
                decimal total = listaCompraDetalle.Sum(d => d.Subtotal);
                lblTotal.Text = total.ToString("C");

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al actualizar monto total: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvDetalleOC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    if (Session["ListaCompraDetalle"] != null)
                    {
                        listaCompraDetalle = (List<CompraDetalle>)Session["ListaCompraDetalle"];

                        if (index >= 0 && index < listaCompraDetalle.Count)
                        {
                            listaCompraDetalle.RemoveAt(index);
                            Session["ListaCompraDetalle"] = listaCompraDetalle;
                            LblAlertaOK.Text = "Producto removido correctamente";
                            PanelAlertaOK.Visible = true;
                        }
                    }
                    ActualizarGridDetalle();
                    ActualizarTotal();
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al eliminar producto del grid: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OcultarAlertas();

            try
            {
                // COMPRUEBO SI LA LISTA DEL GRID ACTUAL ES NULL O ESTA VACIA
                if (Session["ListaCompraDetalle"] == null || ((List<CompraDetalle>)Session["ListaCompraDetalle"]).Count == 0)
                {
                    lblAlerta2.Text = "Debe agregar al menos un producto para generar la compra.";
                    PanelAleta.Visible = true;
                    return;
                }

                Compra CompraActual = new Compra();
                CompraActual.Detalles = (List<CompraDetalle>)Session["ListaCompraDetalle"];

                CompraActual.Proveedor = new Proveedor();
                CompraActual.Proveedor.IdProveedor = int.Parse(DDLProveedor.SelectedValue);

                CompraActual.Usuario = (Usuario)Session["Usuario"];

                ComprasNegocio negocio = new ComprasNegocio();

                //negocio.GuardarCompra(CompraActual);
                // GUARDA LA COMPRA EN DB MEDIANTE TRANSACCION ACTUALIZANDO TABLAS Compras, CompraDetalle y Productos(actualizando stock)
                negocio.GuardarCompraConSP(CompraActual);

                LblAlertaOK.Text = "La compra se agrego correctamente.";
                PanelAlertaOK.Visible = true;
                DDLProveedor.Enabled = true;

                // RESETO GENERAL
                DDLProducto.SelectedIndex = 0;
                DDLProveedor.SelectedIndex = 0;
                TxtBPrecio.Text = "$";
                txtCantidad.Text = "0";
                txtCantidad.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;

                // RESET SESSION de LISTA:
                Session["ListaCompraDetalle"] = null;

                // VACIAR Y REFRESCAR GRID:
                ActualizarGridDetalle();
                ActualizarTotal();
                CargarOC();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al efectuar compra: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void btnMas_Click(object sender, EventArgs e) //
        {
            OcultarAlertas();

            int cantidad = 0;
            int.TryParse(txtCantidad.Text, out cantidad);
            cantidad++;
            txtCantidad.Text = cantidad.ToString();
        }

        protected void btnMenos_Click(object sender, EventArgs e) //
        {
            int cantidad = 0;
            int.TryParse(txtCantidad.Text, out cantidad);
            if (cantidad > 0)
                cantidad--;
            txtCantidad.Text = cantidad.ToString();
        }

        protected void DDLProveedor_SelectedIndexChanged(object sender, EventArgs e) //
        {
            OcultarAlertas();

            if (DDLProveedor.SelectedIndex != 0)
            {
                if (DDLProducto.SelectedIndex != 0)
                {
                    // SECCION "CANTIDAD" HABILITADA
                    SeccionCantidadActiva(true);

                }
                else
                {
                    SeccionCantidadActiva(false);
                }

            }
        }

        protected void SeccionCantidadActiva(bool activa) //
        {
            if (activa)
            {
                // SECCION "CANTIDAD" HABILITADA
                txtCantidad.Enabled = true;
                btnMas.Enabled = true;
                btnMenos.Enabled = true;

            }
            else
            {
                // SECCION "CANTIDAD" DESHABILITADA
                txtCantidad.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;
            }
        }

        protected void OcultarAlertas()
        {
            // OCULTO ALERTAS PARA QUE NO SE SUPERPONGAN
            PanelAleta.Visible = false;
            PanelAlertaOK.Visible = false;
        }



        /*******Productos*******/
        private void CargarProductos()
        {
            ProductoNegocio negocioProd = new ProductoNegocio();
            GridProductos.DataSource = negocioProd.ListarConSp();
            GridProductos.DataBind();
        }

        protected void GridProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProductos.PageIndex = e.NewPageIndex;
            CargarProductos();
        }

        /*******Proveedores*******/
        private void CargarProveedores()
        {
            ProveedorNegocio negocioProv = new ProveedorNegocio();
            GridProveedores.DataSource = negocioProv.Listar();
            GridProveedores.DataBind();
        }

        protected void GridProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProveedores.PageIndex = e.NewPageIndex;
            CargarProveedores();
        }

        /******OC Pendientes**********/

        private void CargarOC()
        {
            ComprasNegocio negocio = new ComprasNegocio();
            List<Compra> lista = negocio.Listar();

            rptCompras.DataSource = lista;
            rptCompras.DataBind();
        }

        protected void btnDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string idCompra = btn.CommandArgument;
            Response.Redirect("CompraDetalles.aspx?ID=" + idCompra);
        }

        /********Historial de previos*******/

        private void CargarProductosHistorial()
        {
            if (Session["ProductosHistorial"] == null)
            {
                ProductoNegocio negocioProd = new ProductoNegocio();
                var listaProductos = negocioProd.ListarConSp();
                Session["ProductosHistorial"] = listaProductos;
            }

            string selectedValue = DDLHistPrecios.SelectedValue;

            DDLHistPrecios.DataSource = Session["ProductosHistorial"];
            DDLHistPrecios.DataTextField = "Nombre";
            DDLHistPrecios.DataValueField = "IdProducto";
            DDLHistPrecios.DataBind();

            DDLHistPrecios.Items.Insert(0, new ListItem("- Seleccione producto -", "0"));

            if (!string.IsNullOrEmpty(selectedValue) && DDLHistPrecios.Items.FindByValue(selectedValue) != null)
            {
                DDLHistPrecios.SelectedValue = selectedValue;
            }
        }

        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                pnlAlerta.Visible = false;
                pnlHistorial.Visible = false;
                rptHistorial.DataSource = null;
                rptHistorial.DataBind();
                /*
                if (DDLHistPrecios.SelectedIndex <= 0)
                {
                    lblAlerta.Text = "Por favor, seleccione un producto para ver el historial de precios.";
                    pnlAlerta.Visible = true;
                   
                }*/

                int idProducto = Convert.ToInt32(DDLHistPrecios.SelectedValue);

                ComprasNegocio negocio = new ComprasNegocio();
                var historial = negocio.HistorialPreciosPorProducto(idProducto);

                if (historial != null)
                {
                    LblItem.Text = DDLHistPrecios.SelectedItem.Text;
                    pnlAlerta.Visible = false;
                    pnlHistorial.Visible = true;
                    rptHistorial.DataSource = historial;
                    rptHistorial.DataBind();
                }
                else if (DDLHistPrecios.SelectedIndex <= 0)
                {
                    lblAlerta.Text = "Por favor, seleccione un producto para ver el historial de precios.";
                    pnlAlerta.Visible = true;
                    pnlHistorial.Visible = false;
                }
                else
                {
                    lblAlerta.Text = "No se encontró historial de precios para el producto seleccionado.";
                    pnlAlerta.Visible = true;
                    pnlHistorial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblAlerta.Text = "Ocurrió un error al obtener el historial de precios: " + ex.Message;
                pnlAlerta.Visible = true;
            }
            finally
            {
                upSecciones.Update();
            }
        }

        /********Stock crítico*********/

        private void ActualizarNotificacionStock()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productosCriticos = negocio.ListarStockCritico();

            int cantidad = productosCriticos.Count;
            LblNotif.Text = cantidad.ToString();
            LblNotif.Visible = cantidad > 0;
            pnlNotificacion.Visible = cantidad > 0;
        }

        private void CargarProductosCriticos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productosCriticos = negocio.ListarStockCritico();
            int cantidad = productosCriticos.Count;
            pnlNotificacion.Visible = cantidad > 0;

            if (cantidad > 0)
            {
                GVStockCritico.DataSource = productosCriticos;
                GVStockCritico.DataBind();
                GVStockCritico.Visible = true;
                pnlSinStockCritico.Visible = false;
            }
            else
            {
                GVStockCritico.Visible = false;
                pnlSinStockCritico.Visible = true;
            }
        }
        protected void GVStockCritico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProductos.PageIndex = e.NewPageIndex;
            CargarProductosCriticos();
        }

        protected void lnkNotificaciones_Click(object sender, EventArgs e)
        {
            hfSeccionActiva.Value = "StockCritico";
            MostrarSeccionActiva();
        }



    }
}

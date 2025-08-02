using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WebForms
{
    public partial class Vendedores : System.Web.UI.Page
    {
        List<VentaDetalle> listaVentaDetalle = new List<VentaDetalle>();//
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Seguridad.sesionActiva((Usuario)Session["Usuario"]))
            {
                Session.Add("Error", "Debes estar logueado");
                Response.Redirect("Error.aspx", false);
                return;
            }

            Session["PaginaAnterior"] = Request.UrlReferrer?.AbsoluteUri;

            if (!IsPostBack)
            {
                cargarDropdowns();
            }

            Usuario usuario = (Usuario)Session["Usuario"];

            lblNombreUsuario.Text = usuario.Nombre + " " + usuario.Apellido;
            VentaNegocio negocio = new VentaNegocio();
            lblNumFactura.Text = negocio.obtenerNumProxVenta().ToString("D8");
            lblFechaVenta.Text = DateTime.Now.ToString();

        }
        public void cargarDropdowns()
        {
            try
            {
                ProductoNegocio negocioProducto = new ProductoNegocio();
                List<Producto> listaProductos = negocioProducto.ListarProductosFiltro();
                DDLProductos.DataSource = listaProductos;
                DDLProductos.DataTextField = "Nombre";
                DDLProductos.DataValueField = "CodigoArticulo";
                DDLProductos.DataBind();
                DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));

                TipoProductoNegocio negocioTipo = new TipoProductoNegocio();
                List<TipoProducto> listaTipoProductos = negocioTipo.ListarTipoProductosFiltro();
                DDLTipoProductos.DataSource = listaTipoProductos;
                DDLTipoProductos.DataTextField = "Nombre";
                DDLTipoProductos.DataValueField = "IdTipoProducto";
                DDLTipoProductos.DataBind();
                DDLTipoProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Tipo --", "0"));

                CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                List<Categoria> listaCategorias = negocioCategoria.ListarCategoriasfiltro();
                DDLCategorias.DataSource = listaCategorias;
                DDLCategorias.DataTextField = "Nombre";
                DDLCategorias.DataValueField = "IdCategoria";
                DDLCategorias.DataBind();
                DDLCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Categoria --", "0"));

                MarcaNegocio negocioMarca = new MarcaNegocio();
                List<Marca> listaMarcas = negocioMarca.listarMarcasFiltro();
                DDLMarcas.DataSource = listaMarcas;
                DDLMarcas.DataTextField = "Nombre";
                DDLMarcas.DataValueField = "IdMarca";
                DDLMarcas.DataBind();
                DDLMarcas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Marca --", "0"));

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar los desplegables: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void DDLMarcas_SelectedIndexChanged(object sender, EventArgs e) // OK
        {
            int idMarca = int.Parse(DDLMarcas.SelectedValue);
            int idCategoria = int.Parse(DDLCategorias.SelectedValue);
            int idTipoProducto = int.Parse(DDLTipoProductos.SelectedValue);

            Filtrar(idMarca, idCategoria, idTipoProducto);

        }

        protected void DDLCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMarca = int.Parse(DDLMarcas.SelectedValue);
            int idCategoria = int.Parse(DDLCategorias.SelectedValue);
            int idTipoProducto = int.Parse(DDLTipoProductos.SelectedValue);

            Filtrar(idMarca, idCategoria, idTipoProducto);
        }

        protected void DDLTipoProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMarca = int.Parse(DDLMarcas.SelectedValue);
            int idCategoria = int.Parse(DDLCategorias.SelectedValue);
            int idTipoProducto = int.Parse(DDLTipoProductos.SelectedValue);

            Filtrar(idMarca, idCategoria, idTipoProducto);
        }

        public void Filtrar(int idMarca, int idCategoria, int idTipoProducto)
        {
            try
            {
                if (idMarca == 0 && idCategoria == 0 && idTipoProducto == 0)
                {
                    // CARGAR DDL MARCAS
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarcas = negocioMarca.listarMarcasFiltro();
                    DDLMarcas.DataSource = listaMarcas;
                    DDLMarcas.DataTextField = "Nombre";
                    DDLMarcas.DataValueField = "IdMarca";
                    DDLMarcas.DataBind();
                    DDLMarcas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Marca --", "0"));

                    // CARGAR DDL CATEGORIA
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategorias = negocioCategoria.ListarCategoriasfiltro();
                    DDLCategorias.DataSource = listaCategorias;
                    DDLCategorias.DataTextField = "Nombre";
                    DDLCategorias.DataValueField = "IdCategoria";
                    DDLCategorias.DataBind();
                    DDLCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Categoria --", "0"));

                    // CARGAR DDL TIPOPRODUCTO
                    TipoProductoNegocio negocioTipo = new TipoProductoNegocio();
                    List<TipoProducto> listaTipoProductos = negocioTipo.ListarTipoProductosFiltro();
                    DDLTipoProductos.DataSource = listaTipoProductos;
                    DDLTipoProductos.DataTextField = "Nombre";
                    DDLTipoProductos.DataValueField = "IdTipoProducto";
                    DDLTipoProductos.DataBind();
                    DDLTipoProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Tipo --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarConSp();
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca != 0 && idCategoria == 0 && idTipoProducto == 0)
                {
                    // CARGAR DDL CATEGORIAS
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategorias = negocioCategoria.ListarCategoriasPorMarca(idMarca);
                    DDLCategorias.DataSource = listaCategorias;
                    DDLCategorias.DataTextField = "Nombre";
                    DDLCategorias.DataValueField = "IdCategoria";
                    DDLCategorias.DataBind();
                    DDLCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Categoria --", "0"));

                    // CARGAR DDL TIPOPRODUCTO
                    TipoProductoNegocio negocioTipo = new TipoProductoNegocio();
                    List<TipoProducto> listaTipoProductos = negocioTipo.ListarTiposPorMarca(idMarca);
                    DDLTipoProductos.DataSource = listaTipoProductos;
                    DDLTipoProductos.DataTextField = "Nombre";
                    DDLTipoProductos.DataValueField = "IdTipoProducto";
                    DDLTipoProductos.DataBind();
                    DDLTipoProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Tipo --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorMarca(idMarca);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca == 0 && idCategoria != 0 && idTipoProducto == 0)
                {
                    // CARGAR DDL TIPOPRODUCTO
                    TipoProductoNegocio negocioTipo = new TipoProductoNegocio();
                    List<TipoProducto> listaTipoProductos = negocioTipo.ListarTiposPorCategoria(idCategoria);
                    DDLTipoProductos.DataSource = listaTipoProductos;
                    DDLTipoProductos.DataTextField = "Nombre";
                    DDLTipoProductos.DataValueField = "IdTipoProducto";
                    DDLTipoProductos.DataBind();
                    DDLTipoProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Tipo --", "0"));

                    // CARGAR DDL MARCAS
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarcas = negocioMarca.ListarMarcasPorCategoria(idCategoria);
                    DDLMarcas.DataSource = listaMarcas;
                    DDLMarcas.DataTextField = "Nombre";
                    DDLMarcas.DataValueField = "IdMarca";
                    DDLMarcas.DataBind();
                    DDLMarcas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Marca --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorCategoria(idCategoria);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca == 0 && idCategoria == 0 && idTipoProducto != 0)
                {
                    // CARGAR DDL CATEGORIAS
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategorias = negocioCategoria.ListarCategoriasPorTipo(idTipoProducto);
                    DDLCategorias.DataSource = listaCategorias;
                    DDLCategorias.DataTextField = "Nombre";
                    DDLCategorias.DataValueField = "IdCategoria";
                    DDLCategorias.DataBind();
                    DDLCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Categoria --", "0"));

                    // CARGAR DDL MARCAS
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarcas = negocioMarca.ListarMarcasPorTipo(idTipoProducto);
                    DDLMarcas.DataSource = listaMarcas;
                    DDLMarcas.DataTextField = "Nombre";
                    DDLMarcas.DataValueField = "IdMarca";
                    DDLMarcas.DataBind();
                    DDLMarcas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Marca --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorTipoProducto(idTipoProducto);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca != 0 && idCategoria != 0 && idTipoProducto == 0)
                {
                    // CARGAR DDL TIPOPRODUCTO
                    TipoProductoNegocio negocioTipo = new TipoProductoNegocio();
                    List<TipoProducto> listaTipoProductos = negocioTipo.ListarTiposPorMarcaCategoria(idMarca, idCategoria);
                    DDLTipoProductos.DataSource = listaTipoProductos;
                    DDLTipoProductos.DataTextField = "Nombre";
                    DDLTipoProductos.DataValueField = "IdTipoProducto";
                    DDLTipoProductos.DataBind();
                    DDLTipoProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Tipo --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorMarcaYCategoria(idMarca, idCategoria);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca != 0 && idCategoria == 0 && idTipoProducto != 0)
                {
                    // CARGAR DDL MARCAS
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCategoria.ListarCategoriasPorMarcaYTipo(idMarca, idTipoProducto);
                    DDLCategorias.DataSource = listaCategoria;
                    DDLCategorias.DataTextField = "Nombre";
                    DDLCategorias.DataValueField = "IdCategoria";
                    DDLCategorias.DataBind();
                    DDLCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Categoria --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorMarcaYTipoProducto(idMarca, idTipoProducto);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca == 0 && idCategoria != 0 && idTipoProducto != 0)
                {
                    // CARGAR DDL MARCAS
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarcas = negocioMarca.ListarMarcasPorCategoriaYTipo(idCategoria, idTipoProducto);
                    DDLMarcas.DataSource = listaMarcas;
                    DDLMarcas.DataTextField = "Nombre";
                    DDLMarcas.DataValueField = "IdMarca";
                    DDLMarcas.DataBind();
                    DDLMarcas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Marca --", "0"));

                    // CARGAR DDL PRODUCTO
                    ProductoNegocio negocioProducto = new ProductoNegocio();
                    List<Producto> listaProductos = negocioProducto.ListarPorCategoriaYTipoProducto(idCategoria, idTipoProducto);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }
                else if (idMarca != 0 && idCategoria != 0 && idTipoProducto != 0)
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    List<Producto> listaProductos = negocio.ListarPorMarcaCategoriaTipo(idMarca, idCategoria, idTipoProducto);
                    DDLProductos.DataSource = listaProductos;
                    DDLProductos.DataTextField = "Nombre";
                    DDLProductos.DataValueField = "CodigoArticulo";
                    DDLProductos.DataBind();
                    DDLProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar Producto --", "0"));
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema con los filtros: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }

        }


        // BOTON BUSCAR CLIENTE DNI
        protected void btnimg_Click(object sender, ImageClickEventArgs e)
        {
            string dniIngresado = txtDNICliente.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(dniIngresado))
                {
                    // DNI VACIO
                    string script = "alert('Debe ingresar un DNI antes de continuar.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaVacio", script, true);
                    return;
                }

                if (dniIngresado.Length != 8)
                {
                    // DNI < 8 DIGITOS
                    string script = "alert('Debe ingresar un DNI de 8 dígitos.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaLongitud", script, true);
                    return;
                }

                // DNI CON 8 DIGITOS
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.BuscarClienteDNI(dniIngresado);

                if (cliente != null)
                {
                    // DNI 8 DIGITOS REGISTRADO
                    txtDNICliente.Text = cliente.Dni;
                    txtNombreCliente.Text = cliente.Nombre;
                    txtApellidoCliente.Text = cliente.Apellido;
                    txtTelefonoCliente.Text = cliente.Telefono;
                    txtMailCliente.Text = cliente.Email;
                    txtDireccionCliente.Text = cliente.Direccion;

                    if (Session["VentaActual"] == null)
                        Session["VentaActual"] = new Venta();

                    Venta VentaActual = (Venta)Session["VentaActual"];
                    VentaActual.Cliente = cliente;

                    string script = "alert('Cliente encontrado y datos cargados.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaEncontrado", script, true);
                }
                else
                {
                    // DNI 8 DIGITOS NO REGISTRADO
                    string script = @"
                                    if (confirm('El DNI ingresado no está registrado.\n¿Desea registrar un nuevo cliente?')) {
                                        window.open('/AltaCliente.aspx', 'RegistroCliente', 'width=600,height=900');
                                    }
                                ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "popupRegistro", script, true);
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al buscar cliente: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void GVVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    if (Session["ListaVentaDetalle"] != null)
                    {
                        listaVentaDetalle = (List<VentaDetalle>)Session["ListaVentaDetalle"];

                        if (index >= 0 && index < listaVentaDetalle.Count)
                        {
                            listaVentaDetalle.RemoveAt(index);
                            Session["ListaVentaDetalle"] = listaVentaDetalle;
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
                Session.Add("Error", "Ocurrió un problema al remover producto del detalle: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnMas_Click(object sender, EventArgs e) //
        {
            PanelAleta.Visible = false;

            try
            {
                if (Session["StockDisponibleProducto"] == null)
                    Session["StockDisponibleProducto"] = new int();
                int StockActualProducto = (int)Session["StockDisponibleProducto"];

                if (txtCantidad.Text == "0")
                {
                    // ME GUARDO EL STOCK DISPONIBLE DEL PRODUCTO SELECCIONADO
                    string codigoProducto = DDLProductos.SelectedValue;
                    ProductoNegocio negocio = new ProductoNegocio();
                    StockActualProducto = negocio.StockActualProducto(codigoProducto);
                    Session["StockDisponibleProducto"] = StockActualProducto;
                }
                int cantidad = 0;

                if (int.TryParse(txtCantidad.Text, out cantidad))
                {
                    if (cantidad < StockActualProducto)
                    {
                        cantidad++;
                        txtCantidad.Text = cantidad.ToString();
                    }
                    else
                    {
                        // msj MAXIMO STOCK DISPONIBLE
                        lblAlerta2.Text = "La cantidad esta sujeta a stock disponible";
                        PanelAleta.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al aumentar cantidad: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnMenos_Click(object sender, EventArgs e) //
        {
            PanelAleta.Visible = false;

            try
            {
                int cantidad = 0;
                int.TryParse(txtCantidad.Text, out cantidad);
                if (cantidad > 0)
                    cantidad--;
                txtCantidad.Text = cantidad.ToString();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al disminuir cantidad: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void DDLProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarAlertas();
            txtCantidad.Text = "0";

            try
            {
                // SI SE SELECCIONO UN PRODUCTO
                if (DDLProductos.SelectedIndex != 0)
                {
                    // HABILITO CONTROLES CANTIDAD
                    btnMas.Enabled = true;
                    btnMenos.Enabled = true;
                    txtCantidad.Enabled = true;

                    // BUSCO PRODUCTO EN LISTA PARA GUARDARME SUS VALORES
                    ProductoNegocio negocioProd = new ProductoNegocio();
                    string codigoSeleccionado = DDLProductos.SelectedValue;
                    Producto producto = negocioProd.ListarConSp()
                                            .FirstOrDefault(p => p.CodigoArticulo == codigoSeleccionado);

                    if (producto != null)
                    {
                        // RECUPERO LISTA ACTUAL DE COMPRADETALLE
                        if (Session["ListaVentaDetalle"] == null)
                            Session["ListaVentaDetalle"] = new List<VentaDetalle>();

                        List<VentaDetalle> listaVentaDetalle = (List<VentaDetalle>)Session["ListaVentaDetalle"];

                        // BUSCO SI EL PRODUCTO SELECCIONADO YA SE ENCUENTRA EN LA LISTA ACTUAL DE COMPRADETALLE
                        VentaDetalle detalleExistente = listaVentaDetalle.FirstOrDefault(d => d.Producto.CodigoArticulo == codigoSeleccionado);

                        if (detalleExistente != null)
                        {
                            // SI EL PRODUCTO YA ESTABA EN LA LISTA, PRECARGO LA CANTIDAD QUE LE ASIGNARON LA ANTERIOR VEZ
                            txtCantidad.Text = detalleExistente.Cantidad.ToString();
                        }
                    }
                    //// RESET Valores
                    //txtCantidad.Text = "0";
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al buscar producto en detalle: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void BtnPlus_Click(object sender, ImageClickEventArgs e)
        {
            OcultarAlertas();

            try
            {
                // GUARDO PRODUCTO A AGREGAR PARA LUEGO CARGARLO EN SU CORRESPONDIENTE VENTADETALLE
                ProductoNegocio negocioProd = new ProductoNegocio();
                string codigoSeleccionado = DDLProductos.SelectedValue;
                Producto producto = negocioProd.ListarConSp()
                                      .FirstOrDefault(p => p.CodigoArticulo == codigoSeleccionado);

                if (producto == null)
                {
                    lblAlerta2.Text = "Debe Seleccionar un producto";
                    PanelAleta.Visible = true;
                    return;
                }

                // RECUPERO LISTA ACTUAL DE COMPRADETALLE
                if (Session["ListaVentaDetalle"] == null)
                    Session["ListaVentaDetalle"] = new List<VentaDetalle>();

                List<VentaDetalle> listaVentaDetalle = (List<VentaDetalle>)Session["ListaVentaDetalle"];

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
                VentaDetalle detalleExistente = listaVentaDetalle.FirstOrDefault(d => d.Producto.CodigoArticulo == codigoSeleccionado);

                if (detalleExistente != null)
                {
                    // SI YA ESTABA AGREGADO LE ACTUALIZO SU CANTIDAD CON LA CANTIDAD NUEVA INGRESADA
                    detalleExistente.Cantidad = cantidad;
                    detalleExistente.Subtotal = detalleExistente.PrecioVenta * cantidad;
                    LblAlertaOK.Text = "Cantidad actualizada correctamente.";
                    PanelAlertaOK.Visible = true;
                }
                else
                {
                    // SI NO ESTABA AGREGADO LO AÑADO A LA LISTA
                    VentaDetalle nuevo = new VentaDetalle();
                    nuevo.Producto = new Producto();
                    nuevo.Producto = producto;
                    nuevo.Producto.IdProducto = negocioProd.GetIdproducto(producto.CodigoArticulo);
                    nuevo.Cantidad = cantidad;
                    nuevo.PrecioCompra = producto.PrecioCompra;
                    nuevo.PrecioVenta = producto.PrecioCompra * ((producto.PorcentajeGanancia / 100) + 1);
                    nuevo.Subtotal = cantidad * nuevo.PrecioVenta;

                    // ORDEN DE LISTA EN GRID OPCIONAL
                    //listaVentaDetalle.Add(nuevo); // ULTIMO AGREGADO AL FINAL DE LISTA
                    listaVentaDetalle.Insert(0, nuevo); // EL ULTIMO AGREGADO AL INICIO DE LA LISTA

                    LblAlertaOK.Text = "Producto agregado correctamente.";
                    PanelAlertaOK.Visible = true;
                }

                // RESGUARDO LA LISTA EN SESSION PARA ACTUALIZARLE CON EL PROXIMO PRODUCTO A AGREGAR
                Session["ListaVentaDetalle"] = listaVentaDetalle;

                // RESET Valor y controles
                txtCantidad.Text = "0";
                txtCantidad.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;

                cargarDropdowns();
                DDLProductos.SelectedIndex = 0;
                DDLCategorias.SelectedIndex = 0;
                DDLMarcas.SelectedIndex = 0;
                DDLTipoProductos.SelectedIndex = 0;

                //// ACTUALIZO
                ActualizarGridDetalle();
                ActualizarTotal();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar el producto a la lista: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void ActualizarGridDetalle()
        {
            if (Session["ListaVentaDetalle"] == null)  //
                Session["ListaVentaDetalle"] = new List<VentaDetalle>(); //
            listaVentaDetalle = (List<VentaDetalle>)Session["ListaVentaDetalle"]; //
            GVVenta.DataSource = listaVentaDetalle; //
            GVVenta.DataBind();
        }

        private void ActualizarTotal()
        {
            decimal total = listaVentaDetalle.Sum(d => d.Subtotal);
            lblTotal.Text = total.ToString("C");
        }

        protected void btnFacturar_Click(object sender, EventArgs e)
        {

            OcultarAlertas();

            try
            {
                if (Session["VentaActual"] == null)
                    Session["VentaActual"] = new Venta();
                Venta VentaActual = (Venta)Session["VentaActual"];

                // COMPRUEBO SI LA LISTA DEL GRID ACTUAL ES NULL O ESTA VACIA
                VentaActual.Detalles = (List<VentaDetalle>)Session["ListaVentaDetalle"];
                if (VentaActual.Detalles == null)
                {
                    lblAlerta2.Text = "Debe agregar al menos un producto para generar factura de la venta.";
                    PanelAleta.Visible = true;
                    return;
                }

                // COMPRUEBO SI CLIENTE ESTA CARGADO
                if (VentaActual.Cliente == null)
                {
                    lblAlerta2.Text = "Campos de cliente vacios. Ingrese su DNI y presione el boton de busqueda.";
                    PanelAleta.Visible = true;
                    return;
                }

                // VINCULO USUARIO ACTIVO A LA VENTA ACTUAL
                VentaActual.Usuario = (Usuario)Session["Usuario"];
                VentaNegocio negocio = new VentaNegocio();

                // GUARDA LA COMPRA EN DB MEDIANTE TRANSACCION ACTUALIZANDO TABLAS Compras, CompraDetalle y Productos(actualizando stock)
                negocio.GuardarVentaConSP(VentaActual);

                LblAlertaOK.Text = "La factura se genero correctamente.";
                PanelAlertaOK.Visible = true;

                // RESETO GENERAL
                txtDNICliente.Text = "";
                txtNombreCliente.Text = "";
                txtApellidoCliente.Text = "";
                txtTelefonoCliente.Text = "";
                txtMailCliente.Text = "";
                txtDireccionCliente.Text = "";

                // DDLs A 0
                DDLProductos.SelectedIndex = 0;
                DDLCategorias.SelectedIndex = 0;
                DDLMarcas.SelectedIndex = 0;
                DDLTipoProductos.SelectedIndex = 0;

                lblTotal.Text = "$ 0,00";
                txtCantidad.Text = "0";
                txtCantidad.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;

                // ACTUALIZO NUM FACTURA PARA LA PROXIMA VENTA
                lblNumFactura.Text = negocio.obtenerNumProxVenta().ToString("D8");

                // NULO SESSIONS Menos de USUARIO:
                Session["ListaVentaDetalle"] = null;
                Session["VentaActual"] = null;
                Session["StockDisponibleProducto"] = null;

                // VACIAR Y REFRESCAR GRID:
                ActualizarGridDetalle();
                ActualizarTotal();

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al facturar: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lkbVer_Click(object sender, EventArgs e)
        {
            VentaNegocio negocio = new VentaNegocio();
            int idVentaFactura = negocio.obtenerNumProxVenta() - 1;
            string script = "window.open('/Factura.aspx?ID=" + idVentaFactura + "', '_blank', 'width=1000,height=900');";
            ScriptManager.RegisterStartupScript(this, GetType(), "imprimirFactura", script, true);
        }

        protected void lkbImprimir_Click(object sender, EventArgs e)
        {
            VentaNegocio negocio = new VentaNegocio();
            int idVentaFactura = negocio.obtenerNumProxVenta() - 1;
            string script = "window.open('/Factura.aspx?ID=" + idVentaFactura + "&imprimir=true', '_blank', 'width=1000,height=900');";
            ScriptManager.RegisterStartupScript(this, GetType(), "imprimirFactura", script, true);

        }

        protected void OcultarAlertas()
        {
            PanelAleta.Visible = false;
            PanelAlertaOK.Visible = false;
        }

        protected void lkbEnviar_Click(object sender, EventArgs e)
        {
            OcultarAlertas();

            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                int idUltimaVenta = ventaNegocio.obtenerNumProxVenta() - 1;
                Venta venta = ventaNegocio.BuscarVenta(idUltimaVenta);

                if (venta == null)
                {
                    lblAlerta2.Text = "No existe la factura que intenta enviar.";
                    PanelAleta.Visible = true;
                    return;
                }

                string cuerpoHTML = $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .header {{ color: #3498db; font-size: 18px; }}
                    .footer {{ margin-top: 20px; font-style: italic; color: #7f8c8d; }}
                    .saludo {{ margin-top: 15px; }}
                </style>
            </head>
            <body>
                <div class='header'>Factura Tecno Hogar Nº {venta.IdVenta}</div>
                <p class='saludo'>Estimado cliente,</p>
                <p>Adjunto encontrará la factura de su compra realizada el {venta.Fecha?.ToShortDateString() ?? "fecha no disponible"}.</p>
                <p>Gracias por elegirnos.</p>
                <p>Atentamente,</p>
                <p><strong>Equipo de ventas</strong></p>
                <p><strong>Tecno Hogar</strong></p>
                <div class='footer'>
                    <p>Este es un correo automático, por favor no responda.</p>
                </div>
            </body>
            </html>";


                EmailService emailService = new EmailService();
                emailService.ArmarCorreo(venta.Cliente.Email,$"Factura Tecno Hogar Nº {venta.IdVenta}",cuerpoHTML, true);

                // Generar PDF
                byte[] pdfBytes = GenerarPdfFactura(venta);

                // Adjuntar PDF
                System.Net.Mail.Attachment adj = new System.Net.Mail.Attachment(new MemoryStream(pdfBytes), $"Factura_{venta.IdVenta}.pdf", "application/pdf");
                emailService.AgregarAdjunto(adj);

                emailService.EnviarEmail();

                LblAlertaOK.Text = "La factura fue enviada correctamente.";
                PanelAlertaOK.Visible = true;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }

        public byte[] GenerarPdfFactura(Venta venta)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Título
                Paragraph titulo = new Paragraph("Factura Tecno Hogar", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph($"Factura Nº: {venta.IdVenta}"));
                //doc.Add(new Paragraph($"Fecha: {venta.Fecha.ToShortDateString()}"));
                doc.Add(new Paragraph($"Fecha: {venta.Fecha?.ToShortDateString() ?? "Fecha no disponible"}"));
                doc.Add(new Paragraph($"Cliente: {venta.Cliente.Nombre} {venta.Cliente.Apellido}"));
                doc.Add(new Paragraph($"DNI: {venta.Cliente.Dni}"));
                doc.Add(new Paragraph($"Dirección: {venta.Cliente.Direccion}"));
                doc.Add(new Paragraph(" "));

                // Tabla con detalle
                PdfPTable tabla = new PdfPTable(4) { WidthPercentage = 100 };
                tabla.SetWidths(new float[] { 40, 15, 20, 25 });

                tabla.AddCell("Producto");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Precio Unitario");
                tabla.AddCell("Subtotal");

                foreach (var detalle in venta.Detalles)
                {
                    tabla.AddCell(detalle.Producto.Nombre);
                    tabla.AddCell(detalle.Cantidad.ToString());
                    tabla.AddCell(detalle.PrecioVenta.ToString("C"));
                    tabla.AddCell((detalle.Cantidad * detalle.PrecioVenta).ToString("C"));
                }

                doc.Add(tabla);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Total: {venta.Total.ToString("C")}", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));

                doc.Close();

                return ms.ToArray(); // bytes del PDF
            }
        }




    }
}
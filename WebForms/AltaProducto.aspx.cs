using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebForms
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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



            if (!IsPostBack)
            {
                try
                {
                    cargarDropdowns();

                    if (Request.QueryString["Id"] != null)
                    {
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;

                        ProductoNegocio negocio = new ProductoNegocio();
                        int id = int.Parse(Request.QueryString["Id"]);
                        Producto producto = negocio.buscarProducto(id);

                        txtNombre.Text = producto.Nombre;
                        txtCodProducto.Text = producto.CodigoArticulo;
                        txtDescripcion.Text = producto.Descripcion;
                        TxtPrecio.Text = producto.PrecioCompra.ToString();
                        txtPorcentaje.Text = producto.PorcentajeGanancia.ToString();
                        txtImagenUrl.Text = producto.ImagenUrl;
                        txtStockActual.Text = producto.StockActual.ToString();
                        txtStockMinimo.Text = producto.StockMinimo.ToString();
                        ddlMarca.SelectedValue = producto.Marca.IdMarca.ToString();
                        ddlCategoria.SelectedValue = producto.TipoProducto.categoria.IdCategoria.ToString();

                        // CARGO DDL DE TipoProducto SEGUN CATEGORIA SELECCIONADA
                        cargarTiposPorCategoria(producto.TipoProducto.categoria.IdCategoria);
                        ddlTipoProducto.SelectedValue = producto.TipoProducto.IdTipoProducto.ToString();
                    }
                    else
                    {
                        btnAgregar.Visible = true;
                        btnModificar.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    Session.Add("Error", "Ocurrió un problema al cargar el producto: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }


            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidarCampos())
                {
                    Producto producto = new Producto();

                    producto.CodigoArticulo = txtCodProducto.Text;
                    producto.Nombre = txtNombre.Text;
                    producto.Descripcion = txtDescripcion.Text;
                    producto.PrecioCompra = Convert.ToDecimal(TxtPrecio.Text);
                    producto.PorcentajeGanancia = Convert.ToDecimal(txtPorcentaje.Text);
                    producto.StockActual = Convert.ToInt32(txtStockActual.Text);
                    producto.StockMinimo = Convert.ToInt32(txtStockMinimo.Text);
                    producto.ImagenUrl = txtImagenUrl.Text;
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = ddlMarca.SelectedIndex;

                    producto.TipoProducto = new TipoProducto();
                    //producto.TipoProducto.IdTipoProducto = ddlTipoProducto.SelectedIndex;
                    producto.TipoProducto.IdTipoProducto = int.Parse(ddlTipoProducto.SelectedValue);


                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.AgregarProducto(producto);

                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Producto agregado exitosamente.";

                    /* BOTONES */
                    // Deshabilitarlos
                    //btnAgregar.Enabled = false;
                    //btnCancelar.Enabled = false;

                    // RESETEAR CAMPOS y DDLs
                    ResetCampos();

                    // O Sacarlos de la vista
                    btnAgregar.Visible = false;
                    btnCancelar.Visible = false;


                    /* DELAY POST AGREGAR PRODUCTO Y POSTERIOR REDIRECCIONAMIENTO */
                    string script = "<script>setTimeout(function() { window.location.href = 'ListaProductos.aspx'; }, 1000);</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);

                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = int.Parse(Request.QueryString["Id"]);
                    producto.CodigoArticulo = txtCodProducto.Text;
                    producto.Nombre = txtNombre.Text;
                    producto.Descripcion = txtDescripcion.Text;
                    producto.PrecioCompra = Convert.ToDecimal(TxtPrecio.Text);
                    producto.PorcentajeGanancia = Convert.ToDecimal(txtPorcentaje.Text);
                    producto.StockActual = Convert.ToInt32(txtStockActual.Text);
                    producto.StockMinimo = Convert.ToInt32(txtStockMinimo.Text);
                    producto.ImagenUrl = txtImagenUrl.Text;
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = int.Parse(ddlMarca.SelectedValue);
                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = int.Parse(ddlTipoProducto.SelectedValue);

                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.ModificarProducto(producto); // Asegurate de tener este método en la capa negocio

                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Producto modificado exitosamente.";

                    ResetCampos();

                    btnAgregar.Visible = false;
                    btnModificar.Visible = false;
                    btnCancelar.Visible = false;

                    string script = "<script>setTimeout(function() { window.location.href = 'ListaProductos.aspx'; }, 1000);</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProductos.aspx", false);
        }

        private void cargarDropdowns()
        {
            try
            {
                // DDL MARCA
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                ddlMarca.DataSource = marcaNegocio.listarMarcas();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "IdMarca";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("-- Seleccionar Marca --", "0"));

                // DDL CATEGORIA
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = categoriaNegocio.ListarCategorias();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "IdCategoria";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Seleccionar Categoría --", "0"));

                // DDL TIPOPRODUCTO (VACIO PARCIALMENTE HASTA QUE UNA CATEGORIA SEA SELECCIONADA)
                ddlTipoProducto.Items.Clear();
                ddlTipoProducto.Items.Insert(0, new ListItem("-- Seleccione una Categoría primero --", "0"));
                ddlTipoProducto.Enabled = false;

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar los desplegables: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool ValidarCampos()
        {
            try
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;

                string codigo = txtCodProducto.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string precio = TxtPrecio.Text.Trim();
                string ganancia = txtPorcentaje.Text.Trim();
                string urlImagen = txtImagenUrl.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                string stockActual = txtStockActual.Text.Trim();
                string stockMinimo = txtStockMinimo.Text.Trim();
                string marca = ddlMarca.SelectedItem?.ToString();
                string tipoProducto = ddlTipoProducto.SelectedItem?.ToString();

                // Validando vacios
                if (string.IsNullOrWhiteSpace(codigo) ||
                    string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(descripcion) ||
                    string.IsNullOrWhiteSpace(precio) ||
                    string.IsNullOrWhiteSpace(stockActual) ||
                    string.IsNullOrWhiteSpace(stockMinimo) ||
                    string.IsNullOrWhiteSpace(marca) ||
                    string.IsNullOrWhiteSpace(tipoProducto))
                {
                    lblMensaje.Text = "Todos los campos deben estar completos.";
                    return false;
                }

                /* Valida Codigo */
                ProductoNegocio negocio = new ProductoNegocio();
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    lblMensaje.Text = "El código de producto no puede estar vacío.";
                    return false;
                }
                else if (btnAgregar.Visible) // Producto Nuevo
                {
                    if (negocio.CodExistente(codigo))
                    {
                        lblMensaje.Text = "El código de producto ya existe. Por favor ingrese otro.";
                        return false;
                    }

                }
                else // Producto modificar
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    Producto producto = negocio.buscarProducto(id);

                    if (producto.CodigoArticulo != codigo && negocio.CodExistente(codigo))
                    {
                        lblMensaje.Text = "El código de producto ya existe. Por favor ingrese otro.";
                        return false;
                    }

                }
                if (!Regex.IsMatch(codigo, @"^[a-zA-Z0-9\-]+$"))
                {
                    lblMensaje.Text = "El código de producto solo puede contener letras, números y guiones.";
                    return false;
                }

                /* Valida Url Imagen */
                if (string.IsNullOrEmpty(urlImagen))
                {
                    lblMensaje.Text = "Debe ingresar una URL de imagen.";
                    return false;
                }
                if (!(urlImagen.StartsWith("http://") || urlImagen.StartsWith("https://")))
                {
                    lblMensaje.Text = "La URL debe comenzar con http:// o https://";
                    return false;
                }

                /* Valida Precio */
                if (!decimal.TryParse(precio, out decimal precioValor) || precioValor <= 0)
                {
                    lblMensaje.Text = "El precio debe ser un número positivo.";
                    return false;
                }

                /* Valida Ganancia */
                if (!decimal.TryParse(ganancia, out decimal gananciavalor) || gananciavalor <= 0)
                {
                    lblMensaje.Text = "la ganancia % debe ser un número positivo.";
                    return false;
                }

                /* Valida Stocks */
                // Stock minimo
                if (!int.TryParse(stockMinimo, out int stockMin) || stockMin < 0)
                {
                    lblMensaje.Text = "El stock mínimo debe ser un número entero positivo.";
                    return false;
                }
                // Stock actual
               /* if (!int.TryParse(stockActual, out int stockValor) || stockValor < stockMin)
                {
                    lblMensaje.Text = "El stock actual debe ser un número mayor o igual que el stock mínimo.";
                    return false;
                }*/

                /* Valida Marca */
                if (ddlMarca.SelectedIndex == 0)
                {
                    lblMensaje.Text = "Debe seleccionar una Marca.";
                    return false;
                }

                /* Valida TipoProducto */
                if (ddlTipoProducto.SelectedIndex == 0)
                {
                    lblMensaje.Text = "Debe seleccionar un Tipo de Producto.";
                    return false;
                }

                return true; // SE VALIDO TODO

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al al validar campos de producto: " + ex.Message);
                Response.Redirect("Error.aspx", false);
                return false;
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoriaSeleccionada;

            try
            {
                if (int.TryParse(ddlCategoria.SelectedValue, out idCategoriaSeleccionada) && idCategoriaSeleccionada > 0)
                {
                    TipoProductoNegocio tipoNegocio = new TipoProductoNegocio();
                    List<TipoProducto> listaTipos = tipoNegocio.ListarPorCategoria(idCategoriaSeleccionada); // Usá el método que ya tenés

                    ddlTipoProducto.DataSource = listaTipos;
                    ddlTipoProducto.DataTextField = "Nombre";
                    ddlTipoProducto.DataValueField = "IdTipoProducto";
                    ddlTipoProducto.DataBind();

                    ddlTipoProducto.Items.Insert(0, new ListItem("-- Seleccionar Tipo --", "0"));
                    ddlTipoProducto.Enabled = true;
                }
                else
                {
                    ddlTipoProducto.Items.Clear();
                    ddlTipoProducto.Items.Insert(0, new ListItem("-- Seleccione una Categoría primero --", "0"));
                    ddlTipoProducto.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar desplegable Tipo Producto: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void cargarTiposPorCategoria(int idCategoria)
        {
            try
            {
                TipoProductoNegocio tipoNegocio = new TipoProductoNegocio();
                List<TipoProducto> listaTipos = tipoNegocio.ListarPorCategoria(idCategoria);

                ddlTipoProducto.DataSource = listaTipos;
                ddlTipoProducto.DataTextField = "Nombre";
                ddlTipoProducto.DataValueField = "IdTipoProducto";
                ddlTipoProducto.DataBind();

                ddlTipoProducto.Items.Insert(0, new ListItem("-- Seleccionar Tipo --", "0"));
                ddlTipoProducto.Enabled = true;

            }
            catch (Exception ex)
            {
                Session.Add("Error", "Ocurrió un problema al cargar desplegable Tipo Producto: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void ResetCampos()
        {
            // RESETEAR CAMPOS y DDLs
            txtNombre.Text = string.Empty;
            txtCodProducto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            TxtPrecio.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
            txtImagenUrl.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtStockMinimo.Text = string.Empty;
            ddlTipoProducto.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;

            txtNombre.Enabled = false;
            txtCodProducto.Enabled = false;
            txtDescripcion.Enabled = false;
            TxtPrecio.Enabled = false;
            txtPorcentaje.Enabled = false;
            txtImagenUrl.Enabled = false;
            txtStockActual.Enabled = false;
            txtStockMinimo.Enabled = false;
            ddlTipoProducto.Enabled = false;
            ddlCategoria.Enabled = false;
            ddlMarca.Enabled = false;
        }
    }
}
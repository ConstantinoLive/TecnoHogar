using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class VentaDetalleNegocio
    {
        public List<VentaDetalle> ObtenerDetallesPorVenta(int idVenta) // A detalleCompra.Negocio
        {
            List<VentaDetalle> detalles = new List<VentaDetalle>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
	                                    VD.IdVentaDetalle,
	                                    VD.IdVenta,
	                                    VD.Cantidad,
	                                    VD.PrecioUnit,
	                                    VD.IdProducto,
	                                    P.CodigoArticulo,
	                                    P.Nombre AS NombreProducto,
	                                    P.Descripcion, 
	                                    P.PrecioCompra,
	                                    P.PorcentajeGanancia, 
	                                    P.StockActual, 
	                                    P.StockMinimo, 
	                                    P.ImagenUrl, 
	                                    P.IdMarca,
	                                    M.Nombre AS Marca, 
	                                    TP.IdTipoProducto,
	                                    TP.Nombre AS NombreTP,
	                                    C.IdCategoria,
	                                    C.Nombre AS Categoria
                                    FROM VentaDetalle VD
                                    INNER JOIN Productos P ON VD.IdProducto = P.IdProducto
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE VD.IdVenta = @idVenta;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    VentaDetalle detalle = new VentaDetalle();

                    detalle.IdVentaDetalle = (int)datos.Lector["IdVentaDetalle"];
                    detalle.IdVenta = (int)datos.Lector["IdVenta"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    detalle.PrecioVenta = (decimal)datos.Lector["PrecioUnit"];
                    detalle.Producto = new Producto();
                    detalle.Producto.IdProducto = (int)datos.Lector["IdProducto"];
                    //detalle.Producto.Nombre = datos.Lector["NombreProducto"].ToString();
                    detalle.Producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    detalle.Producto.Nombre = datos.Lector["NombreProducto"].ToString();
                    detalle.Producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    detalle.Producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    detalle.Producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    detalle.Producto.StockActual = (int)datos.Lector["StockActual"];
                    detalle.Producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    detalle.Producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    detalle.Producto.Marca = new Marca();
                    detalle.Producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    detalle.Producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    detalle.Producto.TipoProducto = new TipoProducto();
                    detalle.Producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    detalle.Producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    detalle.Producto.TipoProducto.categoria = new Categoria();
                    detalle.Producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    detalle.Producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return detalles;
        }

        public void GuardarDetalleVenta(List<VentaDetalle> ventaDetalle)
        {
            try
            {
                foreach (var detalle in ventaDetalle)
                {
                    AccesoDatos datos = new AccesoDatos(); // 🔁 NUEVO EN CADA ITERACIÓN

                    string consulta = @"INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnit) 
                                VALUES (@idVenta, @idProducto, @cantidad, @precioUnit);";

                    datos.setearConsulta(consulta);
                    datos.setearParametro("@idVenta", detalle.IdVenta);
                    datos.setearParametro("@idProducto", detalle.Producto.IdProducto);
                    datos.setearParametro("@cantidad", detalle.Cantidad);
                    datos.setearParametro("@precioUnit", detalle.PrecioVenta);

                    datos.ejecutarAccion();  // conexión cerrada automáticamente si usás finally ahí
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public decimal ObtenerMontoTotal(List<VentaDetalle> detalle)
        {
            decimal total = 0;
            foreach (VentaDetalle aux in detalle)
            {
                total += aux.Cantidad * aux.PrecioVenta;
            }


            return total;
        }
    }
}

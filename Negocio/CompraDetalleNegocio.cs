using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CompraDetalleNegocio
    {
        public List<CompraDetalle> ObtenerDetallesPorCompra(int idCompra) // A detalleCompra.Negocio
        {
            List<CompraDetalle> detalles = new List<CompraDetalle>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                        CD.IdCompra,
                                        CD.Cantidad,
                                        CD.PrecioUnit,
                                        CD.IdProducto,
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
                                    FROM CompraDetalle CD
                                    INNER JOIN Productos P ON CD.IdProducto = P.IdProducto
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE CD.IdCompra = @idCompra;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCompra", idCompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CompraDetalle detalle = new CompraDetalle();
                    detalle.IdCompraDetalle = (int)datos.Lector["IdCompra"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)datos.Lector["PrecioUnit"];
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

        public decimal ObtenerMontoTotal(List<CompraDetalle> detalle)
        {
            decimal total = 0;
            foreach (CompraDetalle aux in detalle)
            {
                total += aux.Cantidad * aux.PrecioUnitario;
            }


            return total;
        }

        public void GuardarDetalleCompra(List<CompraDetalle> compraDetalle)
        {
            try
            {
                foreach (var detalle in compraDetalle)
                {
                    AccesoDatos datos = new AccesoDatos(); //NUEVO EN CADA ITERACIÓN

                    string consulta = @"INSERT INTO CompraDetalle (IdCompra, IdProducto, Cantidad, PrecioUnit) 
                                        VALUES (@idCompra, @idProducto, @cantidad, @precioUnit);";

                    datos.setearConsulta(consulta);
                    datos.setearParametro("@idCompra", detalle.IdCompra);
                    datos.setearParametro("@idProducto", detalle.Producto.IdProducto);
                    datos.setearParametro("@cantidad", detalle.Cantidad);
                    datos.setearParametro("@precioUnit", detalle.PrecioUnitario);

                    datos.ejecutarAccion(); // SE CIERRA EN EL Finally 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarDetalleCompraConSP(List<CompraDetalle> compraDetalle)
        {
            try
            {
                foreach (var detalle in compraDetalle)
                {
                    AccesoDatos datos = new AccesoDatos();
                    datos.setearProcedimiento("SP_InsertDetalleYActualizarStock");
                    datos.setearParametro("@idCompra", detalle.IdCompra);
                    datos.setearParametro("@idProducto", detalle.Producto.IdProducto);
                    datos.setearParametro("@cantidad", detalle.Cantidad);
                    datos.setearParametro("@precioUnit", detalle.PrecioUnitario);

                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

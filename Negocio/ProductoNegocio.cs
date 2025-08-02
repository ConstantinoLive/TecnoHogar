using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1
                                    ORDER BY C.Nombre, P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }


        public List<Producto> ListarConSp()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {


                datos.setearProcedimiento("SP_ListarProductos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public Producto buscarProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            Producto producto = null;

            try
            {
                string consulta = @"SELECT 
                                        P.IdProducto,
                                        P.CodigoArticulo, 
                                        P.Nombre, 
                                        P.Descripcion, 
                                        P.PrecioCompra, 
                                        CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                        P.PorcentajeGanancia, 
                                        P.StockActual, 
                                        P.StockMinimo, 
                                        P.ImagenUrl, 
	                                    P.IdMarca,
                                        P.Activo,
                                        M.Nombre AS Marca, 
	                                    TP.IdTipoProducto,
	                                    TP.Nombre AS NombreTP,
	                                    C.IdCategoria,
                                        C.Nombre AS Categoria
                                    FROM Productos P
	                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
	                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
	                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.IdProducto = @idProducto;";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    producto = new Producto();
                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

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

            return producto;
        }

        public void AgregarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"INSERT INTO Productos 
                                        (CodigoArticulo,
                                        Nombre,
                                        Descripcion,
                                        PrecioCompra,
                                        PorcentajeGanancia,
                                        StockActual,
                                        StockMinimo,
                                        ImagenUrl,
                                        IdMarca,
                                        IdTipoProducto)
                                    VALUES
                                        (@codigoArticulo,
                                        @nombre,
                                        @descripcion,
                                        @precioCompra,
                                        @porcentajeGanancia,
                                        @stockActual,
                                        @stockMinimo,
                                        @imagenUrl,
                                        @idMarca,
                                        @idTipoProducto);";

                datos.setearConsulta(consulta);
                datos.setearParametro("@codigoArticulo", producto.CodigoArticulo);
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@descripcion", producto.Descripcion);
                datos.setearParametro("@precioCompra", producto.PrecioCompra);
                datos.setearParametro("@porcentajeGanancia", producto.PorcentajeGanancia);
                datos.setearParametro("@stockActual", producto.StockActual);
                datos.setearParametro("@stockMinimo", producto.StockMinimo);
                datos.setearParametro("@imagenUrl", producto.ImagenUrl);
                datos.setearParametro("@idMarca", producto.Marca.IdMarca);
                datos.setearParametro("@idTipoProducto", producto.TipoProducto.IdTipoProducto);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void ModificarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"UPDATE Productos SET 
                                        CodigoArticulo = @codigoArticulo,
                                        Nombre = @nombre,
                                        Descripcion = @descripcion,
                                        PrecioCompra = @precioCompra,
                                        PorcentajeGanancia = @porcentajeGanancia,
                                        StockActual = @stockActual,
                                        StockMinimo = @stockMinimo,
                                        ImagenUrl = @imagenUrl,
                                        IdMarca = @idMarca,
                                        IdTipoProducto = @idTipoProducto
                                    WHERE IdProducto = @idProducto;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@codigoArticulo", producto.CodigoArticulo);
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@descripcion", producto.Descripcion);
                datos.setearParametro("@precioCompra", producto.PrecioCompra);
                datos.setearParametro("@porcentajeGanancia", producto.PorcentajeGanancia);
                datos.setearParametro("@stockActual", producto.StockActual);
                datos.setearParametro("@stockMinimo", producto.StockMinimo);
                datos.setearParametro("@imagenUrl", producto.ImagenUrl);
                datos.setearParametro("@idMarca", producto.Marca.IdMarca);
                datos.setearParametro("@idTipoProducto", producto.TipoProducto.IdTipoProducto);


                datos.setearParametro("@idProducto", producto.IdProducto);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"DELETE FROM Productos 
                                    WHERE IdProducto = @idProducto;";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarProductoLogico(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"UPDATE Productos 
                                    SET Activo = 0 
                                    WHERE IdProducto = @idProducto;";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Producto> ListarStockCritico()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> productosCriticos = new List<Producto>();
            try
            {
                datos.setearProcedimiento("SP_ListarProductosStockBajo");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto PC = new Producto();

                    PC.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    PC.Nombre = datos.Lector["Producto"].ToString();
                    PC.Descripcion = datos.Lector["Descripcion"].ToString();
                    PC.StockActual = (int)datos.Lector["StockActual"];
                    PC.StockMinimo = (int)datos.Lector["StockMinimo"];

                    PC.Marca = new Marca();
                    PC.Marca.Nombre = datos.Lector["Marca"].ToString();
                        
                    PC.TipoProducto = new TipoProducto();
                    PC.TipoProducto.Nombre = datos.Lector["TipoProducto"].ToString();
                   

                    productosCriticos.Add(PC);
                }
                return productosCriticos;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Producto> ListarEliminados()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 0
                                    ORDER BY C.Nombre, P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public void ReactivarProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"UPDATE Productos
                                    SET Activo = 1
                                    WHERE IdProducto = @IdProducto";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int GetIdproducto(string CodigoProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            int idProducto = 0;

            string consulta = @"SELECT IdProducto
                        FROM Productos
                        WHERE CodigoArticulo = @codArticulo";

            datos.setearConsulta(consulta);
            datos.setearParametro("@codArticulo", CodigoProducto);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                idProducto = (int)datos.Lector["IdProducto"];
            }

            return idProducto;
        }

        public int StockActualProducto(string CodigoProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            int StockActual = 0;

            string consulta = @"SELECT StockActual
                                FROM Productos
                                WHERE CodigoArticulo = @codArticulo";

            datos.setearConsulta(consulta);
            datos.setearParametro("@codArticulo", CodigoProducto);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                StockActual = (int)datos.Lector["StockActual"];
            }

            return StockActual;
        }
        public bool CodExistente(string codArticulo)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.ListarConSp();

            return lista.Any(x => x.CodigoArticulo == codArticulo);
        }


        /***************** FILTROS *************/
        public List<Producto> ListarProductosFiltro()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorMarca(int IdMarca)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorCategoria(int IdCategoria)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND C.IdCategoria = @IdCategoria
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorTipoProducto(int IdTipoProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorMarcaYCategoria(int IdMarca, int IdCategoria)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca AND C.IdCategoria = @IdCategoria
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorMarcaYTipoProducto(int IdMarca, int IdTipoProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorCategoriaYTipoProducto(int IdCategoria, int IdTipoProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    P.IdProducto,
                                    P.CodigoArticulo, 
                                    P.Nombre, 
                                    P.Descripcion, 
                                    P.PrecioCompra, 
                                    CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                    P.PorcentajeGanancia, 
                                    P.StockActual, 
                                    P.StockMinimo, 
                                    P.ImagenUrl, 
	                                P.IdMarca,
									P.Activo,
                                    M.Nombre AS Marca, 
	                                TP.IdTipoProducto,
	                                TP.Nombre AS NombreTP,
	                                C.IdCategoria,
                                    C.Nombre AS Categoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND C.IdCategoria = @IdCategoria AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY P.Nombre ASC;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        public List<Producto> ListarPorMarcaCategoriaTipo(int IdMarca, int IdCategoria, int IdTipoProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
                                        P.IdProducto,
                                        P.CodigoArticulo, 
                                        P.Nombre, 
                                        P.Descripcion, 
                                        P.PrecioCompra, 
                                        CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
                                        P.PorcentajeGanancia, 
                                        P.StockActual, 
                                        P.StockMinimo, 
                                        P.ImagenUrl, 
	                                    P.IdMarca,	
                                        M.Nombre AS Marca,
	                                    TP.IdTipoProducto,	
	                                    TP.Nombre AS NombreTP,
	                                    C.IdCategoria,
                                        C.Nombre AS Categoria,
	                                    P.Activo
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca AND TP.IdTipoProducto = @IdTipoProducto AND C.IdCategoria = @IdCategoria
                                    ORDER BY P.Nombre ASC;;";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.CodigoArticulo = datos.Lector["CodigoArticulo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = datos.Lector["Marca"].ToString();

                    producto.TipoProducto = new TipoProducto();
                    producto.TipoProducto.IdTipoProducto = (int)datos.Lector["IdTipoProducto"];
                    producto.TipoProducto.Nombre = datos.Lector["NombreTP"].ToString();
                    producto.TipoProducto.categoria = new Categoria();
                    producto.TipoProducto.categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.TipoProducto.categoria.Nombre = datos.Lector["Categoria"].ToString();

                    producto.Activo = (Boolean)datos.Lector["Activo"];

                    listaProductos.Add(producto);
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

            return listaProductos;
        }

        /************** FIN FILTROS ***************/
    }
}
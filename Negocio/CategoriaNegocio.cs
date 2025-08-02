using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ListarCategoria");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria CTGR = new Categoria();
                    CTGR.IdCategoria = (int)datos.Lector["IdCategoria"];
                    CTGR.Nombre = datos.Lector["Nombre"].ToString();
                    CTGR.Activo = (bool)datos.Lector["Activo"];

                    listaCategoria.Add(CTGR);
                }
                return listaCategoria;
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

        public void AgregarCategoria(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarCategoria");
                datos.setearParametro("@Nombre", nuevo.Nombre);

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

        public void ModificarCategoria(Categoria ctgr)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarCategoria");
                datos.setearParametro("@IdCategoria", ctgr.IdCategoria);
                datos.setearParametro("@Nombre", ctgr.Nombre);

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

        public void EliminarCategoria(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_EliminarCategoria");
                datos.setearParametro("IdCategoria", id);
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

        public List<Categoria> ListarCategoriasEliminadas()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ListarCategoriaEliminada");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria CTGR = new Categoria();
                    CTGR.IdCategoria = (int)datos.Lector["IdCategoria"];
                    CTGR.Nombre = datos.Lector["Nombre"].ToString();
                    CTGR.Activo = (bool)datos.Lector["Activo"];

                    listaCategoria.Add(CTGR);
                }
                return listaCategoria;
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

        public void ReactivarCategoria(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AltaCategoria");
                datos.setearParametro("IdCategoria", idCategoria);
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

        /***************** FILTROS *************/
        public List<Categoria> ListarCategoriasfiltro()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT DISTINCT
	                                    C.IdCategoria,
                                        C.Nombre AS NombreCategoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1
                                    ORDER BY C.Nombre";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria CTGR = new Categoria();
                    CTGR.IdCategoria = (int)datos.Lector["IdCategoria"];
                    CTGR.Nombre = datos.Lector["NombreCategoria"].ToString();

                    listaCategoria.Add(CTGR);
                }
                return listaCategoria;
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

        public List<Categoria> ListarCategoriasPorMarca(int IdMarca)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
                                	    C.IdCategoria,
                                        C.Nombre AS NombreCategoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca
                                    ORDER BY C.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = datos.Lector["NombreCategoria"].ToString();
                    lista.Add(categoria);
                }

                return lista;
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

        public List<Categoria> ListarCategoriasPorTipo(int IdTipoProducto)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
	                                    C.IdCategoria,
                                        C.Nombre AS NombreCategoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY C.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = datos.Lector["NombreCategoria"].ToString();
                    lista.Add(categoria);
                }

                return lista;
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

        public List<Categoria> ListarCategoriasPorMarcaYTipo(int IdMarca, int IdTipoProducto)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
	                                    C.IdCategoria,
                                        C.Nombre AS NombreCategoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND M.IdMarca = @IdMarca AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY C.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", IdMarca);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = datos.Lector["NombreCategoria"].ToString();
                    lista.Add(categoria);
                }

                return lista;
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
        /***************** FIN FILTROS *************/
    }
}

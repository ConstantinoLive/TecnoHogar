using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listarMarcas()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT IdMarca, Nombre
                                    FROM Marcas";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Nombre = datos.Lector["Nombre"].ToString();
                    lista.Add(marca);
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

        public List<Marca> ListarMarcaConSp()
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ListarMarca");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca MRK= new Marca();
                    MRK.IdMarca = (int)datos.Lector["IdMarca"];
                    MRK.Nombre = datos.Lector["Nombre"].ToString();
                    MRK.Activo = (bool)datos.Lector["Activo"];

                    listaMarcas.Add(MRK);
                }
                return listaMarcas;
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

        public void AgregarMarca(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarMarca");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                
                /*datos.setearParametro("@Activo", 1)*/;
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

        public void ModificarMarca(Marca mrk)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarMarca");
                datos.setearParametro("@IdMarca", mrk.IdMarca);
                datos.setearParametro("@Nombre", mrk.Nombre);
               
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

        public void EliminarMarca(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_EliminarMarca");
                datos.setearParametro("IdMarca", id);
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

        public List<Marca> ListarMarcaEliminadas()
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ListarMarcaEliminadas");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca MRK = new Marca();
                    MRK.IdMarca = (int)datos.Lector["IdMarca"];
                    MRK.Nombre = datos.Lector["Nombre"].ToString();
                    MRK.Activo = (bool)datos.Lector["Activo"];

                    listaMarcas.Add(MRK);
                }
                return listaMarcas;
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

        public void ReactivarMarca(int idMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AltaMarca");
                datos.setearParametro("IdMarca", idMarca);
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
        public List<Marca> listarMarcasFiltro()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
	                              	    P.IdMarca,	
                                        M.Nombre AS Nombre
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1
                                    ORDER BY M.Nombre";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Nombre = datos.Lector["Nombre"].ToString();
                    lista.Add(marca);
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

        public List<Marca> ListarMarcasPorCategoria(int IdCategoria)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
                                	    M.Nombre AS NombreMarca,
	                                    M.IdMarca
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND C.IdCategoria = @IdCategoria
                                    ORDER BY M.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Nombre = datos.Lector["NombreMarca"].ToString();
                    lista.Add(marca);
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

        public List<Marca> ListarMarcasPorTipo(int IdTipoProducto)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
                                	    M.Nombre AS NombreMarca,
	                                    M.IdMarca
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY M.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Nombre = datos.Lector["NombreMarca"].ToString();
                    lista.Add(marca);
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

        public List<Marca> ListarMarcasPorCategoriaYTipo(int IdCategoria, int IdTipoProducto)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT
                                	    M.Nombre AS NombreMarca,
	                                    M.IdMarca
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
                                    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
                                    WHERE P.Activo = 1 AND C.IdCategoria = @IdCategoria AND TP.IdTipoProducto = @IdTipoProducto
                                    ORDER BY M.Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdCategoria", IdCategoria);
                datos.setearParametro("@IdTipoProducto", IdTipoProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Nombre = datos.Lector["NombreMarca"].ToString();
                    lista.Add(marca);
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

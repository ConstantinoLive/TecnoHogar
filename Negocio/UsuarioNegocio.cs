using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //datos.setearConsulta(@"SELECT 1 
                //                    FROM Usuario 
                //                    WHERE NombreUsuario = @usuario AND Contrasena = @contrasena");
                datos.setearProcedimiento("SP_Loguear");
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Contrasena", usuario.Contrasena);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    //usuario.TipoUsuario = (bool)datos.Lector["Admin"] ? TipoUsuario.Administrador : TipoUsuario.Vendedor;
                    usuario.Admin = (bool)datos.Lector["Admin"];
                    return true;
                }
                return false;
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

        public List<Usuario> Listar()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT * FROM Usuario where activo = 1 ORDER BY NombreUsuario, apellido, nombre asc;";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Contrasena = datos.Lector["Contrasena"].ToString();
                    usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    usuario.Admin = (bool)datos.Lector["Admin"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    listaUsuarios.Add(usuario);
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

            return listaUsuarios;
        }

        public List<Usuario> ListarEliminados()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                

                datos.setearProcedimiento("SP_ListarUsuariosEliminados");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Contrasena = datos.Lector["Contrasena"].ToString();
                    usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    usuario.Admin = (bool)datos.Lector["Admin"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    listaUsuarios.Add(usuario);
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

            return listaUsuarios;
        }

        public void AgregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_AgregarUsuario");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@NombreUsuario", nuevo.NombreUsuario);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Contraseña", nuevo.Contrasena);
                datos.setearParametro("@FechaAlta", nuevo.FechaAlta);
                datos.setearParametro("@Admin", nuevo.Admin);

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

        public void ModificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ModificarUsuario");
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.setearParametro("NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Apellido", usuario.Apellido);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Contraseña", usuario.Contrasena);
                datos.setearParametro("@FechaAlta", usuario.FechaAlta);
                datos.setearParametro("@Admin", usuario.Admin);

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

        public void EliminarUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_EliminarUsuario");
                datos.setearParametro("@IdUsuario", id);
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

        public void ReactivarUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ReactivarUsuario");
                datos.setearParametro("@IdUsuario", id);
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
    }
}





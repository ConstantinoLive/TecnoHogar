using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> ListarConSp()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ListarClientes");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Cliente cli = new Cliente();
                    cli.IdCliente = (int)datos.Lector["IdCliente"];
                    cli.Nombre = datos.Lector["Nombre"].ToString();
                    cli.Apellido = datos.Lector["Apellido"].ToString();
                    cli.Dni = datos.Lector["Dni"].ToString();
                    cli.Direccion = datos.Lector["Direccion"].ToString();
                    cli.Telefono = datos.Lector["Telefono"].ToString();
                    cli.Email = datos.Lector["Email"].ToString();
                    cli.Activo = (bool)datos.Lector["Activo"];
                    listaClientes.Add(cli);
                }
                return listaClientes;
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

        public List<Cliente> ListarEliminados()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Cliente> lista = new List<Cliente>();

            try
            {
                datos.setearProcedimiento("SP_ListarClientesEliminados");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cli = new Cliente();
                    cli.IdCliente = (int)datos.Lector["IdCliente"];
                    cli.Nombre = datos.Lector["Nombre"].ToString();
                    cli.Apellido = datos.Lector["Apellido"].ToString();
                    cli.Dni = datos.Lector["Dni"].ToString();
                    cli.Direccion = datos.Lector["Direccion"].ToString();
                    cli.Telefono = datos.Lector["Telefono"].ToString();
                    cli.Email = datos.Lector["Email"].ToString();
                    cli.Activo = (bool)datos.Lector["Activo"];
                    lista.Add(cli);
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
        public void AgregarCliente(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarCliente");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Direccion", nuevo.Direccion);

                datos.setearParametro("@Activo", 1);
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

        public void ModificarCliente(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarCliente");
                datos.setearParametro("@IdCliente", cliente.IdCliente);
                datos.setearParametro("@Nombre", cliente.Nombre);
                datos.setearParametro("@Apellido", cliente.Apellido);
                datos.setearParametro("@Dni", cliente.Dni);
                datos.setearParametro("@Telefono", cliente.Telefono);
                datos.setearParametro("@Email", cliente.Email);
                datos.setearParametro("@Direccion", cliente.Direccion);
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

        public void EliminarCliente(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_EliminarCliente");
                datos.setearParametro("IdCliente", id);
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

        public void ReactivarCliente(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ReactivarCliente");
                datos.setearParametro("Id", id);
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

        public Cliente BuscarClienteDNI(string dni)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT *
                                    FROM Clientes
                                    WHERE Dni = @dni";
                datos.setearConsulta(consulta);
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    cliente = new Cliente();
                    cliente.IdCliente = (int)datos.Lector["IdCliente"];
                    cliente.Nombre = datos.Lector["Nombre"].ToString();
                    cliente.Apellido = datos.Lector["Apellido"].ToString();
                    cliente.Dni = datos.Lector["Dni"].ToString();
                    cliente.Direccion = datos.Lector["Direccion"].ToString();
                    cliente.Telefono = datos.Lector["Telefono"].ToString();
                    cliente.Email = datos.Lector["Email"].ToString();
                    cliente.Activo = (bool)datos.Lector["Activo"];
                }
                return cliente;
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

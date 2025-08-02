using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ListarProveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor proveedor = new Proveedor();

                    proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    proveedor.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    proveedor.CUIT = datos.Lector["CUIT"].ToString();
                    proveedor.Direccion = datos.Lector["Direccion"].ToString();
                    proveedor.Telefono = datos.Lector["Telefono"].ToString();
                    proveedor.Email = datos.Lector["Email"].ToString();
                    proveedor.Activo = (bool)datos.Lector["Activo"];

                    listaProveedores.Add(proveedor);
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

            return listaProveedores;
        }

        public List<Proveedor> ListarEliminados()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ListarProveedoresEliminados");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor proveedor = new Proveedor();

                    proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    proveedor.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    proveedor.CUIT = datos.Lector["CUIT"].ToString();
                    proveedor.Direccion = datos.Lector["Direccion"].ToString();
                    proveedor.Telefono = datos.Lector["Telefono"].ToString();
                    proveedor.Email = datos.Lector["Email"].ToString();
                    proveedor.Activo = (bool)datos.Lector["Activo"];

                    listaProveedores.Add(proveedor);
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

            return listaProveedores;
        }

        public void AltaPorveedor(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarProveedor");
                datos.setearParametro("@RazonSocial", nuevo.RazonSocial);
                datos.setearParametro("@Cuit", nuevo.CUIT);
                datos.setearParametro("@Direccion", nuevo.Direccion);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
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

        public void ModificarProveedor(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ModificarProveedor");
                datos.setearParametro("@IdProveedor", nuevo.IdProveedor);
                datos.setearParametro("@RazonSocial", nuevo.RazonSocial);
                datos.setearParametro("@Cuit", nuevo.CUIT);
                datos.setearParametro("@Direccion", nuevo.Direccion);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);

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

        public void EliminarProveedor(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_EliminarProveedor");
                datos.setearParametro("@IdProveedor", id);
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

        public void ReactivarProveedor(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ReactivarProveedor");
                datos.setearParametro("@IdProveedor", id);
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

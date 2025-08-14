using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ComisionesNegocio
    {
        public List<Comisiones> listar(string anio, int mes, int idUsuario)
        {
            List<Comisiones> lista = new List<Comisiones>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT c.IdComision, c.IdVenta, c.IdUsuario, c.PorcentajeAplicado, c.MontoComision, c.Fecha " +
                    "FROM Comisiones c " +
                    "WHERE YEAR(c.Fecha) = @anio AND MONTH(c.Fecha) = @mes AND c.IdUsuario = @idUsuario"
                );
                datos.setearParametro("@anio", anio);
                datos.setearParametro("@mes", mes);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Comisiones comision = new Comisiones();
                    comision.IdComision = (int)datos.Lector["IdComision"];
                    comision.IdVenta = new Venta { IdVenta = (int)datos.Lector["IdVenta"] };
                    comision.IdUsuario = new Usuario { IdUsuario = (int)datos.Lector["IdUsuario"] };
                    comision.PorcentajeAplicado = (decimal)datos.Lector["PorcentajeAplicado"];
                    comision.MontoComision = (decimal)datos.Lector["MontoComision"];
                    comision.Fecha = (DateTime)datos.Lector["Fecha"];
                    lista.Add(comision);
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

        public decimal sumarMontoComision(string anio, int mes, int idUsuario)
        {
            decimal suma = 0;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT SUM(c.MontoComision) " +
                    "FROM Comisiones c " +
                    "WHERE YEAR(c.Fecha) = @anio AND MONTH(c.Fecha) = @mes AND c.IdUsuario = @idUsuario"
                );
                datos.setearParametro("@anio", anio);
                datos.setearParametro("@mes", mes);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                if (datos.Lector.Read() && !datos.Lector.IsDBNull(0))
                {
                    suma = datos.Lector.GetDecimal(0);
                }
                return suma;
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

        public void Comisionar(int venta, int user, decimal porcentaje, decimal comision, DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT into Comisiones (IdVenta, IdUsuario, PorcentajeAplicado, MontoComision, Fecha)"+
                    "VALUES (@Venta, @Usuario, @Porcentaje, @Comision, @Fecha)");
                datos.setearParametro("@Venta", venta);
                datos.setearParametro("@Usuario", user);
                datos.setearParametro("@Porcentaje", porcentaje);
                datos.setearParametro("@Comision",comision);
                datos.setearParametro("@Fecha",fecha);

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
    
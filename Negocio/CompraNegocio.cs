using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> Listar()
        {
            List<Compra> listaCompras = new List<Compra>();
            CompraDetalleNegocio negocio = new CompraDetalleNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                    C.IdCompra,
                                    C.Fecha,
                                    C.IdProveedor,
                                    P.RazonSocial,
                                    P.CUIT,
                                    P.Direccion,
                                    P.Telefono,
                                    P.Email,
                                    SUM(CD.Cantidad * CD.PrecioUnit) AS Total
                                    FROM Compras C
                                    INNER JOIN CompraDetalle CD ON C.IdCompra = CD.IdCompra
                                    INNER JOIN Proveedores P ON C.IdProveedor = P.IdProveedor
                                    GROUP BY 
                                        C.IdCompra, C.Fecha, C.IdProveedor, 
                                        P.RazonSocial, P.CUIT, P.Direccion, P.Telefono, P.Email
                                    ORDER BY C.IdCompra;";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra();

                    compra.IdCompra = (int)datos.Lector["IdCompra"];
                    compra.Fecha = (DateTime)datos.Lector["Fecha"];
                    compra.Proveedor = new Proveedor();
                    compra.Proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    compra.Proveedor.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    compra.Proveedor.CUIT = datos.Lector["CUIT"].ToString();
                    compra.Proveedor.Direccion = datos.Lector["Direccion"].ToString();
                    compra.Proveedor.Telefono = datos.Lector["Telefono"].ToString();
                    compra.Proveedor.Email = datos.Lector["Email"].ToString();
                    compra.Total = (decimal)datos.Lector["Total"];
                    compra.Detalles = new List<CompraDetalleNegocio>();
                    compra.Detalles = negocio.ObtenerDetallesPorCompra(compra.IdCompra);

                    listaCompras.Add(compra);
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

            return listaCompras;
        }

        public List<CompraHistorial> HistorialPreciosPorProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            List<CompraHistorial> lista = new List<CompraHistorial>();

            try
            {
                datos.setearProcedimiento("SP_HistorialPreciosProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CompraHistorial ch = new CompraHistorial();
                    ch.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    ch.FechaCompra = Convert.ToDateTime(datos.Lector["FechaCompra"]);
                    ch.PrecioUnitario = Convert.ToDecimal(datos.Lector["PrecioUnitario"]);
                    lista.Add(ch);
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

        public void GuardarCompra(Compra compra)
        {
            CompraDetalleNegocio negocio = new CompraDetalleNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"INSERT INTO Compras (idProveedor, IdUsuario, Total) 
                                    VALUES (@idProveedor, @idUsuario, @total);
                                    SELECT SCOPE_IDENTITY();";

                datos.setearConsulta(consulta);
                //datos.setearParametro("@fecha", compra.Fecha); // FECHA ESTA COMO DEFAULT GETDATE EN DB
                datos.setearParametro("@idProveedor", compra.Proveedor.IdProveedor);
                datos.setearParametro("@idUsuario", compra.Usuario.IdUsuario);

                decimal total = negocio.ObtenerMontoTotal(compra.Detalles);
                datos.setearParametro("@total", total);

                // OBTENGO IdCompra GENERADO DEL INSERT EN TABLA "Compras"
                int idCompra = Convert.ToInt32(datos.ejecutarScalar());

                // ASIGNO IdCompra A CADA CompraDetalle DE LA LISTA
                foreach (var detalle in compra.Detalles)
                {
                    detalle.IdCompra = idCompra;
                }

                // GUARDO DETALLES (USANDO UNA CONEXION INDEPENDIENTE)
                negocio.GuardarDetalleCompraConSP(compra.Detalles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarCompraConSP(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_InsertarCompraCompleta");
                datos.setearParametro("@IdProveedor", compra.Proveedor.IdProveedor);
                datos.setearParametro("@IdUsuario", compra.Usuario.IdUsuario);

                // CREO DATATABLE PARA GUARDAR CADA CompraDetalle
                DataTable tablaDetalles = new DataTable();
                // DEFINO LAS COLUMNAS CON LOS MISMOS NOMBRES Y TIPOS CREADOS EN DB ("dbo.CompraDetalleType")
                tablaDetalles.Columns.Add("IdProducto", typeof(int));
                tablaDetalles.Columns.Add("Cantidad", typeof(int));
                tablaDetalles.Columns.Add("PrecioUnit", typeof(decimal));

                foreach (var detalle in compra.Detalles)
                {
                    // EN CADA ITERACION AGREGO FILA EN DATATABLE CON DATOS OBTENIDOS
                    tablaDetalles.Rows.Add(
                        detalle.Producto.IdProducto,
                        detalle.Cantidad,
                        detalle.PrecioUnitario
                    );
                }

                // SETEO PARAMETROS EN TVP "Detalles" (Table-Valued Parameter) CON EL DATATABLE QUE INSTANCIAMOS
                datos.setearParametroTVP("@Detalles", tablaDetalles);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

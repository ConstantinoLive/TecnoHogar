using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> listaVentas = new List<Venta>();
            VentaDetalleNegocio negocio = new VentaDetalleNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                        V.IdVenta, 
                                        V.Fecha,
	                                    SUM(VD.Cantidad * VD.PrecioUnit) AS Total,
                                        C.IdCliente,
                                        C.Nombre,
                                        C.Apellido,
                                        C.Dni,
                                        C.Telefono,
                                        C.Email,
                                        C.Direccion,
                                        U.IdUsuario,
                                        U.Nombre AS NombreUsuario,
	                                    U.Apellido AS ApellidoUsuario,
	                                    U.Email AS EmailUsuario,
	                                    U.FechaAlta,
	                                    U.Admin,	
                                        COUNT(VD.IdVentaDetalle) AS CantidadProductos
                                    FROM Ventas V
                                    INNER JOIN Clientes C ON C.IdCliente = V.IdCliente
                                    INNER JOIN Usuario U ON V.IdUsuario = U.IdUsuario
                                    INNER JOIN VentaDetalle VD ON V.IdVenta = VD.IdVenta
                                    GROUP BY 
                                        V.IdVenta, V.Fecha,
	                                    C.IdCliente, C.Nombre, C.Apellido, C.Dni, C.Telefono, C.Email, C.Direccion,
                                        U.IdUsuario, U.Nombre, U.Apellido, U.Email, U.FechaAlta, U.Admin";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.IdVenta = (int)datos.Lector["IdVenta"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.Cliente = new Cliente();
                    venta.Cliente.IdCliente = (int)datos.Lector["IdCliente"];
                    venta.Cliente.Nombre = datos.Lector["Nombre"].ToString();
                    venta.Cliente.Apellido = datos.Lector["Apellido"].ToString();
                    venta.Cliente.Dni = datos.Lector["DNI"].ToString();
                    venta.Cliente.Telefono = datos.Lector["Telefono"].ToString();
                    venta.Cliente.Email = datos.Lector["Email"].ToString();
                    venta.Cliente.Direccion = datos.Lector["Direccion"].ToString();
                    venta.Usuario = new Usuario();
                    venta.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    venta.Usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    venta.Usuario.Apellido = datos.Lector["ApellidoUsuario"].ToString();
                    venta.Usuario.Email = datos.Lector["EmailUsuario"].ToString();
                    //venta.Usuario.Contrasena = datos.Lector["Contrasena"].ToString();
                    venta.Usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    venta.Usuario.Admin= (bool)datos.Lector["Admin"];
                    venta.Detalles = new List<VentaDetalle>();
                    venta.Detalles = negocio.ObtenerDetallesPorVenta(venta.IdVenta);

                    listaVentas.Add(venta);
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

            return listaVentas;
        }

        public void GuardarVenta(Venta venta)
        {
            VentaDetalleNegocio negocio = new VentaDetalleNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"INSERT INTO Ventas (Fecha, IdCliente, IdUsuario, Total) 
                                    VALUES (@fecha, @idCliente, @idUsuario, @total);
                                    SELECT SCOPE_IDENTITY();";

                datos.setearConsulta(consulta);
                datos.setearParametro("@fecha", venta.Fecha);
                datos.setearParametro("@idCliente", venta.Cliente.IdCliente);
                datos.setearParametro("@idUsuario", venta.Usuario.IdUsuario);

                decimal total = negocio.ObtenerMontoTotal(venta.Detalles);
                datos.setearParametro("@total", total);

                // ejecuta y obtiene el ID generado
                int idVenta = Convert.ToInt32(datos.ejecutarScalar());

                // se lo asignamos a cada detalle
                foreach (var detalle in venta.Detalles)
                {
                    detalle.IdVenta = idVenta;
                }

                // guarda detalles (usa otra conexión internamente)
                negocio.GuardarDetalleVenta(venta.Detalles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Venta BuscarVenta(int idVenta)
        {
            Venta venta = new Venta();
            AccesoDatos datos = new AccesoDatos();
            VentaDetalleNegocio negocio = new VentaDetalleNegocio();

            string consulta = @"SELECT 
                                    V.IdVenta, 
                                    V.Fecha,
                                    SUM(VD.Cantidad * VD.PrecioUnit) AS Total,
                                    C.IdCliente,
                                    C.Nombre,
                                    C.Apellido,
                                    C.Dni,
                                    C.Telefono,
                                    C.Email,
                                    C.Direccion,
                                    U.IdUsuario,
                                    U.Nombre AS NombreUsuario,
                                    U.Apellido AS ApellidoUsuario,
                                    U.Email AS EmailUsuario,
                                    U.FechaAlta,
                                    U.Admin,	
                                    COUNT(VD.IdVentaDetalle) AS CantidadProductos
                                FROM Ventas V
                                INNER JOIN Clientes C ON C.IdCliente = V.IdCliente
                                INNER JOIN Usuario U ON V.IdUsuario = U.IdUsuario
                                INNER JOIN VentaDetalle VD ON V.IdVenta = VD.IdVenta
                                WHERE V.IdVenta = @idVenta
                                GROUP BY 
                                    V.IdVenta, V.Fecha,
                                    C.IdCliente, C.Nombre, C.Apellido, C.Dni, C.Telefono, C.Email, C.Direccion,
                                    U.IdUsuario, U.Nombre, U.Apellido, U.Email, U.FechaAlta, U.Admin;";

            datos.setearConsulta(consulta);
            datos.setearParametro("@idVenta", idVenta);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                //Venta venta = new Venta();

                venta.IdVenta = (int)datos.Lector["IdVenta"];
                venta.Fecha = (DateTime)datos.Lector["Fecha"];
                venta.Total = (decimal)datos.Lector["Total"];
                venta.Cliente = new Cliente();
                venta.Cliente.IdCliente = (int)datos.Lector["IdCliente"];
                venta.Cliente.Nombre = datos.Lector["Nombre"].ToString();
                venta.Cliente.Apellido = datos.Lector["Apellido"].ToString();
                venta.Cliente.Dni = datos.Lector["DNI"].ToString();
                venta.Cliente.Telefono = datos.Lector["Telefono"].ToString();
                venta.Cliente.Email = datos.Lector["Email"].ToString();
                venta.Cliente.Direccion = datos.Lector["Direccion"].ToString();
                venta.Usuario = new Usuario();
                venta.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                venta.Usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                venta.Usuario.Apellido = datos.Lector["ApellidoUsuario"].ToString();
                venta.Usuario.Email = datos.Lector["EmailUsuario"].ToString();
                //venta.Usuario.Contrasena = datos.Lector["Contrasena"].ToString();
                venta.Usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                venta.Usuario.Admin = (bool)datos.Lector["Admin"];
                venta.Detalles = new List<VentaDetalle>();
                venta.Detalles = negocio.ObtenerDetallesPorVenta(venta.IdVenta);
            }
            return venta;
        }

        public int obtenerNumProxVenta()
        {
            AccesoDatos datos = new AccesoDatos();
            int num = 0;

            string consulta = @"SELECT MAX(IdVenta) AS IdVenta
                                FROM Ventas";

            datos.setearConsulta(consulta);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                num = (int)datos.Lector["IdVenta"] + 1;
            }

            return num;
        }

        public void GuardarVentaConSP(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_InsertarVentaCompleta");
                datos.setearParametro("@IdCliente", venta.Cliente.IdCliente);
                datos.setearParametro("@IdUsuario", venta.Usuario.IdUsuario);

                // CREO DATATABLE PARA GUARDAR CADA CompraDetalle
                DataTable tablaDetalles = new DataTable();
                // DEFINO LAS COLUMNAS CON LOS MISMOS NOMBRES Y TIPOS CREADOS EN DB ("dbo.CompraDetalleType")
                tablaDetalles.Columns.Add("IdProducto", typeof(int));
                tablaDetalles.Columns.Add("Cantidad", typeof(int));
                tablaDetalles.Columns.Add("PrecioUnit", typeof(decimal));

                foreach (var detalle in venta.Detalles)
                {
                    // EN CADA ITERACION AGREGO FILA EN DATATABLE CON DATOS OBTENIDOS
                    tablaDetalles.Rows.Add(
                        detalle.Producto.IdProducto,
                        detalle.Cantidad,
                        detalle.PrecioVenta
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

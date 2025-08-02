<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="FacturaVtaMs.aspx.cs" Inherits="WebForms.FacturaVtaMs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/StyleFactura.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">

    <div class="invoice-box">
        <div class="row mb-4 align-items-center">
            <div class="col-md-6">
                <img src="./image/LogoSinMarca.png" class="logo-img" />
                <h4 class="mt-1">Tecno Hogar S.A.</h4>
                <p class="mb-0">
                    CUIT: 30-12345678-9<br />
                    Av. Siempre Viva 123, CABA<br />
                    Tel: (011) 4567-8900<br />
                    Email: contacto@tecnohogar.com.ar
                </p>
            </div>

            <div class="col-md-6 text-end">
                <h2 class="mb-1">FACTURA B</h2>
                <p class="mb-0">
                    Nº:
                    <asp:Label ID="lblNumeroFactura" runat="server" /><br />
                    Fecha:
                    <asp:Label ID="lblFecha" runat="server" /><br />
                    Usuario:
                    <asp:Label ID="lblUsuario" runat="server" />
                </p>
            </div>
        </div>

        <hr />

        <h5 class="mb-2">Datos del Cliente</h5>
        <p>
            Nombre:
            <asp:Label ID="lblClienteNombre" runat="server" /><br />
            Dirección:
            <asp:Label ID="lblClienteDireccion" runat="server" /><br />
            DNI:
            <asp:Label ID="lblClienteDNI" runat="server" />
        </p>

        <asp:Repeater ID="rptDetalles" runat="server">
            <HeaderTemplate>
                <table class="table table-bordered">
                    <thead>
                        <tr class="heading">
                            <td>Producto</td>
                            <td>Cantidad</td>
                            <td>Precio Unitario</td>
                            <td>Subtotal</td>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="item">
                    <td><%# Eval("Producto.Nombre") %></td>
                    <td><%# Eval("Cantidad") %></td>
                    <td><%# String.Format("${0:N0}", Eval("PrecioVenta")) %></td>
                    <td><%# String.Format("${0:N0}", Eval("Subtotal")) %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>


        <div class="total">
            Total:
            <asp:Label ID="lblTotal" runat="server" />
        </div>

        <p class="mt-4"><strong>Método de pago:</strong> Tarjeta de Crédito (Visa 1 pago)</p>
        <p><strong>Observaciones:</strong> Garantía oficial de fábrica en todos los productos. Entrega en 48 hs.</p>

        <div class="firma-linea mt-5">Firma Cliente</div>

        <%--        <div class="footer">
            Gracias por su compra. Visite <strong>www.tecnohogar.com.ar</strong> para más ofertas.
        </div>--%>
    </div>

</asp:Content>

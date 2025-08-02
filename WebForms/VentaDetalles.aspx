<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="VentaDetalles.aspx.cs" Inherits="WebForms.VentaDetalles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center m-5">Detalle de Venta</h1>
    <asp:GridView ID="GVVentaDetalle" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover text-center gridview"
        HeaderStyle-CssClass="thead-dark">
        <Columns>
            <asp:BoundField DataField="Producto.CodigoArticulo" HeaderText="Codigo" />
            <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Producto.Marca.Nombre" HeaderText="Marca" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra Unit." DataFormatString="{0:C}" HtmlEncode="false" />
            <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta Unit." DataFormatString="{0:C}" HtmlEncode="false" />
        </Columns>
    </asp:GridView>
  <asp:Button runat="server" Text="Volver" ID="btnVolver" OnClick="btnVolver_Click"
    CssClass="btn btn-primary btn-lg shadow-sm" />
</asp:Content>

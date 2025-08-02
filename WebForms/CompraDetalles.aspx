<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompraDetalles.aspx.cs" Inherits="WebForms.CompraDetalles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="Css/StyleCompras.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1 class="display-4 text-center mt-5 mb-4">Detalle de Compra</h1>
<asp:GridView ID="GVCompraDetalle" runat="server" AutoGenerateColumns="False"
    CssClass="table table-striped table-bordered table-hover text-center gridview"
    HeaderStyle-CssClass="thead-dark text-white titCol">
    <Columns>
        <asp:BoundField DataField="Producto.CodigoArticulo" HeaderText="Codigo" />            
        <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Producto.Marca.Nombre" HeaderText="Marca" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="Producto.PrecioCompra" HeaderText="Precio Compra" DataFormatString="{0:C}" HtmlEncode="false" />
    </Columns>
</asp:GridView>
    <a href="Compras.aspx" class="back"><img class="imgback" src="/Icon/FlechaI.png"></a>
<!--<asp:Button runat="server" Text="Volver a Compras" ID="btnVolver" OnClick="btnVolver_Click" CssClass="btn btn-primary" />-->
</asp:Content>

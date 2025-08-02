<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListaCompras.aspx.cs" Inherits="WebForms.ListaCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center m-5">Listado de Compras</h1>
    <asp:GridView ID="GVCompras" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover text-center gridview"
        HeaderStyle-CssClass="thead-dark">
        <Columns>
            <asp:BoundField DataField="IdCompra" HeaderText="IdCompra" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="Proveedor.RazonSocial" HeaderText="Proveedor" />
            <asp:BoundField DataField="Total" HeaderText="Monto" DataFormatString="{0:C}" HtmlEncode="false" />
<%--        <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
            <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
            <asp:BoundField DataField="Email" HeaderText="Email" />--%>
            <asp:TemplateField HeaderText="Detalles">
                <ItemTemplate>
                    <asp:Button ID="btnDetalles" runat="server" Text="Ver Detalles" CssClass="btn btn-primary" PostBackUrl='<%# "CompraDetalles.aspx?ID=" + Eval("IdCompra") %>' />
<%--                    <a href='<%# "CompraDetalles.aspx?ID=" + Eval("IdCompra") %>' class="icono" title="Ver Detalles">
                        <i class="fa-solid fa-search" style="color: dimgrey; margin: 10px"></i>
                    </a>--%>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <asp:Button runat="server" Text="Nueva Compra" ID="btnNuevaCompra" OnClick="btnNuevaCompra_Click" CssClass="btn btn-primary" />
</asp:Content>

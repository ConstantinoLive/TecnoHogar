<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AltaCatTipoMarca.aspx.cs" Inherits="WebForms.AltaCatTipoMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="max-width: 600px; margin: 40px auto; padding: 30px; background-color: #f9f9f9; border-radius: 10px; box-shadow: rgb(38, 57, 77) 0px 20px 30px -10px;">
        <h2 style="text-align: center; margin-bottom: 30px;">Gestión de Marca, Categoría y Tipo</h2>

        <%-- Sección: Agregar Marca --%>
        <div style="margin-bottom: 25px;">
            <asp:Label ID="lblMarca" runat="server" Text="Nueva Marca:"></asp:Label><br />
            <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control miTextBox"
                         placeholder="Samsung, LG, TCL" Width="100%" />
            <asp:Button ID="btnAgregarMarca" runat="server" Text="Agregar Marca" CssClass="btn btn-primary mt-2" Width="100%" OnClick="btnAgregarMarca_Click" />
        </div>

        <%-- Sección: Agregar Categoría --%>
        <div style="margin-bottom: 25px;">
            <asp:Label ID="lblCategoria" runat="server" Text="Nueva Categoría:"></asp:Label><br />
            <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control miTextBox"
                         placeholder="Electrodomésticos, Audio, Telefonía" Width="100%" />
            <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoría" CssClass="btn btn-primary mt-2" Width="100%" OnClick="btnAgregarCategoria_Click" />
        </div>

        <%-- Sección: Agregar Tipo de Producto --%>
        <div style="margin-bottom: 25px;">
            <asp:Label ID="lblTipoProducto" runat="server" Text="Nuevo Tipo de Producto:"></asp:Label><br />
            <asp:TextBox ID="txtTipoProducto" runat="server" CssClass="form-control miTextBox"
                         placeholder="Heladera, Microondas, Cocina" Width="100%" />
            
            <asp:Label ID="lblSeleccionCategoria" runat="server" Text="Categoría asociada:" CssClass="mt-2 d-block" />
            <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-control miTextBox" Width="100%">
                <asp:ListItem Text="Seleccione una categoría..." Value="" />
                <%-- Se carga dinámicamente desde el code-behind --%>
            </asp:DropDownList>

            <asp:Button ID="btnAgregarTipoProducto" runat="server" Text="Agregar Tipo de Producto" CssClass="btn btn-primary mt-2" Width="100%" OnClick="btnAgregarTipoProducto_Click" />
        </div>

        <%-- Mensaje general de salida --%>
        <asp:Label ID="lblMensajeGeneral" runat="server" ForeColor="Red" />
    </div>
</asp:Content>

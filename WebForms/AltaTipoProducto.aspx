<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="AltaTipoProducto.aspx.cs" Inherits="WebForms.AltaTipoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListaTipoProducto.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	  <div  class="centered-container"> 
	<asp:Panel ID="pnlTpProductos" runat="server" DefaultButton="btnAceptar" CssClass="form-container">
	
			<h2 style="text-align: center; margin-bottom: 25px; color: #2c3e50; font-weight: 600;">Alta tipo de producto</h2>

			<div style="display: grid; grid-template-columns: 1fr 3fr; gap: 20px; margin-bottom: 20px;">
				<div>
					<asp:Label ID="lblID" runat="server" Text="ID:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #34495e;"></asp:Label>
					<asp:TextBox ID="txtID" runat="server" Enabled="false" CssClass="form-control" Style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 6px;" />
				</div>
				<div>
					<asp:Label ID="lblNombre" runat="server" Text="Nombre:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #34495e;"></asp:Label>
					<asp:TextBox ID="txtNombre" runat="server" AutoPostBack="true" OnTextChanged="txtNombre_TextChanged" CssClass="form-control" Style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
				</div>
				</div>
				<div>
					<asp:Label ID="lblCategoria" runat="server" Text="Categoría:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #34495e;"></asp:Label>
					<asp:DropDownList ID="DDLCategorias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" CssClass="form-control" Style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;"></asp:DropDownList>
					<p style="font-size:12px; font-style:oblique; margin-top:8px;">Si no encuentra la categoría que está buscando, haga click <asp:HyperLink runat="server" NavigateUrl="~/AltaCategoria.aspx">aqui</asp:HyperLink></p>
				</div>
			
			<div class="d-grid gap-2 d-md-flex justify-content-md-end">
				<asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
					CssClass="btn btn-outline-secondary me-md-2" OnClick="btnCancelar_Click" />
				<asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
					CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
			</div>
			        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Style="display: block; margin-top: 20px; text-align: center;"></asp:Label>
    
</asp:Panel>
					</div>

</asp:Content>

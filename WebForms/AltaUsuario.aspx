<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="WebForms.AltaVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListUsuarios.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="centered-container">
		<asp:Panel ID="pnlUsuario" runat="server" DefaultButton="btnAceptar" CssClass="form-container">

			<h2 style="text-align: center; margin-bottom: 25px; color: #2c3e50; font-weight: 600; border-bottom: 2px solid #f1f1f1; padding-bottom: 10px;">
				<i class="fas fa-user-cog" style="margin-right: 10px;"></i>Gestión de Usuario
			</h2>

			<asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Style="display: block; margin-bottom: 20px; text-align: center; padding: 10px; background-color: #ffeeee; border-radius: 5px;"></asp:Label>


			<div style="display: grid; grid-template-columns: 80px 1fr; gap: 15px; margin-bottom: 15px;">
				<div>
					<asp:Label ID="lblIdUsuario" runat="server" Text="ID:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
					<asp:TextBox ID="txtIdUsuario" runat="server" ReadOnly="true" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px; background-color: #f9f9f9;" OnTextChanged="txtIdUsuario_TextChanged" AutoPostBack="true"></asp:TextBox>
				</div>
				<div>
					<asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre Usuario:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
					<asp:TextBox ID="txtNombreUsuario" AutoPostBack="true" OnTextChanged="txtNombreUsuario_TextChanged" runat="server" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
				</div>
			</div>


			<div style="display: grid; grid-template-columns: 1fr 1fr; gap: 15px; margin-bottom: 15px;">
				<div>
					<asp:Label ID="lblNombre" runat="server" Text="Nombre:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
					<asp:TextBox ID="txtNombre" OnTextChanged="txtNombre_TextChanged" AutoPostBack="true" runat="server" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
				</div>
				<div>
					<asp:Label ID="lblApellido" runat="server" Text="Apellido:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
					<asp:TextBox ID="txtApellido" AutoPostBack="true" OnTextChanged="txtApellido_TextChanged" runat="server" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
				</div>
			</div>



			<div style="margin-bottom: 15px;">
				<asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
				<asp:TextBox ID="txtContrasena" AutoPostBack="true" OnTextChanged="txtContrasena_TextChanged" runat="server" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
			</div>
			<div style="margin-bottom: 15px;">
				<asp:Label ID="lblEmail" runat="server" Text="Email:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
				<asp:TextBox ID="txtEmail" runat="server" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true" TextMode="Email" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
				<asp:Label Text="" ID="lblEmailMensaje" runat="server" Style="color: red; font-size: 17px; font-weight: 500; display: inline-block; margin-top: 10px" />
			</div>
			<div style="margin-bottom: 15px;">
				<asp:Label ID="lblComision" runat="server" Text="% Comisión: " Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
				<asp:TextBox ID="txbComision" AutoPostBack="true" OnTextChanged="txbComision_TextChanged" runat="server" Style="width: 50%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px;"></asp:TextBox>
			</div>


			<div style="display: grid; grid-template-columns: 1fr 1fr; gap: 15px; margin-bottom: 20px;">
				<div class="form-group">
					<asp:Label ID="lblFechaAlta" runat="server"
						Text="Fecha Alta:"
						CssClass="form-label"></asp:Label>
					<asp:TextBox ID="txtFechaAlta" runat="server"
						TextMode="Date"
						CssClass="form-control mt-1"
						Enabled="false"
						ReadOnly="true"></asp:TextBox>
				</div>
				<div>
					<asp:Label ID="lblRol" runat="server" Text="Rol:" Style="display: block; margin-bottom: 5px; font-weight: 500; color: #5d6d7e; font-size: 14px;"></asp:Label>
					<asp:DropDownList ID="ddlRol" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" runat="server" Style="width: 100%; padding: 8px 10px; border: 1px solid #e0e0e0; border-radius: 5px; background-color: white;">
						<asp:ListItem Text="Seleccione Rol" Value="" Selected="true" />
						<asp:ListItem Text="Admin" Value="True" />
						<asp:ListItem Text="Vendedor" Value="False" />
					</asp:DropDownList>
				</div>
			</div>

			<div class="d-grid gap-2 d-md-flex justify-content-md-end">
				<asp:Button ID="Button1" runat="server" Text="Cancelar"
					CssClass="btn btn-outline-secondary me-md-2" OnClick="btnCancelar_Click" />
				<asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
					CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
			</div>


			<asp:Label Text="" ID="lblAviso" runat="server" Style="color: red; font-size: 17px; font-weight: 500; display: inline-block; margin-top: 10px" />

		</asp:Panel>
	</div>
</asp:Content>

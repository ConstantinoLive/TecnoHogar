<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="WebForms.PanelAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StylePanelAdmin.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--	<div style="max-width: 800px; margin: 60px auto; padding: 30px; background-color: #f9f9f9; border-radius: 10px; box-shadow: rgb(38, 57, 77) 0px 20px 30px -10px">
		<h2 class="text-center mt-4">Panel de Administración</h2>

		<div class="container mt-5">
			<div class="row row-cols-1 row-cols-md-3 g-4 justify-content-center">

				<div class="col text-center">
					<%--<asp:Button ID="btnProductos" runat="server" Text="📦 Productos" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaProductos.aspx" />--%>
					<asp:LinkButton ID="lkbProductos" runat="server" CssClass="btn-PA" OnClick="lkbProductos_Click">
<img src="/Icon/pallet.png" style="width: 20px; height: 20px;" />
Productos 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%--< asp:Button ID = "btnClientes" runat = "server" Text = "🧍 Clientes" CssClass = "btn btn-primary btn-lg w-100" PostBackUrl = "~/ListaClientes.aspx" /> --%>
					<asp:LinkButton ID="lkbCliente" runat="server" CssClass="btn-PA" OnClick="lkbCliente_Click">
<img src="/Icon/user.png" style="width: 20px; height: 20px;" />
Cliente 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%--<asp:Button ID="btnVentas" runat="server" Text="🧾 Ventas" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/AltaVenta.aspx" />--%>
					<asp:LinkButton ID="lkbVentas" runat="server" CssClass="btn-PA" OnClick="lkbVentas_Click">
<img src="/Icon/chango.png" style="width: 20px; height: 20px;" />
Ventas 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%-- <asp:Button ID="btnUsuarios" runat="server" Text="👤 Usuarios" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaUsuarios.aspx" />--%>
					<asp:LinkButton ID="lkbUsuario" runat="server" CssClass="btn-PA" OnClick="lkbUsuario_Click">
<img src="/Icon/id-card.png" style="width: 20px; height: 20px;" />
Usuario 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%-- <asp:Button ID="btnProveedores" runat="server" Text="🏢 Proveedores" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaProveedores.aspx" />--%>
					<asp:LinkButton ID="lkbProveedores" runat="server" CssClass="btn-PA" OnClick="lkbProveedores_Click">
<img src="/Icon/truck.png" style="width: 20px; height: 20px;" />
Proveedores 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%-- <asp:Button ID="btnCompras" runat="server" Text="🛒 Compras" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/Compras.aspx" />--%>
					<asp:LinkButton ID="lkbCompras" runat="server" CssClass="btn-PA" OnClick="lkbCompras_Click">
<img src="/Icon/cash.png" style="width: 20px; height: 20px;" />
Compras 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%-- <asp:Button ID="BtnMarcas" runat="server" Text="🏢 Marcas" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaMarcas.aspx" />--%>
					<asp:LinkButton ID="lkbMarcas" runat="server" CssClass="btn-PA" OnClick="lkbMarcas_Click">
<img src="/Icon/certificate.png" style="width: 20px; height: 20px;" />
Marcas 
					</asp:LinkButton>
				</div>

				<div class="col text-center">
					<%-- <asp:Button ID="BtnCategorias" runat="server" Text="🏢 Categorias" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaCategorias.aspx" />--%>
					<asp:LinkButton ID="lkbCategoria" runat="server" CssClass="btn-PA" OnClick="lkbCategoria_Click">
<img src="/Icon/box.png" style="width: 20px; height: 20px;" />
Categorías 
					</asp:LinkButton>
				</div>
				<div class="col text-center">
					<%-- <asp:Button ID="BtnTProd" runat="server" Text="🏢 Tipo de Producto" CssClass="btn btn-primary btn-lg w-100" PostBackUrl="~/ListaTipoProducto.aspx" />--%>
					<asp:LinkButton ID="lkbTipoProducto" runat="server" CssClass="btn-PA" OnClick="lkbTipoProducto_Click">
<img src="/Icon/lavadora.png" style="width: 20px; height: 20px;" />
Tipo producto 
					</asp:LinkButton>
				</div>

			</div>
		</div>


	</div>-->


		<div class="Lgd" style="margin-left:150px; margin-top:150px;">
<img src="/Image/Tecno.png" class="tecno" alt="Tecno">
<img src="/Image/LogoSinMarca.png" class="logo" alt="Logo">
<img src="/Image/Hogar.png" class="hogar" alt="Hogar">
	</div>
</asp:Content>

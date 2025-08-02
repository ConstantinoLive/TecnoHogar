<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="ListaProductos.aspx.cs" Inherits="WebForms.ListaProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListProveedores.css">
	<link rel="stylesheet" href="Css/StyleListMarcas.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="row mb-4">
		<div class="col-12">
			<h1 class="display-4 text-center mt-5 mb-4">Listado de Producto</h1>

			<% if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin == true) { %>
    <div class="d-flex justify-content-between mb-3">
        <a href="PanelAdmin.aspx" class="back">
            <img class="imgback" src="/Icon/FlechaI.png" />
        </a>
        <asp:LinkButton ID="lkbAdregar" runat="server" CssClass="btn-agregar" OnClick="lkbAdregar_Click">
            <img src="/Icon/add.png" style="width: 20px; height: 20px;" />
            Agregar producto 
        </asp:LinkButton>
    </div>
<% } %>

		</div>
	</div>
	<div class="card mb-4 shadow-0 border-0">
		<div class="card-body">
			<div class="row align-items-center justify-content-center">
				<div class="col-md-6 mb-3 mb-md-0">
					<asp:Panel runat="server" DefaultButton="btnimg">
						<div class="input-group">
							<asp:TextBox ID="txtBuscarCuit" runat="server"
								CssClass="form-control form-control-lg me-3"
								placeholder="Ingrese Nombre o Descripcion"
								MaxLength="13"></asp:TextBox>
							<div class="input-group-append">
								<asp:ImageButton ID="btnimg" CssClass="btnimg_lup"
									ImageUrl="~/Icon/Lupa2.png"
									runat="server" OnClick="btnimg_Click" />
							</div>
						</div>
					</asp:Panel>

				</div>
				<div class="col-md-3 mb-3 mb-md-0">
					<div class="checkbox-wrapper-39">
						<label>
							<asp:CheckBox ID="CheckEliminados" OnCheckedChanged="CheckEliminados_CheckedChanged"
								AutoPostBack="true" runat="server" />
							<span class="checkbox"></span>
						</label>
						<span class="lblME">Mostrar eliminados </span>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col-12">
			<div class="table-responsive shadow-sm rounded">
				<asp:GridView ID="GVProductos" runat="server" AutoGenerateColumns="False"
					CssClass="table table-striped table-bordered table-hover text-center gridview"
					HeaderStyle-CssClass="thead-dark text-white titCol"
					RowStyle-CssClass="align-middle"
					AlternatingRowStyle-CssClass="table-light"
					EmptyDataText="No se encontraron Productos"
					GridLines="None"
					AllowPaging="true" PageSize="10"
					OnPageIndexChanging="GVProductos_PageIndexChanging"
					DataKeyNames="IdProducto"
					OnSelectedIndexChanged="GVProductos_SelectedIndexChanged"
					OnRowCommand="GVProductos_RowCommand"
					OnRowDeleting="GVProductos_RowDeleting">
					<Columns>
						<asp:BoundField DataField="TipoProducto.categoria.Nombre" HeaderText="Categoría" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="CodigoArticulo" HeaderText="Código" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" DataFormatString="{0:C2}" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Ganancia" DataFormatString="{0}%" HeaderStyle-CssClass="py-3" />
						<asp:TemplateField HeaderText="Precio Venta" HeaderStyle-CssClass="py-3">
							<ItemTemplate>
								<%# CalculoPrecioVenta(Eval("PrecioCompra"), Eval("PorcentajeGanancia")) %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="StockActual" HeaderText="Stock" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="StockMinimo" HeaderText="Stock Mín" HeaderStyle-CssClass="py-3" />
						<asp:TemplateField HeaderText="Imagen">
							<ItemTemplate>
								<asp:Image ID="imgProducto" runat="server"
									ImageUrl='<%# Eval("ImagenUrl") %>'
									CssClass="img-thumbnail"
									Style="max-width: 80px; height: auto;"
									onerror="this.src='https://via.placeholder.com/80?text=Sin+Imagen';" />
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Acciones" ItemStyle-Width="5%">
							<ItemTemplate>
								<div class="btn-container">

									<asp:LinkButton ID="lnkEdit" runat="server"
										CommandName="Select"
										CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
										CssClass="btnEdit_Delete"
										ToolTip="Editar"
										Visible='<%# Convert.ToBoolean(Eval("Activo")) %>'>
<img src='<%= ResolveUrl("~/Icon/IconModificarG.png") %>' alt="Editar" />
									</asp:LinkButton>

									<asp:LinkButton ID="lnkDelete" runat="server"
										CommandName="Delete"
										CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
										CssClass="btnEdit_Delete"
										ToolTip="Eliminar"
										Visible='<%# Convert.ToBoolean(Eval("Activo")) %>'
										OnClientClick="return confirm('¿Está seguro que desea eliminar esta Producto?');">
<img src='<%= ResolveUrl("~/Icon/IconBasuraG.png") %>' alt="Eliminar" />
									</asp:LinkButton>

									<asp:LinkButton ID="lnkReactivar" runat="server"
										CommandName="Reactivar"
										CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
										CssClass="btnEdit_Delete"
										ToolTip="Reactivar"
										Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>'
										OnClientClick="return confirm('¿Está seguro que desea reactivar esta Producto?');">
<img src='<%= ResolveUrl("~/Icon/iconAñadir.png") %>' alt="Reactivar" />
									</asp:LinkButton>
								</div>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<EmptyDataTemplate>
						<div class="alert alert-info text-center py-4">
							No hay productos registrados.
                       
                       
						</div>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</div>
	</div>

</asp:Content>

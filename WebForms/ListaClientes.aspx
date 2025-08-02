<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="ListaClientes.aspx.cs" Inherits="WebForms.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListClientes.css">
	<link rel="stylesheet" href="Css/StyleListMarcas.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="row mb-4">
		<div class="col-12">
			<h1 class="display-4 text-center mb-4 mt-5">Listado de Clientes</h1>
			
				<%if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin == true)
					{  %>
			<div class="d-flex justify-content-between mb-3">
				<a href="PanelAdmin.aspx" class="back">
					<img class="imgback" src="/Icon/FlechaI.png"></a> <%}
				 else
				{ %>
				<div class="d-flex justify-content-end">
				<a href="PanelVentas.aspx" class="back" style="display:none">
					<img class="imgback" src="/Icon/FlechaI.png"></a> <%} %>
				
				<asp:LinkButton ID="lkbAdregar" runat="server" CssClass="btn-agregar" OnClick="lkbAdregar_Click">
<img src="/Icon/add.png" style="width: 20px; height: 20px;" />
Agregar cliente 
				</asp:LinkButton>
			</div>
		</div>
	</div>
	<div class="card mb-4 shadow-0 border-0">
		<div class="card-body">
			<div class="row align-items-center justify-content-center">
				<div class="col-md-6 mb-3 mb-md-0">
					<div class="input-group">
						<asp:TextBox ID="txtBuscarDni" runat="server"
							CssClass="form-control form-control-lg me-3"
							placeholder="Ingrese DNI del cliente"
							MaxLength="8"></asp:TextBox>
						<div class="input-group-append">
							<%--<asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" Text="Buscar"
								CssClass="btn btn-primary btn-lg" />--%>
							<asp:ImageButton ID="btnimgBuscar" CssClass="btnimg_lup"
								ImageUrl="~/Icon/Lupa2.png"
								runat="server" OnClick="btnimgBuscar_Click" />
						</div>
					</div>
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
				<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
				<asp:UpdatePanel runat="server">
					<ContentTemplate>
						<asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="False"
							CssClass="table table-striped table-bordered table-hover text-center gridview"
							HeaderStyle-CssClass="thead-dark text-white titCol"
							RowStyle-CssClass="align-middle"
							EmptyDataText="No se encontraron clientes"
							AllowPaging="True" PageSize="10"
							PagerStyle-CssClass="pagination"
							PagerSettings-Mode="NumericFirstLast"
							GridLines="None"
							CellPadding="10"
							DataKeyNames="IdCliente"
							OnSelectedIndexChanged="dgvClientes_SelectedIndexChanged"
							OnRowCommand="dgvClientes_RowCommand"
							OnPageIndexChanging="dgvClientes_PageIndexChanging"
							OnRowDeleting="dgvClientes_RowDeleting">

							<Columns>
								<asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="10%" />
								<asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="10%" />
								<asp:BoundField DataField="Dni" HeaderText="DNI" ItemStyle-Width="12%" />
								<asp:BoundField DataField="Telefono" HeaderText="Teléfono" ItemStyle-Width="12%" />
								<asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="18%" />
								<asp:BoundField DataField="Direccion" HeaderText="Dirección" ItemStyle-Width="20%" />

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
												OnClientClick="return confirm('¿Está seguro que desea eliminar este cliente?');">
                                            <img src='<%= ResolveUrl("~/Icon/IconBasuraG.png") %>' alt="Eliminar" />
											</asp:LinkButton>

											<asp:LinkButton ID="lnkReactivar" runat="server"
												CommandName="Reactivar"
												CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
												CssClass="btnEdit_Delete"
												ToolTip="Reactivar"
												Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>'
												OnClientClick="return confirm('¿Está seguro que desea reactivar este cliente?');">
                                                <img src='<%= ResolveUrl("~/Icon/iconAñadir.png") %>' alt="Reactivar" />
											</asp:LinkButton>
										</div>
									</ItemTemplate>

								</asp:TemplateField>
							</Columns>
							<%--<HeaderStyle CssClass="bg-primary text-white text-center" />
                            <AlternatingRowStyle CssClass="bg-light" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pagination" />--%>
							<EmptyDataTemplate>
								<div class="alert alert-info text-center py-4">
									No hay clientes registrados.
								</div>
							</EmptyDataTemplate>
						</asp:GridView>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

</asp:Content>

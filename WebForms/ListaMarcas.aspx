<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="ListaMarcas.aspx.cs" Inherits="WebForms.ListaMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListMarcas.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="row mb-4">
		<div class="col-12">
			<h1 class="display-4 text-center mt-5 mb-4">Listado de Marcas</h1>
			<div class="d-flex justify-content-end">
				<!--<a href="PanelAdmin.aspx" class="back">
					<img class="imgback" src="/Icon/FlechaI.png"></a>-->
				<%--<asp:Button runat="server" Text="Agregar Marca" ID="btnAgregarMarca" OnClick="btnAgregarMarca_Click"
					CssClass="btn btn-primary btn-lg shadow-sm" />--%>
				<asp:LinkButton ID="lkbAdregar" runat="server" CssClass="btn-agregar" OnClick="lkbAdregar_Click">
<img src="/Icon/add.png" style="width: 20px; height: 20px;" />
Agregar marca 
				</asp:LinkButton>
			</div>
		</div>
	</div>
	<div class="card mb-4 shadow-0 border-0">
		<div class="card-body">
			<div class="row align-items-center justify-content-center">
				<div class="col-md-9 mb-3 mb-md-0">
					<div class="input-group">
						<asp:TextBox ID="txtBuscarMarca" runat="server"
							CssClass="form-control form-control-lg me-3"
							placeholder="Ingrese una marca "
							MaxLength="13"></asp:TextBox>
						<div class="input-group-append">
							<%-- <asp:Button ID = "btnBuscar" OnClick = "btnBuscar_Click" runat = "server" Text = "Buscar"
								CssClass = "btn btn-primary btn-lg" />--%>
							<asp:ImageButton ID="btnimg" CssClass="btnimg_lup"
								ImageUrl="~/Icon/Lupa2.png"
								runat="server" OnClick="btnimg_Click" />
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
				<asp:GridView ID="GVMarcas" runat="server" AutoGenerateColumns="False"
					CssClass="table table-striped table-bordered table-hover text-center gridview"
					HeaderStyle-CssClass="thead-dark text-white titCol"
					RowStyle-CssClass="align-middle"
					AlternatingRowStyle-CssClass="table-light"
					EmptyDataText="No se encontró la marca que buscaba"
					GridLines="None"
					AllowPaging="true" PageSize="10"
					OnPageIndexChanging="GVMarcas_PageIndexChanging"
					DataKeyNames="IdMarca, Activo"
					OnSelectedIndexChanged="GVMarcas_SelectedIndexChanged"
					OnRowCommand="GVMarcas_RowCommand"
					OnRowDeleting="GVMarcas_RowDeleting">
					<Columns>
						<asp:BoundField DataField="IdMarca" HeaderText="ID"
							ItemStyle-Width="5%" />
						<asp:BoundField DataField="Nombre" HeaderText="Marca"
							ItemStyle-Width="15%" />
						<asp:BoundField DataField="Activo" HeaderText="Estado"
							ItemStyle-Width="10%"
							DataFormatString="{0:Activa;Inactiva}" Visible="false" />

						<asp:TemplateField HeaderText="Acciones" ItemStyle-Width="5%">
							<ItemTemplate>
								<div class="btn-container">

									<asp:LinkButton ID="lnkEdit" runat="server"
										CommandName="Select"
										CommandArgument='<%# Eval("IdMarca") %>'
										CssClass="btnEdit_Delete"
										ToolTip="Editar"
										Visible='<%# Convert.ToBoolean(Eval("Activo")) %>'>
            <img src='<%= ResolveUrl("~/Icon/IconModificarG.png") %>' alt="Editar" />
									</asp:LinkButton>

									<asp:LinkButton ID="lnkDelete" runat="server"
										CommandName="Delete"
										CommandArgument='<%# Eval("IdMarca") %>'
										CssClass="btnEdit_Delete"
										ToolTip="Eliminar"
										Visible='<%# Convert.ToBoolean(Eval("Activo")) %>'
										OnClientClick="return confirm('¿Está seguro que desea eliminar esta marca?');">
            <img src='<%= ResolveUrl("~/Icon/IconBasuraG.png") %>' alt="Eliminar" />
									</asp:LinkButton>

									<asp:LinkButton ID="lnkReactivar" runat="server"
										CommandName="Reactivar"
										CommandArgument='<%# Eval("IdMarca") %>'
										CssClass="btnEdit_Delete"
										ToolTip="Reactivar"
										Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>'
										OnClientClick="return confirm('¿Está seguro que desea reactivar esta marca?');">
            <img src='<%= ResolveUrl("~/Icon/iconAñadir.png") %>' alt="Reactivar" />
									</asp:LinkButton>
								</div>
							</ItemTemplate>

						</asp:TemplateField>


					</Columns>
					<EmptyDataTemplate>
						<div class="alert alert-info text-center py-4">
							No hay marcas registradas.
						</div>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</div>
	</div>
</asp:Content>

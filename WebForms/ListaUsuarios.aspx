<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="WebForms.ListaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleListUsuarios.css">
	<link rel="stylesheet" href="Css/StyleListMarcas.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="row mb-4">
		<div class="col-12">
			<h1 class="display-4 text-center mt-4 mb-4">Listado de Usuarios</h1>
			<div class="d-flex justify-content-end">
				<!--<asp:Button runat="server" Text="Regresar" ID="btnVolver" OnClick="btnVolver_Click"
                    CssClass="btn btn-outline-secondary btn-lg shadow-sm" />
                <a href="PanelAdmin.aspx" class="back">
                    <img class="imgback" src="/Icon/FlechaI.png"></a>-->

				<asp:LinkButton ID="lkbAdregar" runat="server" CssClass="btn-agregar" OnClick="lkbAdregar_Click">
<img src="/Icon/add.png" style="width: 20px; height: 20px;" />
Agregar usuario 
				</asp:LinkButton>

			</div>
		</div>
	</div>
	<div class="card mb-4 shadow-0 border-0">
		<div class="card-body">
			<div class="row align-items-center justify-content-center">
				<div class="col-md-6 mb-3 mb-md-0">
					<div class="input-group">
						<asp:TextBox ID="txtBuscarUsuario" runat="server"
							CssClass="form-control form-control-lg me-3"
							placeholder="Ingrese nombre de usuario"></asp:TextBox>
						<div class="input-group-append">
							<asp:ImageButton ID="btnimg" CssClass="btnimg_lup" 
ImageUrl="~/Icon/Lupa2.png"
runat="server" OnClick="btnimg_Click"/>
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
				<asp:GridView ID="GVUsuarios" runat="server" AutoGenerateColumns="False"
					CssClass="table table-striped table-bordered table-hover text-center gridview"
					HeaderStyle-CssClass="thead-dark text-white titCol"
					RowStyle-CssClass="align-middle"
					AlternatingRowStyle-CssClass="align-middle"
					GridLines="None"
					EmptyDataText="No se encontraron usuarios"
					DataKeyNames="IdUsuario"
					OnSelectedIndexChanged="GVUsuarios_SelectedIndexChanged"
					OnRowDeleting="GVUsuarios_RowDeleting"
					OnPageIndexChanging="GVUsuarios_PageIndexChanging"
					OnRowCommand="GVUsuarios_RowCommand"
					AllowPaging="True" PageSize="10">
					<Columns>
						<asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="py-3" />
						<asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderStyle-CssClass="py-3" />
						<asp:TemplateField HeaderText="Rol" HeaderStyle-CssClass="py-3">
							<ItemTemplate>
								<span class='badge <%# Eval("Admin") != null && (bool)Eval("Admin") ? "bg-danger" : "bg-success" %> rounded-pill '>
									<%# Eval("Admin") != null && (bool)Eval("Admin") ? "Administrador" : "Vendedor" %>
								</span>
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
										OnClientClick="return confirm('¿Está seguro que desea eliminar esta usuario?');">
<img src='<%= ResolveUrl("~/Icon/IconBasuraG.png") %>' alt="Eliminar" />
									</asp:LinkButton>

									<asp:LinkButton ID="lnkReactivar" runat="server"
										CommandName="Reactivar"
										CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
										CssClass="btnEdit_Delete"
										ToolTip="Reactivar"
										Visible='<%# !Convert.ToBoolean(Eval("Activo")) %>'
										OnClientClick="return confirm('¿Está seguro que desea reactivar esta usuario?');">
<img src='<%= ResolveUrl("~/Icon/iconAñadir.png") %>' alt="Reactivar" />
									</asp:LinkButton>
								</div>
							</ItemTemplate>

						</asp:TemplateField>


					</Columns>
					<EmptyDataTemplate>
						<div class="alert alert-info text-center py-4">
							No hay usuarios registrados.
						</div>
					</EmptyDataTemplate>

				</asp:GridView>
			</div>
		</div>
	</div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="AltaProveedor.aspx.cs" Inherits="WebForms.AltaProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="Css/StyleListProveedores.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			    <div  class="centered-container"> 
			<asp:Panel ID="pnlAltaProveedor" runat="server" DefaultButton="btnAceptar" CssClass="form-container">
				<div class="container">
					<div class="row justify-content-center">
						<div class="col-md-8 col-lg-6">
							<div class="card shadow-sm mt-5">
								<div class="card-header bg-primary text-white">
									<h2 class="card-title text-center mb-0">Registro de Proveedor</h2>
								</div>
								<div class="card-body">
									<asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" Visible="false"></asp:Label>

									<div class="mb-3">
										<asp:Label ID="lblId" runat="server" Text="ID" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtId" runat="server" CssClass="form-control" disabled="true"></asp:TextBox>
									</div>

									<div class="mb-3">
										<asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRazonSocial_TextChanged"></asp:TextBox>
										<div class="invalid-feedback">Por favor ingrese la razón social</div>
									</div>

									<div class="mb-3">
										<asp:Label ID="lblCuit" runat="server" Text="CUIT" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtCuit" runat="server" MaxLength="13" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
									</div>

									<div class="mb-3">
										<asp:Label ID="lblDireccion" runat="server" Text="Dirección" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDireccion_TextChanged"></asp:TextBox>
									</div>

									<div class="mb-3">
										<asp:Label ID="lblTelefono" runat="server" Text="Teléfono" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtTelefono" runat="server" TextMode="Number" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTelefono_TextChanged"></asp:TextBox>

									</div>

									<div class="mb-4">
										<asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label fw-bold"></asp:Label>
										<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
										<asp:Label ID="lblEmailMensaje" runat="server" Text=""
											Style="color: red; font-size: 17px; font-weight: 500; margin-top: 10px; display: inline-block;" />
									</div>

									<div class="d-grid gap-2 d-md-flex justify-content-md-end">
										<asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
											CssClass="btn btn-outline-secondary me-md-2" OnClick="btnCancelar_Click" />
										<asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
											CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
									</div>
									<asp:Label Text="" runat="server" ID="lblAviso" Style="color: red; font-size: 17px; font-weight: 500; display: inline-block;" />
								</div>
							</div>
						</div>
					</div>
				</div>
			</asp:Panel>
					</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

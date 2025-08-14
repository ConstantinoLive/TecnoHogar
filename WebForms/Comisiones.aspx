<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="Comisiones.aspx.cs" Inherits="WebForms.Comisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="Css/StyleComisiones.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="row mb-4">
		<div class="col-12">
			<h1 class="display-4 text-center mt-5 mb-4">Comisiones</h1>

			<% if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin == true)
				{ %>
			<div class="d-flex justify-content-between mb-3">
				<a href="PanelAdmin.aspx" class="back">
					<img class="imgback" src="/Icon/FlechaI.png" />
				</a>

			</div>
			<% } %>
		</div>
	</div>

	

	  <div class="filters-container">
          <%
               if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin == true)
				{
          %>
          <div class="filter-group">
              <label class="form-label">Usuario</label>
              <asp:DropDownList ID="ddlUsuario" CssClass="form-select form-control-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged" runat="server">
              </asp:DropDownList>
              </div>
          <%} %>
            <div class="filter-group">
                <label class="form-label">Año</label>
                <asp:DropDownList ID="ddlAnio" CssClass="form-select form-control-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged" runat="server">
                    <asp:ListItem Text="-- Seleccionar año --" />
                    <asp:ListItem Text="2025" />
                    <asp:ListItem Text="2026" />
                    <asp:ListItem Text="2027" />
                    <asp:ListItem Text="2028" />
                    <asp:ListItem Text="2029" />
                    <asp:ListItem Text="2030" />
                    <asp:ListItem Text="2031" />
                    <asp:ListItem Text="2032" />
                </asp:DropDownList>
            </div>
            
            <div class="filter-group">
                <label class="form-label">Mes</label>
                <asp:DropDownList ID="ddlMes" CssClass="form-select form-control-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" runat="server">
                    <asp:ListItem Text="-- Seleccionar mes --" Value="0" />
                    <asp:ListItem Text="Enero" Value="1" />
                    <asp:ListItem Text="Febrero" Value="2" />
                    <asp:ListItem Text="Marzo" Value="3" />
                    <asp:ListItem Text="Abril" Value="4" />
                    <asp:ListItem Text="Mayo" Value="5" />
                    <asp:ListItem Text="Junio" Value="6" />
                    <asp:ListItem Text="Julio" Value="7" />
                    <asp:ListItem Text="Agosto" Value="8" />
                    <asp:ListItem Text="Septiembre" Value="9" />
                    <asp:ListItem Text="Octubre" Value="10" />
                    <asp:ListItem Text="Noviembre" Value="11" />
                    <asp:ListItem Text="Diciembre" Value="12" />
                </asp:DropDownList>
            </div>
            
            <div class="commission-info">
                <label>Porcentaje de comisión</label>
                <div class="porcentaje">
                    <asp:Label ID="lblPorcentajeComision" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-8 info">
                <div class="table-container">
                    <asp:Repeater ID="rptComisiones" runat="server">
                        <HeaderTemplate>
                            <table class="commission-table">
                                <thead>
                                    <tr>
                                        <th>Venta</th>
                                        <th>Fecha</th>
                                        <th>Monto Comisión</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("IdVenta.IdVenta") %></td>
                                <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("MontoComision", "{0:C}") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    
                   
                </div>
                 <asp:Label ID="lblNoData" runat="server" CssClass="no-data" >
     No hay comisiones registradas para el período seleccionado
 </asp:Label>
            </div>
            
            <div class="col-lg-4">
                <div class="total-card" style="box-shadow: rgb(38, 57, 77) 0px 20px 30px -10px;">
                    <h3>Total comisionado</h3>
                    <div class="total-amount">
                        <asp:Label ID="lblTotalComision" runat="server"></asp:Label>
                    </div>
                    <p style="font-style:oblique; font-size:12px">Comisiones acumuladas para el mes seleccionado</p>
                </div>
            </div>
        </div>
    
</asp:Content>

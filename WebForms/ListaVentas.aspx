<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="ListaVentas.aspx.cs" Inherits="WebForms.Ventas1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/StyleListVentas.css">
    <link rel="stylesheet" href="Css/StyleListProveedores.css">
    <link rel="stylesheet" href="Css/StyleListMarcas.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mb-4">
        <div class="col-12">
            <h1 class="display-4 text-center mb-4 mt-4">Listado de Ventas</h1>
            <div class="d-flex justify-content-end">
                <!--<asp:Button runat="server" Text="Regresar" ID="btnVolver" OnClick="btnVolver_Click"
                    CssClass="btn btn-outline-secondary btn-lg shadow-sm" />-->

                <%--     <a href="PanelAdmin.aspx" class="back">
                    <img class="imgback" src="/Icon/FlechaI.png"></a>
                
                <asp:Button runat="server" Text="Agregar venta" ID="btnAgregarVenta" OnClick="btnAgregarVenta_Click"
                    CssClass="btn btn-primary btn-lg shadow-sm" />--%>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-12">
            <div class="table-responsive shadow-sm rounded">
                <asp:GridView ID="GVVentas" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered table-hover text-center gridview"
                    HeaderStyle-CssClass="thead-dark text-white titCol">
                    <Columns>
                        <asp:BoundField DataField="IdVenta" HeaderText="IdVenta" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Cliente.Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Cliente.Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Total" HeaderText="Monto" DataFormatString="{0:C}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Factura">
                            <ItemTemplate>
                                <%--<asp:Button ID="btnDetalles" runat="server" Text="Ver Detalles" CssClass="btn btn-primary" PostBackUrl='<%# "VentaDetalles.aspx?ID=" + Eval("IdVenta") %>' />
								<a href='<%# "VentaDetalles.aspx?ID=" + Eval("IdVenta") %>' class="icono" title="Ver Detalles">
									<i class="fa-solid fa-search" style="color: dimgrey; margin: 10px"></i>
								</a>--%>


                                <%--<asp:LinkButton ID="lnkFactura" runat="server"
									CommandName="Select"
									CommandArgument='<%# Eval("IdVenta") %>'
									CssClass="btnEdit_Delete"
									ToolTip="Factura" PostBackUrl='<%# "Factura.aspx?ID=" + Eval("IdVenta") %>'>
                                    <img src='<%= ResolveUrl("~/Icon/Factura2.png") %>' alt="Factura"/>
								</asp:LinkButton>--%>
                                <asp:LinkButton
                                    ID="LinkButton1"
                                    runat="server"
                                    CssClass="btnEdit_Delete"
                                    ToolTip="Factura"
                                    OnClientClick='<%# "window.open(\"Factura.aspx?ID=" + Eval("IdVenta") + "\", \"_blank\", \"width=1000,height=900\"); return false;" %>'>
				                    <img src="/Icon/Factura2.png" style="width: 30px; height: 30px;" alt="Factura" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>

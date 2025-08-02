<%@ Page Title="" Language="C#" MasterPageFile="~/Administradores.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="WebForms.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/StyleCompras.css">
    <script src="<%= ResolveUrl("~/Scripts/Funciones.js") %>"></script>

    <script type="text/javascript">
        function mostrarSeccion(id) {
            var secciones = document.querySelectorAll('#contenedor-secciones > div');
            secciones.forEach(function (sec) {
                sec.style.display = 'none';
            });

            document.getElementById(id).style.display = 'block';
            document.getElementById('<%= hfSeccionActiva.ClientID %>').value = id;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfSeccionActiva" runat="server" />

    <div class="sidebar">
        <ul class="nav flex-column ">
            <li>
                <div class="icon-wrapper">
                    <img src="/Icon/CamionB.png">
                </div>
                <span class="btnMenu">Ordenes de compra</span>
                <ul>
                    <li>
                        <asp:LinkButton ID="lnkOCPendientes" runat="server" OnClick="MostrarSeccion" CommandArgument="OCPendientes">
                    OC Realizadas
                        </asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="lnkNuevaOC" runat="server" OnClick="MostrarSeccion" CommandArgument="NuevaOC">
                    OC Nueva
                        </asp:LinkButton>
                    </li>
                </ul>
            </li>
            <!--<li>
                <div class="icon-wrapper">
                    <img src="/Icon/GraficoB.png">
                </div>
                <asp:LinkButton ID="lnkHistorialPrecios" CssClass="btnMenu" runat="server" OnClick="MostrarSeccion" CommandArgument="HistorialPrecios">
            Historial de precios
        </asp:LinkButton>
            </li>-->
            <li>
                <div class="icon-wrapper">
                    <img src="/Icon/PortafolioB.png">
                </div>
                <asp:LinkButton ID="lnkProveedores" CssClass="btnMenu" runat="server" OnClick="MostrarSeccion" CommandArgument="Proveedores">
            Proveedores
                </asp:LinkButton>
            </li>
            <li>
                <div class="icon-wrapper">
                    <img src="/Icon/TiendaB.png">
                </div>
                <asp:LinkButton ID="lnkProductos" CssClass="btnMenu" runat="server" OnClick="MostrarSeccion" CommandArgument="Productos">
            Productos
                </asp:LinkButton>
            </li>
            <li>
                <div class="icon-wrapper">
                    <img src="/Icon/CampanaB.png">
                </div>
                <asp:Panel ID="pnlNotificacion" runat="server" CssClass="CantNoti" Visible="false">
                    <asp:Label ID="LblNotif" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:LinkButton ID="lnkNotificaciones" CssClass="btnMenu" runat="server" OnClick="MostrarSeccion" CommandArgument="StockCritico">
                Stock crítico
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <!--Pantallas-->
    <asp:UpdatePanel ID="upSecciones" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <a href="PanelAdmin.aspx" class="back">
                <img class="imgback mt-4" src="/Icon/FlechaI.png"></a>
            <div id="contenedor-secciones">
                <!--Pantalla OC pendientes-->
                <div id="OCPendientes" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Ordenes de compra realizadas</h1>
                    <asp:Repeater ID="rptCompras" runat="server">
                        <HeaderTemplate>
                            <table class="table tblOC">
                                <thead class="tblOCcont">
                                    <tr>
                                        <th>IdCompra</th>
                                        <th>Fecha</th>
                                        <th>Proveedor</th>
                                        <th>Monto</th>
                                        <th>Detalles</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="tblOCcont">
                                <td><%# Eval("IdCompra") %></td>
                                <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("Proveedor.RazonSocial") %></td>
                                <td><%# Eval("Total", "{0:C}") %></td>
                                <td>

                                    <asp:ImageButton ID="btnDetalle"
                                        OnClick="btnDetalle_Click"
                                        CssClass="btnEdit_Delete"
                                        CommandArgument='<%# Eval("IdCompra") %>'
                                        ImageUrl="~/Icon/lupa3.png" runat="server" alt="Buscar" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
        </table>
   
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
                <!--Pantalla OC Nueva-->
                <div id="NuevaOC" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Orden de compra</h1>
                    <div class="NewOC">
                        <div class="row">
                            <div class="NOC col">
                                <label>Orden de compra: </label>
                                <asp:Label ID="lblNumOC" runat="server" Text=""></asp:Label>  
                            </div>
                            <div class="FOC col">
                                <label>Fecha</label>
                                <asp:TextBox ID="TxtFecha" CssClass="input" Style="width: 200px;" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label>Proveedor</label>
                            <asp:DropDownList ID="DDLProveedor" CssClass="form-select" Style="width: 300px;" OnSelectedIndexChanged="DDLProveedor_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-10">
                                <label>Producto</label>
                                <asp:DropDownList ID="DDLProducto" CssClass="form-select" Style="width: 600px;" OnSelectedIndexChanged="DDLProducto_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <label>Cantidad</label>

                                <div class="qty" style="width: 100px; display: flex; align-items: center;">
                                    <asp:Button ID="btnMenos" runat="server" Text="-" OnClick="btnMenos_Click" Enabled="false"
                                        Style="width: 30px; height: 30px; font-size: 16px; font-weight: bold; border: none; background-color: transparent; color: #47484b; cursor: pointer; padding: 0;" />

                                    <asp:TextBox ID="txtCantidad" runat="server" Text="0" ReadOnly="true" Enabled="false"
                                        Style="width: 40px; margin: 0 5px; border: none; background-color: transparent; font-weight: bold; font-size: 14px; text-align: center;" />

                                    <asp:Button ID="btnMas" runat="server" Text="+" OnClick="btnMas_Click" Enabled="false"
                                        Style="width: 30px; height: 30px; font-size: 16px; font-weight: bold; border: none; background-color: transparent; color: #47484b; cursor: pointer; padding: 0;" />
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-9">
                                <label>Precio unitario</label>
                                <asp:TextBox ID="TxtBPrecio"
                                    CssClass="input"
                                    Style="width: 300px;"
                                    runat="server"
                                    Text="$"
                                    AutoPostBack="False"></asp:TextBox>
                            </div>
                            <div class="col-3 mt-2">
                                <asp:ImageButton ID="BtnPlus" CssClass="btnimgPlus" OnClick="BtnPlus_Click" ImageUrl="./Icon/plus3.png" runat="server" />
                            </div>
                        </div>
                        <asp:Panel ID="PanelAleta" runat="server" CssClass="alert alert-warning mt-3" Visible="false">
                            <asp:Label ID="lblAlerta2" runat="server" CssClass="mb-0" Text=""></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="PanelAlertaOK" runat="server" CssClass="alert alert-success mt-3" Visible="false">
                            <asp:Label ID="LblAlertaOK" runat="server" CssClass="mb-0" Text=""></asp:Label>
                        </asp:Panel>
                        <div class="tbl">
                            <asp:GridView ID="gvDetalleOC" runat="server" AutoGenerateColumns="False"
                                CssClass="table" OnRowCommand="gvDetalleOC_RowCommand">
                                <Columns>
                                    <%-- <asp:BoundField DataField="IdProducto" HeaderText="ID" Visible="False" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />--%>
                                    <asp:BoundField DataField="Producto.IdProducto" HeaderText="ID" Visible="False" />
                                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CssClass="btnEdit_Delete" ImageUrl="./Icon/IconBasuraG.png"
                                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div>
                            <label>Total: $</label>
                            <asp:Label ID="lblTotal" runat="server" Text="$0.00"></asp:Label>
                        </div>
                        <asp:Button ID="Button1" runat="server" CssClass="btnaceptar" Text="Agregar" OnClick="btnAgregar_Click" />

                    </div>
                </div>
                <!--Pantalla Historial de precios-->
                <div id="HistorialPrecios" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Historial de precios</h1>

                    <div class="container-fluid busqueda">
                        <label>Producto</label>
                        <asp:DropDownList ID="DDLHistPrecios" CssClass="lbhp" runat="server" EnableViewState="true" ViewStateMode="Enabled"></asp:DropDownList>
                        <asp:ImageButton ID="btnimg"
                            CssClass="btnimg_lup"
                            CausesValidation="false"
                            UseSubmitBehavior="true"
                            OnClick="btnimg_Click" ImageUrl="~/Icon/Lupa2.png" runat="server" />
                    </div>

                    <asp:Panel ID="pnlAlerta" runat="server" CssClass="alert alert-warning mt-3" Visible="false">
                        <asp:Label ID="lblAlerta" runat="server" CssClass="mb-0" Text=""></asp:Label>
                    </asp:Panel>


                    <asp:Panel ID="pnlHistorial" runat="server" Visible="false">

                        <asp:Label ID="LblItem" runat="server" Text=""></asp:Label>
                        <asp:Repeater ID="rptHistorial" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Razón Social</th>
                                            <th>Fecha</th>
                                            <th>Precio</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("RazonSocial") %></td>
                                    <td><%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %></td>
                                    <td>$<%# Eval("PrecioUnitario", "{0:N2}") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
            </table>
       
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </div>



                <!--Pantalla Productos-->
                <div id="Productos" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Productos</h1>

                    <div class="tblProd fixed-header-gridview shadow-sm rounded">
                        <asp:GridView ID="GridProductos" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10"
                            CssClass="table table-striped table-bordered table-hover text-center gridview"
                            HeaderStyle-CssClass="thead-dark text-white titCol"
                            RowStyle-CssClass="align-middle"
                            AlternatingRowStyle-CssClass="align-middle"
                            GridLines="None" OnPageIndexChanging="GridProductos_PageIndexChanging"
                            PagerStyle-CssClass="pagination pagination-sm justify-content-center"
                            PageIndex="0">
                            <Columns>
                                <asp:TemplateField HeaderText="✔" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion" runat="server" onclick="actualizarEstadoBotones()" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="TipoProducto.categoria.Nombre" HeaderText="Categoría" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="CodigoArticulo" HeaderText="Código" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" DataFormatString="{0:C2}" />
                                <asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Ganancia" DataFormatString="{0}%" />
                                <asp:BoundField DataField="StockActual" HeaderText="Stock" />
                                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mín" />
                                <asp:TemplateField HeaderText="Imagen">
                                    <ItemTemplate>
                                        <asp:Image ID="imgProducto" runat="server"
                                            ImageUrl='<%# Eval("ImagenUrl") %>'
                                            CssClass="img-thumbnail"
                                            Style="max-width: 80px; height: auto;"
                                            onerror="this.src='https://via.placeholder.com/80?text=Sin+Imagen';" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
                <!--Pantalla Proveedores-->
                <div id="Proveedores" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Proveedores</h1>
                    <div class="tblProd">
                        <div class="table-responsive shadow-sm rounded">
                            <asp:GridView ID="GridProveedores" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                CssClass="table table-striped table-bordered table-hover text-center gridview"
                                HeaderStyle-CssClass="thead-dark text-white titCol"
                                GridLines="None" OnPageIndexChanging="GridProveedores_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="✔" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                                    <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                    <asp:BoundField DataField="Email" HeaderText="E-mail" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                <!--Pantalla Stock crítico-->
                <div id="StockCritico" runat="server">
                    <h1 class="display-4 text-center mt-5 mb-4">Productos con stock crítico</h1>
                    <asp:Panel ID="pnlSinStockCritico" runat="server" CssClass="alert alert-info mt-5" Visible="false">
                        No hay productos con stock crítico en este momento.
		
                    </asp:Panel>
                    <div class="tblProd">
                        <div class="table-responsive shadow-sm rounded">
                            <asp:GridView ID="GVStockCritico" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                CssClass="table table-striped table-bordered table-hover text-center gridview"
                                HeaderStyle-CssClass="thead-dark text-white titCol"
                                GridLines="None" OnPageIndexChanging="GVStockCritico_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="CodigoArticulo" HeaderText="Código" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="TipoProducto" HeaderText="Tipo de producto" />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

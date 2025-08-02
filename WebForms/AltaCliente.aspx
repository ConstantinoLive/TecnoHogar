<%@ Page Title="" Language="C#" MasterPageFile="~/Venta.Master" AutoEventWireup="true" CodeBehind="AltaCliente.aspx.cs" Inherits="WebForms.AltaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/StyleListClientes.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div  class="centered-container"> 
            <asp:Panel ID="pnlAltaCliente" runat="server" DefaultButton="btnAceptar" CssClass="form-container">
                
                    <h2 style="text-align: center; margin-bottom: 25px; color: #2c3e50; font-weight: 600; font-size: 28px;">Registro de Cliente</h2>

                    <asp:Label ID="lblMensaje" runat="server" ForeColor="#e74c3c" Style="display: block; margin-bottom: 20px; text-align: center;"></asp:Label>

                    <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px; margin-bottom: 20px;">
                        <div>
                            <asp:Label ID="lblId" runat="server" Text="ID Cliente" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Width="100%" disabled="true" OnTextChanged="txtId_TextChanged" AutoPostBack="true"
                                Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; background-color: #f8f9fa; color: #95a5a6;" />
                        </div>
                        <div>
                            <asp:Label ID="lblDni" runat="server" Text="DNI" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                            <asp:TextBox ID="txtDni" MaxLength="8" textmode="number" OnTextChanged="txtDni_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" Width="100%"
                                Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                            <asp:Label ID="lblDniMensaje" runat="server" Text=""
                                Style="color: red; font-size: 17px; font-weight: 500; margin-top: 10px; display: inline-block;" />
                        </div>
                    </div>

                    <div style="margin-bottom: 20px;">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre/Razón Social" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" AutoPostBack="true" OnTextChanged="txtNombre_TextChanged" CssClass="form-control" Width="100%"
                            Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                    </div>

                    <div style="margin-bottom: 20px;">
                        <asp:Label ID="lblApellido" runat="server" Text="Apellido" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                        <asp:TextBox ID="txtApellido" runat="server" OnTextChanged="txtApellido_TextChanged" AutoPostBack="true" CssClass="form-control" Width="100%"
                            Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                    </div>

                    <div style="margin-bottom: 20px;">
                        <asp:Label ID="lblDireccion" runat="server" Text="Dirección" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDireccion_TextChanged" Width="100%"
                            Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                    </div>

                    <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px; margin-bottom: 25px;">
                        <div>
                            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server"  CssClass="form-control" Width="100%" OnTextChanged="txtTelefono_TextChanged" AutoPostBack="true"
                                Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                            <asp:Label ID="lblTelefonoMensaje" runat="server" Text=""
                                Style="color: red; font-size: 17px; font-weight: 500; margin-top: 10px; display: inline-block;" />
                        </div>
                        <div>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" Style="display: block; margin-bottom: 5px; color: #7f8c8d; font-size: 14px; font-weight: 500;"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged"
                                Style="padding: 10px; border: 1px solid #ddd; border-radius: 6px; transition: border 0.3s;" />
                            <asp:Label ID="lblEmailMensaje" runat="server" Text=""
                                Style="color: red; font-size: 17px; font-weight: 500; margin-top: 10px; margin-left: 3px; display: inline-block;" />
                        </div>
                    </div>

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="btn btn-outline-secondary me-md-2" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
                            CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                    </div>
                    <%--<div style="display: flex; gap: 15px; margin-top: 30px;">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"
                            Style="flex: 1; padding: 12px; background: linear-gradient(135deg, #3498db, #2980b9); color: white; border: none; border-radius: 6px; font-weight: 600; cursor: pointer; transition: all 0.3s;"
                            onmouseover="this.style.background='linear-gradient(135deg, #2980b9, #3498db)'; this.style.transform='translateY(-1px)';"
                            onmouseout="this.style.background='linear-gradient(135deg, #3498db, #2980b9)'; this.style.transform='translateY(0)';" />

                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            Style="flex: 1; padding: 12px; background: linear-gradient(135deg, #e74c3c, #c0392b); color: white; border: none; border-radius: 6px; font-weight: 600; cursor: pointer; transition: all 0.3s;"
                            onmouseover="this.style.background='linear-gradient(135deg, #c0392b, #e74c3c)'; this.style.transform='translateY(-1px)';"
                            onmouseout="this.style.background='linear-gradient(135deg, #e74c3c, #c0392b)'; this.style.transform='translateY(0)';" />
                    </div>--%>
                    <asp:Label ID="lblAviso" runat="server" Text=""
                        Style="color: red; font-size: 17px; font-weight: 500; margin-top: 10px; display: inline-block;" />
                
            </asp:Panel>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

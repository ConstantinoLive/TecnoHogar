<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebForms.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/StyleError.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="error-container text-center">

        <div class="error-icon">
            <i class="bi bi-exclamation-triangle-fill"></i>
        </div>


        <h1 class="mb-3">¡Ups! Algo salió mal</h1>


        <asp:Label ID="lblMensaje" runat="server" CssClass="fs-5 text-muted mb-4 d-block" Text=""></asp:Label>

        <%if (Session["Usuario"] == null)
            {  %>;
        <a href="Default.aspx" class="btn-home">
            <i class="bi bi-house-door me-2"></i>Volver al inicio
</a>
        <%}

            else if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin)
            {  %>;
       
        <a href="PanelAdmin.aspx" class="btn-home">
            <i class="bi bi-house-door me-2"></i>Volver al inicio
</a>
        <%}
            else
            {  %>
        <a href="AltaVenta.aspx" class="btn-home">
            <i class="bi bi-house-door me-2"></i>Volver al inicio
</a>
        <%} %>
        <%--        <%if (Session["Usuario"] != null && ((Dominio.Usuario)Session["Usuario"]).Admin)
            {  %>;
        <a href="PanelAdmin.aspx" class="btn-home">
            <i class="bi bi-house-door me-2"></i>Volver al inicio
        </a>
        <%}
            else
            {  %>
        <a href="Default.aspx" class="btn-home">
            <i class="bi bi-house-door me-2"></i>Volver al inicio
        </a>
        <%} %>--%>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="WebForms.Confirmacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/StyleError.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="error-container text-center">

        <div class="success-icon">
            <i class="bi bi-check-circle-fill"></i>
        </div>


        <h3 class="mb-3">¡Email enviado a <strong><%=(Session["EmailUsuario"])%></strong>!</h3>


        <asp:Label ID="lblMensaje" runat="server"
            CssClass="fs-5 text-muted mb-4 d-block"
            Text="Revisa tu bandeja de entrada y la carpeta de spam."></asp:Label>


        <div class="d-flex gap-3 justify-content-center">
            <a href="Default.aspx" class="btn-home">
                <i class="bi bi-house-door me-2"></i>Volver al inicio
            </a>
            <asp:Button ID="btnReenviar" runat="server"
                CssClass="btn btn-outline-primary"
                Text="Reenviar email"
                OnClick="btnReenviar_Click" />

        </div>


        <p class="text-muted mt-3">Serás redirigido en <span id="countdown">10</span> segundos...</p>
    </div>
    <script>
        let seconds = 10;
        const timer = setInterval(() => {
            document.getElementById('countdown').textContent = seconds;
            if (seconds-- <= 0) {
                clearInterval(timer);
                window.location.href = 'Default.aspx';
            }
        }, 1000);
    </script>
</asp:Content>


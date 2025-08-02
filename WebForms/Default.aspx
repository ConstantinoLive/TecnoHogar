<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
	<link rel="stylesheet" href="Style_default.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<!--<div class="d-flex justify-content-center align-content-center">-->
	<!--<img src="/Image/Logo3.png" alt="Logo" style="width: 100%;">-->
	<%if (animation)
		{ %>
	<div class="Lgd" style="margin-left:150px; margin-top:100px;">
		<img src="/Image/Tecno.png" class="animate__animated animate__slideInLeft tecno" alt="Tecno">
		<img src="/Image/LogoSinMarca.png" class="animate__animated animate__zoomIn logo" alt="Logo">
		<img src="/Image/Hogar.png" class="animate__animated animate__slideInRight hogar" alt="Hogar">
	</div>
	<%}
		else
		{ %>
	<div class="Lgd" style="margin-left:150px; margin-top:100px;">
	<img src="/Image/Tecno.png" class="tecno" alt="Tecno">
	<img src="/Image/LogoSinMarca.png" class="logo" alt="Logo">
	<img src="/Image/Hogar.png" class="hogar" alt="Hogar">
		</div>
		<%} %>

	<!--</div>-->
        
   

</asp:Content>


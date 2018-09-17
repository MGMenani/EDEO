<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoValidacion.aspx.cs" Inherits="EDEO.MedicoValidacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Validación</title>
    <link href="http://fonts.googleapis.com/css?family=Oswald:400,300" rel="stylesheet" type="text/css" />
    <link href="PrincipalStyle.css" rel="stylesheet" type="text/css" media="screen" />

</head>
<body>
<div id="wrapper">
	<div id="menu-wrapper">
		<div id="menu" class="container">
			<ul>
				<li><asp:HyperLink runat="server" ID="lnkEstimacion" NavigateUrl="~/MedicoEstimacion.aspx">Estimación</asp:HyperLink></li>
				<li class="current_page_item"><asp:HyperLink runat="server" ID="lnkValidacion" NavigateUrl="~/MedicoValidacion.aspx">Validación</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkHistorial" NavigateUrl="~/Login.aspx">Historial</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkPacientes" NavigateUrl="~/MedicoPacientes.aspx">Pacientes</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkLogin" NavigateUrl="~/Login.aspx">Salir</asp:HyperLink></li>
			</ul>
		</div>
	</div>
	<div id="header">
		<div id="logo" class="container">
			<h1><a href="#">Validación </a></h1>
		</div>
	</div>	
</div>

</body>

</html>

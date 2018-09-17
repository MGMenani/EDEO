<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoEstimacion.aspx.cs" Inherits="EDEO.MedicoEstimacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Estimación</title>
    <link href="http://fonts.googleapis.com/css?family=Oswald:400,300" rel="stylesheet" type="text/css" />
    <link href="PrincipalStyle.css" rel="stylesheet" type="text/css" media="screen" />

</head>
<body>
<div id="wrapper">
	<div id="menu-wrapper">
		<div id="menu" class="container">
			<ul>
				<li class="current_page_item"><asp:HyperLink runat="server" ID="lnkEstimacion" NavigateUrl="~/MedicoEstimacion.aspx">Estimación</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkValidacion" NavigateUrl="~/MedicoValidacion.aspx">Validación</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkHistorial" NavigateUrl="~/Login.aspx">Historial</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkPacientes" NavigateUrl="~/MedicoPacientes.aspx">Pacientes</asp:HyperLink></li>
				<li><asp:HyperLink runat="server" ID="lnkLogin" NavigateUrl="~/Login.aspx">Salir</asp:HyperLink></li>
			</ul>
		</div>
	</div>
	<div id="header">
		<div id="logo" class="container">
			<h1><a href="#">Estimación de edad ósea </a></h1>
		</div>
	</div>
	<div id="page" class="container">
		<div id="content">
				<h2><a href="#">Bienvenido a EDEO </a></h2>
				<div class="entry">
					<p>Esto es una <strong>hablada</strong>.</p>
			</div>
		</div>
		<!-- end #content -->
		<div id="sidebar1">
			<div>
				<h2>Título1</h2>
				<ul class="list-style2">
					<li class="first"><a href="#">Algo</a></li>
					<li><a href="#">Algo</a></li>
				</ul>
			</div>
		</div>
		<div id="sidebar2">
			<div>
				<h2>Título2</h2>
				<ul class="list-style2">
					<li class="first"><a href="#">Algo</a></li>
					<li><a href="#">Algo</a></li>
				</ul>
			</div>
		</div>
		<!-- end #sidebar -->
	</div>
	<!-- end #page -->
	
</div>
<div id="footer-content" class="container">
	<div id="footer-bg">
		<div id="column1">
			<h2>Untitled Inc.</h2>
			<p>&copy; Untitled</p>
			<p>All rights reserved.</p>
			<p>Design by <a href="http://templated.co" rel="nofollow">TEMPLATED</a>. Photos by <a href="http://fotogrph.com/">Fotogrph</a>.</p>
		</div>
		<div id="column2">
			<h2>Rhoncus volutpat</h2>
			<ul class="list-style3">
				<li class="first"><a href="#">Pellentesque consectetuer gravida blandit.</a></li>
				<li><a href="#">Lorem consectetuer adipiscing elit.</a></li>
				<li><a href="#">Phasellus pellentesque conguen lectus</a></li>
				<li><a href="#">Cras vitae pellentesque pharetra.</a></li>
				<li><a href="#">Maecenas vitae vitae feugiat eleifend.</a></li>
				<li><a href="#">Pellentesque consectetuer gravida blandit.</a></li>
			</ul>
		</div>
		<div id="column3">
			<h2>Recommended Links</h2>
			<ul class="list-style3">
				<li class="first"><a href="#">Pellentesque consectetuer gravida blandit.</a></li>
				<li><a href="#">Lorem consectetuer adipiscing elit.</a></li>
				<li><a href="#">Phasellus pellentesque conguen lectus</a></li>
				<li><a href="#">Cras vitae pellentesque pharetra.</a></li>
				<li><a href="#">Maecenas vitae vitae feugiat eleifend.</a></li>
				<li><a href="#">Pellentesque consectetuer gravida blandit.</a></li>
			</ul>
		</div>
		<div id="column4">
			<h2>Social</h2>
			<ul class="list-style3">
				<li class="first"><a href="#">Twitter</a></li>
				<li><a href="#">Facebook</a></li>
				<li><a href="#">Flickr</a></li>
				<li><a href="#">Google +</a></li>
				<li><a href="#">Instagram</a></li>
				<li><a href="#">RSS</a></li>
			</ul>
		</div>
	</div>
</div>
<!-- end #footer -->
</body>
</html>

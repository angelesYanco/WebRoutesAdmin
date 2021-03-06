﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUbicaciones.aspx.cs" Inherits="WebRoutesAdmin.AspNet.frmUbicaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- 1. Bootstrap stuff -->
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <!-- 1. Fin-->

    <!-- 2. Location picker -->
    <script src='https://maps.google.com/maps/api/js?libraries=places'></script>
    <script src="js/main.js"></script>
    <script src="js/locationpicker.jquery.js"></script>
    <%--<script src="js/main.js"></script>--%>

    <!-- 2. Fin --> 
    <title>Creación de Rutas</title>
</head>
<body>

    <form id="form1" runat="server">
        <!--Creamos nuestro contenedor-->
        <div class="container">

            <div class="row">

                <!--Aqui ponemos la funcionalidad de google maps-->
                <div class="col-sm-4">
                    <br />
                    <!--Caja de texto para la busqueda de direcciones-->
                    <div class="form-group">

                        <label for="exampleInputEmail1">Pedido</label>
                        <asp:TextBox ID="txtPedido" CssClass="form-control" runat="server"></asp:TextBox>

                        <label for="exampleInputEmail1">Ubicación</label>
                        <asp:HiddenField ID="txtID" runat="server" />
                        <asp:TextBox ID="txtUbicacion" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Latitud y longitud -->
                    <div class="form-group">
                        <label for="exampleInputPassword1">Lat.:</label>
                        <asp:TextBox ID="txtLat" Text="19.3508698" CssClass="form-control" runat="server"></asp:TextBox>
                        <label for="exampleInputPassword1">Long.:</label>
                        <asp:TextBox ID="txtLong" Text="-99.0796852" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Controles de altas, cambios, bajas y limpiar-->
                    <div class="btn-group-sm">
                        <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar" UseSubmitBehavior="False" OnClick="Agregar_Registro" />
                        <asp:Button ID="btnModificar" CssClass="btn btn-warning" runat="server" Text="Modificar" UseSubmitBehavior="False" Enabled="False" OnClick="Modificar_Registro"/>
                        <asp:Button ID="btnEliminar" CssClass="btn btn-danger" runat="server" Text="Eliminar" UseSubmitBehavior="False" Enabled="False" OnClick="Eliminar_Registro"/>
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-default" runat="server" Text="Limpiar" UseSubmitBehavior="False" OnClick="LimpiarPL" />
                    </div>

                </div>

                <div class="col-sm-8">
                    <br />
                    <!--Nuestro mapa-->
                    <div class="form-group">
                        <div id="mapPreview" style="width: 100%; height: 300px"></div>
                    </div>
                </div>

            </div>     

            <div>
                <!--TODO: Aqui ponemos el grid view y los filtros necesarios para filtrar las rutas.-->
                <h1>Ubicaciones</h1>
                <asp:GridView ID="gvwUbicaciones" runat="server" CssClass="table table table-bordered table-striped" OnRowCommand="Seleccion_Registro">
                    <Columns>
                        <asp:ButtonField CommandName="btnSeleccionar" Text="Seleccionar">
                        <ControlStyle CssClass="btn btn-info" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </form>

    <!--TODO: pasar los scripts a un archivo externo js-->
    <script>
        $('#mapPreview').locationpicker({
            radius: 0,
            location: {
                latitude: $('#<%=txtLat.ClientID%>').val(),
                longitude: $('#<%=txtLong.ClientID%>').val()
            },
            inputBinding: {
                latitudeInput: $('#<%=txtLat.ClientID%>'),
                longitudeInput: $('#<%=txtLong.ClientID%>'),
                locationNameInput: $('#<%=txtUbicacion.ClientID%>')
            },
            enableAutocomplete: true
        });

    </script>

</body>
</html>

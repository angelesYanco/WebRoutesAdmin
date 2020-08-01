<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUbicaciones.aspx.cs" Inherits="WebRoutesAdmin.AspNet.frmUbicaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- 1. Bootstrap stuff -->
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <!-- 1. Fin-->

    <!-- 2. Location picker -->
    <script type="text/javascript" src='https://maps.google.com/maps/api/js?sensor=false&libraries=places&key='></script>
    <script src="js/locationpicker.jquery.js"></script>

    <!-- 2. Fin -->
    <title>Creacion de Ruta</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-4">

                    <div class="form-group">
                        <label for="exampleInputEmail1">Ubicación</label>
                        <asp:HiddenField ID="txtID" runat="server" />
                        <asp:TextBox ID="txtUbicacion" Text="Porrua" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <div id="ModalMapPreview" style="width: 100%; height: 300px"></div>
                    </div>

                    <!-- Latitud y longitud -->
                    <div class="form-group">
                        <label for="exampleInputPassword1">Direccion:</label>
                        <asp:TextBox ID="txtDireccion" Text="Porrua" CssClass="form-control" runat="server"></asp:TextBox>
                        <label for="exampleInputPassword1">Lat.:</label>
                        <asp:TextBox ID="txtLat" Text="19.3508698" CssClass="form-control" runat="server"></asp:TextBox>
                        <label for="exampleInputPassword1">Long.:</label>
                        <asp:TextBox ID="txtLong" Text="-99.0796852" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Controles de altas y bajas-->
                    <div class="btn-group">
                        <asp:Button ID="btnAgregar" CssClass="btn btn-succes" runat="server" Text="Agregar" UseSubmitBehavior="false" />
                        <asp:Button ID="btnModificar" CssClass="btn btn-warning" runat="server" Text="Modificar" UseSubmitBehavior="false" Enabled="false"/>
                        <asp:Button ID="btnEliminar" CssClass="btn btn-danger" runat="server" Text="Eliminar" UseSubmitBehavior="false" Enabled="false"/>
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-default" runat="server" Text="Limpiar" UseSubmitBehavior="false" />
                    </div>

                </div>
                <div class="col-md-8">
                    <br />
                    <h1>Ubicaciones</h1>
                </div>

            </div>
        </div>
    </form>
    
    <script>
        $('#ModalMapPreview').locationpicker({
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

            enableAutocomplete: true,
            enableReverseGeocode: true
        });

    </script>

</body>
</html>

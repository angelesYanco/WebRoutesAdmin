using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace WebRoutesAdmin.Win
{
    public partial class frmRutas : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        // Ruta automatizada
        bool trazarRuta = false;
        int contadorIndicadoresRuta = 0;
        PointLatLng inicial;
        PointLatLng final;

        int filaSeleccionada = 0;
        double LatInicial = 19.3509128151425;
        double LngInicial = -99.0796852111816;

        public frmRutas()
        {
            InitializeComponent();
            
        }
        private void frmRutas_Load(object sender, EventArgs e)
        {
            //Configuracion Inicial del mapa.
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 20;
            gMapControl1.AutoScroll = true;

            //Marcador
            markerOverlay = new GMapOverlay("Mrcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green);
            markerOverlay.Markers.Add(marker);

            //Agregamos un tooltip de texto a los marcadores.
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud: {1}", LatInicial, LngInicial);

            //Ahora agregamos el mapa y el marcador al map control
            gMapControl1.Overlays.Add(markerOverlay);

            //Agregamos datos al datagridview mediante un data table
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("Lat", typeof(double)));
            dt.Columns.Add(new DataColumn("Long", typeof(double)));

            //Insertamos datos al dt para mostrar en la lista
            dt.Rows.Add("Porrua Iz", LatInicial, LngInicial);
            dataGridView1.DataSource = dt;

            //Ocultamos las columnas de latitud y longitud
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;

        }
        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaSeleccionada = e.RowIndex;
            //Recuperamos los datos del grid y los asignamos al textbox
            txtDescripcion.Text = dataGridView1.Rows[filaSeleccionada].Cells[0].Value.ToString();
            txtLatitud.Text = dataGridView1.Rows[filaSeleccionada].Cells[1].Value.ToString();
            txtLongitud.Text = dataGridView1.Rows[filaSeleccionada].Cells[2].Value.ToString();

            //Se asignan los valores del grid al marcador.
            marker.Position = new PointLatLng(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(txtLongitud.Text));
            gMapControl1.Position = marker.Position;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }
        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Se obtiene los datos de latitud y longitud
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            // Se posicionan en el txt de latitud y longitud
            txtLatitud.Text = lat.ToString();
            txtLongitud.Text = lng.ToString();

            // Creamos el marcador para moverlo al lugar indicado
            marker.Position = new PointLatLng(lat, lng);

            // Tambien se agrega el mensaje al marcador(tooltp)
            marker.ToolTipText = string.Format("Ubicación \n Latitud: {0} \n Longitud: {1}", lat, lng);

            CrearDireccionTrazarRuta(lat, lng);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Validamos que la ubicacion ya exita en el data grid
            string mensaje = string.Format("Ubicación \n Latitud: {0} \n Longitud: {1}", txtLatitud.Text.ToString(), txtLongitud.Text.ToString());
            decimal lat = Convert.ToDecimal(txtLatitud.Text);
            decimal lng = Convert.ToDecimal(txtLongitud.Text);

            lat = decimal.Round(lat, 10);

            if (!buscar_lat_lng(lat, lng))
            {
                dt.Rows.Add(txtDescripcion.Text, txtLatitud.Text, txtLongitud.Text);
                return;
            }

            MessageBox.Show(mensaje);
            //TODO: agregar procedimiento para insertar en al base de datos.
        }
        private bool buscar_lat_lng(decimal latitud, decimal longitud)
        {
            bool existe = false;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells[1].Value.ToString() == latitud.ToString() && 
                    fila.Cells[2].Value.ToString() == longitud.ToString())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(filaSeleccionada);

            //TODO: agregar procedimiento para dar de baja en la base de datos.
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GMapOverlay Poligono = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();

            //variables para almacenar datos
            double lng, lat;
            //Agarramos los datos del grid
            for(int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }

            GMapPolygon poligonoPuntos = new GMapPolygon(puntos, "Poligono");
            Poligono.Polygons.Add(poligonoPuntos);
            gMapControl1.Overlays.Add(Poligono);
            // atualizamos el mapa
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
        }
        private void btnRuta_Click(object sender, EventArgs e)
        {
            GMapOverlay Ruta = new GMapOverlay("CapaRuta");
            List<PointLatLng> puntos = new List<PointLatLng>();

            //variables para almacenar datos
            double lng, lat;
            //Agarramos los datos del grid
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }

            GMapRoute PoligonosRuta = new GMapRoute(puntos, "Poligono");
            Ruta.Routes.Add(PoligonosRuta);
            gMapControl1.Overlays.Add(Ruta);

            // atualizamos el mapa
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
        }
        private void btnComoLLegar_Click(object sender, EventArgs e)
        {
            //trazarRuta = true;            
            //btnComoLLegar.Enabled = false;

            //CrearDireccionTrazarRuta();
            CrearRuta_01();
        }
        public void CrearDireccionTrazarRuta(double lat, double lng)
        {
            List<PointLatLng> puntos = new List<PointLatLng>();

            if (trazarRuta)
            {
                switch (contadorIndicadoresRuta)
                {
                    case 0: // primer marcador o inicio de la ruta
                        contadorIndicadoresRuta++;
                        inicial = new PointLatLng(lat, lng);
                        break;
                    case 1: //segudo marcador o final de la ruta
                        contadorIndicadoresRuta++;
                        final = new PointLatLng(lat, lng);
                        GDirections direccion;
                        puntos = wayPoints();

                        var RutasDireccion = GMapProviders.GoogleMap.GetDirections(
                            out direccion, inicial, puntos, final, false, false, false, false, false);
                        GMapRoute RutaObtenida = new GMapRoute(direccion.Route, "Ruta ubicación");
                        GMapOverlay CapaRutas = new GMapOverlay("Capa de la ruta");
                        CapaRutas.Routes.Add(RutaObtenida);
                        gMapControl1.Overlays.Add(CapaRutas);
                        gMapControl1.Zoom = gMapControl1.Zoom + 1;
                        gMapControl1.Zoom = gMapControl1.Zoom - 1;
                        contadorIndicadoresRuta = 0;
                        trazarRuta = false;
                        btnComoLLegar.Enabled = true;

                        break;
                }
            }
        }
        public void CrearDireccionTrazarRuta()
        {
            double lat = 0;
            double lng = 0;

            List<PointLatLng> puntosIntermedios;

            //Sabemos que el origen y el fin seran el mismo punto
            inicial = new PointLatLng(LatInicial, LngInicial);
            lat = 19.435792;
            lng = -99.131586;
            final = new PointLatLng(lat, lng);

            try
            {
                //Obtenemos los puntos intermedios
                puntosIntermedios = new List<PointLatLng>();
                puntosIntermedios = wayPoints();

                //Creamos la ruta
                GDirections direcciones = new GDirections();
                
                //var RutasDireccion = GMapProviders.GoogleMap.GetDirections(
                //    out direcciones, inicio, puntosIntermedios, final, false, false, false, false, false);

                var RutasDireccion = GMapProviders.GoogleMap.GetDirections(
                    out direcciones, inicial, final, false, false, false, false, false);

                GMapRoute RutaObtenida = new GMapRoute(direcciones.Route, "Ruta ubicación");
                GMapOverlay CapaRutas = new GMapOverlay("Capa de la ruta");
                CapaRutas.Routes.Add(RutaObtenida);
                gMapControl1.Overlays.Add(CapaRutas);
                gMapControl1.Zoom = gMapControl1.Zoom + 1;
                gMapControl1.Zoom = gMapControl1.Zoom - 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.ToString() , this.Text, MessageBoxButtons.OK);
            }
            
        }
        
        public List<PointLatLng> wayPoints()
        {
            List<PointLatLng> puntos = new List<PointLatLng>();

            //variables para almacenar datos
            double lng, lat;

            //Leemos los datos del grid
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }

            return puntos;
        }
    }
}

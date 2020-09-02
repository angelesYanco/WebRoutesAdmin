using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRoutesAdmin.BLL;
using WebRoutesAdmin.DAL;

namespace WebRoutesAdmin.AspNet
{
    public partial class frmUbicaciones : System.Web.UI.Page
    {
        DAL.Ubicaciones _UbicacionesDAL;
        BLL.Ubicaciones _UbicacionesBLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarUbicaciones();
            }
        }

        protected void btnPosicion_Click(object sender, EventArgs e)
        {
            
        }

        public void ListarUbicaciones()
        {
            _UbicacionesDAL = new DAL.Ubicaciones();
            gvwUbicaciones.DataSource = _UbicacionesDAL.Listar();
            gvwUbicaciones.DataBind();
            LimpiarCampos();
        }

        public BLL.Ubicaciones datosUbicacion()
        {
            int id = 0;
            int.TryParse(txtID.Value, out id);

            int pedido = 0;
            int.TryParse(txtPedido.Text, out pedido);

            //Recolectamos los datos de PL
            _UbicacionesBLL = new BLL.Ubicaciones();
            _UbicacionesBLL.ID = id;
            _UbicacionesBLL.Pedido = pedido;
            _UbicacionesBLL.Ubicacion = txtUbicacion.Text;
            _UbicacionesBLL.Longitud = txtLong.Text;
            _UbicacionesBLL.Latitud = txtLat.Text;

            return _UbicacionesBLL;
        }

        protected void Agregar_Registro(object sender, EventArgs e)
        {
            _UbicacionesDAL = new DAL.Ubicaciones();
            _UbicacionesDAL.Agregar(datosUbicacion());
            ListarUbicaciones();
        }

        protected void Seleccion_Registro(object sender, GridViewCommandEventArgs e)
        {
            int fila = int.Parse(e.CommandArgument.ToString());
            
            txtID.Value = gvwUbicaciones.Rows[fila].Cells[1].Text;
            txtPedido.Text = gvwUbicaciones.Rows[fila].Cells[2].Text;
            txtUbicacion.Text = gvwUbicaciones.Rows[fila].Cells[3].Text;
            txtLat.Text = gvwUbicaciones.Rows[fila].Cells[4].Text;
            txtLong.Text = gvwUbicaciones.Rows[fila].Cells[5].Text;

            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
        }

        protected void Eliminar_Registro(object sender, EventArgs e)
        {
            _UbicacionesDAL = new DAL.Ubicaciones();
            _UbicacionesDAL.Eliminar(datosUbicacion());
            ListarUbicaciones();
        }

        protected void Modificar_Registro(object sender, EventArgs e)
        {
            _UbicacionesDAL = new DAL.Ubicaciones();
            _UbicacionesDAL.Modificar(datosUbicacion());
            ListarUbicaciones();
        }

        public void LimpiarCampos()
        {
            txtID.Value = null;
            txtLat.Text = "19.3508698";
            txtLong.Text = "-99.0796852";
            txtUbicacion.Text = "Porrua Iztapalapa";
            txtPedido.Text = "";

            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnAgregar.Enabled = true;


        }

        protected void LimpiarPL(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
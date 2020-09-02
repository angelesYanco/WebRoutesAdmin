using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using WebRoutesAdmin.BLL;

namespace WebRoutesAdmin.DAL
{
    public class Ubicaciones
    {
        private SqlDBHelper _oConexion;

        public Ubicaciones() {
            _oConexion = new SqlDBHelper();
        }
        public bool Agregar(BLL.Ubicaciones ubicacionesBLL) {
            bool agregar;
            SqlCommand cmdDireciones;

            try
            {
                cmdDireciones = new SqlCommand();
                cmdDireciones.CommandText = 
                    "insert into DIRECCIONES "+
                    "(Pedido, Ubicacion, Latitud, Longitud) values " +
                    "(@Pedido, @Ubicacion, @Latitud, @Longitud) ";
                cmdDireciones.Parameters.Add("@Pedido", SqlDbType.VarChar).Value = ubicacionesBLL.Pedido;
                cmdDireciones.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = ubicacionesBLL.Ubicacion;
                cmdDireciones.Parameters.Add("@Latitud", SqlDbType.VarChar).Value = ubicacionesBLL.Latitud;
                cmdDireciones.Parameters.Add("@Longitud", SqlDbType.VarChar).Value = ubicacionesBLL.Longitud;

                agregar =  _oConexion.EjecutarComandoSQL(cmdDireciones);
                
            }
            catch (Exception)
            {
                throw;
            }

            return agregar;
        }
        public bool Eliminar(BLL.Ubicaciones ubicacionesBLL) {
            bool eliminar;
            SqlCommand cmdEliminar;

            try
            {
                cmdEliminar = new SqlCommand();
                cmdEliminar.CommandText = "delete from DIRECCIONES where ID = @ID";
                cmdEliminar.Parameters.Add("@ID", SqlDbType.Int).Value = ubicacionesBLL.ID;
                eliminar = _oConexion.EjecutarComandoSQL(cmdEliminar);
            }
            catch (Exception)
            {
                throw;
            }

            return eliminar;
        }
        public bool Modificar(BLL.Ubicaciones ubicacionesBLL) {
            bool modificar;
            SqlCommand cmdModificar;

            try
            {
                cmdModificar = new SqlCommand();
                cmdModificar.CommandText = "update DIRECCIONES set " +
                    " ubicacion = @ubicacion, latitud=@latitud, longitud = @longitud " +
                    " where ID = @ID";
                cmdModificar.Parameters.Add("@ubicacion", SqlDbType.VarChar).Value = ubicacionesBLL.Ubicacion;
                cmdModificar.Parameters.Add("@latitud", SqlDbType.VarChar).Value = ubicacionesBLL.Latitud;
                cmdModificar.Parameters.Add("@longitud", SqlDbType.VarChar).Value = ubicacionesBLL.Longitud;
                cmdModificar.Parameters.Add("@ID", SqlDbType.Int).Value = ubicacionesBLL.ID;
                modificar = _oConexion.EjecutarComandoSQL(cmdModificar);
            }
            catch (Exception)
            {
                throw;
            }

            return modificar;
        }
        public DataTable Listar() {
            DataTable dtDirecciones = new DataTable();
            SqlCommand cmdDireciones;

            try
            {
                cmdDireciones = new SqlCommand();
                cmdDireciones.CommandText = "select id, pedido, ubicacion, latitud, longitud " + 
                    "from DIRECCIONES order by id desc";
                cmdDireciones.CommandType = CommandType.Text;
                dtDirecciones = _oConexion.EjecutarSentenciaSQL(cmdDireciones);
                dtDirecciones.Columns[2].ColumnName = "Ubicación";
            }
            catch (Exception)
            {
                throw;
            }

            return dtDirecciones;
        }

    }
}

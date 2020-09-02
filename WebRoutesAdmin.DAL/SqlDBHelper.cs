using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WebRoutesAdmin.DAL
{
    public class SqlDBHelper
    {
        private DataTable _tabla;
        private SqlConnection _sqlConexion;
        private SqlCommand _sqlCmd;

        public SqlDBHelper()
        {
            _sqlConexion = new SqlConnection(
            "Data Source=.\\SQLEXPRESS;Initial Catalog=BDUbicaciones;Integrated Security=True");
            _sqlCmd = new SqlCommand();
    }

        public bool EjecutarComandoSQL(SqlCommand sqlCommand) {
            bool respuesta;

            try
            {
                _sqlCmd = sqlCommand;
                _sqlCmd.Connection = _sqlConexion;
                _sqlConexion.Open();
                respuesta = _sqlCmd.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlConexion.Close();
            }

            return respuesta;
        }
        public DataTable EjecutarSentenciaSQL(SqlCommand sqlCommand) {

            if (_tabla is null)
            {
                _tabla = new DataTable();
            }else if(_tabla.Rows.Count > 0)
            {
                _tabla.Rows.Clear();
            }
            
            try
            {
                _sqlCmd = sqlCommand;
                _sqlCmd.Connection = _sqlConexion;
                _sqlConexion.Open();                
                _tabla.Load(_sqlCmd.ExecuteReader()); 
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlConexion.Close();
            }

            return _tabla;

        }

    }
}

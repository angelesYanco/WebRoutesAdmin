using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebRoutesAdmin.BLL
{
    public class Ubicaciones
    {
        public int ID { get; set; }
        public int Pedido { get; set; }
        public string Ubicacion { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }

    }
}

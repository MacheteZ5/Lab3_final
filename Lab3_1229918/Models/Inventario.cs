using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3_1229918.Models
{
    public class Inventario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripción { get; set; }
        public string Precio { get; set; }
        public string CasaFarmaceutica { set; get; }
    }
}
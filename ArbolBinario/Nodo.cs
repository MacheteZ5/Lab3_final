using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBinario
{
    public class Medicamentos
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripción { get; set; }
        public string Precio { get; set; }
        public string CasaFarmaceutica { set; get; }
    }

    public class Nodo
    {
	    public Medicamentos elemento;
        public Nodo hijoIzquierdo;
        public Nodo hijoDerecho;

        public Nodo(Medicamentos dato)
        {
            this.elemento = dato;
            this.hijoIzquierdo = null;
            this.hijoDerecho = null;
        }
    }
}

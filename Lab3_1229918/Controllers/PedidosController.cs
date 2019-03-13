using ArbolBinario;
using Lab3_1229918.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Lab3_1229918.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        //lectura del archivo
        //Para empezar la lectura del archivo debe de entrar Pedidos/Index
        public ActionResult Index()
        {
            return View(new List<Inventario>());
        }
        //una vez ingresa a la vista del controlador, lo siguiente que debe de hacer es seleccionar el archivo que se desea leer
        //En la carpeta del programa existe una carpeta llamada "Archivo De Lectura", dentro de ella se encuentra el archvio y después debe de presionar el botón importar.
        static List<Inventario> lista = new List<Inventario>();
        static Arbol arbol = new Arbol();
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<Inventario> inventario = new List<Inventario>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                //dirección del archivo
                string path = Server.MapPath("~/archivo/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
                int contador = 0;
                string csvData = System.IO.File.ReadAllText(filePath);
                //El split del archivo es por columna
                foreach (string row in csvData.Split('\n'))
                {
                    if ((!string.IsNullOrEmpty(row)) && (contador != 0))
                    {
                        Inventario inventarios = new Inventario();
                        inventarios.ID = Convert.ToInt32(row.Split(';')[0]);
                        inventarios.Nombre = " " + row.Split(';')[1];
                        inventarios.Descripción = row.Split(';')[2];
                        inventarios.CasaFarmaceutica = row.Split(';')[3];
                        inventarios.Precio = row.Split(';')[4];
                        inventarios.Cantidad = Convert.ToInt32(row.Split(';')[5]);
                        inventario.Add(inventarios);
                        lista.Add(inventarios);
                    }
                    else
                    {
                        contador++;
                    }
                }
            }
            // al finalizar la lectura del archivo, los datos se ingresaron a una lista de forma ordenada
            // Se recorre la lista nodo por nodo para ingresar cada elemento que contenga el nodo dentro del arbol binario.
            foreach (Inventario nodo in inventario)
            {
                Medicamentos medicamentos = new Medicamentos();
                medicamentos.ID = nodo.ID;
                medicamentos.Nombre = nodo.Nombre;
                medicamentos.Descripción = nodo.Descripción;
                medicamentos.CasaFarmaceutica = nodo.CasaFarmaceutica;
                medicamentos.Cantidad = nodo.Cantidad;
                medicamentos.Precio = nodo.Precio;
                Nodo actual = new Nodo(medicamentos);
                arbol.Insertar(actual);
            }
            return View(inventario);
        }
        //El index 2 permite que los datos leidos del archivo se miren en la forma en la que ingresaron
        public ActionResult Index2()
        {
            return View(lista);
        }

        //La vista de ConecciónB se logra ver que mira los menús (los botones para realizar los siguientes metodos que se encuentran dentro del controlador) 
        public ActionResult ConecciónB()
        {
            return View();
        }
        //realiza la busqueda del dato que se desea buscar recorriendo de forma recursiva los nodos del arbol.
        public ActionResult busqueda()
        {
            LinkedList<Inventario> listaenlazada = new LinkedList<Inventario>();
            Inventario tabla = new Inventario();
            Medicamentos medicamentos = new Medicamentos();
            //se debe de ingresar el nombre del medicamento de forma correcta
            string Nombre = Request.Form["nombre"].ToString();
            //si encuentra el valor, la función de busqueda devolverá un valor de tipo medicamento, el cual contiene toda la información del nodo 
            medicamentos = arbol.Buscar(arbol.raiz, Nombre);
            tabla.ID = medicamentos.ID;
            tabla.Nombre = medicamentos.Nombre;
            tabla.Descripción = medicamentos.Descripción;
            tabla.Precio = medicamentos.Precio;
            tabla.Cantidad = medicamentos.Cantidad;
            tabla.CasaFarmaceutica = medicamentos.CasaFarmaceutica;
            if (listaenlazada.Count != 0)
            {
                listaenlazada.Remove(listaenlazada.First);
            }
            listaenlazada.AddFirst(tabla);

            var model = from puntos in listaenlazada
                        select puntos;
            return View("busqueda", model);
        }
        public ActionResult ConPed()
        {
            var model = from puntos in lista
                        select puntos;
            return View("ConPed", model);
        }
        // Para realizar el pedido, se le pedirá al usuario su nombre, nombre de la Medicina, Doc. Identificación, dirección y la cantidad que desea compra.
        public ActionResult HacerPedido()
        {
            LinkedList<Inventario> listaenlazada = new LinkedList<Inventario>();
            Inventario tabla = new Inventario();
            Medicamentos medicamentos = new Medicamentos();
            // se debe de ingresar el nombre del medicamento de forma correcta
            string Nombre = Request.Form["nombre"].ToString();
            int cantidad = Convert.ToInt32(Request.Form["cantidad"]);
            medicamentos = arbol.HacerPedido(arbol.raiz, Nombre, cantidad);
            tabla.ID = medicamentos.ID;
            tabla.Nombre = medicamentos.Nombre;
            tabla.Descripción = medicamentos.Descripción;
            tabla.Precio = medicamentos.Precio;
            tabla.Cantidad = medicamentos.Cantidad;
            tabla.CasaFarmaceutica = medicamentos.CasaFarmaceutica;
            // si la cantidad de medicamentos pedidos es 1 o menor, el nodo se debe de eliminar.
            if ((tabla.Cantidad == 0) || (tabla.Cantidad < 0))
            {
                arbol.eliminar(arbol.raiz, Nombre);
                tabla = null;
            }
            listaenlazada.AddFirst(tabla);

            var model = from puntos in listaenlazada
                        select puntos;
            return View("HacerPedido", model);
        }
        // Recorridos de los árboles en las tres formas, InOrden, PosOrden, PreOrden
        public ActionResult InOrden()
        {
            Arbol Aux = new Arbol();
            LinkedList<Medicamentos> listaenlazada = new LinkedList<Medicamentos>();
            listaenlazada = Aux.InOrden(arbol.raiz);
            LinkedList<Inventario> listainventario = new LinkedList<Inventario>();
            foreach (Medicamentos nodo in listaenlazada)
            {
                Inventario medicamentos = new Inventario();
                medicamentos.ID = nodo.ID;
                medicamentos.Nombre = nodo.Nombre;
                medicamentos.Descripción = nodo.Descripción;
                medicamentos.CasaFarmaceutica = nodo.CasaFarmaceutica;
                medicamentos.Cantidad = nodo.Cantidad;
                medicamentos.Precio = nodo.Precio;
                if (listainventario.Count == 0)
                {
                    listainventario.AddFirst(medicamentos);
                }
                else
                {
                    listainventario.AddAfter(listainventario.Last, medicamentos);
                }
            }
            var model = from puntos in listainventario
                        select puntos;
            return View("InOrden", model);
        }
        public ActionResult PreOrden()
        {
            Arbol Aux = new Arbol();
            LinkedList<Medicamentos> listaenlazada = new LinkedList<Medicamentos>();
            listaenlazada = Aux.PreOrden(arbol.raiz);
            LinkedList<Inventario> listainventario = new LinkedList<Inventario>();

            foreach (Medicamentos nodo in listaenlazada)
            {
                Inventario medicamentos = new Inventario();
                medicamentos.ID = nodo.ID;
                medicamentos.Nombre = nodo.Nombre;
                medicamentos.Descripción = nodo.Descripción;
                medicamentos.CasaFarmaceutica = nodo.CasaFarmaceutica;
                medicamentos.Cantidad = nodo.Cantidad;
                medicamentos.Precio = nodo.Precio;
                if (listainventario.Count == 0)
                {
                    listainventario.AddFirst(medicamentos);
                }
                else
                {
                    listainventario.AddAfter(listainventario.Last, medicamentos);
                }
            }
            var model = from puntos in listainventario
                        select puntos;
            return View("PreOrden", model);
        }
        public ActionResult PosOrden()
        {
            Arbol Aux = new Arbol();
            LinkedList<Medicamentos> listaenlazada = new LinkedList<Medicamentos>();
            listaenlazada = Aux.PosOrden(arbol.raiz);
            LinkedList<Inventario> listainventario = new LinkedList<Inventario>();

            foreach (Medicamentos nodo in listaenlazada)
            {
                Inventario medicamentos = new Inventario();
                medicamentos.ID = nodo.ID;
                medicamentos.Nombre = nodo.Nombre;
                medicamentos.Descripción = nodo.Descripción;
                medicamentos.CasaFarmaceutica = nodo.CasaFarmaceutica;
                medicamentos.Cantidad = nodo.Cantidad;
                medicamentos.Precio = nodo.Precio;
                if (listainventario.Count == 0)
                {
                    listainventario.AddFirst(medicamentos);
                }
                else
                {
                    listainventario.AddAfter(listainventario.Last, medicamentos);
                }
            }
            var model = from puntos in listainventario
                        select puntos;
            return View("PosOrden", model);
        }
        //Si se desea realizar una eliminación de un nodo del árbol por alguna razón o algún mal ingreso de dato, este metodo se puede eliminar
        public ActionResult eliminar()
        {
            string dato = Request.Form["nombre"].ToString();
            arbol.eliminar(arbol.raiz, dato);
            return View();
        }

        //el ActionResult Json y Jsondeserealización, permiten convertir los datos a Json por medio de la serialización y a valores normales por medio de la deserialización.
        string intento;
        public ActionResult Json()
        {

            string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string final = docPath + "\\Segundo año\\Primer Ciclo\\Estructura de Datos I Lab\\Lab3_1229918\\Archivo de Lectura\\Archivo donde los datos se van a guardar Json";
            List<string> entrar = new List<string>();
            foreach (Inventario inventario in lista)
            {
                intento = Newtonsoft.Json.JsonConvert.SerializeObject(inventario);
                entrar.Add(intento);
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(final, "JsonSerialización.txt")))
            {
                foreach (string line in entrar)
                    outputFile.WriteLine(line);
            }
            if (ultimo.Count == 0)
            {
                ultimo = entrar;
            }
            
            return View();
        }
        static List<string> ultimo = new List<string>();
        int contador = 0;
        public ActionResult JsonDeserialización()
        {
            Inventario dato = new Inventario();
            LinkedList<Inventario> ult = new LinkedList<Inventario>();
            foreach (string lectura in ultimo)
            {
                Newtonsoft.Json.JsonConvert.PopulateObject(lectura, dato);
                if(contador == 0)
                {
                    ult.AddFirst(dato);
                    contador++;
                }
                else
                {
                    ult.AddAfter(ult.Last, dato);
                }
            }

            var model = from puntos in ult
                        select puntos;
            return View("JsonDeserialización", model);
        }
    }
}
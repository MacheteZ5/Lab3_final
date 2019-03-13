using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBinario
{
    public class Arbol
    {
        //Nodo razí
        public Nodo raiz;
        // comprobando si el arbol = null
        public Arbol()
        {
            raiz = null;
        }
        //Metodo de inserción
        public void Insertar(Nodo dato)
        {
            Nodo nuevo = dato;

            if (raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                //debido a que es un valor de tipo string el que se ingresa, se compara para verificar si es hijo izquierdo o hijo derecho.
                int valornombre = nuevo.elemento.Nombre.CompareTo(raiz.elemento.Nombre);
                if ((/*nuevo.elemento.ID*/ valornombre > /*raiz.elemento.ID*/0) || (valornombre == 0))
                {
                    InsertarAux(nuevo, raiz, true);
                }
                else if (/*nuevo.elemento.ID*/valornombre < 0/*raiz.elemento.ID*/)
                {
                    InsertarAux(nuevo, raiz, false);
                }

            }
        }
        //InsertarAux, permite el ingreso de forma recursiva al arbol
        public void InsertarAux(Nodo nuevo, Nodo padre, bool esHijoDerecho)
        {
            if (esHijoDerecho)
            {
                if (padre.hijoDerecho == null)
                {
                    padre.hijoDerecho = nuevo;
                }
                else
                {
                    int valornombre = nuevo.elemento.Nombre.CompareTo(padre.hijoDerecho.elemento.Nombre);
                    if ((/*nuevo.elemento.ID*/valornombre > 0 /*padre.hijoDerecho.elemento.ID*/) || (valornombre == 0)/*||(valornombre ==0)*/)
                    {
                        InsertarAux(nuevo, padre.hijoDerecho, true);
                    }
                    else if (/*nuevo.elemento.ID */valornombre < 0 /*padre.hijoDerecho.elemento.ID*/)
                    {
                        InsertarAux(nuevo, padre.hijoDerecho, false);
                    }

                }
            }
            else
            {
                if (padre.hijoIzquierdo == null)
                {
                    padre.hijoIzquierdo = nuevo;
                }
                else
                {
                    int valornombre = nuevo.elemento.Nombre.CompareTo(padre.hijoIzquierdo.elemento.Nombre);
                    if ((valornombre > 0) || (valornombre == 0))
                    {
                        InsertarAux(nuevo, padre.hijoIzquierdo, true);
                    }
                    else if (valornombre < 0)
                    {
                        InsertarAux(nuevo, padre.hijoIzquierdo, false);
                    }

                }
            }
        }
        //El metodo de busqueda se ingresa tipo nodo (nodos del arbol) y el dato (nombre del medicamento)
        // retorna un tipo medicamento que contiene toda la información del medicamento.
        Medicamentos valor = new Medicamentos();
        public Medicamentos Buscar(Nodo arbol, string dato)
        {
            if (arbol == null)
            {
                return null;
            }
            else
            {
                Buscar(arbol.hijoIzquierdo, dato);
                if (arbol.elemento == null)
                {
                    return null;
                }
                if (arbol.elemento.Nombre == dato)
                {
                    valor.ID = arbol.elemento.ID;
                    valor.Nombre = arbol.elemento.Nombre;
                    valor.Descripción = arbol.elemento.Descripción;
                    valor.CasaFarmaceutica = arbol.elemento.CasaFarmaceutica;
                    valor.Precio = arbol.elemento.Precio;
                    valor.Cantidad = arbol.elemento.Cantidad;
                }
                Buscar(arbol.hijoDerecho, dato);
            }
            return valor;
        }
        //Recorridos de los árboles Pre-,Pos-,In-Orden
        //retornan un tipo lista enlazada para mostrar la forma en la que se recorrió el árbol
        LinkedList<Medicamentos> listaPos = new LinkedList<Medicamentos>();
        int contadorD = 0;
        public LinkedList<Medicamentos> PosOrden(Nodo arbol)
        {
            if (arbol == null)
            {
                return null;
            }
            else
            {
                PosOrden(arbol.hijoIzquierdo);
                PosOrden(arbol.hijoDerecho);
                Medicamentos medlista = new Medicamentos();
                medlista.ID = arbol.elemento.ID;
                medlista.Nombre = arbol.elemento.Nombre;
                medlista.Descripción = arbol.elemento.Descripción;
                medlista.CasaFarmaceutica = arbol.elemento.CasaFarmaceutica;
                medlista.Cantidad = arbol.elemento.Cantidad;
                medlista.Precio = arbol.elemento.Precio;
                if (contadorD == 0)
                {
                    listaPos.AddFirst(medlista);
                    contadorD++;
                }
                else
                {
                    listaPos.AddAfter(listaPos.Last, medlista);
                }
            }
            return listaPos;
        }
        LinkedList<Medicamentos> listaP = new LinkedList<Medicamentos>();
        int contador = 0;
        public LinkedList<Medicamentos> PreOrden(Nodo arbol)
        {
            if (arbol == null)
            {
                return null;
            }
            else
            {
                Medicamentos medlista = new Medicamentos();
                medlista.ID = arbol.elemento.ID;
                medlista.Nombre = arbol.elemento.Nombre;
                medlista.Descripción = arbol.elemento.Descripción;
                medlista.CasaFarmaceutica = arbol.elemento.CasaFarmaceutica;
                medlista.Cantidad = arbol.elemento.Cantidad;
                medlista.Precio = arbol.elemento.Precio;
                if (contador == 0)
                {
                    listaP.AddFirst(medlista);
                    contador++;
                }
                else
                {
                    listaP.AddAfter(listaP.Last, medlista);
                }
                PreOrden(arbol.hijoIzquierdo);
                PreOrden(arbol.hijoDerecho);
            }
            return listaP;
        }

        LinkedList<Medicamentos> listaI = new LinkedList<Medicamentos>();
        int contadorI = 0;
        public LinkedList<Medicamentos> InOrden(Nodo arbol)
        {
            Medicamentos medlista = new Medicamentos();
            if (arbol == null)
            {
                return null;
            }
            else
            {
                InOrden(arbol.hijoIzquierdo);
                medlista.ID = arbol.elemento.ID;
                medlista.Nombre = arbol.elemento.Nombre;
                medlista.Descripción = arbol.elemento.Descripción;
                medlista.CasaFarmaceutica = arbol.elemento.CasaFarmaceutica;
                medlista.Cantidad = arbol.elemento.Cantidad;
                medlista.Precio = arbol.elemento.Precio;
                if (contadorI == 0)
                {
                    listaI.AddFirst(medlista);
                    contadorI++;
                }
                else
                {
                    listaI.AddAfter(listaI.Last, medlista);
                }
                InOrden(arbol.hijoDerecho);
            }
            return listaI;
        }

        //método de eliminación
        int cont = 0;
        int com = 0;
        public void eliminar(Nodo arbol, string dato)
        {
            Nodo padrez = arbol;
            if (cont == 0)
            {
                Nodo padrex = raiz;
                com = arbol.elemento.Nombre.CompareTo(raiz.elemento.Nombre);
                if ((com > 0) || (com == 0))
                {
                    padrex = padrex.hijoDerecho;
                }
                else
                {
                    padrex = padrex.hijoIzquierdo;
                }
                cont++;
            }
            if (arbol == null)
            {
                return;
            }
            else
            {
                eliminar(arbol.hijoIzquierdo, dato);
                if (arbol.elemento.Nombre == dato)
                {
                    if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo == null))
                    {
                        arbol.elemento = null;
                        arbol = padrez;
                        arbol.hijoIzquierdo = null;
                        arbol.hijoDerecho = null;
                    }
                    else
                    {
                        if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo != null))
                        {
                            arbol.elemento = null;
                            arbol = arbol.hijoIzquierdo;
                        }
                        else
                        {
                            if ((arbol.hijoDerecho != null) && (arbol.hijoIzquierdo == null))
                            {
                                arbol.elemento = null;
                                arbol = arbol.hijoDerecho;
                            }
                            else
                            {
                                com = arbol.elemento.Nombre.CompareTo(raiz.elemento.Nombre);
                                if ((com > 0) || (com == 0))
                                {
                                    arbol.elemento = null;
                                    Nodo extra = arbol.hijoDerecho;
                                    Nodo extra2 = arbol.hijoIzquierdo;
                                    Nodo extra3 = arbol;
                                    arbol.hijoDerecho = null;
                                    arbol.hijoIzquierdo = null;
                                    arbol = extra2;

                                    if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo == null))
                                    {
                                        extra3 = arbol;
                                    }
                                    else
                                    {
                                        while ((arbol.hijoDerecho != null && arbol.hijoIzquierdo != null))
                                        {
                                            arbol = arbol.hijoDerecho;
                                            extra3 = arbol;
                                        }
                                    }
                                    if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo == null))
                                    {
                                        arbol = extra3;
                                        arbol.hijoDerecho = extra;
                                    }
                                    else
                                    {
                                        arbol = extra3;
                                        arbol.hijoDerecho = extra;
                                        arbol.hijoIzquierdo = extra2;
                                    }

                                }
                                else
                                {
                                        arbol.elemento = null;
                                        Nodo extra = arbol.hijoDerecho;
                                        Nodo extra2 = arbol.hijoIzquierdo;
                                        Nodo extra3 = arbol;
                                        arbol.hijoDerecho = null;
                                        arbol.hijoIzquierdo = null;
                                        arbol = extra;

                                        if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo == null))
                                        {
                                            extra3 = arbol;
                                        }
                                        else
                                        {
                                            while ((arbol.hijoDerecho != null) && (arbol.hijoIzquierdo != null))
                                            {
                                                arbol = arbol.hijoDerecho;
                                                extra3 = arbol;
                                            }
                                        }
                                        if ((arbol.hijoDerecho == null) && (arbol.hijoIzquierdo == null))
                                        {
                                            arbol = extra3;
                                            arbol.hijoDerecho = extra2;
                                        }
                                        else
                                        {
                                            arbol = extra3;
                                            arbol.hijoDerecho = extra2;
                                            arbol.hijoIzquierdo = extra;
                                        }
                                    }//if (arbol.hijoDerecho == null)
                                     //{
                                     //    arbol = extra2;
                                     //}
                                     //while (arbol.hijoDerecho != null)
                                     //{
                                     //    arbol = arbol.hijoDerecho;
                                     //}
                                     //if ((arbol != extra2))
                                     //{
                                     //    arbol.hijoDerecho = extra2;
                                     //    arbol.hijoIzquierdo = extra;
                                     //}
                                     //else
                                     //{
                                     //    arbol.hijoIzquierdo = extra;
                                     //}

                                
                            }
                        }
                    }
                }
                eliminar(arbol.hijoDerecho, dato);
            }
        }
        //Metodo para realizar pedido del medicamento
        // Es similar al metodo de busqueda, sin embargo a esté le afecta la cantidad de medicamentos que el cliente desea compra
        Medicamentos valores = new Medicamentos();
        public Medicamentos HacerPedido(Nodo arbol, string dato, int num)
        {
            if (arbol == null)
            {
                return null;
            }
            else
            {
                HacerPedido(arbol.hijoIzquierdo, dato,num);
                if (arbol.elemento == null)
                {
                    return null;
                }
                if (arbol.elemento.Nombre == dato)
                {
                    valores.ID = arbol.elemento.ID;
                    valores.Nombre = arbol.elemento.Nombre;
                    valores.Descripción = arbol.elemento.Descripción;
                    valores.CasaFarmaceutica = arbol.elemento.CasaFarmaceutica;
                    valores.Precio = arbol.elemento.Precio;
                    arbol.elemento.Cantidad -= num;
                    valores.Cantidad = arbol.elemento.Cantidad;
                }
                HacerPedido(arbol.hijoDerecho, dato,num);
            }
            return valores;
        }
    }
}

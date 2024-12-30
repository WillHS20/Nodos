using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;  //Librería para dibujar figuras geométricas
using System.Windows.Forms;
using System.Threading; //Librería para manejo de hilos
using System.Data;

namespace Arbol_NodosP
{
    internal class clsNodo
    {
        public int info;        // Dato a almacenar en el nodo
        public clsNodo Izquierdo;    // Nodo izquierdo del árbol
        public clsNodo Derecho;  // Nodo derecho del árbol
        public clsNodo Padre;    //Nodo raíz del árbol
        public int altura;
        public int nivel;
        public Rectangle nodo;      //Para dibujar el nodo del árbol

        private clsArbolBinario arbol; // declarando un objeto de tipo Árbol Binario
        public clsNodo()         //Constructor por defecto
        {
        }

        public clsArbolBinario Arbol  //Constructor por defecto para el objeto de tipo Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }

        //Constructor con parámetros
        public clsNodo(int nueva_info, clsNodo izquierdo, clsNodo derecho, clsNodo padre)
        {
            info = nueva_info;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Padre = padre;
            altura = 0;
        }

        // Función para insertar un nodo en el Árbol Binario 

        public clsNodo Insertar(int x, clsNodo t, int Level)
        {
            if (t == null)
            {
                t = new clsNodo(x, null, null, null);
                t.nivel = Level;
            }
            else if (x < t.info)    // Si el valor a insertar es menor que la raíz
            {
                Level++;
                t.Izquierdo = Insertar(x, t.Izquierdo, Level);
            }
            else if (x > t.info)    // Si el valor a insertar es mayor que la raíz
            {
                Level++;
                t.Derecho = Insertar(x, t.Derecho, Level);
            }
            else
            {
                MessageBox.Show("Dato Existente en el Árbol", "Error de Ingreso");
            }
            return t;
        }

        // Función para calcular la altura de un nodo en el Árbol Binario 
        private static int Alturas(clsNodo t)
        {
            return t == null ? -1 : t.altura;
        }

        // Función para eliminar un nodo del Árbol Binario 
        public void Eliminar(int x, ref clsNodo t)
        {
            if (t != null)  // Si raíz es distinta de null 
            {
                if (x < t.info) // Si el valor a eliminar es menor que la raíz
                {
                    Eliminar(x, ref t.Izquierdo);
                }
                else
                {
                    if (x > t.info) // Si el valor a eliminar es mayor que la raíz
                    {
                        Eliminar(x, ref t.Derecho);
                    }
                    else
                    {
                        clsNodo NodoEliminar = t; //Ya se ubicó el nodo que se desea eliminar 
                        if (NodoEliminar.Derecho == null)   // Se verifica si tiene hijo derecho
                        {
                            t = NodoEliminar.Izquierdo;
                        }
                        else
                        {
                            if (NodoEliminar.Izquierdo == null) // Se verifica si tiene hijo izquierdo
                            {
                                t = NodoEliminar.Derecho;
                            }
                            else
                            {
                                if (Alturas(t.Izquierdo) - Alturas(t.Derecho) > 0)  // Para verificar que hijo pasa a ser nueva raíz del subárbol
                                {
                                    clsNodo AuxiliarNodo = null;
                                    clsNodo Auxiliar = t.Izquierdo; bool Bandera = false;
                                    while (Auxiliar.Derecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.Derecho;
                                        Bandera = true;
                                    }

                                    t.info = Auxiliar.info;     // Se crea un nodo temporal
                                    NodoEliminar = Auxiliar;

		                            if (Bandera == true)
                                    {
                                        AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
                                    }
                                    else
                                    {
                                        t.Izquierdo = Auxiliar.Izquierdo;
                                    }
                                }
                                else
                                {
                                    if (Alturas(t.Derecho) - Alturas(t.Izquierdo) > 0)
                                    {
                                        clsNodo AuxiliarNodo = null;
                                        clsNodo Auxiliar = t.Derecho;
                                        bool Bandera = false;

                                        while (Auxiliar.Izquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
		                                    Auxiliar = Auxiliar.Izquierdo;
                                            Bandera = true;
                                        }

                                        t.info = Auxiliar.info;
                                        NodoEliminar = Auxiliar;

		                                if (Bandera == true)
                                        {
                                            AuxiliarNodo.Izquierdo = Auxiliar.Derecho;
                                        }
                                        else
                                        {
                                            t.Derecho = Auxiliar.Derecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(t.Derecho) - Alturas(t.Izquierdo) == 0)
                                        {
                                            clsNodo AuxiliarNodo = null;
                                            clsNodo Auxiliar = t.Izquierdo;
                                            bool Bandera = false;

                                            while (Auxiliar.Derecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.Derecho;
                                                Bandera = true;
                                            }

                                            t.info = Auxiliar.info;
                                            NodoEliminar = Auxiliar;

                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
		                                    }
                                            else
                                            {
                                                t.Izquierdo = Auxiliar.Izquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nodo NO Existente en el Arbol", "Error de Eliminación");
            }
        }

        // Final de la función Eliminar
        // Función para recorrer el Árbol Binario (búsqueda de un nodo)
        public void buscar(int x, clsNodo t)
        {
            if (t != null)
            {
                if (x < t.info) // Búsqueda por el subárbol izquierdo
                {
                    buscar(x, t.Izquierdo);
                }
                else
                {
                    if (x == t.info)
                    {
                        MessageBox.Show("Nodo Encontrado en el Árbol", "Busqueda Satisfecha");
                    }
                    else if (x > t.info) // Búsqueda por el subárbol derecho
                    {
                        buscar(x, t.Derecho);
                    }
                }
            }
            else
                MessageBox.Show("Nodo NO Encontrado en el Árbol", "Error de Búsqueda");
        }

        // ***** Funciones para el dibujo de los nodos del Arbol Binario en el Formulario *****

        //Variable que define el tamaño de los círculos que representarán los nodos del árbol

        private const int Radio = 30;
        private const int DistanciaH = 80; // variable para manejar distancia horizontal
        private const int DistanciaV = 10; // variable para manejar distancia vertical

        private int CoordenadaX; // variable para manejar posición Eje X
        private int CoordenadaY; //varíable para manejar posición Eje Y
        private Rectangle prueba;
        //Función para encontrar la posición donde se creará (dibujará) el nodo
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);

            //obtiene la posición del Sub-Arbol izquierdo.
            if (Izquierdo != null)
            {
                Izquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if ((Izquierdo != null) && (Derecho != null))
            {
                xmin += DistanciaH;
            }

            //Si existe el nodo derecho y el nodo izquierdo deja un espacio entre ellos
            if (Derecho != null)
            {
                Derecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if ((Izquierdo != null) && (Derecho != null))
                CoordenadaX = (int)((Izquierdo.CoordenadaX + Derecho.CoordenadaX)/2);
            else if (Izquierdo != null)
            {
                aux1 = Izquierdo.CoordenadaX;
                Izquierdo.CoordenadaX = CoordenadaX - 80;
                CoordenadaX = aux1;
            }
            else if (Derecho != null)
            {
                aux2 = Derecho.CoordenadaX;
                //no hay nodo izquierdo, centrar en nodo derecho.
                Derecho.CoordenadaX = CoordenadaX + 80;
                CoordenadaX = aux2;
            }
            else
            {
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        //Función para dibujar las ramas de los nodos izquierdo y derecho
        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if (Izquierdo != null) //Dibujará rama izquierda

            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, Izquierdo.CoordenadaX, Izquierdo.CoordenadaY);
                Izquierdo.DibujarRamas(grafo, Lapiz);
            }
            if (Derecho != null)    //Dibujará rama derecha
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, Derecho.CoordenadaX, Derecho.CoordenadaY);
                Derecho.DibujarRamas(grafo, Lapiz);
            }
        }

        //Función para dibujar el nodo en la posición especificada

        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {

            //Dibuja el contorno del nodo
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            grafo.FillEllipse(encuentro, rect);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            //Para dibujar el nombre del nodo, es decir el contenido
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);

            //Dibuja los nodos hijos derecho e izquierdo.
            if (Izquierdo != null)
            {
                Izquierdo.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
            if (Derecho != null)
            {
                Derecho.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
        }

        //Función para dar color al nodo del Arbol Binario
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            //Dibuja el nombre
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);
        }
    }
}

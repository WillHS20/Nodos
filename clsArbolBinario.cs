using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbol_NodosP
{
    internal class clsArbolBinario
    {
        public clsNodo Raiz;
        public clsNodo aux;

        //Constructor por defecto
        public clsArbolBinario()
        {
            aux = new clsNodo();
        }

        //Constructor con parametros
        public clsArbolBinario(clsNodo nueva_raiz)
        {
            Raiz = nueva_raiz;
        }
        //Función para agregar un nuevo nodo (valor) al Arbol Binario
        public void Insertar(int x)
        {
            if (Raiz == null)
            {
                Raiz = new clsNodo(x, null, null, null);
                Raiz.nivel = 0;
            }
            else
                Raiz = Raiz.Insertar(x, Raiz, Raiz.nivel);
        }

        //Función para eliminar un nodo (valor) del Arbol Binario.
        public void Eliminar(int x)
        {
            if (Raiz == null)
                Raiz = new clsNodo(x, null, null, null);
            else
                Raiz.Eliminar(x, ref Raiz);
        }

        public void Buscar(int x)
        {
            if (Raiz != null)
            {
                Raiz.buscar(x, Raiz);
            }
        }

        // ******* Funciones para el dibujo del Arbol Binario en el Formulario ******

        //Función que dibuja el Arbol Binario
        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            //Posiciones de la raíz del árbol
            int x = 400;
            int y = 75;
            if (Raiz == null) return;

            Raiz.PosicionNodo(ref x, y);        //Posición de cada nodo

            Raiz.DibujarRamas(grafo, Lapiz);    //Dibuja los Enlaces entre nodos

            //Dibuja todos los Nodos
            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
        }

        public int x1 = 400;    // Posiciones iniciales de la raiz del arbol
        public int y2 = 75;

        //Función para Colorear los nodos
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, clsNodo Raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;
            if (inor == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);     //pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post, inor, preor);
                }
            }
            else if (preor == true)
            {
                if (Raiz != null)
                {
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);     //pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post, inor, preor);
                }
            }
            else if (post == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);  // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                }
            }
        }

        string Orden = "";
        //METODO PARA RECORRER EL ARBOL DE FORMA ENORDEN
        public string Ordenada(clsNodo nodo)
        {
            //Verifica si hay nodos
            if (nodo != null)
            {
                //Recorre el nodo a la izquierda
                Ordenada(nodo.Izquierdo);
                //Escribe el valor del nodo
                Orden += nodo.info + " - ";
                //Recorre el nodo a la derecha
                Ordenada(nodo.Derecho);
            }
            //Regresa una cadena de texto del recorrido
            return Orden;
        }

        string preOrden = "";
        //METODO PARA RECORRER EL ARBOL DE FORMA PREORDEN
        public string PreOrdenada(clsNodo nodo)
        {
            if (nodo != null)
            {
                preOrden += nodo.info + " - ";
                PreOrdenada(nodo.Izquierdo);
                PreOrdenada(nodo.Derecho);
            }
            return preOrden;
        }
        string posOrden = "";
        //METODO PARA RECORRER EL ARBOL DE FORMA POSTORDEN
        public string PosOrdenada(clsNodo nodo)
        {
            if (nodo != null)
            {
                // Recorrer el subárbol izquierdo 
                PosOrdenada(nodo.Izquierdo);
                // Recorrer el subárbol derecho
                PosOrdenada(nodo.Derecho);
                // Sumar el valor del nodo actual al resultado
                posOrden += nodo.info + " - ";

            }
            // Retornar el resultado del recorrido en postorden
            return posOrden;
        }


        //METODO PARA SUMAR LOS VALORES DE LOS NODOS QUE CONFORMAN EL ARBOL
        public int SumaNodos(clsNodo NodoAct)
        {
            int acuS = 0;
            //Verifica que hay nodos    
            if (NodoAct != null)
            {

                acuS += SumaNodos(NodoAct.Izquierdo); // Sumar los nodos del subárbol izquierdo
                acuS += Convert.ToInt32(NodoAct.info); // Sumar el nodo actual
                acuS += SumaNodos(NodoAct.Derecho); //Sumar los nodos del subárbol derecho
            }
            return acuS;

        }

        public string EncontrarMinMax(clsNodo RecNodo)
        {
            // Inicializar el máximo con el valor mínimo posible
            int max = int.MinValue;
            // Inicializar el mínimo con el valor máximo posible
            int min = int.MaxValue;

            string mensaje = "";

            EncontrarMinMaxRecur(RecNodo, ref max, ref min);

            mensaje = "Elemento máximo: " + max.ToString() + " y el Elemento mínimo: " + min.ToString();
            return mensaje;
        }
        public void EncontrarMinMaxRecur(clsNodo nodo, ref int maximo, ref int minimo)
        {
            if (nodo == null)
                return;

            // Comprobar si el valor del nodo actual es mayor que el máximo actual
            if (nodo.info > maximo)
                maximo = nodo.info;

            // Comprobar si el valor del nodo actual es menor que el mínimo actual
            if (nodo.info < minimo)
                minimo = nodo.info;

            // Recorrer el subárbol izquierdo
            EncontrarMinMaxRecur(nodo.Izquierdo, ref maximo, ref minimo);

            // Recorrer el subárbol derecho
            EncontrarMinMaxRecur(nodo.Derecho, ref maximo, ref minimo);
        }

        //METODO PARA SUMAR NODOS MULTIPLOS DE 2,3 Y 5
        public int SumaMultiplos(clsNodo nodo)
        {
            int suma = 0;
            string mensaje = "";

            if (nodo != null)
            {
                // Verificar si el nodo actual es múltiplo de 2, 3 y 5
                if (nodo.info % 2 == 0 && nodo.info % 3 == 0 && nodo.info % 5 == 0)
                {
                    mensaje += "Nodo " + nodo.info + " es múltiplo de 2, 3 y 5" + " || ";
                    suma += nodo.info;
                }

                // Recorrer el subárbol izquierdo
                suma += SumaMultiplos(nodo.Izquierdo);

                // Recorrer el subárbol derecho
                suma += SumaMultiplos(nodo.Derecho);
            }
            MessageBox.Show(mensaje);
            return suma;
        }

        public int AlturaArbol(clsNodo nodo)
        {
            //Verifica que no este vacio el arbol
            if (nodo == null)
            {
                return 0; // Si el árbol está vacío altura es 0
            }
            else
            {
                int alturaIzquierda = AlturaArbol(nodo.Izquierdo);
                int alturaDerecha = AlturaArbol(nodo.Derecho);

                // La altura del árbol es la máxima altura entre su subárbol izquierdo y derecho
                return Math.Max(alturaIzquierda, alturaDerecha)+1;
            }
        }


    }
}

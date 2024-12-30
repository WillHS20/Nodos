using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbol_NodosP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int Dato = 0;
        int cont = 0;

        clsArbolBinario my_Arbol = new clsArbolBinario(null);
        Graphics g;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            my_Arbol.DibujarArbol(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, Brushes.Black);

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtDato.Text == "")
            {
                MessageBox.Show("Debe Ingresar un Valor");
            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                if (Dato <= 0 || Dato >= 100)
                    MessageBox.Show("Solo Recibe Valores desde 1 hasta 99", "Error de Ingreso");
                else
                {
                    my_Arbol.Insertar(Dato);
                    txtDato.Clear();
                    txtDato.Focus();

                    cont++;

                    Refresh();
                    Refresh();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtDato.Text == "")
            {
                MessageBox.Show("Debe Ingresar un Valor");
            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                if (Dato <= 0 || Dato >= 100)
                    MessageBox.Show("Solo Recibe Valores desde 1 hasta 99", "Error de Ingreso");
                else
                {
                    my_Arbol.Eliminar(Dato);
                    txtDato.Clear();
                    txtDato.Focus();

                    cont--;

                    Refresh();
                    Refresh();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtDato.Text == "")
            {
                MessageBox.Show("Debe Ingresar un Valor");
            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                if (Dato <= 0 || Dato >= 100)
                    MessageBox.Show("Solo Recibe Valores desde 1 hasta 99", "Error de Ingreso");
                else
                {
                    my_Arbol.Buscar(Dato);
                    txtDato.Clear();
                    txtDato.Focus();

                    Refresh();
                    Refresh();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            string resultado = my_Arbol.PreOrdenada(my_Arbol.Raiz);
            txtResultados.Text = resultado;
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            string resultado = my_Arbol.Ordenada(my_Arbol.Raiz);
            txtResultados.Text = resultado;
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            string resultado = my_Arbol.PosOrdenada(my_Arbol.Raiz);
            txtResultados.Text = resultado;
        }

        private void btnSuma_Click(object sender, EventArgs e)
        {
            string mensaje = "La suma total de los nodos es igual a: " + my_Arbol.SumaNodos(my_Arbol.Raiz);
            txtResultados.Text = mensaje;
        }

        private void btnMinMax_Click(object sender, EventArgs e)
        {
            string mensaje = my_Arbol.EncontrarMinMax(my_Arbol.Raiz);
            txtResultados.Text = mensaje;
        }

        private void btnSumMulti_Click(object sender, EventArgs e)
        {
            string mensaje = "La suma de todos los multiplos es: " + my_Arbol.SumaMultiplos(my_Arbol.Raiz);
            txtResultados.Text = mensaje;
        }

        private void btnAltura_Click(object sender, EventArgs e)
        {
            string mensaje = "La altura de este arbol es: " + my_Arbol.AlturaArbol(my_Arbol.Raiz);
            txtResultados.Text = mensaje;
        }
    }
}

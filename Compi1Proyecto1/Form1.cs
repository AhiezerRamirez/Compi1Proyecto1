using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compi1Proyecto1
{
    public partial class Form1 : Form
    {
        public static String EPSILON = "Epsion";
        Analizador analizador = new Analizador();
        ArrayList rutas,rutas1,rutas2;
        int contador = 0,contador1=0,contador2=0;

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                TextArea1.Text = System.IO.File.ReadAllText(path);
            }
        }
        //Botón para Analizar el básico
        private void BtnCargar1_Click(object sender, EventArgs e)
        {
            string[] lineas = TextArea1.Text.Split('\n');
            rutas=analizador.AnalizarEntrada(lineas);
            if (rutas != null)
            {
                string[] auxrutas = (string[])rutas[0];
                string[] auxrutas2 = (string[])rutas[1];
                Console.WriteLine(auxrutas[0]);
                Console.WriteLine(auxrutas2[0]);
                AFNba.Image = new Bitmap(auxrutas[0]);
                AFDba.Image = new Bitmap(auxrutas[1]);
                tablaba.Image = new Bitmap(auxrutas[2]);
                if (rutas.Count > 0)
                {
                    btnAnBa.Enabled = true;
                    btSgBa.Enabled = true;
                }
                consolaBa.Text = auxrutas[3]+auxrutas2[3];
            }
            /*string[] exp = { "_", "digito", "|", "letra", "|", "*", "letra", "." };
            //string[] exp = { ".",".",".","*", "|", ".","c" ,"d", ".", "a", "b", "b", "c", "+", "d"};
            //exp.Reverse();
            Estructura estru = new Estructura(exp);
            estru.estructurar();
            estru.graficarAFN();
            estru.makeAFD();
            estru.graficarAFD();
            estru.graficarTabla();
            estru.validarLexema("bcdddd");
            /*List<Conjunto> con = new List<Conjunto>();
            Arbol arbol = new Arbol("r1",con);
            List<Token> tokens = new List<Token>();
            foreach (string item in exp)
            {
                if(item.Equals("*")|| item.Equals("|")|| item.Equals("+")|| item.Equals(".")|| item.Equals("?"))
                {
                    tokens.Add(new Token(item, ".", 1, 2, true));
                }
                else
                {
                    tokens.Add(new Token(item, ".", 1, 2, false));
                }
            }
            Nodo root =arbol.makeTree(tokens);
            Console.WriteLine(root.lexema);
            arbol.postfix(root);*/
        }
        //Botón adelante para el báisco
        private void button4_Click(object sender, EventArgs e)
        {
            if (contador>0 && contador <= rutas.Count-1)
            {
                //contador++;
                string[] rut = (string[])rutas[contador];
                AFNba.ImageLocation = @rut[0];
                AFNba.Refresh();
                AFDba.Load(rut[1]);
                AFDba.Update();
                tablaba.Image = Image.FromFile(rut[2]);
                tablaba.Update();
                contador++;
            }
            else
            {
                contador = rutas.Count - 1;
            }
        }
        //Boton atrás para el básico
        private void btnAnBa_Click(object sender, EventArgs e)
        {
            
            if (contador >=0 && contador<rutas.Count)
            {
                //contador--;
                string[] rut=(string[])rutas[contador];
                AFNba.Image = new Bitmap(rut[0]);
                AFDba.Image = new Bitmap(rut[1]);
                tablaba.Image = new Bitmap(rut[2]);
                contador--;
            }
            else
            {
                contador = 0;
            }
        }
        //Botón de analizar para el avanzado
        private void btnAnaav_Click(object sender, EventArgs e)
        {
            string[] lineas = inputav.Text.Split('\n');
            rutas2 = analizador.AnalizarEntrada(lineas);
            if (rutas2 != null)
            {
                string[] auxrutas = (string[])rutas2[0];
                string[] auxrutas2 = (string[])rutas2[1];
                Console.WriteLine(auxrutas[0]);
                Console.WriteLine(auxrutas2[0]);
                AFNav.Image = new Bitmap(auxrutas[0]);
                AFDav.Image = new Bitmap(auxrutas[1]);
                tablaav.Image = new Bitmap(auxrutas[2]);
                if (rutas2.Count > 0)
                {
                    btnAnaav.Enabled = true;
                    btnSgav.Enabled = true;
                }
                consolaav.Text = auxrutas[3];
            }
        }
        //butón sigueiente para los avanzados
        private void btnSgav_Click(object sender, EventArgs e)
        {
            if (contador2 > 0 && contador2 <= rutas1.Count - 1)
            {
                //contador++;
                string[] rut = (string[])rutas2[contador2];
                AFNav.ImageLocation = @rut[0];
                AFNav.Refresh();
                AFDav.Load(rut[1]);
                AFDav.Update();
                tablaav.Image = Image.FromFile(rut[2]);
                tablaav.Update();
                contador2++;
            }
            else
            {
                contador1 = rutas1.Count - 1;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextWriter txt = new StreamWriter(saveFileDialog1.FileName);
                txt.Write(TextArea1.Text);
                txt.Close();
            }
            
        }

        //Botón atras para los avanazados
        private void btnAnav_Click(object sender, EventArgs e)
        {
            if(contador2 >= 0 && contador2 < rutas2.Count)
            {
                //contador--;
                string[] rut = (string[])rutas2[contador2];
                AFNav.Image = new Bitmap(rut[0]);
                AFDav.Image = new Bitmap(rut[1]);
                tablaav.Image = new Bitmap(rut[2]);
                contador2--;
            }
            else
            {
                contador2 = 0;
            }
        }

        //Boton analizar para el intermedio
        private void btnAnain_Click(object sender, EventArgs e)
        {
            string[] lineas = inputin.Text.Split('\n');
            rutas1 = analizador.AnalizarEntrada(lineas);
            if (rutas1 != null)
            {
                string[] auxrutas = (string[])rutas1[0];
                string[] auxrutas2 = (string[])rutas1[1];
                Console.WriteLine(auxrutas[0]);
                Console.WriteLine(auxrutas2[0]);
                AFNin.Image = new Bitmap(auxrutas[0]);
                AFDin.Image = new Bitmap(auxrutas[1]);
                tablain.Image = new Bitmap(auxrutas[2]);
                if (rutas1.Count > 0)
                {
                    btnAnIn.Enabled = true;
                    btnSgIn.Enabled = true;
                }
                consolain.Text = auxrutas[3];
            }
        }
        //Botón de siguiente para el intermedio
        private void btnSgIn_Click(object sender, EventArgs e)
        {
            if (contador1 > 0 && contador1 <= rutas1.Count - 1)
            {
                //contador++;
                string[] rut = (string[])rutas1[contador1];
                AFNin.ImageLocation = @rut[0];
                AFNin.Refresh();
                AFDin.Load(rut[1]);
                AFDin.Update();
                tablain.Image = Image.FromFile(rut[2]);
                tablain.Update();
                contador1++;
            }
            else
            {
                contador1 = rutas1.Count - 1;
            }
        }
        //Botón de anterior para el intermedio
        private void btnAnIn_Click(object sender, EventArgs e)
        {
            if (contador1 >= 0 && contador1 < rutas1.Count)
            {
                //contador--;
                string[] rut = (string[])rutas1[contador1];
                AFNin.Image = new Bitmap(rut[0]);
                AFDin.Image = new Bitmap(rut[1]);
                tablain.Image = new Bitmap(rut[2]);
                contador1--;
            }
            else
            {
                contador1 = 0;
            }
        }
    }
}

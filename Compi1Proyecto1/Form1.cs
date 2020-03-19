using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void BtnCargar1_Click(object sender, EventArgs e)
        {
            string[] lineas = TextArea1.Text.Split('\n');
            analizador.AnalizarEntrada(lineas);
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
    }
}

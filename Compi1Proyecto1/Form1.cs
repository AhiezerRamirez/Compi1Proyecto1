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
        }
    }
}

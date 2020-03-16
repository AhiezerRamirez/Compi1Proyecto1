using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    public class Transicion
    {
        public Estado inicio, fin;
        public string simbolo;
        public Transicion(Estado inicio, Estado fin, string simbolo)
        {
            this.inicio = inicio;
            this.fin = fin;
            this.simbolo = simbolo;
        }

        public Estado getInicio()
        {
            return inicio;
        }

        public void setInicio(Estado inicio)
        {
            this.inicio = inicio;
        }

        public Estado getFin()
        {
            return fin;
        }


        public void setFin(Estado fin)
        {
            this.fin = fin;
        }

        public string getSimbolo()
        {
            return simbolo;
        }

        public void setSimbolo(string simbolo)
        {
            this.simbolo = simbolo;
        }


        public String toString()
        {
            return "(" + inicio.getId() + "-" + simbolo + "-" + fin.getId() + ")";
        }
        public String DOT_String()
        {
            return ("\""+this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"" + this.simbolo + "\"];");
        }

        public string DOT_Tabla()
        {
            return "";
        }
    }
}

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
        public string simbolo,tipo;
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
            if (this.simbolo.Equals("\n"))
            {
                return ("\"" + this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"SaltoLinea\"];");
            }else if (this.simbolo.Equals("\t"))
            {
                return ("\"" + this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"tabulacion\"];");
            }else if (this.simbolo.Equals("\'"))
            {
                return ("\"" + this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"\\\'\"];");
            }else if (this.simbolo.Equals("\""))
            {
                return ("\"" + this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"\\\"\"];");
            }else
                return ("\""+this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"" + this.simbolo + "\"];");
        }

        public string DOT_Tabla()
        {
            return "";
        }
    }
}

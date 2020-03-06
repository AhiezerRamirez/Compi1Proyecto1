using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Token
    {
        public string lexema,tipo;
        public int i, j;
        public bool operador;
        public Token(string lexema,string tipo,int i, int j,bool op)
        {
            this.lexema = lexema;
            this.tipo = tipo;
            this.i = i;
            this.j = j;
            this.operador = op;
        }
    }
}

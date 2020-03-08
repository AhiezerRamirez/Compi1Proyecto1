using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Expresion
    {
        public Token nombre;
        public List<Token> preorden,inorden;
        public Expresion(Token nombre)
        {
            this.nombre = nombre;
            this.preorden = new List<Token>();
            this.inorden = new List<Token>();
        }
    }
}

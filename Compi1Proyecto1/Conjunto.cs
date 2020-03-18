using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Conjunto
    {
        public Token nombre;
        public List<Token> elementos;
        public Conjunto(Token nombre)
        {
            this.nombre = nombre;
            this.elementos = new List<Token>();
        }

        public Conjunto() { }
    }
}

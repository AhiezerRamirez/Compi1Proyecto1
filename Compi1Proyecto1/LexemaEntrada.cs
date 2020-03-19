using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class LexemaEntrada
    {
        public Token nombre;
        public string entrada;
        public LexemaEntrada(Token nombre,string entrada)
        {
            this.nombre = nombre;
            this.entrada = entrada;
        }
    }
}

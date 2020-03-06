using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Compi1Proyecto1
{
    class Core
    {
        ArrayList tokens;
        List<Token> conjutos,expresiones,lexemas;
        public Core() {
            this.conjutos = new List<Token>();
            this.expresiones = new List<Token>();
            this.lexemas = new List<Token>();
        }

        public void separarConjuntos(ArrayList tok)
        {
            tokens = tok;
            int auxExpresion = 0;
            int auxLexemas = 0;
            
        }   
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Automata
    {
        Estado inicia;
        ArrayList aceptacion,estados;
        HashSet<string> alfabeto;       //puede que se necesite cambiar el tipo de dato al almacenar
        string tipo;

        public Automata()
        {
            this.estados = new ArrayList();
            this.aceptacion = new ArrayList();
            this.alfabeto = new HashSet<string>();

        }
    }
}

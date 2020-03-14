using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    public class Cerradura
    {
        public Cerradura() { }

        public HashSet<Estado> metodoCerradura(Estado state)
        {
            Stack<Estado> pila = new Stack<Estado>();
            Estado actual = state;
            actual.getTransiciones();
            HashSet<Estado> rusultado = new HashSet<Estado>();
            pila.Push(actual);
            while (pila.Count != 0)
            {
                actual = pila.Pop();
                foreach (Transicion item in actual.getTransiciones())
                {
                    if(item.getSimbolo().Equals(Form1.EPSILON)&& !rusultado.Contains(item.getFin()))
                    {
                        rusultado.Add(item.getFin());
                        pila.Push(item.getFin());
                    }
                }
            }
            rusultado.Add(state);
            return rusultado;
        }

        /*public Estado mover(Estado estado, string simbolo)
        {
            ArrayList alcanzados = new ArrayList();
           
            foreach (Transicion transicion in (ArrayList)estado.getTransiciones())
            {
                Estado estadosiguiente = transicion.getFin();
                string lexema = transicion.getSimbolo();
                if (lexema.Equals(simbolo) && !alcanzados.Contains(estadosiguiente))
                {
                    alcanzados.Add(estadosiguiente);
                }
            }
            return (Estado)alcanzados[0];
        }*/

        public HashSet<Estado>mover(HashSet<Estado> estados, string simbolo)
        {
            HashSet<Estado> alcanzados = new HashSet<Estado>();
            IEnumerator<Estado> iterador = estados.GetEnumerator();
            while (iterador.MoveNext())
            {
                
                foreach (Transicion item in (ArrayList)iterador.Current.getTransiciones())
                {
                    Estado siguiente = item.getFin();
                    string simb = item.getSimbolo();
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }
                }
            }
            return alcanzados;
        }
    }
}

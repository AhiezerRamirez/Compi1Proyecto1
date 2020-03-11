using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    /*class Tipo
    {
        public  enum tipo
        {
            OR,AND,KLEEN,TERM
        }
    }*/
    abstract class Estructura
    {
        //Tipo tipo;
        //int cont;
        //public abstract Object ejecutar();
        //public abstract Object numerar();
        List<Token> ERs;
        Automata AFN;
        public Estructura(List<Token> ers)
        {
            this.ERs = ers;
        }

        public void estructurar()
        {
            Stack<Automata> pila = new Stack<Automata>();
            try {
                foreach (Token item in ERs)
                {
                    switch (item.lexema)
                    {
                        case "*":
                            Automata kleen;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public Automata cerraduraKleen(Automata afn)
        {
            Automata afnkleen = new Automata();
            Estado nuevoinicio = new Estado(0);
            afnkleen.addEstados(nuevoinicio);
            afnkleen.setEstadoInicial(nuevoinicio);

            int q = 1;
            foreach (Estado item in afn.getEstados())
            {
                Estado temp = item;
                temp.setId(q);
                q++;
                afnkleen.addEstados(temp);
            }

            Estado nuevoFin = new Estado(afn.getEstados().Count + 1);
            afnkleen.addEstados(nuevoFin);
            afnkleen.addEstadosAceptacion(nuevoFin);

            Estado anteriorInicio = afn.getEstadoInicial();
            ArrayList anteriorFin = afn.getEstadosAceptacion();
            

            nuevoinicio.getTransiciones().Add(new Transicion(nuevoinicio, anteriorInicio, Form1.EPSILON));
            nuevoinicio.getTransiciones().Add(new Transicion(nuevoinicio, nuevoFin, Form1.EPSILON));

            foreach (Estado estado in anteriorFin)
            {
                estado.getTransiciones().Add(new Transicion(estado, anteriorInicio, Form1.EPSILON));
                estado.getTransiciones().Add(new Transicion(estado, nuevoFin, Form1.EPSILON));
            }
            afnkleen.setAlfabeto(afn.getAlfabeto());

            return afnkleen;
        }
    }

    /*class Or : Estructura
    {
        Estructura est1,est2;
        int n1, n2, n3, n4, n5, n6;

        public Or(Estructura est1,Estructura est2)
        {
            this.est1 = est1;
            this.est2 = est2;
        }

        public override Object ejecutar()
        {
            return null;
        }
    }

    class And : Estructura
    {
        Estructura est1, est2;
        int n1, n2, n3;
        public And(Estructura est1, Estructura est2)
        {
            this.est1 = est1;
            this.est2 = est2;
        }
        public override object ejecutar()
        {
            return null;
        }
    }

    class Kleen : Estructura
    {
        Estructura est1;
        int n1, n2, n3, n4;
        public Kleen(Estructura est1)
        {
            this.est1 = est1;
        }
        public override object ejecutar()
        {
            return null;
        }
    }*/
}

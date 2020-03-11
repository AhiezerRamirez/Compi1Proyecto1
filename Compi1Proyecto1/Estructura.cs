using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Tipo
    {
        public  enum tipo
        {
            OR,AND,KLEEN,TERM
        }
    }
    abstract class Estructura
    {
        Tipo tipo;
        int cont;
        public abstract Object ejecutar();
        //public abstract Object numerar();
    }

    class Or : Estructura
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
    }
}

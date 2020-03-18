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
        List<Conjunto> conjuntos;
        List<Expresion> expresiones;
        List<Token> auxconjuntos,auxexpresiones,lexemas;
        public Core() {
            this.auxconjuntos = new List<Token>();
            this.conjuntos = new List<Conjunto>();
            this.auxexpresiones = new List<Token>();
            this.lexemas = new List<Token>();
            this.expresiones = new List<Expresion>();
        }

        public void separarConjuntos(ArrayList tok)
        {
            for(int i=0; i< tok.Count; i++)
            {
                Token auxtoken = (Token)tok[i];
                if (auxtoken.lexema.Equals("CONJ"))
                {
                    while (!auxtoken.lexema.Equals(";"))
                    {
                        auxconjuntos.Add(auxtoken);
                        i++;
                        auxtoken = (Token)tok[i];
                    }
                    auxconjuntos.Add(auxtoken);
                    i++;
                }
            }

            for (int i = 0; i < auxconjuntos.Count-1; i++)
            {
                Token auxtoken = (Token)auxconjuntos[i];
                if (auxtoken.lexema == " ")
                    auxconjuntos.RemoveAt(i);
            }

            for (var item=0; item <auxconjuntos.Count;item++)
            {
                Token auxtoken = (Token)auxconjuntos[item];
                string nombre = "";
                if (auxtoken.lexema == "CONJ")
                {
                    item+=2;
                    auxtoken = (Token)auxconjuntos[item];
                    Token auxtoken1 = (Token)auxconjuntos[item + 1];
                    int x = auxtoken.i,y=auxtoken.j;
                    while (!auxtoken.lexema.Equals("-") && !auxtoken1.lexema.Equals(">"))
                    {
                        nombre += auxtoken.lexema;
                        item++;
                        auxtoken1 = (Token)auxconjuntos[item + 1];
                        auxtoken = (Token)auxconjuntos[item];
                    }
                    item+=2;
                    Conjunto tempconjunto = new Conjunto(new Token(nombre, "Econjunto", x, y, false));
                    auxtoken = (Token)auxconjuntos[item];
                    while (!auxtoken.lexema.Equals(";"))
                    {
                        tempconjunto.elementos.Add(auxtoken);
                        item++;
                        auxtoken = (Token)auxconjuntos[item];
                    }
                    tempconjunto.elementos.Add(auxtoken);
                    conjuntos.Add(tempconjunto);
                }
            }
            foreach (var item in conjuntos)
            {
                Console.Write("Nombre del conjunto es: ");
                Console.WriteLine(item.nombre.lexema);
                foreach (Token i in item.elementos)
                {
                  Console.Write(i.lexema);
                }
                Console.WriteLine();
            }
        }

        public void separarExpresiones(ArrayList exp)
        {
            for (int i = 2; i < exp.Count-5; i++)
            {
                Token auxtoken = (Token)exp[i-1];
                Token auxtoken1 = (Token)exp[i];
                Token auxtoken2 = (Token)exp[i+1];
                if (!auxtoken.lexema.Equals("CONJ") && auxtoken2.lexema.Equals("->"))
                {
                    i += 1;
                    while (!auxtoken1.lexema.Equals(";"))
                    {
                        auxexpresiones.Add(auxtoken1);
                        auxtoken1 = (Token)exp[i];
                        i++;
                    }
                    auxexpresiones.Add(auxtoken1);
                }
            }
            /*foreach (Token item in auxexpresiones)
            {
                Console.WriteLine(item.lexema);
            }*/
            
            for (int i = 0; i < auxexpresiones.Count; i++)
            {
                
                Token auxtoken1 = (Token)auxexpresiones[i];
                if (auxtoken1.lexema == "->")
                {
                    Token auxtoken = (Token)auxexpresiones[i-1];
                    Expresion auxexpresion = new Expresion(auxtoken);
                    i++;
                    while (!auxtoken.lexema.Equals(";"))
                    {
                        auxtoken = (Token)auxexpresiones[i];
                        if (auxexpresiones[i - 1].lexema.Equals("{"))
                        {
                            auxtoken.lexema = "{" + auxtoken.lexema+"}";
                        }
                        auxexpresion.preorden.Add(auxtoken);
                        i++;
                    }
                    expresiones.Add(auxexpresion);
                }
            }

            
            foreach (Expresion item in expresiones)
            {
                Console.WriteLine(item.nombre.lexema);
                foreach (var item2 in item.preorden)
                {
                    Console.Write(item2.lexema);
                }
                Console.WriteLine();
            }
        }

        public void separarLexemas(ArrayList lex)
        {

        }

        public void maketreeValidacion()
        {
            Arbol arbol = new Arbol("r1", conjuntos);
            expresiones[0].preorden.RemoveAll(tokens => tokens.lexema.Equals("{"));
            expresiones[0].preorden.RemoveAll(tokens => tokens.lexema.Equals("}"));
            Nodo root = arbol.makeTreeValidacion(expresiones[0].preorden);
            Console.WriteLine(root.lexema);
            arbol.postfix(root);
        }
    }
}

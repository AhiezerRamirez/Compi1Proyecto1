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
        List<LexemaEntrada> lexemaEntrada;
        List<Token> auxconjuntos,auxexpresiones,lexemas;

        public Core() {
            this.auxconjuntos = new List<Token>();
            this.conjuntos = new List<Conjunto>();
            this.auxexpresiones = new List<Token>();
            this.lexemas = new List<Token>();
            this.expresiones = new List<Expresion>();
            this.lexemaEntrada = new List<LexemaEntrada>();
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
            for (int i=0;i<lex.Count-1;i++)
            {
                Token ttoken = (Token)lex[i];
                Token auxtoken = (Token)lex[i+1];
                if (auxexpresiones.Any(x => x.lexema.Equals(ttoken.lexema))&& auxtoken.lexema.Equals(":"))
                {
                    i+=2;
                    auxtoken = (Token)lex[i];
                    lexemaEntrada.Add(new LexemaEntrada(ttoken, auxtoken.lexema));
                }
            }

            Console.WriteLine(lexemaEntrada.Count);
            foreach (LexemaEntrada item in lexemaEntrada)
            {
                Console.Write(item.nombre.lexema+": ");
                Console.WriteLine(item.entrada);
            }
        }

        public void maketreeValidacion()
        {
            foreach (Expresion item in this.expresiones)
            {
                Arbol arbol = new Arbol(item.nombre.lexema, this.conjuntos);
                item.preorden.RemoveAll(tokens => tokens.lexema.Equals("{"));
                item.preorden.RemoveAll(tokens => tokens.lexema.Equals("}"));
                Nodo root1=arbol.makeTree(item.preorden);
                arbol.postfix2(root1);
                List<string> listatokens2 = arbol.tokens2;
                listatokens2.Reverse();
                string[] tokensstrin2 = listatokens2.ToArray();
                Estructura estru2 = new Estructura(tokensstrin2);
                estru2.estructurar();
                estru2.graficarAFN();
                estru2.makeAFD();
                estru2.graficarAFN();
                estru2.graficarTabla();//Tengo que mandarle un número para que cada gráfica se llame difente

                //-----------------Para validar lexemas

                Nodo root = arbol.makeTreeValidacion(item.preorden);
                arbol.postfix(root);
                List<string> listatokens = arbol.tokens;
                listatokens.Reverse();
                string[] tokensstring = listatokens.ToArray();
                Estructura estru = new Estructura(tokensstring);
                estru.estructurar();
                estru.makeAFD();
                foreach(LexemaEntrada lex in this.lexemaEntrada)
                {
                    if (item.nombre.lexema.Equals(lex.nombre.lexema))
                    {
                        estru.validarLexema(lex.entrada);//Solo falta probar y afinar detalles
                    }
                }
                //estru.validarLexema("bcdddd");
                try
                {
                    //arbol.
                }
                catch (Exception)
                {

                    throw;
                }
            }
            /*Arbol arbol = new Arbol("r1", conjuntos);
            expresiones[1].preorden.RemoveAll(tokens => tokens.lexema.Equals("{"));
            expresiones[1].preorden.RemoveAll(tokens => tokens.lexema.Equals("}"));
            Nodo root = arbol.makeTreeValidacion(expresiones[1].preorden);
            arbol.postfix(root);
            Console.WriteLine("\n");
            List<string> listatokens = arbol.tokens;
            listatokens.Reverse();
            string[] tokensstring = listatokens.ToArray();
            foreach (string item in tokensstring)
            {
                Console.Write(item);
            }
            Console.WriteLine("Fin de la entrada mofificada");
            Console.WriteLine("\n");
            
            Estructura estru = new Estructura(tokensstring);
            estru.estructurar();
            //estru.graficarAFN();
            estru.makeAFD();
            estru.graficarAFD();
            estru.validarLexema("bcdddd");*/
        }
    }
}

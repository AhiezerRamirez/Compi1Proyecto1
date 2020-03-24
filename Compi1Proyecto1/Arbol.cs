using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    public class Nodo
    {
        public Nodo left, right;
        public string lexema;
        public bool operador;
        public Nodo(string lexema)
        {
            this.lexema = lexema;
            this.left = null;
            this.right = null;
            this.operador = false;
        }
    }

    class Arbol
    {
        
        string NombreExpresion;
        List<Conjunto> conjuntos;
        public List<Nodo> tokens,tokens2; 

        public Arbol(string nombre,List<Conjunto> conjunto)
        {
            this.NombreExpresion = nombre;
            this.conjuntos = conjunto;
            this.tokens = new List<Nodo>();
            this.tokens2 = new List<Nodo>();
        }
        
        public Nodo makeTree(List<Token> prefix)
        {
            Stack<Nodo> stack = new Stack<Nodo>();
            Nodo t, t1, t2,t3,t4;
            prefix.Reverse();
            foreach (Token token in prefix)
            {
                //Console.WriteLine(token.lexema + " Dentro del arbol");
                if (token.operador == false)
                {
                    t = new Nodo(token.lexema);
                    stack.Push(t);
                }else if (token.lexema.Equals(".") || token.lexema.Equals("|"))
                {
                    t = new Nodo(token.lexema);
                    t1 = stack.Pop();
                    t2 = stack.Pop();
                    t.right = t2;
                    t.left = t1;
                    t.operador = true;
                    stack.Push(t);
                }else if (token.lexema.Equals("*"))
                {
                    t = new Nodo(token.lexema);
                    t1 = stack.Pop();
                    t.right = t1;
                    t.operador = true;
                    stack.Push(t);
                }
                else if (token.lexema.Equals("+"))
                {
                    t = new Nodo(".");
                    t.operador = true;
                    t2 = new Nodo("*");
                    t2.operador = true;
                    t.right = t2;
                    t4 = stack.Pop();
                    t3 = t4;
                    t.left = t3;
                    t2.right = t4;
                    stack.Push(t);
                }
                else if (token.lexema.Equals("?"))
                {
                    t = new Nodo("|");
                    t.operador = true;
                    t2 = new Nodo(Form1.EPSILON);
                    t.left = t2;
                    t1 = stack.Pop();
                    t.right = t1;
                    stack.Push(t);
                }
            }
            t = stack.Pop();
            return t;
        }

        public void postfix(Nodo root)
        {
            if (root == null)
                return;
            tokens.Add(root);
            postfix(root.left);
            //Console.Write(root.lexema);
            postfix(root.right);
            //Console.Write(root.lexema);
            
        }

        public void postfix2(Nodo root)
        {
            if (root == null)
                return;
            tokens2.Add(root);
            postfix2(root.left);
            postfix2(root.right);

        }

        public Nodo makeTreeValidacion(List<Token> prefix)
        {
            Stack<Nodo> stack = new Stack<Nodo>();
            Nodo t, t1, t2, t3, t4;
            //prefix.Reverse();
            foreach (Token token in prefix)
            {
                //Console.WriteLine(token.lexema + " Dentro del arbol de validar");
                if (token.operador == false)
                {
                    if (token.lexema.StartsWith("{") && !token.tipo.Equals("cadena"))
                    {
                        Stack<Nodo> st = new Stack<Nodo>();
                        //Console.WriteLine("El lexema del conjunto es {0}", token.lexema);
                        string auxlexema = token.lexema.Trim('{', '}');
                        //Console.WriteLine("El trim  es {0}", auxlexema);
                        Conjunto auxconjunto = new Conjunto() ;
                        foreach (Conjunto item in conjuntos)
                        {
                            if (item.nombre.lexema.Equals(auxlexema))
                            {
                                auxconjunto = item;
                                auxconjunto.nombre = item.nombre;
                                auxconjunto.elementos = item.elementos;
                            }
                        }
                        if (auxconjunto.elementos[1].lexema.Equals("~"))
                        {
                            for (int i = (int)Char.Parse(auxconjunto.elementos[0].lexema); i <=(int)Char.Parse(auxconjunto.elementos[2].lexema); i++)
                            {
                                //char ch = ((char)i);
                                //if (!Char.IsLetterOrDigit(ch))
                                //{
                                    t1 = new Nodo(((char)i).ToString());
                                    st.Push(t1);
                                //}
                                
                            }
                            int c = st.Count;
                            while (st.Count != 0)
                            {
                                if (c == st.Count)
                                {
                                    Nodo aux1 = st.Pop();
                                    Nodo aux2 = st.Pop();
                                    Nodo or = new Nodo("|");
                                    or.operador = true;
                                    or.left = aux1;
                                    or.right = aux2;
                                    stack.Push(or);
                                }
                                t3 = new Nodo("|");
                                t3.operador = true;
                                t1 = st.Pop();
                                t2 = stack.Pop();
                                t3.left = t2;
                                t3.right = t1;
                                stack.Push(t3);
                            }
                        }
                        else
                        {
                            foreach (Token item in auxconjunto.elementos)
                            {
                                st.Push(new Nodo(item.lexema));
                            }
                            int c = st.Count;
                            while (st.Count != 0)
                            {
                                if (c == st.Count)
                                {
                                    Nodo aux1 = st.Pop();
                                    Nodo aux2 = st.Pop();
                                    Nodo or = new Nodo("|");
                                    or.operador = true;
                                    or.left = aux1;
                                    or.right = aux2;
                                    stack.Push(or);
                                }
                                t3 = new Nodo("|");
                                t3.operador = true;
                                t1 = st.Pop();
                                t2 = stack.Pop();
                                t3.left = t2;
                                t3.right = t1;
                                stack.Push(t3);
                            }

                        }
                    }
                    else
                    {
                        t = new Nodo(token.lexema);
                        stack.Push(t);
                    }
                    
                }
                else if (token.lexema.Equals(".") || token.lexema.Equals("|"))
                {
                    t = new Nodo(token.lexema);
                    t1 = stack.Pop();
                    t2 = stack.Pop();
                    t.right = t2;
                    t.left = t1;
                    t.operador = true;
                    stack.Push(t);
                }
                else if (token.lexema.Equals("*"))
                {
                    t = new Nodo(token.lexema);
                    t1 = stack.Pop();
                    t.right = t1;
                    t.operador = true;
                    stack.Push(t);
                }
                else if (token.lexema.Equals("+"))
                {
                    t = new Nodo(".");
                    t.operador = true;
                    t2 = new Nodo("*");
                    t2.operador = true;
                    t.right = t2;
                    t4 = stack.Pop();
                    t3 = t4;
                    t.left = t3;
                    t2.right = t4;
                    stack.Push(t);
                }
                else if (token.lexema.Equals("?"))
                {
                    t = new Nodo("|");
                    t.operador = true;
                    t2 = new Nodo(Form1.EPSILON);
                    t.left = t2;
                    t1 = stack.Pop();
                    t.right = t1;
                    stack.Push(t);
                }
            }
            t = stack.Pop();
            return t;
        }
    }
    
}

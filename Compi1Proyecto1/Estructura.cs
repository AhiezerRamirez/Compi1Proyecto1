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
    public class Estructura
    {
        //Tipo tipo;
        //int cont;
        //public abstract Object ejecutar();
        //public abstract Object numerar();
        List<Nodo> ERs;
        Cerradura cerradura;
        Automata AFN;
        Automata AFD;
        XMLFile mLFile;
        public Estructura(List<Nodo> ers)
        {
            this.ERs = ers;
            this.cerradura = new Cerradura();
            this.mLFile = new XMLFile();
        }

        public void estructurar()
        {
            Stack<Automata> pila = new Stack<Automata>();
            try {
                ERs.Reverse();
                foreach (Nodo item in ERs)
                {
                    if (item.lexema.Equals("*") && item.operador == true)
                    {

                        Automata kleen = cerraduraKleen(pila.Pop());
                        pila.Push(kleen);
                        this.AFN = kleen;
                    }else if (item.lexema.Equals(".") && item.operador == true)
                    {
                        Automata concat_param1 = (Automata)pila.Pop();
                        Automata concat_param2 = (Automata)pila.Pop();
                        Automata concat_result = concatenacion(concat_param2, concat_param1);

                        pila.Push(concat_result);
                        this.AFN = concat_result;
                    }else if(item.lexema.Equals("|") && item.operador == true)
                    {
                        Automata union_param1 = (Automata)pila.Pop();
                        Automata union_param2 = (Automata)pila.Pop();
                        Automata union_result = uninion(union_param2, union_param1);
                        pila.Push(union_result);
                        this.AFN = union_result;
                    }
                    else
                    {
                        Automata simple = Term(item.lexema);
                        pila.Push(simple);
                        this.AFN = simple;
                    }
                }
                List<string> alfab = new List<string>();
                foreach (Nodo item in ERs)
                {
                    alfab.Add(item.lexema);
                }
                string[] auxalfb = alfab.ToArray();
                this.AFN.createAlfabeto(ERs);
                this.AFN.setTipo("AFN");
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error en la expresion regular");
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
            afnkleen.setLenguajeR(afn.getLenguajeR());
            return afnkleen;
        }

        public Automata Term(string simbolo)
        {
            Automata afn = new Automata();
            Estado inicial = new Estado(0);
            Estado aceptacion = new Estado(1);
            Transicion tran = new Transicion(inicial, aceptacion, simbolo);
            inicial.setTransiciones(tran);
            afn.addEstados(inicial);
            afn.addEstados(aceptacion);
            afn.setInicial(inicial);
            afn.addEstadosAceptacion(aceptacion);
            afn.setLenguajeR(simbolo + " ");
            return afn;
        }

        public Automata concatenacion(Automata afn1,Automata afn2)
        {
            Automata afn_concat = new Automata();
            int i = 0;
            for ( i = 0; i < afn2.getEstados().Count; i++)
            {
                Estado temp = (Estado)afn2.getEstados()[i];
                temp.setId(i);
                if (i == 0)
                {
                    afn_concat.setEstadoInicial(temp);
                }
                if (i == afn2.getEstados().Count - 1)
                {
                    for (int k = 0; k < afn2.getEstadosAceptacion().Count; k++)
                    {
                        temp.setTransiciones(new Transicion((Estado)afn2.getEstadosAceptacion()[k],afn1.getEstadoInicial(),Form1.EPSILON));
                    }
                }
                afn_concat.addEstados(temp);
            }

            for (int j = 0; j < afn1.getEstados().Count; j++)
            {
                Estado tmp = (Estado)afn1.getEstados()[j];
                tmp.setId(i);

                //define el ultimo con estado de aceptacion
                if (afn1.getEstados().Count - 1 == j)
                    afn_concat.addEstadosAceptacion(tmp);
                afn_concat.addEstados(tmp);
                i++;
            }
            HashSet<string> alfabeto = new HashSet<string>(afn1.getAlfabeto());
            //alfabeto.Union(afn1.getAlfabeto());
            alfabeto.UnionWith(afn2.getAlfabeto());
            afn_concat.setAlfabeto(alfabeto);
            afn_concat.setLenguajeR(afn1.getLenguajeR() + " " + afn2.getLenguajeR());
            return afn_concat;
        }

        public Automata uninion(Automata afn1, Automata afn2)
        {
            Automata afn_union = new Automata();
            Estado nuevoinicio = new Estado(0);
            nuevoinicio.setTransiciones(new Transicion(nuevoinicio,afn2.getEstadoInicial(),Form1.EPSILON));
            afn_union.addEstados(nuevoinicio);
            afn_union.setEstadoInicial(nuevoinicio);
            int i = 0;
            for (i = 0; i < afn1.getEstados().Count; i++)
            {
                Estado tmp = (Estado)afn1.getEstados()[i];
                tmp.setId(i + 1);
                afn_union.addEstados(tmp);
            }
            for (int j = 0; j < afn2.getEstados().Count; j++)
            {
                Estado tmp = (Estado)afn2.getEstados()[j];
                tmp.setId(i + 1);
                afn_union.addEstados(tmp);
                i++;
            }

            Estado nuevoFin = new Estado(afn1.getEstados().Count+ afn2.getEstados().Count + 1);
            afn_union.addEstados(nuevoFin);
            afn_union.addEstadosAceptacion(nuevoFin);

            Estado anteriorInicio = afn1.getEstadoInicial();
            ArrayList anteriorFin = afn1.getEstadosAceptacion();
            ArrayList anteriorFin2 = afn2.getEstadosAceptacion();

            nuevoinicio.getTransiciones().Add(new Transicion(nuevoinicio, anteriorInicio, Form1.EPSILON));
            //Console.Write(anteriorFin[0]);
            for (int k = 0; k < anteriorFin.Count; k++)
            {
                Estado aux =(Estado) anteriorFin[k];
                aux.getTransiciones().Add(new Transicion((Estado)anteriorFin[k], nuevoFin, Form1.EPSILON));
            }

            for (int k = 0; k < anteriorFin.Count; k++)
            {
                Estado aux = (Estado)anteriorFin2[k];
                aux.getTransiciones().Add(new Transicion((Estado)anteriorFin2[k], nuevoFin, Form1.EPSILON));
            }

            HashSet<string> alfabeto = new HashSet<string>(afn1.getAlfabeto());
            alfabeto.UnionWith(afn2.getAlfabeto());
            afn_union.setAlfabeto(alfabeto);
            afn_union.setLenguajeR(afn1.getLenguajeR()+" "+afn2.getLenguajeR());
            return afn_union;
        }

        public void graficarAFN(string er)
        {
            string texto = "digraph automata_finito {\n";
            texto += "\trankdir=LR;" + "\n";
            texto += "\tnode [shape=doublecircle, style = filled,color = mediumseagreen];";
            foreach (Estado item in this.AFN.getEstadosAceptacion())
            {
                texto += " \"" + item.id+"\"";
                //Console.WriteLine(item.id);
            }

            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";
            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> \"" + this.AFN.getEstadoInicial().id + "\" [label=\"inicio\"];\n";
            foreach (Estado item in this.AFN.getEstados())
            {
                foreach (Transicion transicion in item.transiciones)
                {
                    //Console.WriteLine(transicion.DOT_String());
                    texto += "\t" + transicion.DOT_String()+"\n";
                }
            }
            texto += "}";
            System.IO.File.WriteAllText(@"C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\"+er+"_AFN.dot",texto);
            System.Diagnostics.Process.Start("CMD.exe", "/C dot C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_AFN.dot -Tpng -o C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_AFN.png");
        }

        public Automata getAfn()
        {
            return this.AFN;
        }

        public void setAfn(Automata afn)
        {
            this.AFN = afn;
        }
        
        public void makeAFD()
        {
            Automata automata = new Automata();
            Queue<HashSet<Estado>> cola = new Queue<HashSet<Estado>> ();
            Estado inicial = new Estado(0);
            automata.setInicial(inicial);
            automata.addEstados(inicial);
            HashSet<Estado> arrayInicial = cerradura.metodoCerradura(this.AFN.getEstadoInicial());

            foreach (Estado item in this.AFN.getEstadosAceptacion())
            {
                foreach (Estado estado in arrayInicial)
                {
                    //Console.WriteLine("La primer cerradura es: " + estado.id);
                    if (estado.id.Equals(item.id))
                    {
                        automata.addEstadosAceptacion(inicial);
                    }
                }
                
            }

            cola.Enqueue(arrayInicial);
            ArrayList temp = new ArrayList();
            int index=0;
            
            while (cola.Count!=0)
            {
                HashSet<Estado> aux = cola.Dequeue();
                
                foreach (string item in this.AFN.getAlfabeto())
                {
                    
                    HashSet<Estado> moveresponse = cerradura.mover(aux, item);
                    HashSet<Estado> result = new HashSet<Estado>();
                    foreach (Estado est in moveresponse)
                    {
                        result.UnionWith(cerradura.metodoCerradura(est));
                    }
                    Estado prev =(Estado) automata.getEstados()[index];
                    string auxstring = "";
                    IEnumerable<Estado> query = result.OrderBy(results => results.id);
                    foreach (Estado auxestado in query)
                    {
                        //auxarraestado.Add(auxestado.id.ToString());
                        auxstring += auxestado.id.ToString();
                    }
                    if (result.Count != 0)
                    {
                        if (temp.Contains(auxstring))
                        {
                            //Console.WriteLine("if dentro del primer if");
                            ArrayList prevarray = automata.getEstados();
                            Estado oldstate = prev;
                            Estado nextstate = (Estado)prevarray[temp.IndexOf(auxstring) + 1];
                            //Console.WriteLine(temp.IndexOf(auxstring)+"de if");
                            oldstate.setTransiciones(new Transicion(oldstate, nextstate, item));
                        }
                        else
                        {
                            //Console.WriteLine("if con el eslse adentro");
                            temp.Add(auxstring);
                            cola.Enqueue(result);
                            Estado nuevo = new Estado(temp.IndexOf(auxstring) + 1);
                            //Console.WriteLine(temp.IndexOf(auxstring) + "del else");
                            prev.setTransiciones(new Transicion(prev, nuevo, item));
                            automata.addEstados(nuevo);
                            foreach (Estado aceptacion in this.AFN.getEstadosAceptacion())
                            {

                                foreach (Estado es in result)
                                {
                                    //Console.WriteLine(aceptacion.id + " del afd "+es.id);
                                    if (es.id.Equals(aceptacion.id))
                                    {
                                        automata.addEstadosAceptacion(nuevo);
                                    }
                                }

                            }
                        }
                    }
                }
                index++;
            }
            this.AFD = automata;
            this.AFD.setAlfabeto(this.AFN.getAlfabeto());
            this.AFD.setTipo("AFD");
        }

        public void graficarAFD(string er)
        {
            string texto = "digraph AFD {\n";
            texto += "\trankdir=LR;" + "\n";
            texto += "\tnode [shape=doublecircle, style = filled,color = mediumseagreen];";
            foreach (Estado item in this.AFD.getEstadosAceptacion())
            {
                texto += " \"" + item.id + "\"";
            }

            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";
            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> \"" + this.AFD.getEstadoInicial().id + "\" [label=\"inicio\"];\n";
            foreach (Estado item in this.AFD.getEstados())
            {
                foreach (Transicion transicion in item.transiciones)
                {
                    //Console.WriteLine(transicion.DOT_String());
                    texto += "\t" + transicion.DOT_String() + "\n";
                }
            }
            texto += "}";
            System.IO.File.WriteAllText(@"C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\"+er+"_AFD.dot", texto);
            System.Diagnostics.Process.Start("CMD.exe", "/C dot C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_AFD.dot -Tpng -o C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_AFD.png");
        }

        public void graficarTabla(string er)
        {
            List<nodovertical> listavertical = new List<nodovertical>();
            List<nodohorizontal> listahorizontal = new List<nodohorizontal>();
            string texto = "digraph tabla{ \n \trankdir=TB;\n\tnode [shape=rectangle, height=0.5, width=0.5];\n\tgraph[ nodesep = 0.5];\n";
            
            foreach (Estado item in this.AFD.getEstados())
            {
                nodovertical auxvertical=new nodovertical(item.id.ToString(), "y" + item.id.ToString());
                foreach (Transicion trans in item.getTransiciones())
                {
                    if (trans.simbolo.Equals("\n"))
                    {
                        auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + item.id.ToString(), "SalLin"));
                    }
                    else if (trans.simbolo.Equals("\t"))
                    {
                        auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + item.id.ToString(), "Tab"));
                    }
                    else if (trans.simbolo.Equals("\""))
                    {
                        auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + item.id.ToString(), "\"\\\""));
                    }
                    else if (trans.simbolo.Equals("'"))
                    {
                        auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + item.id.ToString(), "\"\\\'\""));
                    }
                    else
                    {
                        auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + item.id.ToString(), trans.simbolo));
                    }
                    //auxvertical.estados.Add(new TableTrans(trans.fin.toString(), "xy" + con.ToString(),trans.simbolo));
                }
                listavertical.Add(auxvertical);
            }
            int con = 0;
            foreach (string item in this.AFD.getAlfabeto())
            {
                nodohorizontal auxnodo;
                if (item.Equals("\n"))
                {
                    auxnodo = new nodohorizontal("SalLin", "x" + con.ToString());
                }
                else if (item.Equals("\t"))
                {
                    auxnodo= new nodohorizontal("Tab", "x" + con.ToString());
                }
                else if (item.Equals("\""))
                {
                    auxnodo= new nodohorizontal("\\\"", "x" + con.ToString());
                }
                else if (item.Equals("'"))
                {
                    auxnodo= new nodohorizontal("\\'", "x" + con.ToString());
                }
                else
                {
                    auxnodo= new nodohorizontal(item, "x" + con.ToString());
                }
                
                foreach (nodovertical nodo in listavertical)
                {
                    foreach (TableTrans table in nodo.estados)
                    {
                        if (item.Equals(table.trans))
                        {
                            table.nombre = table.nombre + con.ToString();
                            auxnodo.estados.Add(table);
                        }
                        
                    }
                }
                listahorizontal.Add(auxnodo);
                con++;
            }


            foreach (nodohorizontal simbolo in listahorizontal)
            {
                
                texto +="\t"+ simbolo.nombre + "[label=\"" + simbolo.lexema+ "\"];\n";
            }
            texto += "\troot[label=\"root\"];\n";
            foreach (nodovertical item in listavertical)
            {
                
                 texto += "\t" + item.nombre + "[label=\"" + item.estado + "\"];\n";
                
            }
            
            texto += "\troot -> ";
            for(int i=0;i<listahorizontal.Count-1;i++)
            {
                texto +=listahorizontal[i].nombre + " -> ";
            }
            texto += listahorizontal[listahorizontal.Count-1].nombre+"\n";

            
            texto += "\troot -> ";
            for (int i=0;i<listavertical.Count-1;i++)
            {  
                texto += listavertical[i].nombre+ " -> ";
            }
            texto += listavertical[listavertical.Count-1].nombre+"\n";
            
            foreach (nodovertical item in listavertical)
            {
                foreach (TableTrans estado in item.estados)
                {
                    texto+= "\t" +estado.nombre + "[label=\"" + estado.estado2 + "\"];\n";
                }
            }

            foreach (nodovertical item in listavertical)
            {
                texto += "\t" + item.nombre + " -> ";
                for (int i = 0; i < item.estados.Count-1; i++)
                {
                    texto += item.estados[i].nombre + " -> ";
                }
                texto +="\t" + item.estados[item.estados.Count - 1].nombre + "[constraint=false];\n";
            }

            foreach (nodohorizontal item in listahorizontal)
            {
                if (item.estados.Count > 0)
                {
                    texto += "\t" + item.nombre + " -> ";
                    for (int i = 0; i < item.estados.Count - 1; i++)
                    {
                        texto += item.estados[i].nombre + " -> ";
                    }
                    texto += "\t" + item.estados[item.estados.Count - 1].nombre + ";\n";
                }
            }
            
            texto += "\t{ rank=same;root";
            foreach (nodohorizontal item in listahorizontal)
            {
                texto += ";" + item.nombre;
            }
            texto += "}\n";
            
            foreach (nodovertical item in listavertical)
            {
                
                texto += "\t{ rank=same;"+item.nombre;
                foreach (TableTrans estado2 in item.estados)
                {
                    texto +=";" + estado2.nombre;    
                }
                texto += "}\n";
            }
            
            texto += "}";
            System.IO.File.WriteAllText(@"C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\"+er+"_tabla.dot", texto);
            System.Diagnostics.Process.Start("CMD.exe", "/C dot C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_tabla.dot -Tpng -o C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + er + "_tabla.png");
        }

        public string validarLexema(string lexema,Token listatok)
        {
            Estado inicial = this.AFD.getEstadoInicial();
            ArrayList estados = this.AFD.getEstados();
            ArrayList aceptacion = new ArrayList(this.AFD.getEstadosAceptacion());
            HashSet<Estado> conjunto = cerradura.metodoCerradura(inicial);
            foreach (Char ch in lexema.ToCharArray())
            {
                conjunto = cerradura.mover(conjunto, ch.ToString());
                HashSet<Estado> temp = new HashSet<Estado>();
                IEnumerator<Estado> iter = conjunto.GetEnumerator();
                while (iter.MoveNext())
                {
                    Estado siguiente = iter.Current;
                    temp.UnionWith(cerradura.metodoCerradura(siguiente));
                }
                conjunto = temp;
            }
            bool aceptado = false;
            foreach (Estado estadoAceptacion in aceptacion)
            {
                if (conjunto.Contains(estadoAceptacion))
                {
                    aceptado = true;
                }
            }
            if (aceptado)
            {
                mLFile.makeXMLFile(listatok, lexema);
                return "Lexema: "+lexema + " ACEPTADO";
            }
            else
            {
                mLFile.XMLError(listatok, lexema);
                return "Lexema: " + lexema + "  NO ACEPTADO";
            }
        }
    }

    class nodovertical
    {
        public string estado, nombre;
        public List<TableTrans> estados;
        public nodovertical(string sim,string nombre)
        {
            this.estado = sim;
            this.nombre = nombre;
            this.estados = new List<TableTrans>();
        }
    }

    class nodohorizontal
    {
        public string lexema, nombre;
        public List<TableTrans> estados;
        public nodohorizontal(string lexema,string nombre)
        {
            this.lexema = lexema;
            this.nombre = nombre;
            this.estados = new List<TableTrans>();
        }

    }

    class TableTrans
    {
        public string nombre, estado2,trans;
        public List<string> x;

        public TableTrans(string estado2,string nombre,string tran)
        {
            this.estado2 = estado2;
            this.nombre = nombre;
            this.trans = tran;
            this.x = new List<string>();
        }
    }
    
}

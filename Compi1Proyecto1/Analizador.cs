using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Analizador
    {
        int i,j,estado;
        string palabra, linea;
        char auxpalabra;
        ArrayList tokens=new ArrayList();
        ArrayList errores = new ArrayList();
        public Analizador() { }

        private void analizar(string[] cadena)
        {
            estado = 0;
            this.palabra = "";
            for( i=0; i < cadena.Length; i++)
            {
                linea = cadena[i];
                for( j=0; j< linea.Length; j++)
                {
                    auxpalabra = linea[j];
                    switch (estado)
                    {
                        case 0:
                            if (auxpalabra.Equals(':'))
                                tokens.Add(new Token(Char.ToString(auxpalabra), "signo", i, j, false));
                            else if (auxpalabra.Equals(';'))
                            {
                                tokens.Add(new Token(Char.ToString(auxpalabra), "signo", i, j, false));
                                tokens.Add(new Token("\n", "saltoLinea", i, j, false));
                            }
                            else if (auxpalabra == '~')
                                tokens.Add(new Token("~", "signo", i, j, false));
                            else if (auxpalabra == '.')
                                tokens.Add(new Token(".", "singno", i, j, true));
                            else if (auxpalabra == '*')
                                tokens.Add(new Token("*", "signo", i, j, true));
                            else if (auxpalabra == '|')
                                tokens.Add(new Token("|", "signo", i, j, true));
                            else if (auxpalabra == '+')
                                tokens.Add(new Token("+", "signo", i, j, true));
                            else if (auxpalabra == ',')
                                tokens.Add(new Token(",", "signo", i, j, false));
                            else if (auxpalabra == '?')
                                tokens.Add(new Token("?", "signo", i, j, true));
                            else if (Char.IsWhiteSpace(auxpalabra))
                                ;
                            else if (auxpalabra.Equals('/'))
                                estado = 1;
                            else if (auxpalabra == '-')
                                estado = 3;
                            else if (auxpalabra == '<')
                                estado = 4;
                            else if (auxpalabra == '"')
                                estado = 7;
                            else if (auxpalabra == '\\')
                                estado = 12;
                            else if (auxpalabra.Equals('['))
                                estado = 13;
                            else if (Char.IsLetter(auxpalabra))
                            {
                                palabra += auxpalabra;
                                estado = 2;
                            }
                            else if (Char.IsDigit(auxpalabra))
                            {
                                estado = 11;
                                j--;
                            }
                            else
                            {
                                errores.Add(new Error(Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;

                        case 1:
                            if (auxpalabra == '/')
                                estado = 10;
                            else
                            { 
                                errores.Add(new Error(Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;
                        case 2:
                            if (Char.IsLetterOrDigit(auxpalabra))
                            {
                                palabra += auxpalabra;
                            }
                            else if (auxpalabra == '_')
                            {
                                palabra += auxpalabra;
                            }
                            else
                            {
                                if (palabra.ToLower().Equals("conj"))
                                {
                                    tokens.Add(new Token( "CONJ","reservada", i, j, false));
                                    estado = 0;
                                    palabra = "";
                                    j--;
                                }
                                else
                                {
                                    tokens.Add(new Token(palabra,"identificador", i, j, false));
                                    estado = 0;
                                    palabra = "";
                                    j--;
                                }
                            }
                            break;
                        case 3:
                            if (auxpalabra == '>')
                            {
                                tokens.Add(new Token( "->","signo", i, j, false));
                                estado = 9;
                            }
                            else
                            {
                                errores.Add(new Error("expected > not "+Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;
                        case 4:
                            if (auxpalabra == '!')
                            {
                                estado = 5;
                            }
                            else
                            {
                                errores.Add(new Error(Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;
                        case 5:
                            if (auxpalabra == '!')
                                estado = 6;
                            else
                                ;
                            break;
                        case 6:
                            if (auxpalabra == '>')
                                estado = 0;
                            else
                                errores.Add(new Error("expected > not " + Char.ToString(auxpalabra), i, j));
                            estado = 0;
                            break;
                        case 7:
                            if (auxpalabra == '"')
                                estado = 8;
                            else
                                palabra += auxpalabra;
                            break;
                        case 8:
                            tokens.Add(new Token( '"'+palabra+'"',"cadena", i, j, false));
                            palabra = "";
                            estado = 0;
                            j--;
                            break;
                        case 9:
                            tokens.Add(new Token(Char.ToString(auxpalabra), "Econjunto", i, j, false));
                            if (auxpalabra.Equals(';'))
                            {
                                estado = 0;
                                tokens.Add(new Token("\n", "saltoLinea", i, j, false));
                            }
                                
                            break;  
                        case 10:
                            if (j == linea.Length - 1)
                            {
                                estado = 0;
                            }
                            break;
                        case 11:
                            if (Char.IsDigit(auxpalabra))
                                palabra += auxpalabra;
                            else
                            {
                                tokens.Add(new Token( palabra,"numero", i, j, false));
                                estado = 0;
                                j--;
                                palabra = "";
                            }
                            break;
                        case 12:
                            if (auxpalabra.Equals('"'))
                            {
                                tokens.Add(new Token("\\\"", "especial", i, j, false));
                                estado = 0;
                            }else if (auxpalabra.Equals('\''))
                            {
                                tokens.Add(new Token("\\\'", "especial", i, j, false));
                                estado = 0;
                            }
                            else if (auxpalabra.Equals('\n'))
                            {
                                tokens.Add(new Token("\\\n", "especial", i, j, false));
                                estado = 0;
                            }
                            else if (auxpalabra.Equals('\t'))
                            {
                                tokens.Add(new Token("\\\t", "especial", i, j, false));
                                estado = 0;
                            }
                            else
                            {
                                errores.Add(new Error(Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;
                        case 13:
                            if (auxpalabra.Equals(':'))
                            {
                                estado = 14;
                            }
                            else
                            {
                                errores.Add(new Error(Char.ToString(auxpalabra), i, j));
                                estado = 0;
                            }
                            break;
                        case 14:
                            if (auxpalabra.Equals(':'))
                                estado = 15;
                            else
                                palabra += auxpalabra;
                                //tokens.Add(new Token())
                            break;
                        case 15:
                            if (auxpalabra.Equals(']'))
                            {
                                tokens.Add(new Token(palabra, "especial", i, j, false));
                                estado = 0;
                                palabra = "";
                            }
                            break;
                    }
                }
            }
        }

        public void AnalizarEntrada(string[] cadena)
        {
            
            if (errores.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("Errores Lexicos encontrados, Revise archivo PDF");

            }
            else
            {
                foreach(Token tok in tokens)
                {

                }
            }
            
        }


    }
}

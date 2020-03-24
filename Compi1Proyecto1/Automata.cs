using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    public class Automata
    {
        public Estado inicial;
        ArrayList aceptacion,estados;
        HashSet<string> alfabeto;       //puede que se necesite cambiar el tipo de dato al almacenar
        string tipo;
        string lenguajeR;

        public Automata()
        {
            this.estados = new ArrayList();
            this.aceptacion = new ArrayList();
            this.alfabeto = new HashSet<string>();
        }

        public Estado getEstadoInicial()
        {
            return inicial;
        }

        public void setEstadoInicial(Estado inicial)
        {
            this.inicial = inicial;
        }
        public ArrayList getEstadosAceptacion()
        {
            return aceptacion;
        }

        public void addEstadosAceptacion(Estado fin)
        {
            this.aceptacion.Add(fin);
        }

        public ArrayList getEstados()
        {
            return estados;
        }

        public Estado getEstados(int index)
        {
            return (Estado)estados[index];
        }
        public void addEstados(Estado estado)
        {
            this.estados.Add(estado);
        }
        public HashSet<string> getAlfabeto()
        {
            //HashSet<string> aux = this.alfabeto.Reverse<string>();
            return alfabeto;
        }
        public void createAlfabeto(List<Nodo> regex)
        {
            foreach (Nodo ch in regex)
            {

                if (ch.operador == false)
                {
                    this.alfabeto.Add(ch.lexema);
                }
                    
            }
        }
        public void setAlfabeto(HashSet<string> alfabeto)
        {
            this.alfabeto = alfabeto;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }
        public String getTipo()
        {
            return this.tipo;
        }

        public Estado getInicial()
        {
            return inicial;
        }

        public void setInicial(Estado inicial)
        {
            this.inicial = inicial;
        }

        public String getLenguajeR()
        {
            return lenguajeR;
        }

        public void setLenguajeR(String lenguajeR)
        {
            this.lenguajeR = lenguajeR;
        }
    }
}

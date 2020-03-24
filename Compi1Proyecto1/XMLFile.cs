using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Compi1Proyecto1
{
    public class XMLFile
    {
        public XMLFile() { }

        public void makeXMLFile(Token tokens,string lexema)
        {
            XmlTextWriter writer = new XmlTextWriter("C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\"+tokens.lexema+tokens.i+"_Tokens.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("ListaToken");
            createNode(tokens.tipo, lexema, tokens.i.ToString(), tokens.j+1.ToString(), writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

        }

        private void createNode(string nombre,string valor, string fila, string columna, XmlTextWriter writer)
        {
            writer.WriteStartElement("Token");
            writer.WriteStartElement("Nombre");
            writer.WriteString(nombre);
            writer.WriteEndElement();
            writer.WriteStartElement("Valor");
            writer.WriteString(valor);
            writer.WriteEndElement();
            writer.WriteStartElement("Fila");
            writer.WriteString(fila);
            writer.WriteEndElement();
            writer.WriteStartElement("Columna");
            writer.WriteString(columna);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public void XMLError(Token errores,string lexema)
        {
            XmlTextWriter writer = new XmlTextWriter("C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\" + errores.lexema + errores.i + "_Errores.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("ListaErrores");
            createErro(errores.lexema, errores.i.ToString(), errores.j+1.ToString(), writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void createErro(string valor, string fila,string columna,XmlTextWriter writer)
        {
            writer.WriteStartElement("Error");
            writer.WriteStartElement("Valor");
            writer.WriteString(valor);
            writer.WriteEndElement();
            writer.WriteStartElement("Fila");
            writer.WriteString(fila);
            writer.WriteEndElement();
            writer.WriteStartElement("Columna");
            writer.WriteString(columna);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Compi1Proyecto1
{
    public class PDF
    {
        public PDF()
        {

        }

        public void pdfErrors(ArrayList errores)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(@"C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\PDFErrores.pdf", FileMode.Create));
                doc.Open();
                PdfPTable table = new PdfPTable(3);
                table.AddCell("Lexema");
                table.AddCell("Fila");
                table.AddCell("Columna");
                foreach (Token item in errores)
                {
                    table.AddCell(item.lexema);
                    table.AddCell(item.i.ToString());
                    table.AddCell(item.j.ToString());
                }
                doc.Add(table);
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.Message, "Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }

        public void makePDF(ArrayList tokens)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(@"C:\\Users\\Lissette\\source\\repos\\Compi1Proyecto1\\Compi1Proyecto1\\PDFTokens.pdf", FileMode.Create));
                doc.Open();
                PdfPTable table = new PdfPTable(4);
                table.AddCell("Lexema");
                table.AddCell("Tipo");
                table.AddCell("Fila");
                table.AddCell("Columna");
                foreach (Token item in tokens)
                {
                    table.AddCell(item.lexema);
                    table.AddCell(item.tipo);
                    table.AddCell(item.i.ToString());
                    table.AddCell(item.j.ToString());
                }
                doc.Add(table);
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.Message, "Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }
    }
}

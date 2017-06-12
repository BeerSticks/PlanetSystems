using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ReportsGenerators
{
    public class CreateReport
    {
       public static void Main(string[] args)
        {
            CreatePDFReport("FirstTableReport.pdf");
        }

        public static void CreatePDFReport(string reportName)
        {
            FileStream fs = new FileStream(reportName, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            Paragraph paragraph = new Paragraph("This is a sample report");
            doc.Add(paragraph);
            PdfPTable table = new PdfPTable(4);
            table.AddCell(new Phrase("ObjectType"));
            table.AddCell(new Phrase("ObjectName"));
            table.AddCell(new Phrase("StartPosition"));
            table.AddCell(new Phrase("EndPosition"));
            table.HeaderRows = 1;
            table.AddCell(new Phrase("test"));
            table.AddCell(new Phrase("test"));
            table.AddCell(new Phrase("test"));
            table.AddCell(new Phrase("test"));
            doc.Add(table);
            doc.Add(image);
            doc.Close();
        }
    }
}

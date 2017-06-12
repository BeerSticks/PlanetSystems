using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

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
            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
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
            Image image = Image.GetInstance("planet.png");
            doc.Add(image);
            doc.Close();
        }

        public static void CreatePdfReportFromDataGridView(DataGridView dgv)
        {
            PdfPTable table = new PdfPTable(dgv.Columns.Count);

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                table.AddCell(new Phrase(dgv.Columns[i].HeaderText));
            }

            table.HeaderRows = 1;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {

                }
            }
        }
    }
}

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Windows.Forms;

namespace ReportsGenerators
{
    public class CreateReport
    {
        public static void Main(string[] args)
        {
            //CreatePDFReport("FirstTableReport.pdf");
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

<<<<<<< HEAD
            Image image = Image.GetInstance("images\\planet.png");
=======
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("C:\\Users\\Darin-PC\\Desktop\\PlanetSystems\\PlanetSystems\\ReportsGenerators\\images\\planet.png");
>>>>>>> 50f46779293fed951d453a68e00d9ca653388d40

            doc.Add(image);
            doc.Close();
        }

        public static void CreatePdfReportFromDataTable(DataTable dt)
        {
            try
            {
                Document doc = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("Report.pdf", FileMode.Create));

                doc.Open();

                Image img = Image.GetInstance("..\\..\\..\\ReportsGenerators\\Images\\planet.png");
                img.Border = Rectangle.BOX;
                img.BorderColor = BaseColor.BLACK;
                img.BorderWidth = 1.0f;
                img.Alignment = Element.ALIGN_MIDDLE;

                doc.Add(img);
                doc.Add(new Chunk("\n"));

                Paragraph para = new Paragraph("Objects loaded from XML/JSON/XLSX File:");

                doc.Add(para);
                doc.Add(new Chunk("\n"));

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                // Add the headers from the table.
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    table.AddCell(new Phrase(dt.Columns[i].ColumnName));
                }

                // Mark the first row as a header.
                table.HeaderRows = 1;

                // Add the actual rows from the Data table.
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            table.AddCell(new Phrase(dt.Rows[i][j].ToString()));
                        }
                    }
                }

                doc.Add(table);

                doc.Close();

                writer.Close();

                MessageBox.Show(string.Format("PDF report successfully created under the folder: '{0}'", Directory.GetCurrentDirectory()));
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot write to file, because the file is currenly open.");
            }
        }
    }
}
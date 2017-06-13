using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Collections.Generic;
using PlanetSystem.Models.Utilities;
using PlanetSystem.Models.Bodies;
using System.Text;
using System;

namespace ReportsGenerators
{
    public static class GeneralUIReportGenerator
    {

        public static void GenerateReport(
            string planetarySystemName,
            List<AstronomicalBody> bodiesPreMove,
            List<AstronomicalBody> bodiesPostMove,
            Bitmap bm)
        {
            FileStream fs = new FileStream("report.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            Paragraph title = new Paragraph(new Phrase($"Report on {planetarySystemName}"));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);

            Paragraph date = new Paragraph(new Phrase($"Generated on {DateTime.Now}"));
            date.Alignment = Element.ALIGN_CENTER;
            doc.Add(date);

            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(bm, System.Drawing.Imaging.ImageFormat.Bmp);
            pdfImage.Alignment = Element.ALIGN_CENTER;
            doc.Add(pdfImage);

            PdfPTable table = new PdfPTable(4);
            table.AddCell(new Phrase("Type"));
            table.AddCell(new Phrase("Name"));
            table.AddCell(new Phrase("Starting Position"));
            table.AddCell(new Phrase("Final Position"));
            table.HeaderRows = 1;

            for (int i = 0; i < bodiesPreMove.Count; i++)
            {
                var pre = bodiesPreMove[i];
                var post = bodiesPostMove[i];
                if (bodiesPostMove[i] is Star)
                {
                    table.AddCell(new Phrase("Star"));
                }
                else if (bodiesPostMove[i] is Planet)
                {
                    table.AddCell(new Phrase("Planet"));
                }
                else if (bodiesPostMove[i] is Moon)
                {
                    table.AddCell(new Phrase("Moon"));
                }
                else if (bodiesPostMove[i] is Asteroid)
                {
                    table.AddCell(new Phrase("Asteroid"));
                }
                else if (bodiesPostMove[i] is ArtificialObject)
                {
                    table.AddCell(new Phrase("Artificial\nobject"));
                }

                table.AddCell(new Phrase(bodiesPreMove[i].Name));

                StringBuilder sb = new StringBuilder();
                sb.Append($"X: {pre.Center.X:E}\n");
                sb.Append($"Y: {pre.Center.Y:E}\n");
                sb.Append($"Z: {pre.Center.Z:E}");
                table.AddCell(new Phrase(sb.ToString()));

                sb.Clear();
                sb.Append($"X: {post.Center.X:E}\n");
                sb.Append($"Y: {post.Center.Y:E}\n");
                sb.Append($"Z: {post.Center.Z:E}");
                table.AddCell(new Phrase(sb.ToString()));
            }

            doc.Add(table);
            doc.Close();
        }
    }
}

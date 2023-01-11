using System;
using Bytescout.PDF;

namespace CustomHTMLFormatter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var document = new Document();
            var page = new Page(PaperFormat.A4);
            document.Pages.Add(page);
            var canvas = page.Canvas;
            var brush = new SolidBrush();

            // Use standard PDF font
            Font standardFont = new Font(StandardFonts.Times, 16);
            canvas.DrawString("Standard font.", standardFont, brush, 20, 80);

            // Use font installed in system
            Font systemFont = new Font("Arial", 11);

            canvas.DrawString("System font.", systemFont, brush, 20, 50);

            HTMLParser.ParseHTML(document, 0, systemFont, brush);

            document.Save("result.pdf");

            Console.WriteLine("Hello World!");
        }
    }
}

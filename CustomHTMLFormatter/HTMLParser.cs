using Bytescout.PDF;
using HtmlAgilityPack;
using System;
using System.Xml;

namespace CustomHTMLFormatter
{
    public static class HTMLParser
    {
        public static void ParseHTML(Document document, int pageNumber, Font font, Brush brush)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml("<p>Hello World</p>");
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//p"))
            {
                var text = node.InnerText;
                document.Pages[pageNumber].Canvas.DrawString(text, font, brush, 50, 120);
            }

            //example for bold
            string htmlContent = "<p>This is a <b>bold</b> text.</p>";
            doc.LoadHtml(htmlContent);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//b"))
            {
                font = new Font("Arial", 11, bold: false, italic: true, underline: false, false);
                var text = node.InnerText;
                document.Pages[pageNumber].Canvas.DrawString(text, font, brush, 50, 150);
            }

            //example for italic
            htmlContent = "<p>This is a <i>italic</i> text.</p>";
            doc.LoadHtml(htmlContent);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//i"))
            {
                font = new Font("Arial", 11, bold: false, italic: true, underline: false, false);
                var text = node.InnerText;
                document.Pages[pageNumber].Canvas.DrawString(text, font, brush, 50, 160);
            }

            //example for underline
            htmlContent = "<p>This is a <u>underline</u> text.</p>";
            doc.LoadHtml(htmlContent);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//u"))
            {
                font = new Font("Arial", 11, bold: false, italic: false, underline: true, false);
                var text = node.InnerText;
                document.Pages[pageNumber].Canvas.DrawString(text, font, brush, 50, 170);
            }

            htmlContent = "<a href='https://www.google.com'>This is a link text.</a>";
            doc.LoadHtml(htmlContent);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
            {
                var linkText = node.InnerText;
                var linkUrl = node.GetAttributeValue("href", "");
                var linkTextWithUrl = linkText + " " + linkUrl;

                URIAction action = new URIAction(new Uri(linkUrl));
                var linkAnnotation = new LinkAnnotation(action, 60, 190, 100, 20);
                linkAnnotation.HighlightingMode = LinkAnnotationHighlightingMode.Outline;
                linkAnnotation.Color = new ColorRGB(0, 0, 255);

                // add the link annotation to the page
                document.Pages[0].Annotations.Add(linkAnnotation);

                Pen bluePen = new SolidPen(new ColorRGB(0, 0, 255));
                document.Pages[0].Canvas.DrawString(linkText, font,brush, bluePen, 50, 190);
            }
        }
    }
}

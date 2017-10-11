using System;
using System.Net;
using HtmlAgilityPack;

namespace WikiGet
{
    class Program
    {
        static void Main(string[] args)
        {
            string articleHtml;
            using (WebClient client = new WebClient ())
            {
                articleHtml = client.DownloadString("https://pl.wikipedia.org/wiki/Linux");
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(articleHtml);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']//*[self::h2 or self::p]");

            string extractedText = "";
            foreach (var node in nodes)
            {
                // this could probably be written more elegantly
                extractedText += node.InnerText + "\n\n";
            }

            Console.WriteLine(extractedText);
        }
    }
}

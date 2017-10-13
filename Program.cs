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
            using (WebClient client = new WebClient())
            {
                articleHtml = client.DownloadString("https://pl.wikipedia.org/wiki/Linux");
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(articleHtml);

            var referenceNodes = doc.DocumentNode.SelectNodes("//sup[@class='reference']");
            foreach (var node in referenceNodes)
            {
                node.Remove();
            }

            var editNodes = doc.DocumentNode.SelectNodes("//span[@class='mw-editsection']");
            foreach (var node in editNodes)
            {
                node.Remove();
            }

            var contentNodes = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']//*[self::h2 or self::p]");
            string extractedText = "";
            for(int i = 0; i < contentNodes.Count - 1; ++i)
            {
                if (contentNodes[i].Name == "h2")
                {
                    if (i+1 >= contentNodes.Count || contentNodes[i+1].Name == "h2" || contentNodes[i+1].InnerText.Trim() == "")
                    {
                        continue;
                    }
                }
                if (contentNodes[i].InnerText.Trim() == "") {
                    continue;
                }
                extractedText += contentNodes[i].InnerText + "\n\n";
            }

            Console.WriteLine(extractedText);
        }
    }
}

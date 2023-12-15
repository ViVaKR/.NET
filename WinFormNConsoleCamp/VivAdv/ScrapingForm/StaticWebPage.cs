using HtmlAgilityPack;
using System.Diagnostics;
using System.Text;

namespace ScrapingForm
{
    public class StaticWebPage
    {
        private readonly string url = @"https://kabc.dongguk.edu/content/view?itemId=ABC_IT&cate=bookName&depth=3&upPath=Z&dataId=ABC_IT_K1027_T_001&pk=K";
        private readonly string xPath = "//*[@id=\"content\"]/div[3]/div[2]/div[1]/div/dl[position()>0]";
        
        public string Get()
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var list = doc.DocumentNode.SelectNodes(xPath);
            
            int count = 0;
            var sb = new StringBuilder();
            foreach (var node in list)
            {
                count++;
                sb.AppendLine(node.InnerText);
            }
            Debug.WriteLine(count);
            return sb.ToString();
            
        }
    }
}

/*
open HtmlAgilityPack
let ExtractContext htmlDoc =
let hap = new HtmlAgilityPack.HtmlDocument()
hap.LoadHtml(htmlDoc)
let nodes = hap.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/table[1]/tbody/tr")
for node in nodes do


 */


// class="xsl_body",
// HTML parser //
// From File
// From string
// From Web
// From Browser

// Selectors //
// SelectNodes()
// SelectSingleNode(string)

// HTML Manipulation //
// InnerHtml
// InnerText
// OuterHtml
// ParentNode

// HTML Traversion //
// ChildNodes
// FirstChild
// LastChild
// NextSibling
// ParentNode

// HTML Writer //
// Save(Stream)
// Save(StreamWriter)
// Save(TextWriter)
// Save(String)
// Save(XmlWriter)
// Save(Stram, Encoding)
// Save(string, Encoding)

// HtmlNode - Methods //
// WriterContentTo()
// WriteContentTo(TextWriter)
// WriteTo()
// WriteTo(TextWriter)
// WriteTo(XmlWriter)


// xpath syntax //
// `nodename`   : Selects all nodes with the name "namename"
// `/`          : Select from the root node
// `//`         : Selects nodes in the document from from the current node that match the selection no matter where thery are
// `@`          : Select attributes
// `..`         : Selects the parent of the current node
// `.`          : Selects the current node


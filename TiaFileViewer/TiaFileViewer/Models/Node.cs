using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TiaFileViewer.Models
{
    class Node
    {
        public string NodeType { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }

        public static IEnumerable<Node> FromTiaFile(string filePath)
        {
            var doc = XDocument.Load(filePath);
            var nodes = doc.Descendants("node").Where(x => x.Attribute("Type") != null); 
            var edges = doc.Descendants("edge").Where(x => x.Attribute("Type") != null);
            var both = nodes.Concat(edges);
            
            return nodes.Concat(edges).Select(x => new Node
            {
                NodeType = x.Attribute("Type").Value,
                Properties = x.Element("properties").Elements("property")
                    .Select(p => new KeyValuePair<string, string>(
                        p.Element("key").Value, 
                        p.Element("value").Value))
            });
        }
    }
    
}

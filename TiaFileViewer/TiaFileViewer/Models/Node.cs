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

        public Dictionary<string, string> Properties { get; set; }

        public static IEnumerable<Node> FromTiaFile(string filePath)
        {
            var doc = XDocument.Load(filePath);
            var nodes = doc.Descendants("node").Where(x => x.Attribute("Type") != null); 
            return nodes.Select(x => new Node
            {
                NodeType = x.Attribute("Type").Value,
                Properties = x.Element("properties").Elements("property")
                    .Select(p => new KeyValuePair<string, string>(
                        p.Element("key").Value, 
                        p.Element("value").Value))
                        .ToDictionary(k => k.Key, v => v.Value)
            });
        }

        public static Dictionary<string, int> SummarizeProperties(IEnumerable<Node> nodes)
        {
            var dict = new Dictionary<string, int>();
            foreach(var node in nodes)
            {
                var label = node.Properties.ContainsKey("Name") ? node.Properties["Name"]
                    : node.Properties.ContainsKey("Id") ? node.Properties["Id"] : null;
                if (dict.ContainsKey(label))
                    dict[label]++;
                else
                    dict.Add(label, 1);
            }
            return dict;
        }
    }
    
}

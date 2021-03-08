using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Windows;

namespace TiaFileViewer.Models
{
    class Node
    {
        public string NodeType { get; set; }

        public Dictionary<string, string> Properties { get; set; }


        public static IEnumerable<Node> FromTiaFile(string filePath)
        {
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(filePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Die Datei ist beschädigt oder nicht lesbar.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var nodesWithType = doc.Descendants("node").Where(x => x.Attribute("Type") != null); 
            return nodesWithType.Select(x => new Node
            {
                NodeType = x.Attribute("Type").Value,
                Properties = x.Element("properties").Elements("property")
                    .Select(p => new KeyValuePair<string, string>(
                        p.Element("key").Value, 
                        p.Element("value").Value))
                        .ToDictionary(k => k.Key, v => v.Value)
            });
        }

        public static Dictionary<string, int> SummarizeProperties(IEnumerable<Node> nodes, bool allPropertiesAlgorithm)
        {
            // Hier war ich mir nicht sicher, wie die Properties zusammengezählt werden sollen - siehe meine Mail von Montag morgen.
            // Ich habe die beiden möglichen Algorithmen eingebaut, die mir sinnvoll erschienen.
            if (allPropertiesAlgorithm)
            {
                var namedProperties = new Dictionary<string, int>();
                foreach (var node in nodes)
                {
                    var nameOrId = node.Properties.ContainsKey("Name") ? node.Properties["Name"]
                        : node.Properties.ContainsKey("Id") ? node.Properties["Id"] : null;
                    if (namedProperties.ContainsKey(nameOrId))
                        namedProperties[nameOrId] += node.Properties.Count;
                    else
                        namedProperties.Add(nameOrId, node.Properties.Count);
                }
                return namedProperties;
            }
            else
            {
                var namedProperties = new Dictionary<string, int>();
                foreach (var node in nodes)
                {
                    var nameOrId = node.Properties.ContainsKey("Name") ? node.Properties["Name"]
                        : node.Properties.ContainsKey("Id") ? node.Properties["Id"] : null;
                    if (namedProperties.ContainsKey(nameOrId))
                        namedProperties[nameOrId]++;
                    else
                        namedProperties.Add(nameOrId, 1);
                }
                return namedProperties;
            }
        }
    }
    
}

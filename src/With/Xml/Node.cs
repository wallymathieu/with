using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using With.Rubyfy;

namespace With.Xml
{
    public class Node
    {
        private readonly XmlNode _node;
        public bool IsText
        {
            get { return _node.NodeType == XmlNodeType.Text; }
        }

        public Node(XmlNode node)
        {
            _node = node;
        }

        public string this[string id]
        {
            get
            {
                var xmlAttribute = _node.Attributes[id];
                if (xmlAttribute == null)
                {
                    return null;
                }

                return xmlAttribute.Value;
            }
        }

        public string Text
        {
            get
            {
                return String.Join("", _node.ChildNodes.Cast<XmlNode>()
                    .Select(node => node.InnerText));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector">xpath or css</param>
        /// <returns></returns>
        public Node[] Search(string selector)
        {
            if (Regex.IsMatch(selector, @"[\/\.\\]"))
            {
                return _node.SelectNodes(selector).Cast<XmlNode>().Select(node => new Node(node)).ToArray(); 
            }
            else
            {
                var split = selector.Split(new[] {' '});
                var first = split.First();
                var rest = string.Join(" ",split.Skip(1).ToArray());
                return
                    GetNodesWithName(_node, first).Select(node => NodeOrNodes(node, rest)).Flatten<Node>().ToArray();
            }
        }

        private static object NodeOrNodes(XmlNode node, string rest)
        {
            var node1 = new Node(node);
            return string.IsNullOrWhiteSpace(rest)
                ? node1
                    : (object) node1.Search(rest);
        }

        private static XmlNode[] GetNodesWithName(XmlNode xmlNode, string selector)
        {
            if (xmlNode.Name == selector) return new[] {xmlNode};
            var list = new List<XmlNode>(xmlNode.ChildNodes.Cast<XmlNode>()
                .Select(node => GetNodesWithName(node, selector))
                .Flatten<XmlNode>());
            return list.ToArray();
        }
    }
}
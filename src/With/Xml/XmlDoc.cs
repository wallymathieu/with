using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using With.Rubyfy;

namespace With.Xml
{
    public class XmlDoc : Node
    {
        public static XmlDoc Xml(string text)
        {
            return new XmlDoc(new XmlDocument().Tap(xdoc => xdoc.LoadXml(text)));
        }

        public XmlDoc(XmlDocument parse)
            : base(parse)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LivelyResxUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            //UpdateResX();
            Legacyv1Tov2();
            Console.Read();
        }

        public static void RenameXMLNode(XmlDocument doc, XmlNode oldRoot, string newname)
        {
            XmlNode newRoot = doc.CreateElement("data");
            newRoot.Attributes.Append(doc.CreateAttribute("name"));
            newRoot.Attributes.Append(doc.CreateAttribute("xml:space"));
            newRoot.Attributes["name"].Value = newname;
            newRoot.Attributes["xml:space"].Value = "preserve";
            foreach (XmlNode childNode in oldRoot.ChildNodes)
            {
                newRoot.AppendChild(childNode.CloneNode(true));
            }
            XmlNode parent = oldRoot.ParentNode;
            parent.InsertBefore(newRoot, oldRoot);
            parent.RemoveChild(oldRoot);
        }

        public static void Legacyv1Tov2()
        {
            var file = new XmlDocument();
            file.Load("xml\\Resources.ru.resw");
            var nodes = file.DocumentElement.ChildNodes;
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var oName = nodes[i].Attributes["name"]?.InnerText;
                foreach (string line in File.ReadLines("xml\\map.txt"))
                {
                    var item = line.Split(" ");
                    if (oName == item[0])
                    {
                        Console.WriteLine("Changing:" + oName + " -> " + item[1]);
                        RenameXMLNode(file, nodes[i], item[1]);
                    }
                }
            }

            foreach (XmlNode oNode in file.DocumentElement.ChildNodes)
            {
                Console.WriteLine(oNode.Attributes["name"]?.InnerText);
            }
            file.Save("xml\\final.resw");
        }

        /// <summary>
        /// Fill the base xml with translated text, leaving the missing translation with base one.
        /// All xml files need the metadata and comments removed (except root.)
        /// newf file: Latest translation file (base)
        /// old file: translated file with missing values.
        /// final.resx: final output, convertion to Unescaped is needed ie replace &lt with < etc.
        /// </summary>
        public static void UpdateResX()
        {
            var old = new XmlDocument();
            old.Load("xml\\Resources.sv-SE.resx");

            var newf = new XmlDocument();
            newf.Load("xml\\Resources.resx");

            foreach (XmlNode oNode in old.DocumentElement.ChildNodes)
            {
                var oName = oNode.Attributes["name"]?.InnerText;
                foreach (XmlNode nNode in newf.DocumentElement.ChildNodes)
                {
                    var nName = nNode.Attributes["name"]?.InnerText;
                    if (nName == oName)
                    {
                        nNode.InnerText = oNode.InnerXml;
                        break;
                    }
                }
            }
            newf.Save("xml\\final.resx");
        }

        /// <summary>
        /// Convert Lively v0.9 or below language resx to v1.0
        /// All xml files need the metadata and comments removed (except root.)
        /// map file: "name" is the v1.0 keyname, and value is the v0.9 keyname.
        /// newf file: New v1.0 resource file.
        /// old file: v0.9 or below resource file.
        /// final.resx: final output, convertion to Unescaped is needed ie replace &lt with < etc.
        /// </summary>
        public static void LegacyToNewResx()
        {
            var map = new XmlDocument();
            map.Load("xml\\mapping.xml");

            var old = new XmlDocument();
            old.Load("xml\\Resources.uk.resx");

            var newf = new XmlDocument();
            newf.Load("xml\\Resources.resx");

            foreach (XmlNode mNode in map.DocumentElement.ChildNodes)
            {
                var mValue = mNode.InnerText;
                var mName = mNode.Attributes["name"]?.InnerText;
                foreach (XmlNode oNode in old.DocumentElement.ChildNodes)
                {
                    var oName = oNode.Attributes["name"]?.InnerText;
                    if (oName == mValue)
                    {
                        foreach (XmlNode nNode in newf.DocumentElement.ChildNodes)
                        {
                            var nName = nNode.Attributes["name"]?.InnerText;
                            if (nName == mName)
                            {
                                nNode.InnerText = oNode.InnerXml;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            newf.Save("xml\\final.resx");
        }
    }
}

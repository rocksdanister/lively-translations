using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// Program to convert Lively v0.9 or below language resx to v1.0
/// All xml files need the metadata and comments removed (except root.)
/// map file: "name" is the v1.0 keyname, and value is the v0.9 keyname.
/// newf file: New v1.0 resource file.
/// old file: v0.9 or below resource file.
/// final.resx: final output, convertion to Unescaped is needed ie replace &lt with < etc.
/// </summary>
namespace LivelyResxUpdater
{
    class Program
    {
        static void Main(string[] args)
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
                    if(oName == mValue)
                    {
                        foreach (XmlNode nNode in newf.DocumentElement.ChildNodes)
                        {
                            var nName = nNode.Attributes["name"]?.InnerText;
                            if(nName == mName)
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

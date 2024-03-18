using System.Xml;

namespace PListGen;

public class Plist
{
    public static void CreatePlistFile(string filename)
    {
        XmlWriterSettings settings = new() { Indent = true };

        XmlWriter writer = XmlWriter.Create(filename, settings);

        // Write the Processing Instruction node. (명령 처리 노드)
        writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
        // Write the DocumentType node.
        writer.WriteDocType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);

        // (plist) Start
        writer.WriteStartElement("plist");
        writer.WriteAttributeString("version", "1.0");

        // (dict) Start
        writer.WriteStartElement("dict");

        // Label
        writer.WriteElementString("key", "Label");

        // String
        writer.WriteElementString("string", "kr.co.kimbumjun.api");

        // ProgramArguments
        writer.WriteElementString("key", "ProgramArguments");

        // Array
        writer.WriteStartElement("array");
        writer.WriteElementString("string", "/opt/homebrew/bin/dotnet");
        writer.WriteElementString("string", "ApiKimBumJun.dll");
        writer.WriteEndElement();

        writer.WriteElementString("key", "WorkingDirectory");
        writer.WriteElementString("string", "/User/vivakr/WebProject/kr.co.kimbumjun/api");

        //
        writer.WriteElementString("key", "GroupName");
        writer.WriteElementString("string", "staff");

        writer.WriteElementString("key", "InitGroups");
        writer.WriteRaw("<true />");

        writer.WriteElementString("key", "UserName");
        writer.WriteElementString("string", "vivakr");

        writer.WriteElementString("key", "RunAtLoad");
        writer.WriteRaw("<true/>");

        writer.WriteElementString("key", "KeepAlive");
        writer.WriteRaw("<true />");

        // Write the close tag for the root element.
        writer.WriteEndElement();

        // (plist) End
        writer.WriteEndDocument();

        // Write the XML to file and close the writer.
        writer.Flush();
        writer.Close();

        Console.WriteLine("End..");
    }
}

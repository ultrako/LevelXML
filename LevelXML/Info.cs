using System.Xml.Linq;

namespace HappyWheels;

public class Info : LevelXMLTag
{
	public const string EditorDefault = @"<info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>";
	public Info(string xml=EditorDefault) : this (StrToXElement(xml)) {}
	internal Info(XElement e) : base("info")
	{
		elt = new XElement(e.Name.ToString());
	}
}

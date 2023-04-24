using System.Xml.Linq;
namespace HappyWheels;
// This class represents any xml tag <> that can go into the import box in Happy Wheels.
public abstract class LevelXMLTag
{
	// This isn't the class name, this is the name in the tag,
	// like <sh t="0"> would have a name of "sh"
	public abstract string Name {get;}
	public static XElement StrToXElement(string xml) {
		return XElement.Parse(xml);
	}
	private static string? getStringOrNull(XElement elt, string attr)
	{
		if (elt.Attribute(attr) is XAttribute val) { return val.Value; }
		else { return null; }
	}
	public static float? getFloatOrNull(XElement elt, string attr) 
	{
		if (getStringOrNull(elt, attr) is string val)
		{
			float result;
			if (float.TryParse(val, out result)) {
				return result;
			}
		}
		return null;
	}
	public static bool? getBoolOrNull(XElement elt, string attr)
	{
		bool? result = null;
		if (getStringOrNull(elt, attr) is string val)
		{
			if (val == "t") { result = true; }
			else if (val == "f") { result = false; }
		}
		return result;
	}
	public static string FormatBool(bool? b)
	{
		if ((bool)b!) { return "t"; } else { return "f"; }
	}
	public XElement ToXElement(uint? index=null) {
		return new XElement(Name, getContents());
	}
	// Some classes will need an index to set all of their attributes
	protected abstract List<XAttribute> getAttributes(int? index=null);
	protected abstract List<XElement> getChildren();
	protected List<object> getContents() {
		IEnumerable<object> contents = new List<object> ();
		contents = contents.Append(getAttributes());
		contents = contents.Append(getChildren());
		return contents.ToList();
	}
}

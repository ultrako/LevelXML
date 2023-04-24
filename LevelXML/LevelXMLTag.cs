using System.Xml.Linq;
namespace HappyWheels;
// This class represents any xml tag <> that can go into the import box in Happy Wheels.
public abstract class LevelXMLTag : XElement
{
	public static XElement StrToXElement(string xml) {
		return XElement.Parse(xml);
	}
	private static string? getStringOrNull(XElement elt, string attr)
	{
		if (elt.Attribute(attr) is XAttribute val) { return val.Value; }
		else { return null; }
	}
	public float? GetFloatOrNull(string attr) { return GetFloatOrNull(this, attr); }
	public static float? GetFloatOrNull(XElement elt, string attr) 
	{
		if (getStringOrNull(elt, attr) is string val)
		{
			float result;
			if (float.TryParse(val, out result)) {
				return result;
			} else { return float.NaN; }
		}
		return null;
	}
	// In happy wheels, bools can be either true, false, or NaN
	// :(
	// So here's an enum
	public enum HWBool 
	{
		False,
		True,
		NaN
	}
	public HWBool? GetBoolOrNull(string attr) { return GetBoolOrNull(this, attr); }
	public static HWBool? GetBoolOrNull(XElement elt, string attr)
	{
		HWBool? result = null;
		if (getStringOrNull(elt, attr) is string val)
		{
			if (val == "t") { result = HWBool.True; }
			else if (val == "f") { result = HWBool.False; }
			else { result = HWBool.NaN; }
		}
		return result;
	}
	public static string FormatBool(HWBool? b)
	{
		// Bool values in happy wheels can hold 3 things
		if (b is HWBool.True) { return "t"; } 
		else if (b is HWBool.False) { return "f"; }
		else { return "NaN"; }
	}
	protected LevelXMLTag(XName name, params object?[] content) : base(name, content) {}
}

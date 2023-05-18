using System.Xml.Linq;
namespace HappyWheels;
// This class represents any xml tag <> that can go into the import box in Happy Wheels.
public abstract class LevelXMLTag
{
	internal XElement elt {get; set;}
	protected static XElement StrToXElement(string xml) {
		return XElement.Parse(xml);
	}
	private string? getStringOrNull(string attr) { return getStringOrNull(this.elt, attr); }
	private static string? getStringOrNull(XElement elt, string attr)
	{
		if (elt.Attribute(attr) is XAttribute val) { return val.Value; }
		else { return null; }
	}
	protected static float ParseFloat(string val)
	{
		float result;
		if (float.TryParse(val, out result)) {
			return result;
		} else { return float.NaN; }
	}
	// We're going to represent all the enum-like types in happy wheels with floats,
	// because they can sometimes hold NaN. And sometimes NaN does unique things for
	// level creators, so we won't occlude all of that from the get-go.
	protected float? GetFloatOrNull(string attr) { return GetFloatOrNull(this.elt, attr); }
	protected static float? GetFloatOrNull(XElement elt, string attr) 
	{
		if (getStringOrNull(elt, attr) is string val)
		{
			return ParseFloat(val);
		}
		return null;
	}
	// In happy wheels, bools can be either true, false, or NaN
	// :(
	// So here's an enum
	public struct HWBool 
	{
		readonly object val;
		public static readonly HWBool True = true;
		public static readonly HWBool False = false;
		public static readonly HWBool NaN = float.NaN;
		private HWBool(bool b) { this.val = b; }
		//private HWBool(float f) { this.val = f; }
		public static implicit operator bool(HWBool hwb)
		{
			if (hwb == HWBool.True) { return true; }
			else { return false; }
		}
		public static implicit operator HWBool(bool b)
		{
			return new HWBool(b);
		}
		public static implicit operator HWBool(float f)
		{
			return NaN;
		}
		public static bool operator ==(HWBool lhs, HWBool rhs) 
		{ 
			if (lhs.val is bool lval)
			{
				if (rhs.val is bool rval) { return lval == rval; }
				else { return false; }
			} else if (rhs.val is bool _) { return false; }
			else { return true; }
		}
		public static bool operator !=(HWBool lhs, HWBool rhs)
		{
			return !(lhs == rhs);
		}
		public override int GetHashCode() { return val.GetHashCode(); }
		public override bool Equals(Object? that)
		{
			if (that is HWBool t) 
			{
				return this == t; 
			} else { return false; }
		}
		public override string ToString()
		{
			if (this == true) { return "t"; }
			else if (this == false) { return "f"; }
			else { return "NaN"; }
		}
	}
	protected HWBool? GetBoolOrNull(string attr) { return GetBoolOrNull(this.elt, attr); }
	protected static HWBool? GetBoolOrNull(XElement elt, string attr)
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
	// All levelXML tags have a Name
	public string Name
	{
		get { return elt.Name.ToString(); }
		set { elt.Name = Name; }
	}
	// Certain LevelXML tags don't make any sense outside of the context of a level
	// Like joints, triggers, or blank art shapes
	// They need a function that takes Entities and returns the index of where
	// those entities are in their depth one tag
	internal virtual void PlaceInLevel(Func<Entity, int> mapper) {}
	new public virtual string ToString() { return ToString(mapper: default!); }
	internal virtual string ToString(Func<Entity, int> mapper) {
		PlaceInLevel(mapper);
		return elt.ToString();
	}
	protected LevelXMLTag(XName name, params object?[] content)
	{
		elt = new XElement(name, content);
	}
}

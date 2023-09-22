using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// This class represents any xml tag that can go into the import box in Happy Wheels.
///</summary>
///<remarks>
/// If it makes sense for a class to be made outside of a level,
/// it will have a constructor that takes a string.
/// All public classes that inherit this will have a public empty constructor,
/// and you will end up with an object that would be the same as if you
/// had opened the level editor and placed that object without changing anything
///</remarks>
public abstract class LevelXMLTag
{
	internal XElement Elt {get; set;}
	protected static XElement StrToXElement(string xml) {
		return XElement.Parse(xml);
	}
	public static string? GetStringOrNull(XElement elt, string attr)
	{
		if (elt.Attribute(attr) is XAttribute val) { return val.Value; }
		else { return null; }
	}
	protected static double ParseDouble(string val)
	{
		double result;
		if (double.TryParse(val, out result)) {
			return result;
		} 
		else if (val == "Infinity")
		{
			return double.PositiveInfinity;
		}
		else if (val == "-Infinity")
		{
			return double.NegativeInfinity;
		}
		else 
		{ 
			return double.NaN; 
		}
	}
	// We're going to represent all the enum-like types in happy wheels with doubles,
	// because they can sometimes hold NaN. And sometimes NaN does unique things for
	// level creators, so we won't occlude all of that from the get-go.
	protected double? GetDoubleOrNull(string attr) { return GetDoubleOrNull(this.Elt, attr); }
	internal static double? GetDoubleOrNull(XElement elt, string attr) 
	{
		if (GetStringOrNull(elt, attr) is string val)
		{
			return ParseDouble(val);
		}
		return null;
	}
	protected void SetDouble(string attr, double val)
	{
		string convertedValue;
		if (val == double.PositiveInfinity)
		{
			convertedValue = "Infinity";
		}
		else if (val == double.NegativeInfinity)
		{
			convertedValue = "-Infinity";
		}
		else
		{
			convertedValue = val.ToString();
		}
		this.Elt.SetAttributeValue(attr, convertedValue);
	}
	///<summary>
	/// In happy wheels, bools can be either true, false, or NaN
	/// This has implicit conversions, so whenever a property
	/// has the type HWBool, just use regular bools instead.
	///<summary>
	public struct HWBool 
	{
		readonly object val;
		public static readonly HWBool True = true;
		public static readonly HWBool False = false;
		///<summary>
		/// If you specifically need to set a happy wheels boolean value to NaN,
		/// then use this static constant.
		///</summary>
		public static readonly HWBool NaN = double.NaN;
		private HWBool(bool b) { this.val = b; }
		//private HWBool(double f) { this.val = f; }
		public static implicit operator bool(HWBool hwb)
		{
			if (hwb == HWBool.True) { return true; }
			else { return false; }
		}
		public static implicit operator HWBool(bool b)
		{
			return new HWBool(b);
		}
		public static implicit operator HWBool(double f)
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
			} else if (that is bool b)
			{
				return this.Equals(new HWBool(b));
			}
			else { return false; }
		}
		public override string ToString()
		{
			if (this == true) { return "t"; }
			else if (this == false) { return "f"; }
			else { return "NaN"; }
		}
	}
	protected HWBool? GetBoolOrNull(string attr) { return GetBoolOrNull(this.Elt, attr); }
	protected static HWBool? GetBoolOrNull(XElement elt, string attr)
	{
		HWBool? result = null;
		if (GetStringOrNull(elt, attr) is string val)
		{
			if (val == "t") { result = HWBool.True; }
			else if (val == "f") { result = HWBool.False; }
			else { result = HWBool.NaN; }
		}
		return result;
	}
	// Certain LevelXML tags don't make any sense outside of the context of a level
	// Like joints, triggers, or blank art shapes
	// They need a function that takes Entities and returns the index of where
	// those entities are in their depth one tag
	internal virtual void PlaceInLevel(Func<Entity, int> mapper) {}
	internal virtual string ToXML(Func<Entity, int> mapper) {
		PlaceInLevel(mapper);
		return Elt.ToString();
	}
	protected LevelXMLTag(XName name, params object?[] content)
	{
		Elt = new XElement(name, content);
	}
}

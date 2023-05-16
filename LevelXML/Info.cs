using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// The Info tag has information about the character and the background of the level
/// </summary>
public class Info : LevelXMLTag
{
	public const string EditorDefault = @"<info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>";
	public float? Version
	{
		get { return GetFloatOrNull("v"); }
		set
		{
			elt.SetAttributeValue("v", value!);
		}
	}
	/// <summary>
	/// The x coordinate of the player in the level
	/// </summary>
	public float? x
	{
		get { return GetFloatOrNull("x"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("x", val);
		}
	}
	public float? y
	{
		get { return GetFloatOrNull("y"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("y", val);
		}
	}
	public float? Character
	{
		get { return GetFloatOrNull("c"); }
		set
		{
			float val = value ?? 1;
			elt.SetAttributeValue("c", Math.Clamp(val, 1, 11));
		}
	}
	public HWBool? ForcedCharacter
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val is HWBool.NaN) { val = HWBool.False; }
			elt.SetAttributeValue("f", FormatBool(val));
		}
	}
	public HWBool? VehicleHidden
	{
		get { return GetBoolOrNull("h"); }
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val is HWBool.NaN) { val = HWBool.False; }
			elt.SetAttributeValue("h", FormatBool(val));
		}
	}
	public float? Background
	{
		get { return GetFloatOrNull("bg"); }
		set
		{
			// Invalid values here make a level that's pretty buggy in the editor
			float val = value ?? float.NaN;
			elt.SetAttributeValue("bg", val);
		}
	}
	public float? BackgroundColor
	{
		get { return GetFloatOrNull("bgc"); }
		set
		{
			float val = value ?? 16777215;
			elt.SetAttributeValue("bgc", val);
		}
	}
	public float? E
	{
		get { return GetFloatOrNull("e"); }
		set
		{
			if (value != 1)
			{
				throw new Exception("This would make the level not import!");
			}
			elt.SetAttributeValue("e", value!);
		}
	}
	protected void setParams(XElement e)
	{
		Version = 1.94f;
		x = GetFloatOrNull(e, "x");
		y = GetFloatOrNull(e, "y");
		Character = GetFloatOrNull(e, "c");
		ForcedCharacter = GetBoolOrNull(e, "f");
		VehicleHidden = GetBoolOrNull(e, "h");
		Background = GetFloatOrNull(e, "bg");
		BackgroundColor = GetFloatOrNull(e, "bgc");
		// Unfortunate naming here
		// idk what Jim Bonacci meant by "e"
		this.E = GetFloatOrNull(e, "e");
	}
	public Info(string xml=EditorDefault) : this (StrToXElement(xml)) {}
	internal Info(XElement e) : base("info")
	{
		elt = new XElement(e.Name.ToString());
		setParams(e);
	}
}

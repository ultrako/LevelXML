using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// The Info tag has information about the character and the background of the level
/// </summary>
internal class Info : LevelXMLTag
{
	public const string EditorDefault = @"<info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>";
	internal double? Version
	{
		get { return GetDoubleOrNull("v"); }
		set
		{
			// Hardcoded because it does nothing
			elt.SetAttributeValue("v", "1.94");
		}
	}
	/// <summary>
	/// The x coordinate of the player in the level
	/// </summary>
	public double? X
	{
		get { return GetDoubleOrNull("x"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("x", val);
		}
	}
	public double? Y
	{
		get { return GetDoubleOrNull("y"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("y", val);
		}
	}
	public double? Character
	{
		get { return GetDoubleOrNull("c"); }
		set
		{
			double val = value ?? 1;
			elt.SetAttributeValue("c", Math.Clamp(val, 1, 11));
		}
	}
	public HWBool? ForcedCharacter
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? false;
			if (val == HWBool.NaN) { val = false; }
			elt.SetAttributeValue("f", val);
		}
	}
	public HWBool? VehicleHidden
	{
		get { return GetBoolOrNull("h"); }
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val == HWBool.NaN) { val = false; }
			elt.SetAttributeValue("h", val);
		}
	}
	public double? Background
	{
		get { return GetDoubleOrNull("bg"); }
		set
		{
			// Invalid values here make a level that's pretty buggy in the editor
			double val = value ?? double.NaN;
			elt.SetAttributeValue("bg", val);
		}
	}
	public double? BackgroundColor
	{
		get { return GetDoubleOrNull("bgc"); }
		set
		{
			double val = value ?? 16777215;
			elt.SetAttributeValue("bgc", val);
		}
	}
	internal double? E
	{
		get { return GetDoubleOrNull("e"); }
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
		X = GetDoubleOrNull(e, "x");
		Y = GetDoubleOrNull(e, "y");
		Character = GetDoubleOrNull(e, "c");
		ForcedCharacter = GetBoolOrNull(e, "f");
		VehicleHidden = GetBoolOrNull(e, "h");
		Background = GetDoubleOrNull(e, "bg");
		BackgroundColor = GetDoubleOrNull(e, "bgc");
		// Unfortunate naming here
		// idk what Jim Bonacci meant by "e"
		this.E = GetDoubleOrNull(e, "e");
	}
	public Info(string xml=EditorDefault) : this (StrToXElement(xml)) {}
	internal Info(XElement e) : base("info")
	{
		elt = new XElement(e.Name.ToString());
		setParams(e);
	}
}

using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// The Info tag has information about the character and the background of the level
/// </summary>
internal class Info : LevelXMLTag, IConvertableToXML
{
	internal const string HappyWheelsVersion = "1.95";
	public const string EditorDefault = @"<info v=""" + HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>";

	internal string? Version
	{
		get { return GetStringOrNull(Elt, "v"); }
		set
		{
			Elt.SetAttributeValue("v", value ?? HappyWheelsVersion);
		}
	}

	public string ToXML() { return ToXML(mapper: default!); }

	public double? X
	{
		get { return GetDoubleOrNull("x"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("x", val);
		}
	}

	public double? Y
	{
		get { return GetDoubleOrNull("y"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("y", val);
		}
	}

	public Character? Character
	{
		get { return GetDoubleOrNull("c"); }
		set
		{
			Elt.SetAttributeValue("c", (Character)(value ?? double.NaN));
		}
	}

	public HWBool? ForcedCharacter
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? false;
			if (val == HWBool.NaN) { val = false; }
			Elt.SetAttributeValue("f", val);
		}
	}

	public HWBool? VehicleHidden
	{
		get { return GetBoolOrNull("h"); }
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val == HWBool.NaN) { val = false; }
			Elt.SetAttributeValue("h", val);
		}
	}

	/// <summary>
	///  The background the level has
	/// </summary>
	public Background? Background
	{
		get { return (Background?)GetDoubleOrNull("bg"); }
		set
		{
			Background val = value ?? HappyWheels.Background.Buggy;;
			Elt.SetAttributeValue("bg", val);
		}
	}

	/// <summary>
	///  The color of the level's background, if the background is set to Blank
	/// </summary>
	public double? BackgroundColor
	{
		get { return GetDoubleOrNull("bgc"); }
		set
		{
			double val = value ?? 16777215;
			Elt.SetAttributeValue("bgc", val);
		}
	}

	internal double? E
	{
		set
		{
			if (value != 1)
			{
				throw new LevelXMLException("This would make the level not import!");
			}
			Elt.SetAttributeValue("e", value!);
		}
	}

	protected void setParams(XElement e)
	{
		Version = Info.HappyWheelsVersion;
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
		Elt = new XElement(e.Name.ToString());
		setParams(e);
	}
}

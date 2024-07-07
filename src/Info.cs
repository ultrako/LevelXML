using System.Xml.Linq;

namespace LevelXML;

/// <summary>
/// The Info tag has information about the character and the background of the level
/// </summary>
internal class Info : LevelXMLTag, IConvertibleToXML
{
	internal const string LevelXMLVersion = "1.95";
	public const string EditorDefault = @"<info v=""" + LevelXMLVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>";

	internal string Version
	{
		get { return GetString("v"); }
		set
		{
			Elt.SetAttributeValue("v", value);
		}
	}

	public string ToXML() { return ToXML(mapper: default!); }

	public double X
	{
		get { return GetDouble("x"); }
		set
		{
			SetDouble("x", value);
		}
	}

	public double Y
	{
		get { return GetDouble("y"); }
		set
		{
			Elt.SetAttributeValue("y", value);
		}
	}

	public Character Character
	{
		get { return GetDouble("c"); }
		set
		{
			Elt.SetAttributeValue("c", value);
		}
	}

	public HWBool ForcedCharacter
	{
		get { return GetBool("f"); }
		set
		{
			HWBool val = value;
			if (val == HWBool.NaN) { val = false; }
			Elt.SetAttributeValue("f", val);
		}
	}

	public HWBool VehicleHidden
	{
		get { return GetBool("h"); }
		set
		{
			HWBool val = value;
			if (val == HWBool.NaN) { val = false; }
			Elt.SetAttributeValue("h", val);
		}
	}

	/// <summary>
	///  The background the level has
	/// </summary>
	public Background Background
	{
		get { return (Background)GetDouble("bg"); }
		set
		{
			Elt.SetAttributeValue("bg", value);
		}
	}

	/// <summary>
	///  The color of the level's background, if the background is set to Blank
	/// </summary>
	public double BackgroundColor
	{
		get { return GetDouble("bgc"); }
		set
		{
			Elt.SetAttributeValue("bgc", value);
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
		Version = Info.LevelXMLVersion;
		X = GetDoubleOrNull(e, "x") ?? double.NaN;
		Y = GetDoubleOrNull(e, "y") ?? double.NaN;
		Character = GetDoubleOrNull(e, "c") ?? double.NaN;
		ForcedCharacter = GetBoolOrNull(e, "f") ?? false;
		VehicleHidden = GetBoolOrNull(e, "h") ?? false;
		Background = GetDoubleOrNull(e, "bg") ?? LevelXML.Background.Buggy;
		BackgroundColor = GetDoubleOrNull(e, "bgc") ?? 16777215;
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

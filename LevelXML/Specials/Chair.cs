using System.Xml.Linq;

namespace HappyWheels;

public class Chair : Special
{
    internal override uint Type => 19;
    public const string EditorDefault = 
    @"<sp t=""19"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""f"" p5=""t""/>";

    public double Rotation
	{
		get { return GetDoubleOrNull("p2") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", value); 
		}
	}

    public HWBool Reverse
	{
		get { return GetBoolOrNull("p3") ?? false; }
		set { Elt.SetAttributeValue("p3", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p4") ?? false; }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public HWBool Interactive
	{
		get { return GetBoolOrNull("p5") ?? true; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Reverse = GetBoolOrNull(e, "p3") ?? false;
        Sleeping = GetBoolOrNull(e, "p4") ?? false;
        Interactive = GetBoolOrNull(e, "p5") ?? true;
    }

    public Chair(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Chair(XElement e) : base(e)
    {
        SetParams(e);
    }
}
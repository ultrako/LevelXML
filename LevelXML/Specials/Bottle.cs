using System.Xml.Linq;

namespace HappyWheels;

public class Bottle : SimpleSpecial
{
    internal override uint Type => 20;
    public const string EditorDefault = 
    @"<sp t=""20"" p0=""0"" p1=""0"" p2=""0"" p3=""1"" p4=""f"" p5=""t""/>";

    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", val); 
		}
	}

    public BottleType? BottleType
    {
        get { return (BottleType?)GetDoubleOrNull("p3"); }
        set { Elt.SetAttributeValue("p3", (BottleType?)value ?? HappyWheels.BottleType.Green); }
    }

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.False); }
	}

    public HWBool? Interactive
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.True); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        BottleType = GetDoubleOrNull(e, "p3");
        Sleeping = GetBoolOrNull(e, "p4");
        Interactive = GetBoolOrNull(e, "p5");
    }

    public Bottle(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Bottle(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace HappyWheels;

public class Toilet : SimpleSpecial
{
    internal override uint Type => 0;
    public const string EditorDefault = 
    @"<sp t=""24"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""f"" p5=""t""/>";

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

    public HWBool? Reverse
    {
        get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.False); }
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
        Reverse = GetBoolOrNull(e, "p3");
        Sleeping = GetBoolOrNull(e, "p4");
        Interactive = GetBoolOrNull(e, "p5");
    }

    public Toilet(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Toilet(XElement e) : base(e)
    {
        SetParams(e);
    }
}
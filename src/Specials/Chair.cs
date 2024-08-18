using System.Xml.Linq;

namespace LevelXML;

public class Chair : Special, IRotatable, IGroupable
{
    internal override uint Type => 19;
    public const string EditorDefault = 
    @"<sp t=""19"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""f"" p5=""t""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{ 
			SetDouble("p2", value); 
		}
	}

    public HWBool Reverse
	{
		get { return GetBool("p3"); }
		set { Elt.SetAttributeValue("p3", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBool("p4"); }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public HWBool Interactive
	{
		get { return GetBool("p5"); }
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
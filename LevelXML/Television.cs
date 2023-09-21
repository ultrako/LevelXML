using System.Xml.Linq;

namespace HappyWheels;

public class Television : SimpleSpecial
{
    internal override uint Type => 21;
    public const string EditorDefault = 
    @"<sp t=""21"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t""/>";

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

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", value ?? HWBool.False); }
	}

    public HWBool? Interactive
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.True); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Sleeping = GetBoolOrNull(e, "p3");
        Interactive = GetBoolOrNull(e, "p4");
    }

    public Television(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Television(XElement e) : base(e)
    {
        SetParams(e);
    }
}
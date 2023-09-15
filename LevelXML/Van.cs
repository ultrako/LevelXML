using System.Xml.Linq;

namespace HappyWheels;

public class Van : Special
{
    internal override uint Type => 0;
    public static string EditorDefault = 
    @"<sp t=""0"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t""/>";

    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("That would make the van disappear!");
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

    internal Van(XElement e) : base(e)
    {
        SetParams(e);
    }
}


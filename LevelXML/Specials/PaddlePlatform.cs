using System.Xml.Linq;

namespace HappyWheels;

public class PaddlePlatform : Special
{
    internal override uint Type => 35;
    public const string EditorDefault = 
    @"<sp t=""35"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""f"" p5=""90"" p6=""10""/>";

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

    public double? Delay
    {
        get { return GetDoubleOrNull("p3"); }
        set
        {
            double val = value ?? 0;
            SetDouble("p3", Math.Clamp(val, 0, 2));
        }
    }

    public HWBool? Reverse
    {
        get { return GetBoolOrNull("p4"); }
        set { Elt.SetAttributeValue("p4", value ?? HWBool.False); }
    }

    public double? MaxAngle
    {
        get { return GetDoubleOrNull("p5"); }
        set 
        {
            double val = value ?? 90;
            SetDouble("p5", Math.Clamp(val, 15, 90));
        }
    }

    public double? Speed
    {
        get { return GetDoubleOrNull("p6"); }
        set
        {
            double val = value ?? 10;
            SetDouble("p6", Math.Clamp(val, 1, 10));
        }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Delay = GetDoubleOrNull(e, "p3");
        Reverse = GetBoolOrNull(e, "p4");
        MaxAngle = GetDoubleOrNull(e, "p5");
        Speed = GetDoubleOrNull(e, "p6");
    }

    public PaddlePlatform(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal PaddlePlatform(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace LevelXML;

public class PaddlePlatform : Special
{
    internal override uint Type => 35;
    public const string EditorDefault = 
    @"<sp t=""35"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""f"" p5=""90"" p6=""10""/>";

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

    public double Delay
    {
        get { return GetDoubleOrNull("p3") ?? 0; }
        set { SetDouble("p3", Math.Clamp(value, 0, 2)); }
    }

    public HWBool Reverse
    {
        get { return GetBoolOrNull("p4") ?? false; }
        set { Elt.SetAttributeValue("p4", value); }
    }

    public double MaxAngle
    {
        get { return GetDoubleOrNull("p5") ?? 90; }
        set { SetDouble("p5", Math.Clamp(value, 15, 90)); }
    }

    public double Speed
    {
        get { return GetDoubleOrNull("p6") ?? 10; }
        set { SetDouble("p6", Math.Clamp(value, 1, 10)); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Delay = GetDoubleOrNull(e, "p3") ?? 0;
        Reverse = GetBoolOrNull(e, "p4") ?? false;
        MaxAngle = GetDoubleOrNull(e, "p5") ?? 90;
        Speed = GetDoubleOrNull(e, "p6") ?? 10;
    }

    public PaddlePlatform(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal PaddlePlatform(XElement e) : base(e)
    {
        SetParams(e);
    }
}
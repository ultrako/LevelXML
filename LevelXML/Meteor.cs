using System.Xml.Linq;

namespace HappyWheels;

public class Meteor : SimpleSpecial
{
    internal override uint Type => 11;
    public const string EditorDefault = 
    @"<sp t=""11"" p0=""0"" p1=""0"" p2=""400"" p4=""f"" p5=""f""/>";

    public double? Width
    {
        get { return GetDoubleOrNull("p2"); }
        set
        {
            double val = value ?? 400;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the width or height to NaN would make the special disappear!");
            }
            double clamped = Math.Clamp(val, 200, 600);
            SetDouble("p2", clamped);
            SetDouble("p3", clamped);
        }
    }

    public double? Height
    {
        get { return Width; }
        set { Width = value; }
    }

    public HWBool? Fixed
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.True); }
	}

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.False); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2");
        Height = GetDoubleOrNull(e, "p3");
        Fixed = GetBoolOrNull(e, "p4");
        Sleeping = GetBoolOrNull(e, "p5");
    }

    public Meteor(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Meteor(XElement e) : base(e)
    {
        SetParams(e);
    }
}
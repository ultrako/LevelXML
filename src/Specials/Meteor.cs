using System.Xml.Linq;

namespace LevelXML;

public class Meteor : Special
{
    internal override uint Type => 11;
    public const string EditorDefault = 
    @"<sp t=""11"" p0=""0"" p1=""0"" p2=""400"" p4=""f"" p5=""f""/>";

    public double Width
    {
        get { return GetDouble("p2"); }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the width or height to NaN would make the special disappear!");
            }
            double clamped = Math.Clamp(value, 200, 600);
            SetDouble("p2", clamped);
            SetDouble("p3", clamped);
        }
    }

    public double Height
    {
        get { return Width; }
        set { Width = value; }
    }

    public HWBool Fixed
	{
		get { return GetBoolOrNull("p4") ?? false; }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p5") ?? false; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? GetDoubleOrNull(e, "p3") ?? 400;
        Height = Width;
        Fixed = GetBoolOrNull(e, "p4") ?? false;
        Sleeping = GetBoolOrNull(e, "p5") ?? false;
    }

    public Meteor(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Meteor(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace HappyWheels;

public class IBeam : SimpleSpecial
{
    internal override uint Type => 3;
    public const string EditorDefault = 
    @"<sp t=""3"" p0=""364"" p1=""112"" p2=""400"" p3=""32"" p4=""0"" p5=""f"" p6=""f"" />";

    public double? Width
    {
        get { return GetDoubleOrNull("p2"); }
        set
        {
            double val = value ?? 400;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the width to NaN would make the special disappear!");
            }
            SetDouble("p2", Math.Clamp(val, 200, 1600));
        }
    }

    public double? Height
    {
        get { return GetDoubleOrNull("p3"); }
        set
        {
            double val = value ?? 32;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the height to NaN would make the special disappear!");
            }
            SetDouble("p3", Math.Clamp(val, 32, 64));
        }
    }

    public double? Rotation
	{
		get { return GetDoubleOrNull("p4"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
			}
			SetDouble("p4", val); 
		}
	}

     public HWBool? Fixed
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.False); }
	}

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p6"); }
		set { Elt.SetAttributeValue("p6", value ?? HWBool.False); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2");
        Height = GetDoubleOrNull(e, "p3");
        Rotation = GetDoubleOrNull(e, "p4");
        Fixed = GetBoolOrNull(e, "p5");
        Sleeping = GetBoolOrNull(e, "p6");
    }

    public IBeam(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal IBeam(XElement e) : base(e)
    {
        SetParams(e);
    }
}
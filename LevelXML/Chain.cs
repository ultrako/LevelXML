using System.Xml.Linq;

namespace HappyWheels;

public class Chain : SimpleSpecial
{
    internal override uint Type => 30;
    public const string EditorDefault = 
    @"<sp t=""30"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t"" p5=""20"" p6=""1"" p7=""0""/>";

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

    /// <summary>
    ///  This is the amount of links the chain has.
    /// </summary>
    public double? LinkCount
    {
        get { return GetDoubleOrNull("p5"); }
        set
        {
            double val = value ?? 20;
            SetDouble("p5", Math.Clamp(val, 2, 40));
        }
    }

    /// <summary>
    ///  This is how large the links of the chain are.
    /// </summary>
    public double? LinkScale
    {
        get { return GetDoubleOrNull("p6"); }
        set
        {
            double val = value ?? 1;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the Link Scale to NaN would make the chain disappear!");
            }
            SetDouble("p6", Math.Clamp(val, 1, 10));
        }
    }

    /// <summary>
    /// This is the angle that each link intersects its neighbors
    /// </summary>
    public double? Curve
    {
        get { return GetDoubleOrNull("p7"); }
        set
        {
            double val = value ?? 1;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the Curve to NaN would make the chain disappear!");
            }
            SetDouble("p7", Math.Clamp(val, -10, 10));
        }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Sleeping = GetBoolOrNull(e, "p3");
        Interactive = GetBoolOrNull(e, "p4");
        LinkCount = GetDoubleOrNull(e, "p5");
        LinkScale = GetDoubleOrNull(e, "p6");
        Curve = GetDoubleOrNull(e, "p7");
    }

    public Chain(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Chain(XElement e) : base(e)
    {
        SetParams(e);
    }
}
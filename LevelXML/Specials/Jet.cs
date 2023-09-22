using System.Xml.Linq;

namespace HappyWheels;

public class Jet : SimpleSpecial
{
    internal override uint Type => 28;
    public const string EditorDefault = 
    @"<sp t=""28"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""1"" p5=""0"" p6=""0"" p7=""f""/>";

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

    /// <summary>
    /// The amount of force the jet provides (and the size of the jet)
    /// </summary>
    public double? Power
    {
        get { return GetDoubleOrNull("p4"); }
        set 
        {
            double val = value ?? 1;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the power to NaN would make the jet disappear!");
            }
            SetDouble("p4", Math.Clamp(val, 1, 10));
        }
    }

    /// <summary>
    /// The amount of time (in seconds) until the jet shuts off.
    /// With a setting of 0, the jet will not shut off.
    /// </summary>
    public double? FiringTime
    {
        get { return GetDoubleOrNull("p5"); }
        set
        {
            double val = value ?? 0;
            SetDouble("p5", Math.Clamp(val, 0, 50));
        }
    }

    /// <summary>
    /// The amount of time (in seconds) until the jet reaches full power.
    /// </summary>
    public double? AccelerationTime
    {
        get { return GetDoubleOrNull("p6"); }
        set
        {
            double val = value ?? 0;
            SetDouble("p6", Math.Clamp(val, 0, 5));
        }
    }

    /// <summary>
    /// Setting this to true will prevent the jet from rotating.
    /// </summary>
    public HWBool? FixedAngle
    {
        get { return GetBoolOrNull("p7"); }
		set { Elt.SetAttributeValue("p7", value ?? HWBool.True); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Sleeping = GetBoolOrNull(e, "p3");
        Power = GetDoubleOrNull(e, "p4");
        FiringTime = GetDoubleOrNull(e, "p5");
        AccelerationTime = GetDoubleOrNull(e, "p6");
        FixedAngle = GetBoolOrNull(e, "p7");
    }

    public Jet(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Jet(XElement e) : base(e)
    {
        SetParams(e);
    }
}
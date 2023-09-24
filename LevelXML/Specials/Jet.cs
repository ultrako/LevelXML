using System.Xml.Linq;

namespace HappyWheels;

public class Jet : Special
{
    internal override uint Type => 28;
    public const string EditorDefault = 
    @"<sp t=""28"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""1"" p5=""0"" p6=""0"" p7=""f""/>";

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

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p3") ?? false; }
		set { Elt.SetAttributeValue("p3", value); }
	}

    /// <summary>
    /// The amount of force the jet provides (and the size of the jet)
    /// </summary>
    public double Power
    {
        get { return GetDoubleOrNull("p4") ?? 1; }
        set 
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the power to NaN would make the jet disappear!");
            }
            SetDouble("p4", Math.Clamp(value, 1, 10));
        }
    }

    /// <summary>
    /// The amount of time (in seconds) until the jet shuts off.
    /// With a setting of 0, the jet will not shut off.
    /// </summary>
    public double FiringTime
    {
        get { return GetDoubleOrNull("p5") ?? 0; }
        set { SetDouble("p5", Math.Clamp(value, 0, 50)); }
    }

    /// <summary>
    /// The amount of time (in seconds) until the jet reaches full power.
    /// </summary>
    public double AccelerationTime
    {
        get { return GetDoubleOrNull("p6") ?? 0; }
        set { SetDouble("p6", Math.Clamp(value, 0, 5)); }
    }

    /// <summary>
    /// Setting this to true will prevent the jet from rotating.
    /// </summary>
    public HWBool FixedAngle
    {
        get { return GetBoolOrNull("p7") ?? true; }
		set { Elt.SetAttributeValue("p7", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Sleeping = GetBoolOrNull(e, "p3") ?? false;
        Power = GetDoubleOrNull(e, "p4") ?? 1;
        FiringTime = GetDoubleOrNull(e, "p5") ?? 0;
        AccelerationTime = GetDoubleOrNull(e, "p6") ?? 0;
        FixedAngle = GetBoolOrNull(e, "p7") ?? false;
    }

    public Jet(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Jet(XElement e) : base(e)
    {
        SetParams(e);
    }
}
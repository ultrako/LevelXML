using System.Xml.Linq;

namespace HappyWheels;

public class ArrowGun : Special
{
    internal override uint Type => 29;
    public const string EditorDefault = 
    @"<sp t=""29"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""5"" p5=""f""/>";

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

    public HWBool? Fixed
	{
		get { return GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", value ?? HWBool.False); }
	}

    /// <summary>
    /// The speed at which this arrow gun fires arrows
    /// </summary>
    public double? RateOfFire
    {
        get { return GetDoubleOrNull("p4"); }
        set { SetDouble("p4", value ?? 5); }
    }

    /// <summary>
    ///  If this is set to false, the arrow gun will only shoot player characters.
    /// </summary>
    public HWBool? ShootPlayer
    {
        // The inversion is here because ingame the parameter is called,
        // "don't shoot player"
        get { return !GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", !value ?? HWBool.False); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Fixed = GetBoolOrNull(e, "p3");
        RateOfFire = GetDoubleOrNull(e, "p4");
        ShootPlayer = !GetBoolOrNull(e, "p5");
    }

    public ArrowGun(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal ArrowGun(XElement e) : base(e)
    {
        SetParams(e);
    }
}
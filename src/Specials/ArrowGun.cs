using System.Xml.Linq;

namespace LevelXML;

public class ArrowGun : Special
{
    internal override uint Type => 29;
    public const string EditorDefault = 
    @"<sp t=""29"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""5"" p5=""f""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{ 
			SetDouble("p2", value); 
		}
	}

    public HWBool Fixed
	{
		get { return GetBoolOrNull("p3") ?? false; }
		set { Elt.SetAttributeValue("p3", value); }
	}

    /// <summary>
    /// The speed at which this arrow gun fires arrows
    /// </summary>
    public double RateOfFire
    {
        get { return GetDoubleOrNull("p4") ?? 5; }
        set { SetDouble("p4", value); }
    }

    /// <summary>
    ///  If this is set to false, the arrow gun will only shoot player characters.
    /// </summary>
    public HWBool ShootPlayer
    {
        // The inversion is here because ingame the parameter is called,
        // "don't shoot player"
        get { return !GetBoolOrNull("p3") ?? true; }
		set { Elt.SetAttributeValue("p3", !value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Fixed = GetBoolOrNull(e, "p3") ?? false;
        RateOfFire = GetDoubleOrNull(e, "p4") ?? 5;
        ShootPlayer = !GetBoolOrNull(e, "p5") ?? true;
    }

    public ArrowGun(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal ArrowGun(XElement e) : base(e)
    {
        SetParams(e);
    }
}
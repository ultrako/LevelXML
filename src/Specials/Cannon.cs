using System.Xml.Linq;

namespace LevelXML;

public class Cannon : Special
{
    internal override uint Type => 33;
    public const string EditorDefault = 
    @"<sp t=""33"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""0"" p5=""1"" p6=""1"" p7=""1"" p8=""5""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{ 
			SetDouble("p2", value); 
		}
	}

    /// <summary>
    ///  The angle the muzzle rests at until objects fall into it.
    /// </summary>
    public double StartRotation
    {
        get { return GetDouble("p3"); }
        set { SetDouble("p3", Math.Clamp(value, -90, 90)); }
    }

    /// <summary>
    /// The angle the muzzle moves to and fires.
    /// </summary>
    public double FiringRotation
    {
        get { return GetDouble("p4"); }
        set { SetDouble("p4", Math.Clamp(value, -90, 90)); }
    }

    /// <summary>
    /// Whether the cannon has circus colors or is metal
    /// </summary>
    public CannonType CannonType
    {
        get { return (CannonType)GetDouble("p5"); }
        set { Elt.SetAttributeValue("p5", value); }
    }

    /// <summary>
    /// The amount of time in seconds before the cannon moves to the firing position
    /// </summary>
    public double Delay
    {
        get { return GetDouble("p6"); }
        set { SetDouble("p6", Math.Clamp(value, 1, 10)); }
    }

    /// <summary>
    /// How large the cannon is
    /// </summary>
    public double MuzzleScale
    {
        get { return GetDouble("p7"); }
        set { SetDouble("p7", Math.Clamp(value, 1, 10)); }
    }

    /// <summary>
    /// The amount of force that firing the cannon applies.
    /// </summary>
    public double Power
    {
        get { return GetDouble("p8"); }
        set { SetDouble("p8", Math.Clamp(value, 1, 10)); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        StartRotation = GetDoubleOrNull(e, "p3") ?? 0;
        FiringRotation = GetDoubleOrNull(e, "p4") ?? 0;
        CannonType = GetDoubleOrNull(e, "p5") ?? CannonType.Circus;
        Delay = GetDoubleOrNull(e, "p6") ?? 1;
        MuzzleScale = GetDoubleOrNull(e, "p7") ?? 1;
        Power = GetDoubleOrNull(e, "p8") ?? 5;
    }

    public Cannon(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Cannon(XElement e) : base(e)
    {
        SetParams(e);
    }
}
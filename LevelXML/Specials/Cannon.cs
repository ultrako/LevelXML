using System.Xml.Linq;

namespace HappyWheels;

public class Cannon : Special
{
    internal override uint Type => 33;
    public const string EditorDefault = 
    @"<sp t=""33"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""0"" p5=""1"" p6=""1"" p7=""1"" p8=""5""/>";

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

    /// <summary>
    ///  The angle the muzzle rests at until objects fall into it.
    /// </summary>
    public double? StartRotation
    {
        get { return GetDoubleOrNull("p3"); }
        set
        {
            double val = value ?? 0;
            SetDouble("p3", Math.Clamp(val, -90, 90));
        }
    }

    /// <summary>
    /// The angle the muzzle moves to and fires.
    /// </summary>
    public double? FiringRotation
    {
        get { return GetDoubleOrNull("p4"); }
        set
        {
            double val = value ?? 0;
            SetDouble("p4", Math.Clamp(val, -90, 90));
        }
    }

    /// <summary>
    /// Whether the cannon has circus colors or is metal
    /// </summary>
    public CannonType? CannonType
    {
        get { return (CannonType?)GetDoubleOrNull("p5"); }
        set { Elt.SetAttributeValue("p5", (CannonType?)value ?? HappyWheels.CannonType.Circus); }
    }

    /// <summary>
    /// The amount of time in seconds before the cannon moves to the firing position
    /// </summary>
    public double? Delay
    {
        get { return GetDoubleOrNull("p6"); }
        set 
        {
            double val = value ?? 1;
            SetDouble("p6", Math.Clamp(val, 1, 10));
        }
    }

    /// <summary>
    /// How large the cannon is
    /// </summary>
    public double? MuzzleScale
    {
        get { return GetDoubleOrNull("p7"); }
        set
        {
            double val = value ?? 1;
            SetDouble("p7", Math.Clamp(val, 1, 10));
        }
    }

    /// <summary>
    /// The amount of force that firing the cannon applies.
    /// </summary>
    public double? Power
    {
        get { return GetDoubleOrNull("p8"); }
        set
        {
            double val = value ?? 5;
            SetDouble("p8", Math.Clamp(val, 1, 10));
        }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        StartRotation = GetDoubleOrNull(e, "p3");
        FiringRotation = GetDoubleOrNull(e, "p4");
        CannonType = GetDoubleOrNull(e, "p5");
        Delay = GetDoubleOrNull(e, "p6");
        MuzzleScale = GetDoubleOrNull(e, "p7");
        Power = GetDoubleOrNull(e, "p8");
    }

    public Cannon(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Cannon(XElement e) : base(e)
    {
        SetParams(e);
    }
}
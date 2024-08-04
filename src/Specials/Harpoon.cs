using System.Xml.Linq;

namespace LevelXML;

public class Harpoon : Special
{
    internal override uint Type => 15;
    public const string EditorDefault = 
    @"<sp t=""15"" p0=""0"" p1=""0"" p2=""0"" p3=""t"" p4=""f"" p5=""0"" p6=""f"" p7=""f""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{
			SetDouble("p2", value); 
		}
	}

    /// <summary>
    /// Whether or not the harpoon has a rope anchored to the base.
    /// </summary>
    public HWBool Anchor
	{
		get { return GetBool("p3"); }
		set { Elt.SetAttributeValue("p3", value); }
	}

    /// <summary>
    /// If true, the turret of the gun will only aim at the specified turret angle.
    /// </summary>
    public HWBool FixedTurret
	{
		get { return GetBool("p4"); }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public double TurretAngle
    {
        get { return GetDouble("p5"); }
        set { SetDouble("p5", value); }
    }

    /// <summary>
    /// If true, the turret of the gun will only fire when signaled by a trigger.
    /// </summary>
    public HWBool TriggerFiring
    {
        get { return GetBool("p6"); }
        set { Elt.SetAttributeValue("p6", value); }
    }

    /// <summary>
    /// If true, the harpoon will be deactivated until activated by a trigger.
    /// </summary>
    public HWBool StartDeactivated
    {
        get { return GetBool("p7"); }
        set { Elt.SetAttributeValue("p7", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Anchor = GetBoolOrNull(e, "p3") ?? true;
        FixedTurret = GetBoolOrNull(e, "p4") ?? false;
        TurretAngle = GetDoubleOrNull(e, "p5") ?? 0;
        TriggerFiring = GetBoolOrNull(e, "p6") ?? false;
        StartDeactivated = GetBoolOrNull(e, "p7") ?? false;
    }

    public Harpoon(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Harpoon(XElement e) : base(e)
    {
        SetParams(e);
    }
}
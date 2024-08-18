using System.Xml.Linq;

namespace LevelXML;

public class NonPlayerCharacter : Special, IRotatable, IGroupable
{
    internal override uint Type => 17;
    public const string EditorDefault =
    @"<sp t=""17"" p0=""0"" p1=""0"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{ 
			SetDouble("p2", value); 
		}
	}

    /// <summary>
    /// Which of the 16 possible characters this NPC is
    /// </summary>
    public NPCType NPCType
    {
        get { return (NPCType)GetDouble("p3"); }
        set { Elt.SetAttributeValue("p3", (NPCType?)value); }
    }

    public HWBool Sleeping
    {
        get { return GetBool("p4"); }
        set { Elt.SetAttributeValue("p4", value); }
    }

    /// <summary>
    ///  If Reverse is true, the NPC faces to the left, otherwise it faces to the right.
    /// </summary>
    public HWBool Reverse
    {
        get { return GetBool("p5"); }
        set { Elt.SetAttributeValue("p5", value); }
    }

    /// <summary>
    ///  Whether or not the NPC's body is rigid
    /// </summary>
    public HWBool HoldPose
    {
        get { return GetBool("p6"); }
        set { Elt.SetAttributeValue("p6", value); }
    }

    public HWBool Interactive
    {
        get { return GetBool("p7"); }
        set { Elt.SetAttributeValue("p7", value); }
    }

    public double NeckAngle
    {
        get { return GetDouble("p8"); }
        set
        {
            SetDouble("p8", Math.Clamp(value, -20, 20));
        }
    }

    public double FrontArmAngle
    {
        get { return GetDouble("p9"); }
        set
        {
            SetDouble("p9", Math.Clamp(value, -180, 60));
        }
    }

    public double BackArmAngle
    {
        get { return GetDouble("p10"); }
        set
        {
            SetDouble("p10", Math.Clamp(value, -180, 60));
        }
    }

    public double FrontElbowAngle
    {
        get { return GetDouble("p11"); }
        set
        {
            SetDouble("p11", Math.Clamp(value, -160, 0));
        }
    }

    public double BackElbowAngle
    {
        get { return GetDouble("p12"); }
        set
        {
            SetDouble("p12", Math.Clamp(value, -160, 0));
        }
    }

    public double FrontLegAngle
    {
        get { return GetDouble("p13"); }
        set
        {
            SetDouble("p13", Math.Clamp(value, -160, 10));
        }
    }

    public double BackLegAngle
    {
        get { return GetDouble("p14"); }
        set
        {
            SetDouble("p14", Math.Clamp(value, -160, 10));
        }
    }

    public double FrontKneeAngle
    {
        get { return GetDouble("p15"); }
        set
        {
            SetDouble("p15", Math.Clamp(value, -0, 150));
        }
    }

    public double BackKneeAngle
    {
        get { return GetDouble("p16"); }
        set
        {
            SetDouble("p16", Math.Clamp(value, -0, 150));
        }
    }

    /// <summary>
    /// Set this if you'd like all attached joints and associated trigger actions to be destroyed when this character dies.
    /// </summary>
    public HWBool ReleaseOnDeath
    {
        get { return GetBool("p17") ; }
        set { Elt.SetAttributeValue("p17", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        NPCType = GetDoubleOrNull(e, "p3") ?? NPCType.WheelchairGuy;
        Sleeping = GetBoolOrNull(e, "p4") ?? false;
        Reverse = GetBoolOrNull(e, "p5") ?? false;
        HoldPose = GetBoolOrNull(e, "p6") ?? false;
        Interactive = GetBoolOrNull(e, "p7") ?? true;
        NeckAngle = GetDoubleOrNull(e, "p8") ?? 0;
        FrontArmAngle = GetDoubleOrNull(e, "p9") ?? 0;
        BackArmAngle = GetDoubleOrNull(e, "p10") ?? 0;
        FrontElbowAngle = GetDoubleOrNull(e, "p11") ?? 0;
        BackElbowAngle = GetDoubleOrNull(e, "p12") ?? 0;
        FrontLegAngle = GetDoubleOrNull(e, "p13") ?? 0;
        BackLegAngle = GetDoubleOrNull(e, "p14") ?? 0;
        FrontKneeAngle = GetDoubleOrNull(e, "p15") ?? 0;
        BackKneeAngle = GetDoubleOrNull(e, "p16") ?? 0;
        ReleaseOnDeath = GetBoolOrNull(e, "p17") ?? false;
    }

    public NonPlayerCharacter(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal NonPlayerCharacter(XElement e) : base(e)
    {
        SetParams(e);
    }
}
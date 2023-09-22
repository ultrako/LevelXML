using System.Xml.Linq;

namespace HappyWheels;

public class NonPlayerCharacter : Special
{
    internal override uint Type => 17;
    public const string EditorDefault =
    @"<sp t=""17"" p0=""0"" p1=""0"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>";

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
    /// Which of the 16 possible characters this NPC is
    /// </summary>
    public NPCType? NPCType
    {
        get { return (NPCType?)GetDoubleOrNull("p3"); }
        set { Elt.SetAttributeValue("p3", (NPCType?)value ?? HappyWheels.NPCType.WheelchairGuy); }
    }

    public HWBool? Sleeping
    {
        get { return GetBoolOrNull("p4"); }
        set { Elt.SetAttributeValue("p4", value ?? HWBool.False); }
    }

    /// <summary>
    ///  If Reverse is true, the NPC faces to the left, otherwise it faces to the right.
    /// </summary>
    public HWBool? Reverse
    {
        get { return GetBoolOrNull("p5"); }
        set { Elt.SetAttributeValue("p5", value ?? HWBool.False); }
    }

    /// <summary>
    ///  Whether or not the NPC's body is rigid
    /// </summary>
    public HWBool? HoldPose
    {
        get { return GetBoolOrNull("p6"); }
        set { Elt.SetAttributeValue("p6", value ?? HWBool.False); }
    }

    public HWBool? Interactive
    {
        get { return GetBoolOrNull("p7"); }
        set { Elt.SetAttributeValue("p7", value ?? HWBool.True); }
    }

    public double? NeckAngle
    {
        get { return GetDoubleOrNull("p8"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p8", Math.Clamp(val, -20, 20));
        }
    }

    public double? FrontArmAngle
    {
        get { return GetDoubleOrNull("p9"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p9", Math.Clamp(val, -180, 60));
        }
    }

    public double? BackArmAngle
    {
        get { return GetDoubleOrNull("p10"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p10", Math.Clamp(val, -180, 60));
        }
    }

    public double? FrontElbowAngle
    {
        get { return GetDoubleOrNull("p11"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p11", Math.Clamp(val, -160, 0));
        }
    }

    public double? BackElbowAngle
    {
        get { return GetDoubleOrNull("p12"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p12", Math.Clamp(val, -160, 0));
        }
    }

    public double? FrontLegAngle
    {
        get { return GetDoubleOrNull("p13"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p13", Math.Clamp(val, -160, 10));
        }
    }

    public double? BackLegAngle
    {
        get { return GetDoubleOrNull("p14"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p14", Math.Clamp(val, -160, 10));
        }
    }

    public double? FrontKneeAngle
    {
        get { return GetDoubleOrNull("p15"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p15", Math.Clamp(val, -0, 150));
        }
    }

    public double? BackKneeAngle
    {
        get { return GetDoubleOrNull("p16"); }
        set
        {
            double val = value ?? 0;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting any pose angle to NaN would make this NPC disappear!");
            }
            SetDouble("p16", Math.Clamp(val, -0, 150));
        }
    }

    /// <summary>
    /// Set this if you'd like all attached joints and associated trigger actions to be destroyed when this character dies.
    /// </summary>
    public HWBool? ReleaseOnDeath
    {
        get { return GetBoolOrNull("p17"); }
        set { Elt.SetAttributeValue("p17", value ?? HWBool.False); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        NPCType = GetDoubleOrNull(e, "p3");
        Sleeping = GetBoolOrNull(e, "p4");
        Reverse = GetBoolOrNull(e, "p5");
        HoldPose = GetBoolOrNull(e, "p6");
        Interactive = GetBoolOrNull(e, "p7");
        NeckAngle = GetDoubleOrNull(e, "p8");
        FrontArmAngle = GetDoubleOrNull(e, "p9");
        BackArmAngle = GetDoubleOrNull(e, "p10");
        FrontElbowAngle = GetDoubleOrNull(e, "p11");
        BackElbowAngle = GetDoubleOrNull(e, "p12");
        FrontLegAngle = GetDoubleOrNull(e, "p13");
        BackLegAngle = GetDoubleOrNull(e, "p14");
        FrontKneeAngle = GetDoubleOrNull(e, "p15");
        BackKneeAngle = GetDoubleOrNull(e, "p16");
        ReleaseOnDeath = GetDoubleOrNull(e, "p17");
    }

    public NonPlayerCharacter(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal NonPlayerCharacter(XElement e) : base(e)
    {
        SetParams(e);
    }
}
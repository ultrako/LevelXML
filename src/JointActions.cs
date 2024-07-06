using System.Xml.Linq;

namespace LevelXML;

/// <summary>
///  This action disables the motor on a Joint, making it fall limp.
/// </summary>
public class DisableMotor : TriggerAction, ITriggerAction<Joint>
{
    public DisableMotor()
    {
        Elt.SetAttributeValue("i", 0);
    }
}

/// <summary>
///  This action changes a Joint's speed.
///  Note that this value is not clamped, even though the corresponding values on a Joint itself are.
/// </summary>
public class ChangeMotorSpeed : TriggerAction, ITriggerAction<Joint>
{
    public const string EditorDefault =
    @"<a i=""1"" p0=""0"" p1=""1""/>";
    public double? Speed
    {
        get { return GetDoubleOrNull("p0"); }
        set { SetDouble("p0", value ?? 0); }
    }
    public double? Duration
    {
        get { return GetDoubleOrNull("p1"); }
        set { SetDouble("p1", value ?? 0); }
    }
    public ChangeMotorSpeed(double speed, double duration)
    {
        Elt.SetAttributeValue("i", 1);
        Speed = speed;
        Duration = duration;
    }
    public ChangeMotorSpeed(string xml=EditorDefault) : this(StrToXElement(xml)) {}
    internal ChangeMotorSpeed(XElement e)
    {
        Elt.SetAttributeValue("i", 1);
        Speed = GetDoubleOrNull(e, "p0");
        Duration = GetDoubleOrNull(e, "p1");
    }
}

/// <summary>
/// This action deletes the Joint.
/// </summary>
public class DeleteSelfJoint : TriggerAction, ITriggerAction<Joint>
{
    public DeleteSelfJoint()
    {
        Elt.SetAttributeValue("i", 2);
    }
}

/// <summary>
///  This action makes the Joint be able to move past its upper and lower limits.
/// </summary>
public class DisableLimits : TriggerAction, ITriggerAction<Joint>
{
    public DisableLimits()
    {
        Elt.SetAttributeValue("i", 3);
    }
}

/// <summary>
///  This action changes the upper and lower limits on a Joint.
///  Note that unlike the UpperLimiy and LowerLimit parameters on a Joint itself,
///  these values are not clamped, so a lower limit can be positive
///  and an upper limit can be negative.
/// </summary>
public class ChangeLimits : TriggerAction, ITriggerAction<Joint>
{
    public const string EditorDefault =
    @"<a i=""4"" p0=""90"" p1=""-90""/>";

    public double? UpperLimit
    {
        get
        {
            return GetDoubleOrNull("p0");
        }
        set
        {
            SetDouble("p0", value ?? 0);
        }
    }

    public double? LowerLimit
    {
        get
        {
            return GetDoubleOrNull("p1");
        }
        set
        {
            SetDouble("p1", value ?? 0);
        }
    }

    public ChangeLimits(double upperLimit, double lowerLimit)
    {
        Elt.SetAttributeValue("i", 4);
        UpperLimit = upperLimit;
        LowerLimit = lowerLimit;
    }

    public ChangeLimits(string xml=EditorDefault) : this(StrToXElement(xml)) {}
    
    internal ChangeLimits(XElement e)
    {
        Elt.SetAttributeValue("i", 4);
        UpperLimit = GetDoubleOrNull(e, "p0");
        LowerLimit = GetDoubleOrNull(e, "p1");
    }
}
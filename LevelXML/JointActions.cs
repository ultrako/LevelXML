using System.Xml.Linq;

namespace HappyWheels;

public class DisableMotor : TriggerAction<Joint>
{
    public DisableMotor()
    {
        Elt.SetAttributeValue("i", 0);
    }
}

public class ChangeMotorSpeed : TriggerAction<Joint>
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

public class DeleteSelfJoint : TriggerAction<Joint>
{
    public DeleteSelfJoint()
    {
        Elt.SetAttributeValue("i", 2);
    }
}

public class DisableLimits : TriggerAction<Joint>
{
    public DisableLimits()
    {
        Elt.SetAttributeValue("i", 3);
    }
}

public class ChangeLimits : TriggerAction<Joint>
{
    public const string EditorDefault =
    @"<a i=""4"" p0=""90"" p1=""-90""/>";
    // Whilst the sliders in the level editor imply that there is a limit to a joint's range,
    // there is no such limit in level xml.
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
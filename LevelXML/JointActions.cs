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
    double? Speed
    {
        get { return GetDoubleOrNull("p0"); }
        set { Elt.SetAttributeValue("p0", value ?? 0); }
    }
    double? Duration
    {
        get { return GetDoubleOrNull("p1"); }
        set { Elt.SetAttributeValue("p1", value ?? 0); }
    }
    public ChangeMotorSpeed(double speed, double duration)
    {
        Elt.SetAttributeValue("i", 1);
        Speed = speed;
        Duration = duration;
    }
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
    // Whilst the sliders in the level editor imply that there is a limit to a joint's range,
    // there is no such limit in level xml.
    double? UpperLimit
    {
        get
        {
            return GetDoubleOrNull("p0");
        }
        set
        {
            Elt.SetAttributeValue("p0", value);
        }
    }
    double? LowerLimit
    {
        get
        {
            return GetDoubleOrNull("p1");
        }
        set
        {
            Elt.SetAttributeValue("p1", value);
        }
    }

    public ChangeLimits(double upperLimit, double lowerLimit)
    {
        Elt.SetAttributeValue("i", 4);
        UpperLimit = upperLimit;
        LowerLimit = lowerLimit;
    }
    internal ChangeLimits(XElement e)
    {
        Elt.SetAttributeValue("i", 4);
        UpperLimit = GetDoubleOrNull(e, "p0");
        LowerLimit = GetDoubleOrNull(e, "p1");
    }
}
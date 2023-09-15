namespace HappyWheels;
using System.Xml.Linq;

/// <summary>
/// A pin joint is a kind of joint that rotates on a point
/// </summary>
public class PinJoint : Joint
{
    public static string EditorDefault = 
	@"<j t=""0"" x=""0"" y=""0"" b1=""-1"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>";
    internal override uint Type => 0;
    public double? Torque
    {
        get
        {
            return GetDoubleOrNull("tq");
        }
        set
        {
            SetDouble("tq", Math.Clamp(value ?? 50, double.NegativeInfinity, 100000));
        }
    }
    public override double? Speed
    {
        get
        {
            return GetDoubleOrNull("sp");
        }
        set
        {
            Elt.SetAttributeValue("sp", Math.Clamp(value ?? 3, -20, 20));
        }
    }
    public override double? UpperLimit
    {
        get
        {
			return GetDoubleOrNull("ua");
		}
		set
		{
			Elt.SetAttributeValue("ua", Math.Clamp(value ?? 90, 0, 180));
		}
    }
    public override double? LowerLimit
    {
        get
        {
			return GetDoubleOrNull("la");
		}
		set
		{
            double val = Math.Abs(value ?? 90);
            double negatedAndClamped = Math.Clamp(-val, -180, 0.0);
            if (negatedAndClamped == -0.0) { negatedAndClamped = 0.0;}
			Elt.SetAttributeValue("la", negatedAndClamped);
		}
    }
    public PinJoint() : this(EditorDefault) {}
    public PinJoint(Entity first, Entity second) : this()
    {
        First = first;
        Second = second;
    }
    public PinJoint(Entity first) : this()
    {
        First = first;
    }
    internal PinJoint(string xml) : this (StrToXElement(xml)) {}
    override protected void SetParams(XElement e, Func<string?, Entity?> reverseJointMapper)
    {
        base.SetParams(e, reverseJointMapper);
        Torque = GetDoubleOrNull(e, "tq");
        Speed = GetDoubleOrNull(e, "sp");
		CollideConnected = GetBoolOrNull(e, "c");
    }
    
    internal PinJoint(XElement e, Func<string?, Entity?> reverseJointMapper = default!) : base(e)
    {
        if (isNotJointed(e))
        {
            reverseJointMapper = (any) => null;
        }
		SetParams(e, reverseJointMapper);
    }
}
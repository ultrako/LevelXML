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
    public override double? UpperAngle
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
    public override double? LowerAngle
    {
        get
        {
			return GetDoubleOrNull("la");
		}
		set
		{
            double val = Math.Abs(value ?? 90);
			Elt.SetAttributeValue("la", Math.Clamp(-val, -180, 0));
		}
    }
    public PinJoint() : this(EditorDefault) {}
    public PinJoint(Entity first, Entity second) : this(EditorDefault)
    {
        First = first;
        Second = second;
    }
    public PinJoint(Entity first) : this(EditorDefault)
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
    
    internal PinJoint(XElement e, Func<string?, Entity?> reverseJointMapper = default!)
    {
        if (e.Name.ToString() != "j" || GetDoubleOrNull(e, "t") != 0)
		{
			//Console.WriteLine($"Name was {elt.Name.ToString()}, and type number was {GetDoubleOrNull(e, "t")}");
			throw new Exception("Did not give a pin joint to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
        if (isNotJointed(e))
        {
            reverseJointMapper = (any) => null;
        }
		SetParams(e, reverseJointMapper);
    }
}
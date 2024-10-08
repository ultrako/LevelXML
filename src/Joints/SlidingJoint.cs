namespace LevelXML;
using System.Xml.Linq;

/// <summary>
/// A pin joint is a kind of joint that slides on an axis
/// </summary>
public class SlidingJoint : Joint
{
    public static string EditorDefault = 
	@"<j t=""1"" x=""0"" y=""0"" b1=""-1"" b2=""-1"" a=""90"" l=""f"" ul=""100"" ll=""-100"" m=""f"" fo=""50"" sp=""3"" c=""f"" />";
    internal override uint Type => 1;

    /// <summary>
    /// The amount of force the joint applies in order to reach full speed.
    /// </summary>
    public double Force
    {
        get { return GetDoubleOrNull("fo") ?? 50; }
        set { SetDouble("fo", Math.Clamp(value, double.NegativeInfinity, 100000)); }
    }

    /// <summary>
    ///  The maximum speed at which this joint will slide.
    /// </summary>
    public override double Speed
    {
        get { return GetDoubleOrNull("sp") ?? 3; }
        set { SetDouble("sp", Math.Clamp(value, -50, 50)); }
    }

    /// <summary>
    ///  This is the highest position that the joint can reach.
    /// </summary>
    public override double UpperLimit
    {
        get { return GetDoubleOrNull("ul") ?? 100; }
		set { SetDouble("ul", Math.Clamp(value, 0, 8000)); }
    }

    /// <summary>
    ///  This is the lowest position that the joint can reach.
    /// </summary>
    public override double LowerLimit
    {
        get { return GetDoubleOrNull("ll") ?? -100; }
		set { Elt.SetAttributeValue("ll", Math.Clamp(value, -8000, 0)); }
    }

    /// <summary>
    /// This is the angle (in degrees) of the axis of movement between the bodies of this joint.
    /// </summary>
    public double Angle
    {
        get { return GetDoubleOrNull("a") ?? double.NaN; }
        set
        {
            SetDouble("a", value);
        }
    }

    /// <summary>
    ///  This constructor makes a joint that isn't pinned to anything.
    ///  Make sure to set First and Second afterwards.
    /// </summary>
    public SlidingJoint() : this(EditorDefault) {}

    /// <summary>
    /// This constructor takes two Entities to be attached to
    /// </summary>
    /// <param name="first"> The first Entity that the slidingjoint is attached to. </param>
    /// <param name="second"> The second Entity that the slidingjoint is attached to. </param>
    public SlidingJoint(Entity first, Entity second) : this()
    {
        First = first;
        Second = second;
    }

    /// <summary>
    /// This constructor takes one Entity to be attached to.
    /// By default this would joint that Entity to the background
    /// </summary>
    /// <param name="first"> The entity to be attached to. </param>
    public SlidingJoint(Entity first) : this()
    {
        First = first;
    }

    internal SlidingJoint(string xml) : this (StrToXElement(xml)) {}
    
    override protected void SetParams(XElement e, Func<string?, Entity?> reverseJointMapper)
    {
        base.SetParams(e, reverseJointMapper);
        Force = GetDoubleOrNull(e, "fo") ?? 50;
        Angle = GetDoubleOrNull(e, "a") ?? double.NaN;
        Speed = GetDoubleOrNull(e, "sp") ?? 3;
		CollideConnected = GetBoolOrNull(e, "c") ?? false;
    }
    
    internal SlidingJoint(XElement e, Func<string?, Entity?> reverseJointMapper = default!) : base(e)
    {
        if (IsNotJointed(e))
        {
            reverseJointMapper = (any) => null;
        }
		SetParams(e, reverseJointMapper);
    }
}
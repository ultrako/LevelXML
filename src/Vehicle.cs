using System.Xml.Linq;

namespace LevelXML;

public class Vehicle : Group
{
    new public const string EditorDefault =
    @"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" v=""t"" sb=""0"" sh=""0"" ct=""0"" a=""1"" l=""0"" cp=""0"" lo=""f"" />";

    /// <summary>
    /// This value determines what happens when spacebar is pressed
    /// </summary>
    public VehicleAction SpacebarAction
    {
        get { return (VehicleAction)GetDouble("sb"); }
        set { Elt.SetAttributeValue("sb", value); }
    }

    /// <summary>
    /// This value determines what happens when shift is pressed
    /// </summary>
    public VehicleAction ShiftAction
    {
        get { return (VehicleAction)GetDouble("sh"); }
        set { Elt.SetAttributeValue("sh", value); }
    }

    /// <summary>
    /// This value determines what happens when control is pressed
    /// </summary>
    public VehicleAction ControlAction
    {
        get { return (VehicleAction)GetDouble("ct"); }
        set { Elt.SetAttributeValue("ct", value); }
    }

    /// <summary>
    /// Determines how quickly attached joints reach their set motor speed when pressing up or down.
    /// </summary>
    public double Acceleration
    {
        get { return GetDouble("a"); }
        set
        {
            SetDouble("a", Math.Clamp(value, 1, 10));
        }
    }

    /// <summary>
    /// Determines how much force is used when leaning left or right.
    /// </summary>
    public double LeaningStrength
    {
        get { return GetDouble("l"); }
        set
        {
            SetDouble("l", Math.Clamp(value, 0, 10));
        }
    }

    /// <summary>
    /// This determines what pose the character will take after grabbing a vehicle handle.
    /// </summary>
    public GrabbingPose GrabbingPose
    {
        get { return (GrabbingPose)GetDouble("cp"); }
        set { Elt.SetAttributeValue("cp", value); }
    }

    /// <summary>
    /// Set this if you'd like all attached joints to slow their speed to 0
    /// when not accelerating or decelerating.
    /// </summary>
    public HWBool LockJoints
    {
        get { return GetBool("lo"); }
        set { Elt.SetAttributeValue("lo", value); }
    }

    protected override void setParams(XElement e)
    {
        base.setParams(e);
        Elt.SetAttributeValue("v", "t");
        SpacebarAction = (VehicleAction?)GetDoubleOrNull(e, "sb") ?? 0;
        ShiftAction = (VehicleAction?)GetDoubleOrNull(e, "sh") ?? 0;
        ControlAction = (VehicleAction?)GetDoubleOrNull(e, "ct") ?? 0;
        Acceleration = GetDoubleOrNull(e, "a") ?? double.NaN;
        LeaningStrength = GetDoubleOrNull(e, "l") ?? double.NaN;
        GrabbingPose = (GrabbingPose?)GetDoubleOrNull(e, "cp") ?? 0;
        LockJoints = GetBoolOrNull(e, "lo") ?? false;
    }

    public Vehicle(params Entity[] content) : this(EditorDefault, content) {}

    protected Vehicle(string xml, params Entity[]? content) : this(StrToXElement(xml), content:content, vertMapper:default!) {}

    internal Vehicle(XElement e, Func<Entity, int> vertMapper, params Entity[]? content) : base(e, vertMapper, content) {}
}
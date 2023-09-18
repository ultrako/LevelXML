using System.Xml.Linq;
namespace HappyWheels;

/// <summary>
/// If a group is sleeping, this trigger action awakes it.
/// </summary>
public class AwakeGroupFromSleep : TriggerAction<Group>
{
	public AwakeGroupFromSleep()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

/// <summary>
/// This trigger action will slowly change the opacity of a group over time
/// </summary>
public class ChangeGroupOpacity : TriggerAction<Group>
{
	public static string EditorDefault =
	@"<a i=""1"" p0=""100"" p1=""1"" />";
	public double? Opacity
	{
		get { return GetDoubleOrNull("p0"); }
		// Test if this is the level editor behavior
		set { Elt.SetAttributeValue("p0", Math.Clamp(value ?? 100, 0, 100)); }
	}
	public double? Duration
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 1); }
	}
	public ChangeGroupOpacity(double Opacity, double Duration)
	{
		Elt.SetAttributeValue("i", 1);
		this.Opacity = Opacity;
		this.Duration = Duration;
	}
	public ChangeGroupOpacity() : this(EditorDefault) {}
	public ChangeGroupOpacity(string xml) : this(StrToXElement(xml)) {}
	internal ChangeGroupOpacity(XElement e)
	{
		Elt.SetAttributeValue("i", 1);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}

/// <summary>
/// This action turns a group into a fixed object (it will not move)
/// </summary>
public class FixGroup : TriggerAction<Group>
{
	public FixGroup()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

/// <summary>
/// This action turns the group into a non fixed object (it can move)
/// </summary>
public class NonfixGroup : TriggerAction<Group>
{
	public NonfixGroup()
	{
		Elt.SetAttributeValue("i", 3);
	}
}

/// <summary>
///  This action applies an X, Y, and Spin force to a group.
/// </summary>
public class ImpulseGroup : TriggerAction<Group>
{
	public static string EditorDefault =
	@"<a i=""4"" p0=""10"" p1=""-10"" p2=""0""/>";
	public double? X
	{
		get { return GetDoubleOrNull("p0"); }
		// Yes, this is the actual happy wheels behavior
		set { SetDouble("p0", value ?? 10);}
	}
	public double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? -10);}
	}
	public double? Spin
	{
		get { return GetDoubleOrNull("p2"); }
		set { SetDouble("p2", value ?? 0);}
	}
	public ImpulseGroup() : this(EditorDefault) {}
	public ImpulseGroup(double x, double y, double spin)
	{
		Elt.SetAttributeValue("i", 4);
		X = x;
		Y = y;
		Spin = spin;
	}
	public ImpulseGroup(string xml) : this(StrToXElement(xml)) {}
	internal ImpulseGroup(XElement e)
	{
		Elt.SetAttributeValue("i", 4);
		X = GetDoubleOrNull(e, "p0");
		Y = GetDoubleOrNull(e, "p1");
		Spin = GetDoubleOrNull(e, "p2");
	}     
}

/// <summary>
/// This action freezes the group in place and makes it non interactive
/// </summary>
public class DeleteShapeGroup : TriggerAction<Group>
{
	public DeleteShapeGroup()
	{
		Elt.SetAttributeValue("i", 5);
	}
}

/// <summary>
/// This action deletes the group
/// </summary>
public class DeleteSelfGroup : TriggerAction<Group>
{
	public DeleteSelfGroup()
	{
		Elt.SetAttributeValue("i", 6);
	}
}

/// <summary>
/// This action changes the group's collision
/// </summary>

public class ChangeGroupCollision : TriggerAction<Group>
{
	public static string EditorDefault =
	@"<a i=""7"" p0=""1""/>";

	public Collision Collision
	{
		get { return (Collision)GetDoubleOrNull(Elt, "p0")!;}
		set { Elt.SetAttributeValue("p0", value);}
	}
	public ChangeGroupCollision(Collision collision)
	{
		Elt.SetAttributeValue("i", 7);
		Elt.SetAttributeValue("p0", collision);
	}
	public ChangeGroupCollision() : this(StrToXElement(EditorDefault)) {}
	internal ChangeGroupCollision(XElement e) {
		Elt.SetAttributeValue("i", 7);
		Collision = (Collision)(GetDoubleOrNull(e, "p0") ?? 1);
	}
}
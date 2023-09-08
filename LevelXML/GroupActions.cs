using System.Xml.Linq;
namespace HappyWheels;
public class AwakeGroupFromSleep : TriggerAction<Group>
{
	public AwakeGroupFromSleep()
	{
		Elt.SetAttributeValue("i", 0);
	}
}
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
		set { Elt.SetAttributeValue("p1", value); }
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

public class FixGroup : TriggerAction<Group>
{
	public FixGroup()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

public class NonfixGroup : TriggerAction<Group>
{
	public NonfixGroup()
	{
		Elt.SetAttributeValue("i", 3);
	}
}

public class ImpulseGroup : TriggerAction<Group>
{
	public static string EditorDefault =
	@"<a i=""4"" p0=""10"" p1=""-10"" p2=""0""/>";
	public double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set { Elt.SetAttributeValue("p0", value);}
	}
	public double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set { Elt.SetAttributeValue("p1", value);}
	}
	public double? Spin
	{
		get { return GetDoubleOrNull("p2"); }
		set { Elt.SetAttributeValue("p2", value);}
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

public class DeleteShapeGroup : TriggerAction<Group>
{
	public DeleteShapeGroup()
	{
		Elt.SetAttributeValue("i", 5);
	}
}

public class DeleteSelfGroup : TriggerAction<Group>
{
	public DeleteSelfGroup()
	{
		Elt.SetAttributeValue("i", 6);
	}
}

public class ChangeGroupCollision : TriggerAction<Shape>
{
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
	internal ChangeGroupCollision(XElement e) {
		Collision? collision = (Collision?)GetDoubleOrNull(e, "p0");
		if (collision is null)
		{
			throw new LevelXMLException("No collision type on change group collision trigger action!");
		}
		else
		{
			Elt.SetAttributeValue("p0", collision);
		}
		Elt.SetAttributeValue("i", 7);
	}
}
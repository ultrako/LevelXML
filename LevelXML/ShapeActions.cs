using System.Xml.Linq;

namespace HappyWheels;

///<summary>
/// If the object is sleeping, this awakes it from sleep.
///</summary>
public class AwakeShapeFromSleep : TriggerAction<Shape>
{
	public AwakeShapeFromSleep()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

public class FixShape : TriggerAction<Shape>
{
	public FixShape()
	{
		Elt.SetAttributeValue("i", 1);
	}
}

public class NonfixShape : TriggerAction<Shape>
{
	public NonfixShape()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

// This is a lot of repeated code
// I'd like this to be ChangeOpacity<Shape> extending TriggerAction<Shape>, but I can't do that.
public class ChangeShapeOpacity : TriggerAction<Shape>
{
	public static string EditorDefault =
	@"<a i=""3"" p0=""100"" p1=""1"" />";
	public double? Opacity
	{
		get { return GetDoubleOrNull("p0"); }
		// Yes, no clamps on this value, weird behavior ensues.
		set { Elt.SetAttributeValue("p0", value); }
	}
	public double? Duration
	{
		get { return GetDoubleOrNull("p1"); }
		set { Elt.SetAttributeValue("p1", value); }
	}
	public ChangeShapeOpacity(double Opacity, double Duration)
	{
		Elt.SetAttributeValue("i", 3);
		this.Opacity = Opacity;
		this.Duration = Duration;
	}
	public ChangeShapeOpacity() : this(EditorDefault) {}
	public ChangeShapeOpacity(string xml) : this(StrToXElement(xml)) {}
	internal ChangeShapeOpacity(XElement e)
	{
		Elt.SetAttributeValue("i", 3);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}

public class ImpulseShape : TriggerAction<Shape>
{
	public const string EditorDefault =
	@"<a i=""4"" p0=""10"" p1=""-10"" p2=""0""/>";
	public double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set { SetDouble("p0", value ?? 0);}
	}
	public double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 0); }
	}
	public double? Spin
	{
		get { return GetDoubleOrNull("p2"); }
		set { SetDouble("p2", value ?? 0);}
	}
	public ImpulseShape() : this(EditorDefault) {}
	public ImpulseShape(double x, double y, double spin)
	{
		Elt.SetAttributeValue("i", 4);
		X = x;
		Y = y;
		Spin = spin;
	}
	public ImpulseShape(string xml) : this(StrToXElement(xml)) {}
	internal ImpulseShape(XElement e)
	{
		Elt.SetAttributeValue("i", 4);
		X = GetDoubleOrNull(e, "p0");
		Y = GetDoubleOrNull(e, "p1");
		Spin = GetDoubleOrNull(e, "p2");
	}     
}

public class DeleteShape : TriggerAction<Shape>
{
	public DeleteShape()
	{
		Elt.SetAttributeValue("i", 5);
	}
}

public class DeleteSelfShape : TriggerAction<Shape>
{
	public DeleteSelfShape()
	{
		Elt.SetAttributeValue("i", 6);
	}
}

public class ChangeShapeCollision : TriggerAction<Shape>
{
	public Collision Collision
	{
		get { return (Collision)GetDoubleOrNull(Elt, "p0")!;}
		set { Elt.SetAttributeValue("p0", value);}
	}
	public ChangeShapeCollision(Collision collision)
	{
		Elt.SetAttributeValue("i", 7);
		Elt.SetAttributeValue("p0", collision);
	}
	public ChangeShapeCollision(string xml) : this(StrToXElement(xml)) {}
	internal ChangeShapeCollision(XElement e) {
		Collision? collision = (Collision?)GetDoubleOrNull(e, "p0");
		if (collision is null)
		{
			throw new LevelXMLException("No collision type on change shape collision trigger action!");
		}
		else
		{
			Elt.SetAttributeValue("p0", collision);
		}
		Elt.SetAttributeValue("i", 7);
	}
}

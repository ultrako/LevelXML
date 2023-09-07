namespace HappyWheels;

using System.ComponentModel;
using System.Xml.Linq;

///<summary>
/// If the object is sleeping, this awakes it from sleep.
///</summary>
public class AwakeFromSleep : TriggerAction<Shape>
{
	public AwakeFromSleep()
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

public class ChangeShapeOpacity : TriggerAction<Shape>
{
	public static string EditorDefault =
	@"<a i=""3"" p0=""100"" p1=""1"" />";
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
	public ChangeShapeCollision(int collision)
	{
		if (collision < 0 || collision > 7)
		{
			throw new LevelXMLException("Collision type must be between 0 and 7!");
		}
		Elt.SetAttributeValue("i", 7);
		Elt.SetAttributeValue("p0", collision);
	}
	internal ChangeShapeCollision(XElement e) {
		int? collision = (int?)GetDoubleOrNull(e, "p0");
		if (collision is null)
		{
			throw new LevelXMLException("No collision type on change shape collision trigger action!");
		}
		else if (collision < 0 || collision > 7)
		{
			throw new LevelXMLException("Collision type must be between 0 and 7!");
		}
		else
		{
			Elt.SetAttributeValue("p0", collision);
		}
		Elt.SetAttributeValue("i", 7);
	}
}

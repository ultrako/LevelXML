using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// This action turns a shape into a fixed object (it will not move)
/// </summary>
public class FixShape : TriggerAction, ITriggerAction<Shape>
{
	public FixShape()
	{
		Elt.SetAttributeValue("i", 1);
	}
}

/// <summary>
/// This action turns a shape into a non fixed object (it can move)
/// </summary>
public class NonfixShape : TriggerAction, ITriggerAction<Shape>
{
	public NonfixShape()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

/// <summary>
/// This action freezes the shape in place and makes it non interactive
/// </summary>
public class DeleteShape : TriggerAction, ITriggerAction<Shape>
{
	public DeleteShape()
	{
		Elt.SetAttributeValue("i", 5);
	}
}

/// <summary>
/// This action deletes the shape.
/// </summary>
public class DeleteSelfShape : TriggerAction, ITriggerAction<Shape>
{
	public DeleteSelfShape()
	{
		Elt.SetAttributeValue("i", 6);
	}
}

/// <summary>
/// This action changes the Shape's collision
/// </summary>
public class ChangeShapeCollision : TriggerAction, ITriggerAction<Shape>
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

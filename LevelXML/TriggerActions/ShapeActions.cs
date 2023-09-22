using System.Xml.Linq;

namespace HappyWheels;

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

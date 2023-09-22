using System.Xml.Linq;
namespace HappyWheels;

/// <summary>
/// This action freezes the group in place and makes it non interactive
/// </summary>
public class DeleteShapeGroup : TriggerAction, ITriggerAction<Group>
{
	public DeleteShapeGroup()
	{
		Elt.SetAttributeValue("i", 5);
	}
}

/// <summary>
/// This action deletes the group
/// </summary>
public class DeleteSelfGroup : TriggerAction, ITriggerAction<Group>
{
	public DeleteSelfGroup()
	{
		Elt.SetAttributeValue("i", 6);
	}
}

/// <summary>
/// This action changes the group's collision
/// </summary>

public class ChangeGroupCollision : TriggerAction, ITriggerAction<Group>
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
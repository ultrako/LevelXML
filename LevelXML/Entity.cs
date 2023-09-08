using System.Xml.Linq;
namespace HappyWheels;
/// <summary>
/// Entities are anything that actually show up when you press play in a level
/// Basically everything that can be two layers deep in levelXML (anything with coords)
/// </summary>
public abstract class Entity : LevelXMLTag
{
	// For entities, there isn't really an editor default for x and y coords,
	// as it depends on where the cursor was when you make it in the editor,
	// but for convenience we'll make it 0,0
	public abstract double? X {get; set;}
	public abstract double? Y {get; set;}
	protected Entity(XName name, params object?[] content) : base(name, content) {}
	// Entities need a FromXElement function because depth one tags need to construct them
	internal static Entity FromXElement(XElement element, Func<XElement, Entity> ReverseTargetMapper=default!, Func<string?, Entity?> ReverseJointMapper=default!)
    {
		return element.Name.ToString() switch {
			"sh" => GetDoubleOrNull(element, "t") switch 
			{
				0 => new Rectangle(element),
				1 => new Circle(element),
				3 => new Polygon(element),
				4 => new Art(element),
				_ => throw new LevelXMLException("Shape type doesn't exist!"),
			},
			"t" => new Trigger(element, ReverseTargetMapper),
			"sp" => GetDoubleOrNull(element, "t") switch 
			{
				16 => new TextBox(element),
				_ => throw new LevelXMLException("Special type doesn't exist!")
			},
			"j" => GetDoubleOrNull(element, "t") switch
			{
				0 => new PinJoint(element, ReverseJointMapper),
				_ => throw new LevelXMLException("Joint type doesn't exist!")
			},
			_ => throw new LevelXMLException("XML tag type isn't an entity!"),
		};
    }
	internal virtual void FinishConstruction() {}
}

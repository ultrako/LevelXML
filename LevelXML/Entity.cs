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
	internal static Entity FromXElement(XElement element, 
		Func<XElement, Entity> ReverseTargetMapper=default!, 
		Func<string?, Entity?> ReverseJointMapper=default!,
		Func<Entity, int> vertMapper=default!)
    {
		return element.Name.ToString() switch {
			"sh" => GetDoubleOrNull(element, "t") switch 
			{
				0 => new Rectangle(element),
				1 => new Circle(element),
				2 => new Triangle(element),
				3 => new Polygon(element, vertMapper),
				4 => new Art(element, vertMapper),
				_ => throw new LevelXMLException("Shape type doesn't exist!"),
			},
			"sp" => GetDoubleOrNull(element, "t") switch 
			{
				0 => new Van(element),
				1 => new DinnerTable(element),
				2 => new Landmine(element),
				3 => new IBeam(element),
				4 => new Log(element),
				5 => new SpringPlatform(element),
				6 => new SpikeSet(element),
				10 => new SoccerBall(element),
				16 => new TextBox(element),
				_ => throw new LevelXMLException("Special type doesn't exist!")
			},
			"g" => new Group(element, vertMapper),
			"j" => GetDoubleOrNull(element, "t") switch
			{
				0 => new PinJoint(element, ReverseJointMapper),
				1 => new SlidingJoint(element, ReverseJointMapper),
				_ => throw new LevelXMLException("Joint type doesn't exist!")
			},
			"t" => GetDoubleOrNull(element, "t") switch
			{
				1 => new ActivateTrigger(element, ReverseTargetMapper),
				2 => new SoundTrigger(element),
				3 => new VictoryTrigger(element),
				_ => throw new LevelXMLException("Trigger has an invalid action type!"),
			},
			_ => throw new LevelXMLException("XML tag type isn't an entity!"),
		};
    }
	internal virtual void FinishConstruction() {}
}

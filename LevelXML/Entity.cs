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
				7 => new WreckingBall(element),
				8 => new Fan(element),
				9 => new FinishLine(element),
				10 => new SoccerBall(element),
				11 => new Meteor(element),
				12 => new Boost(element),
				13 => new BuildingOne(element),
				14 => new BuildingTwo(element),
				15 => new Harpoon(element),
				16 => new TextBox(element),
				17 => new NonPlayerCharacter(element),
				18 => new GlassPanel(element),
				19 => new Chair(element),
				20 => new Bottle(element),
				21 => new Television(element),
				22 => new Boombox(element),
				23 => new Sign(element),
				24 => new Toilet(element),
				25 => new HomingMine(element),
				26 => new TrashCan(element),
				27 => new Rail(element),
				28 => new Jet(element),
				29 => new ArrowGun(element),
				30 => new Chain(element),
				31 => new Token(element),
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

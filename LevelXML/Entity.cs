using System.Xml.Linq;
namespace HappyWheels;
// Entities are anything that actually show up when you press play in a level
// Basically everything that can be two layers deep in levelXML (anything with coords)
public abstract class Entity : LevelXMLTag
{
	// For entities, there isn't really an editor default for x and y coords,
	// as it depends on where the cursor was when you make it in the editor,
	// but for convenience we'll make it 0,0
	public abstract double? x {get; set;}
	public abstract double? y {get; set;}
	/*
	private double _x;
	private double _y;
	public double? x 
	{
		get { return _x; } 
		set 
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This would make the entity disappear!");
			} 
			else { _x = (double)value!; }
		}
	}
	*/
	protected Entity(XName name, params object?[] content) : base(name, content) {}
	// They also all need a FromXElement function because depth one tags need to construct them
	internal static Entity FromXElement(XElement element, Func<XElement, Entity> ReverseMapper=default!)
    {
		return element.Name.ToString() switch {
			"sh" => GetDoubleOrNull(element, "t") switch {
				0 => new Rectangle(element),
				_ => throw new Exception("Shape type doesn't exist!"),
			},
			"t" => new Trigger(element, ReverseMapper),
			_ => throw new Exception("XML tag type isn't an entity!"),
		};
    }
	internal virtual void finishConstruction() {}
}

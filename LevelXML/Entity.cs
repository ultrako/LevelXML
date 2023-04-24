using System.Xml.Linq;
namespace HappyWheels;
// Entities are anything that actually show up when you press play in a level
// Basically everything that can be two layers deep in levelXML (anything with coords)
public abstract class Entity : LevelXMLTag
{
	// For entities, there isn't really an editor default for x and y coords,
	// as it depends on where the cursor was when you make it in the editor,
	// but for convenience we'll make it 0,0
	public abstract float? x {get; set;}
	public abstract float? y {get; set;}
	/*
	private float _x;
	private float _y;
	public float? x 
	{
		get { return _x; } 
		set 
		{
			if (value is null || float.IsNaN((float)value!))
			{
				throw new Exception("This would make the entity disappear!");
			} 
			else { _x = (float)value!; }
		}
	}
	*/
	protected Entity(XName name, params object?[] content) : base(name, content) {}
}

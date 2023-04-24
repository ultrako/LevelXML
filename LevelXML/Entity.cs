namespace HappyWheels;
// Entities are anything that actually show up when you press play in a level
// Basically everything that can be two layers deep in levelXML (anything with coords)
public abstract class Entity : LevelXMLTag
{
	// For entities, there isn't really an editor default for x and y coords,
	// as it depends on where the cursor was when you make it in the editor,
	// but for convenience we'll make it 0,0
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
	public float? y
	{
		get { return _y; } 
		set 
		{
			if (value is null || float.IsNaN((float)value!))
			{
				throw new Exception("This would make the entity disappear!");
			} 
			else { _y = (float)value!; }
		}
	}
}

namespace HappyWheels;
public class Target : LevelXMLTag
{
	public Entity Targeted {get; set;}
	// DONT USE ACTION HERE
	// I just wanted to go to bed for tonight so it's called
	// Action right now, but make sure it's my own happy wheels trigger
	// action type later on.
	public Target(Entity Targeted, params Action?[] actions) : 
	base(Targeted.Name.ToString(), actions) 
	{
		this.Targeted = Targeted;
	}
}

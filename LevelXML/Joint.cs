namespace HappyWheels;
///<summary>
/// A joint connects two non fixed entities together, 
/// specifically shapes, groups, and certain specials.
///</summary>
public abstract class Joint : Entity
{
	protected Joint(params object?[] contents) : base("j", contents) {}
}

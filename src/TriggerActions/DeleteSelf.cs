namespace LevelXML;

public abstract class DeleteSelf : TriggerAction
{
    internal abstract uint Type { get; }
	public DeleteSelf()
	{
		Elt.SetAttributeValue("i", Type);
	}
}


/// <summary>
/// This action deletes the Entity
/// </summary>
public class DeleteSelf<T> : DeleteSelf, ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 6,
        nameof(Group) => 6,
        _ => throw new LevelInvalidException($"Cannot have a Delete Shapes action on a {typeof(T).Name}!", this),
    };
}
namespace LevelXML;

public abstract class DeleteShapes : TriggerAction
{
    internal abstract uint Type { get; }
	public DeleteShapes()
	{
		Elt.SetAttributeValue("i", Type);
	}
}

/// <summary>
/// This action freezes the Entity in place and makes it non interactive
/// </summary>
public class DeleteShapes<T> : DeleteShapes, ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 5,
        nameof(Group) => 5,
        _ => throw new LevelInvalidException($"Cannot have a Delete Shapes action on a {typeof(T).Name}!", this),
    };
}
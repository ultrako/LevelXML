namespace HappyWheels;

/// <summary>
/// This action turns an entity into a fixed object (it will not move)
/// </summary>
public abstract class Fix : TriggerAction
{
    internal abstract uint Type { get; }
	internal Fix()
	{
		Elt.SetAttributeValue("i", Type);
	}
}

public class Fix<T> : Fix , ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 1,
        nameof(Group) => 2,
        _ => throw new LevelXMLException($"You cannot have a trigger action fixing a {typeof(T).Name}!"),
    };
}
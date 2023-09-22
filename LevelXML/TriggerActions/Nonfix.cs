namespace HappyWheels;

/// <summary>
/// If the entity is sleeping, this awakes it from sleep.
/// </summary>
public abstract class Nonfix : TriggerAction
{
    internal abstract uint Type { get; }
	internal Nonfix()
	{
		Elt.SetAttributeValue("i", Type);
	}
}

public class Nonfix<T> : Nonfix , ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 2,
        nameof(Group) => 3,
        _ => throw new LevelXMLException($"You cannot have a trigger action fixing a {typeof(T).Name}!"),
    };
}
namespace HappyWheels;

public abstract class Nonfix : TriggerAction
{
    internal abstract uint Type { get; }
	internal Nonfix()
	{
		Elt.SetAttributeValue("i", Type);
	}
}

/// <summary>
/// This action makes an entity nonfixed (it will be able to move)
/// </summary>
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
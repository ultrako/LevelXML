namespace LevelXML;

public abstract class Nonfix : TriggerAction
{
    internal abstract uint Type { get; }
	internal Nonfix()
	{
		Elt.SetAttributeValue("i", Type);
	}
}

/// <summary>
/// This action makes an Entity nonfixed (it will be able to move)
/// </summary>
public class Nonfix<T> : Nonfix , ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 2,
        nameof(Group) => 4,
        _ => throw new LevelInvalidException($"You cannot have a trigger action nonfixing a {typeof(T).Name}!", this),
    };
}
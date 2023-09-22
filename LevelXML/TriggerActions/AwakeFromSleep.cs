using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// If the special is sleeping, this awakes it from sleep.
/// </summary>
public abstract class AwakeFromSleep : TriggerAction
{
    internal abstract uint Type { get; }

    protected AwakeFromSleep()
	{
		SetDouble("i", Type);
	}
}

public class  AwakeFromSleep<T> : AwakeFromSleep, ITriggerAction<T>
{
    internal override uint Type => 
		typeof(T).Name switch
		{
			nameof(Shape) => 0,
            nameof(SimpleSpecial) => 0,
			nameof(Group) => 0,
			nameof(NonPlayerCharacter) => 0,
			nameof(GlassPanel) => 1,
			_ => throw new LevelXMLException($"You cannot have a trigger action awaking a {typeof(T).Name}!"),
		};
}
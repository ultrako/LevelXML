using System.Xml.Linq;

namespace LevelXML;

public abstract class AwakeFromSleep : TriggerAction
{
    internal abstract uint Type { get; }

    protected AwakeFromSleep()
	{
		SetDouble("i", Type);
	}
}

/// <summary>
/// If the Entity is sleeping, this awakes it from sleep.
/// </summary>
public class AwakeFromSleep<T> : AwakeFromSleep, ITriggerAction<T>
{
    internal override uint Type => 
		typeof(T).Name switch
		{
			nameof(Shape) => 0,
			nameof(Group) => 0,
			nameof(NonPlayerCharacter) => 0,
			nameof(Rail) => 0,
			nameof(SpikeSet) => 0,
			nameof(Television) => 0,
			nameof(Toilet) => 0,
			nameof(TrashCan) => 0,
			nameof(Van) => 0,
			nameof(IBeam) => 0,
			nameof(BladeWeapon) => 0,
			nameof(Chain) => 0,
			nameof(Jet) => 0,
			nameof(Chair) => 0,
			nameof(Boombox) => 0,
			nameof(DinnerTable) => 0,
			nameof(Log) => 0,
			nameof(Food) => 0,
			nameof(Bottle) => 0,
			nameof(Meteor) => 0,
			nameof(GlassPanel) => 1,
			_ => throw new LevelInvalidException($"You cannot have a trigger action awaking a {typeof(T).Name}!", this),
		};
}
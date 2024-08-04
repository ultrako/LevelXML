using System.Xml.Linq;
namespace LevelXML;

public abstract class TriggerAction : LevelXMLTag, IConvertibleToXML
{
	protected TriggerAction() : base("a") {}
	public string ToXML() { return ToXML(mapper: default!); }
}

/// <summary>
/// A TriggerAction is something that a trigger can do to an Entity,
/// like deleting a shape or disabling another trigger.
/// </summary>
public interface ITriggerAction : IConvertibleToXML {}

public interface ITriggerAction<in T> : ITriggerAction
{
	
	internal static ITriggerAction<T> FromXElement(XElement element)
	{
		if (element.Name.ToString() != "a")
		{
			throw new LevelXMLException("Did not give a trigger action!");
		}
		double? ActionType = LevelXMLTag.GetDoubleOrNull(element, "i");
		// Why am I switching on nameof(T) ?
		// Because C# doesn't support template specialization.
		return typeof(T).Name switch {
			nameof(Shape) => ActionType switch 
			{
				0 => (new AwakeFromSleep<Shape>() as ITriggerAction<T>)!,
				1 => (new Fix<Shape>() as ITriggerAction<T>)!,
				2 => (new Nonfix<Shape>() as ITriggerAction<T>)!,
				3 => (new ChangeOpacity<Shape>(element) as ITriggerAction<T>)!,
				4 => (new Impulse<Shape>(element) as ITriggerAction<T>)!,
				5 => (new DeleteShapes<Shape>() as ITriggerAction<T>)!,
				6 => (new DeleteSelf<Shape>() as ITriggerAction<T>)!,
				7 => (new ChangeCollision<Shape>(element) as ITriggerAction<T>)!,
				_ => (new AwakeFromSleep<Shape>() as ITriggerAction<T>)!,
			},
			nameof(Group) => ActionType switch
			{
				0 => (new AwakeFromSleep<Group>() as ITriggerAction<T>)!,
				1 => (new ChangeOpacity<Group>(element) as ITriggerAction<T>)!,
				2 => (new Impulse<Group>(element) as ITriggerAction<T>)!,
				3 => (new Fix<Group>() as ITriggerAction<T>)!,
				4 => (new Nonfix<Group>() as ITriggerAction<T>)!,
				5 => (new DeleteShapes<Group>() as ITriggerAction<T>)!,
				6 => (new DeleteSelf<Group>() as ITriggerAction<T>)!,
				7 => (new ChangeCollision<Group>(element) as ITriggerAction<T>)!,
				_ => (new AwakeFromSleep<Group>() as ITriggerAction<T>)!,
			},
			nameof(Joint) => ActionType switch
			{
				0 => (new DisableMotor() as ITriggerAction<T>)!,
				1 => (new ChangeMotorSpeed(element) as ITriggerAction<T>)!,
				2 => (new DeleteSelfJoint() as ITriggerAction<T>)!,
				3 => (new DisableLimits() as ITriggerAction<T>)!,
				4 => (new ChangeLimits(element) as ITriggerAction<T>)!,
				_ => (new DisableMotor() as ITriggerAction<T>)!,
			},
			nameof(Trigger) => ActionType switch 
			{
				0 => (new Activate() as ITriggerAction<T>)!,
				1 => (new Disable() as ITriggerAction<T>)!,
				2 => (new Enable() as ITriggerAction<T>)!,
				_ => (new Activate() as ITriggerAction<T>)!,
			},
			nameof(Harpoon) => ActionType switch
			{
				0 => (new FireHarpoon() as ITriggerAction<T>)!,
				1 => (new DeactivateHarpoon() as ITriggerAction<T>)!,
				2 => (new ActivateHarpoon() as ITriggerAction<T>)!,
				_ => (new FireHarpoon() as ITriggerAction<T>)!,
			},
			nameof(Rail) or 
			nameof(SpikeSet) or
			nameof(Television) or
			nameof(Toilet) or
			nameof(TrashCan) or
			nameof(Van) or
			nameof(IBeam) or
			nameof(BladeWeapon) or
			nameof(Chain) or
			nameof(Log) or
			nameof(Food) or
			nameof(Bottle) or
			nameof(Meteor)
			=> ActionType switch
			{
				0 => new AwakeFromSleep<T>(),
				1 => new Impulse<T>(),
				_ => new AwakeFromSleep<T>()
			},
			nameof(TextBox) => ActionType switch
			{
				0 => (new ChangeOpacity<TextBox>(element) as ITriggerAction<T>)!,
				1 => (new Slide(element) as ITriggerAction<T>)!,
				_ => (new ChangeOpacity<TextBox>(element) as ITriggerAction<T>)!,
			},
			nameof(NonPlayerCharacter) => ActionType switch
			{
				0 => (new AwakeFromSleep<NonPlayerCharacter>() as ITriggerAction<T>)!,
				1 => (new Impulse<NonPlayerCharacter>(element) as ITriggerAction<T>)!,
				2 => (new HoldPose() as ITriggerAction<T>)!,
				3 => (new ReleasePose() as ITriggerAction<T>)!,
				_ => (new AwakeFromSleep<NonPlayerCharacter>() as ITriggerAction<T>)!,
			},
			nameof(GlassPanel) => ActionType switch
			{
				0 => (new Shatter() as ITriggerAction<T>)!,
				1 => (new AwakeFromSleep<GlassPanel>() as ITriggerAction<T>)!,
				2 => (new Impulse<GlassPanel>(element) as ITriggerAction<T>)!,
				_ => (new Shatter() as ITriggerAction<T>)!,
			},
			_ => throw new LevelXMLException($"Entity type {typeof(T).Name} cannot have trigger actions!"),
		};
	}
}

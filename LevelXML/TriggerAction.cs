using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// A trigger action is something that a trigger can do to an Entity,
/// like deleting a shape or disabling another trigger.
///</summary>
public abstract class TriggerAction : LevelXMLTag, IConvertableToXML
{
	protected TriggerAction() : base("a") {}
	public string ToXML() { return ToXML(mapper: default!); }
}

public abstract class TriggerAction<T> : TriggerAction where T : Entity
{
	protected TriggerAction() : base() {}
	///<summary>
	/// You can make a trigger action from a string,
	/// just that the same xml can make different actions,
	/// depending on what kind of Entity that action is supposed to point to.
	///</summary>
	public static TriggerAction<T> FromXML(string xml)
	{
		return FromXElement(StrToXElement(xml));
	}
	internal static TriggerAction<T> FromXElement(XElement element)
	{
		if (element.Name.ToString() != "a")
		{
			throw new ArgumentException("Did not give a trigger action!");
		}
		double? ActionType = GetDoubleOrNull(element, "i");
		// Why am I switching on nameof(T) ?
		// Because C# doesn't support template specialization.
		return typeof(T).Name switch {
			nameof(Shape) => ActionType switch {
				0 => (new AwakeShapeFromSleep() as TriggerAction<T>)!,
				1 => (new FixShape() as TriggerAction<T>)!,
				2 => (new NonfixShape() as TriggerAction<T>)!,
				3 => (new ChangeShapeOpacity(element) as TriggerAction<T>)!,
				4 => (new ImpulseShape(element) as TriggerAction<T>)!,
				5 => (new DeleteShape() as TriggerAction<T>)!,
				6 => (new DeleteSelfShape() as TriggerAction<T>)!,
				7 => (new ChangeShapeCollision(element) as TriggerAction<T>)!,
				_ => throw new Exception("Invalid id for an action targeting a shape!"),
			},
			nameof(Group) => ActionType switch
			{
				0 => (new AwakeGroupFromSleep() as TriggerAction<T>)!,
				1 => (new ChangeGroupOpacity(element) as TriggerAction<T>)!,
				2 => (new ImpulseGroup(element) as TriggerAction<T>)!,
				3 => (new FixGroup() as TriggerAction<T>)!,
				4 => (new NonfixGroup() as TriggerAction<T>)!,
				5 => (new DeleteShapeGroup() as TriggerAction<T>)!,
				6 => (new DeleteSelfGroup() as TriggerAction<T>)!,
				7 => (new ChangeGroupCollision(element) as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a group!"),
			},
			nameof(Joint) => ActionType switch
			{
				0 => (new DisableMotor() as TriggerAction<T>)!,
				1 => (new ChangeMotorSpeed(element) as TriggerAction<T>)!,
				2 => (new DeleteSelfJoint() as TriggerAction<T>)!,
				3 => (new DisableLimits() as TriggerAction<T>)!,
				4 => (new ChangeLimits(element) as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a joint!"),
			},
			nameof(Trigger) => ActionType switch {
				0 => (new Activate() as TriggerAction<T>)!,
				1 => (new Disable() as TriggerAction<T>)!,
				2 => (new Enable() as TriggerAction<T>)!,
				_ => throw new Exception("Invalid id for an action targeting a trigger!"),
			},
			_ => throw new Exception($"Entity type {typeof(T).Name} not supported!"),
		};
	}
}

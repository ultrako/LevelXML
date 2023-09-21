using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// A trigger action is something that a trigger can do to an Entity,
/// like deleting a shape or disabling another trigger.
///</summary>
public abstract class TriggerAction : LevelXMLTag, IConvertibleToXML
{
	protected TriggerAction() : base("a") {}
	public string ToXML() { return ToXML(mapper: default!); }
}

public abstract class TriggerAction<T> : TriggerAction where T : Entity
{
	protected TriggerAction() : base() {}
	internal static TriggerAction<T> FromXElement(XElement element)
	{
		if (element.Name.ToString() != "a")
		{
			throw new LevelXMLException("Did not give a trigger action!");
		}
		double? ActionType = GetDoubleOrNull(element, "i");
		// Why am I switching on nameof(T) ?
		// Because C# doesn't support template specialization.
		return typeof(T).Name switch {
			nameof(Shape) => ActionType switch 
			{
				0 => (new AwakeShapeFromSleep() as TriggerAction<T>)!,
				1 => (new FixShape() as TriggerAction<T>)!,
				2 => (new NonfixShape() as TriggerAction<T>)!,
				3 => (new ChangeShapeOpacity(element) as TriggerAction<T>)!,
				4 => (new ImpulseShape(element) as TriggerAction<T>)!,
				5 => (new DeleteShape() as TriggerAction<T>)!,
				6 => (new DeleteSelfShape() as TriggerAction<T>)!,
				7 => (new ChangeShapeCollision(element) as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a shape!"),
			},
			nameof(SimpleSpecial) => ActionType switch 
			{
				0 => (new AwakeSpecialFromSleep() as TriggerAction<T>)!,
				1 => (new ImpulseSpecial(element) as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting this special!"),
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
			nameof(Trigger) => ActionType switch 
			{
				0 => (new Activate() as TriggerAction<T>)!,
				1 => (new Disable() as TriggerAction<T>)!,
				2 => (new Enable() as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a trigger!"),
			},
			nameof(Harpoon) => ActionType switch
			{
				0 => (new FireHarpoon() as TriggerAction<T>)!,
				1 => (new DeactivateHarpoon() as TriggerAction<T>)!,
				2 => (new ActivateHarpoon() as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a harpoon!"),
			},
			nameof(TextBox) => ActionType switch
			{
				0 => (new ChangeTextBoxOpacity(element) as TriggerAction<T>)!,
				1 => (new SlideTextBox(element) as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a textbox!"),
			},
			nameof(NonPlayerCharacter) => ActionType switch
			{
				0 => (new AwakeNPCFromSleep() as TriggerAction<T>)!,
				1 => (new ImpulseNPC(element) as TriggerAction<T>)!,
				2 => (new HoldPose() as TriggerAction<T>)!,
				3 => (new ReleasePose() as TriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting an NPC!"),
			},
			_ => throw new LevelXMLException($"Entity type {typeof(T).Name} not supported!"),
		};
	}
}

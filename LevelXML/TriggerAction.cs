using System.Xml.Linq;
namespace HappyWheels;

public abstract class TriggerAction : LevelXMLTag, ITriggerAction
{
	protected TriggerAction() : base("a") {}
	public string ToXML() { return ToXML(mapper: default!); }
}

///<summary>
/// A trigger action is something that a trigger can do to an Entity,
/// like deleting a shape or disabling another trigger.
///</summary>
public interface ITriggerAction : IConvertibleToXML {}

public interface ITriggerAction<in T> : ITriggerAction where T : Entity
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
				0 => (new AwakeShapeFromSleep() as ITriggerAction<T>)!,
				1 => (new FixShape() as ITriggerAction<T>)!,
				2 => (new NonfixShape() as ITriggerAction<T>)!,
				3 => (new ChangeShapeOpacity(element) as ITriggerAction<T>)!,
				4 => (new ImpulseShape(element) as ITriggerAction<T>)!,
				5 => (new DeleteShape() as ITriggerAction<T>)!,
				6 => (new DeleteSelfShape() as ITriggerAction<T>)!,
				7 => (new ChangeShapeCollision(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a shape!"),
			},
			nameof(SimpleSpecial) => ActionType switch 
			{
				0 => (new AwakeSpecialFromSleep() as ITriggerAction<T>)!,
				1 => (new ImpulseSpecial(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting this special!"),
			},
			nameof(Group) => ActionType switch
			{
				0 => (new AwakeGroupFromSleep() as ITriggerAction<T>)!,
				1 => (new ChangeGroupOpacity(element) as ITriggerAction<T>)!,
				2 => (new ImpulseGroup(element) as ITriggerAction<T>)!,
				3 => (new FixGroup() as ITriggerAction<T>)!,
				4 => (new NonfixGroup() as ITriggerAction<T>)!,
				5 => (new DeleteShapeGroup() as ITriggerAction<T>)!,
				6 => (new DeleteSelfGroup() as ITriggerAction<T>)!,
				7 => (new ChangeGroupCollision(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a group!"),
			},
			nameof(Joint) => ActionType switch
			{
				0 => (new DisableMotor() as ITriggerAction<T>)!,
				1 => (new ChangeMotorSpeed(element) as ITriggerAction<T>)!,
				2 => (new DeleteSelfJoint() as ITriggerAction<T>)!,
				3 => (new DisableLimits() as ITriggerAction<T>)!,
				4 => (new ChangeLimits(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a joint!"),
			},
			nameof(Trigger) => ActionType switch 
			{
				0 => (new Activate() as ITriggerAction<T>)!,
				1 => (new Disable() as ITriggerAction<T>)!,
				2 => (new Enable() as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a trigger!"),
			},
			nameof(Harpoon) => ActionType switch
			{
				0 => (new FireHarpoon() as ITriggerAction<T>)!,
				1 => (new DeactivateHarpoon() as ITriggerAction<T>)!,
				2 => (new ActivateHarpoon() as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a harpoon!"),
			},
			nameof(TextBox) => ActionType switch
			{
				0 => (new ChangeTextBoxOpacity(element) as ITriggerAction<T>)!,
				1 => (new SlideTextBox(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a textbox!"),
			},
			nameof(NonPlayerCharacter) => ActionType switch
			{
				0 => (new AwakeNPCFromSleep() as ITriggerAction<T>)!,
				1 => (new ImpulseNPC(element) as ITriggerAction<T>)!,
				2 => (new HoldPose() as ITriggerAction<T>)!,
				3 => (new ReleasePose() as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting an NPC!"),
			},
			nameof(GlassPanel) => ActionType switch
			{
				0 => (new Shatter() as ITriggerAction<T>)!,
				1 => (new AwakeGlassPanelFromSleep() as ITriggerAction<T>)!,
				2 => (new ImpulseGlassPanel(element) as ITriggerAction<T>)!,
				_ => throw new LevelXMLException("Invalid id for an action targeting a glass panel!"),
			},
			_ => throw new LevelXMLException($"Entity type {typeof(T).Name} cannot have trigger actions!"),
		};
	}
}

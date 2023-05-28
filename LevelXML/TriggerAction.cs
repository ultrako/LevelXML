using System.Xml.Linq;
namespace HappyWheels;
public abstract class TriggerAction : LevelXMLTag
{
	protected TriggerAction() : base("a") {}
}
public abstract class TriggerAction<T> : TriggerAction where T : Entity
{
	protected TriggerAction() : base() {}
	internal static TriggerAction<T> FromXElement(XElement element)
	{
		if (element.Name.ToString() != "a")
		{
			throw new Exception("Did not give a trigger action!");
		}
		double? ActionType = GetDoubleOrNull(element, "i");
		// Why am I switching on nameof(T) ?
		// Because C# doesn't support template specialization.
		return typeof(T).Name switch {
			nameof(Shape) => ActionType switch {
				0 => (new AwakeFromSleep() as TriggerAction<T>)!,
				3 => (new ChangeOpacity(element) as TriggerAction<T>)!,
				_ => throw new Exception("Invalid id for an action targeting a shape!"),
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

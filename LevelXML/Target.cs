using System.Xml.Linq;

namespace HappyWheels;

// Weird rules about covariance make it so I can't have a list of Target<T> with varying T,
// so I'll just make an abstract class Target that Target<T> derives from

public abstract class Target : LevelXMLTag 
{ 
	public Entity Targeted {get; set;} = default!;
	protected Target(Entity e) : base(e.elt.Name) { Targeted = e; }
	private Task? setTargeted;
	protected Target(XElement e, Func<XElement, Entity> ReverseMapper) : base(e.Name)
	{
		setTargeted = Task.Run( () => Targeted = ReverseMapper(e));
	}
	// This following function is only called if this object is constructed with a string
	// In LevelXML, targets are set via indexes - but we can only go from index to entity
	// in the context of an entire level.
	internal void finishConstruction() { setTargeted!.Wait(); }
	internal static Target FromXElement(XElement e, Func<XElement, Entity> ReverseMapper)
	{
		return e.Name.ToString() switch
		{
			"sh" => new Target<Shape>(e, ReverseMapper),
			"sp" => new Target<Special>(e, ReverseMapper),
			"g" => new Target<Group>(e, ReverseMapper),
			"j" => new Target<Joint>(e, ReverseMapper),
			"t" => new Target<Trigger>(e, ReverseMapper),
			_ => throw new Exception("Invalid name for a trigger target!"),
		};
	}
}

public class Target<T> : Target where T : Entity
{
	private List<TriggerAction<T>> lst;

	public void Add(TriggerAction<T> action) { lst.Add(action); }
	public void Remove(TriggerAction<T> action) { lst.Remove(action); }
	public void IndexOf(TriggerAction<T> action) { lst.IndexOf(action); }
	public TriggerAction<T> this[int index] { get { return lst[index]; } set { lst[index] = value; } }

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		elt.SetAttributeValue("i", mapper(Targeted));
		elt.RemoveNodes();
		foreach (TriggerAction<T> action in lst)
		{
			elt.Add(action.elt);
		}
	}

	public Target(T targeted, params TriggerAction<T>[] actions) : 
	base(targeted) 
	{
		if (typeof(T) == typeof(Trigger) && actions.Length > 1)
		{
			// The import box allows this, but it makes a really confusing double action,
			// and you can only change them together and it only activates once so it's
			// the same as just setting one action
			throw new Exception("Triggers can only have one action applied to them per source trigger!");
		}
		lst = new(actions);
	}
	// Mapper usually gives indices from Entities.
	// ReverseMapper gives Entities from XElements with a name and a index
	internal Target(XElement e, Func<XElement, Entity> ReverseMapper) :
		base(e, ReverseMapper)
	{
		TriggerAction<T>[] actions = e.Elements()
			.Select(tag => TriggerAction<T>.FromXElement(tag))
			.ToArray();
		lst = new(actions);
	}
}

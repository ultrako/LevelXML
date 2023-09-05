using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;

// Weird rules about covariance make it so I can't have a list of Target<T> with varying T,
// so I'll just make an abstract class Target that Target<T> derives from
///<summary>
/// A Target is an Entity and a list of actions that a trigger does to that Entity.
/// Triggers cannot interleave actions between two different entities.
///</summary>

public abstract class Target : LevelXMLTag 
{ 
	public Entity Targeted {get; set;} = default!;
	public abstract IEnumerator<TriggerAction> GetEnumerator();
	public abstract void Add(TriggerAction action);
	public abstract bool Remove(TriggerAction action);
	public abstract int IndexOf(TriggerAction action);
	public abstract TriggerAction this[int index] {get; set;}
	protected Target(Entity e) : base(e.Elt.Name) { Targeted = e; }
	private Task? setTargeted;
	protected Target(XElement e, Func<XElement, Entity> ReverseTargetMapper) : base(e.Name)
	{
		setTargeted = Task.Run( () => Targeted = ReverseTargetMapper(e));
	}
	// This following function is only called if this object is constructed with a string
	// In LevelXML, targets are set via indexes - but we can only go from index to entity
	// in the context of an entire level.
	internal void finishConstruction() { setTargeted!.Wait(); }
	internal static Target FromXElement(XElement e, Func<XElement, Entity> ReverseTargetMapper)
	{
		return e.Name.ToString() switch
		{
			"sh" => new Target<Shape>(e, ReverseTargetMapper),
			"sp" => new Target<Special>(e, ReverseTargetMapper),
			"g" => new Target<Group>(e, ReverseTargetMapper),
			"j" => new Target<Joint>(e, ReverseTargetMapper),
			"t" => new Target<Trigger>(e, ReverseTargetMapper),
			_ => throw new Exception("Invalid name for a trigger target!"),
		};
	}
}

public class Target<T> : Target where T : Entity
{
	private List<TriggerAction<T>> lst;

	public override void Add(TriggerAction action) {
		// A trigger can only do one thing to another trigger
		if (typeof(T) == typeof(Trigger))
		{
			if (lst.Count > 0)
			{
				throw new Exception("Tried to add a second action to a trigger!");
			}
		}
		if (action is TriggerAction<T> act)
		{
			lst.Add(act); 
		} else
		{
			throw new Exception("Tried to add an action of the wrong Entity type!");
		}
	}
	public override bool Remove(TriggerAction action) 
	{
		if (action is TriggerAction<T> act)
		{
			return lst.Remove(act); 
		}
		return false;
	}
	public override int IndexOf(TriggerAction action) 
	{ 
		if (action is TriggerAction<T> act)
		{
			return lst.IndexOf(act);
		}
		return -1;
	}
	public override IEnumerator<TriggerAction> GetEnumerator() { return lst.GetEnumerator(); }
	public override TriggerAction this[int index] 
	{ 
		get { return lst[index]; } 
		set 
		{
			if (value is TriggerAction<T> act)
			{
				lst[index] = act;
			} else
			{
				throw new Exception("Tried to add an action of the wrong Entity type!");
			}
		} 
	}

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.SetAttributeValue("i", mapper(Targeted));
		Elt.RemoveNodes();
		foreach (TriggerAction<T> action in lst)
		{
			Elt.Add(action.Elt);
		}
	}
	///<summary>
	/// You can construct a Target[T] out of an Entity of type T and a list of Action[T]
	///</summary>

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
	// ReverseTargetMapper gives Entities from XElements with a name and a index
	internal Target(XElement e, Func<XElement, Entity> ReverseTargetMapper) :
		base(e, ReverseTargetMapper)
	{
		TriggerAction<T>[] actions = e.Elements()
			.Select(tag => TriggerAction<T>.FromXElement(tag))
			.ToArray();
		lst = new(actions);
	}
}

using System.Xml.Linq;

namespace HappyWheels;

// Weird rules about covariance make it so I can't have a list of Target<T> with varying T,
// so I'll just make an abstract class Target that Target<T> derives from

public abstract class Target : LevelXMLTag 
{ 
	public Entity Targeted {get; set;}
	protected Target(Entity e) : base(e.elt.Name) { Targeted = e; }
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
		Targeted = targeted;
		lst = new(actions);
	}
	// I think we'll just have Level deal with turning XElement targets into Target objects
	// Otherwise, it's just "pass me the function that turns an XElement into an Entity"
	/*
	internal Target(string xml, Func<int, Entity?> mapper) : this(StrToXElement(xml), mapper) {}
	internal Target(XElement e, Func<int, Entity?> mapper) : base(e.Name)
	{
	}
	*/
}

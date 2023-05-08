using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
namespace HappyWheels;

// Also making an abstract DepthOneTag so that I can have a list of them,
// for cleaner code in Level.cs
abstract public class DepthOneTag : LevelXMLTag
{
	protected static Dictionary<Type, string> EntityToDepthOneTagName = new()
	{
		{typeof(Shape), "shapes"},
		{typeof(Special), "specials"},
		{typeof(Group), "groups"},
		{typeof(Joint), "joints"},
		{typeof(Trigger), "triggers"},
	};
	protected DepthOneTag(XName name) : base(name) {}
}

// Now, <shapes>, <groups>, etc, tags don't really do everything that Xlst.Elements do.
// They don't have any attributes, and they're basically just glorified lists.

public class DepthOneTag<T> : DepthOneTag where T : Entity
{
	private List<T> lst;
	public void Add(T entity) { lst.Add(entity); }
	public bool Remove(T entity) { return lst.Remove(entity); }
	public int IndexOf(T entity) { return lst.IndexOf(entity); }
	public T this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		elt.RemoveNodes();
		foreach (Entity entity in lst)
		{
			entity.PlaceInLevel(mapper);
			elt.Add(entity.elt);
		}
	}
	public DepthOneTag(string xml) : this(StrToXElement(xml)) {}
	DepthOneTag(XElement e) : 
		this(content: e.Elements()
			.Select(element => Entity.FromXElement(element) as T)
			.Where(item => item is not null).Select(item => item!)
			.ToArray()) 
	{}
	public DepthOneTag(params T[] content) : base (EntityToDepthOneTagName[typeof(T)]) 
	{
		lst = new(content);
	}
}

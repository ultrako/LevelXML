using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
namespace HappyWheels;

// Now, <shapes>, <groups>, etc, tags don't really do everything that Xlst.Elements do.
// They don't have any attributes, and they're basically just glorified lists.
// We'll have them inherit from LevelXMLTag but implement IList, so that we
// can print them easily, but also use them like lists.

// Just kidding I don't want to implement all of IList
// I'll just pick and choose a few methods on that interface

public class DepthOneTag<T> : LevelXMLTag where T : Entity
{
	private List<T> lst;
	public void Add(T entity) { lst.Add(entity); }
	public bool Remove(T entity) { return lst.Remove(entity); }
	public int IndexOf(T entity) { return lst.IndexOf(entity); }
	public T this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	static Dictionary<Type, string> EntityToDepthOneTagName = new()
	{
		{typeof(Shape), "shapes"},
		{typeof(Special), "specials"},
		{typeof(Group), "groups"},
		{typeof(Joint), "joints"},
		{typeof(Trigger), "triggers"},
	};
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

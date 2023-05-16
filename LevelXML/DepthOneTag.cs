using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
namespace HappyWheels;

// Also making an abstract DepthOneTag so that I can have a list of them,
// for cleaner code in Level.cs
abstract internal class DepthOneTag : LevelXMLTag
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
	// In case you only have this abstract class and at least want an Entity back
	abstract internal Entity get(int index);
	abstract internal void finishConstruction();
	public abstract int Count { get; }
}

// Now, <shapes>, <groups>, etc, tags don't really do everything that Xlst.Elements do.
// They don't have any attributes, and they're basically just glorified lists.

internal class DepthOneTag<T> : DepthOneTag, IList<T> where T : Entity
{
	private List<T> lst;
	public void Add(T entity) { lst.Add(entity); }
	public bool Remove(T entity) { return lst.Remove(entity); }
	public int IndexOf(T entity) { return lst.IndexOf(entity); }
	public void Insert(int i, T entity) { lst.Insert(i, entity); }
	public void RemoveAt(int i) { lst.RemoveAt(i); }
	public void Clear() { lst.Clear(); }
	public bool Contains(T entity) { return lst.Contains(entity); }
	public void CopyTo(T[] entities, int i) { lst.CopyTo(entities, i); }
	public override int Count { get { return lst.Count; } }
	public bool IsReadOnly { get { return false; } }
	IEnumerator<T> IEnumerable<T>.GetEnumerator() { return lst.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return lst.GetEnumerator(); }
	public T this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	internal override Entity get(int index) { return lst[index]; }
	internal override void finishConstruction() { lst.ForEach(entity => entity.finishConstruction()); }
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		elt.RemoveNodes();
		foreach (Entity entity in lst)
		{
			entity.PlaceInLevel(mapper);
			elt.Add(entity.elt);
		}
	}
	internal DepthOneTag(string xml) : this(StrToXElement(xml)) {}
	internal DepthOneTag(XElement? e, Func<XElement, Entity> ReverseMapper=default!) : 
		this(content: (e ?? new XElement("empty")).Elements()
			.Select(element => Entity.FromXElement(element, ReverseMapper) as T)
			.Where(item => item is not null).Select(item => item!)
			.ToArray()) 
	{}
	internal DepthOneTag(params T[]? content) : base (EntityToDepthOneTagName[typeof(T)]) 
	{
		if (content is not null) {
			lst = new(content);
		} else {
			lst = new();
		}
	}
}

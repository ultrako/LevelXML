using System.Xml.Linq;

namespace LevelXML;

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
	abstract internal Entity GetEntityAt(int index);
	abstract internal void FinishConstruction();
	abstract internal int Count { get;}
}

// Now, <shapes>, <groups>, etc, tags don't really do everything that Xlst.Elements do.
// They don't have any attributes, and they're basically just glorified lists.

internal class DepthOneTag<T> : DepthOneTag where T : Entity
{
	public List<T> lst;
	override internal int Count => lst.Count;
	internal override Entity GetEntityAt(int index) 
	{
		if (index >= 0 && index < lst.Count)  { return lst[index]; }
		else { throw new LevelXMLException("Trigger had a target index that isn't in the level!"); }
	}
	internal override void FinishConstruction() { lst.ForEach(entity => entity.FinishConstruction()); }
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Entity entity in lst)
		{
			entity.PlaceInLevel(mapper);
			Elt.Add(entity.Elt);
		}
	}
	internal DepthOneTag(XElement? e, Func<XElement,
		Entity> reverseTargetMapper=default!, Func<string?, 
		Entity?> reverseJointMapper=default!, 
		Func<Entity, int> vertMapper=default!,
		Func<int, Entity> reverseVertMappper=default!) : 
		this(content: (e ?? new XElement("empty")).Elements()
			.Select(element => Entity.FromXElement(element, reverseTargetMapper, reverseJointMapper, vertMapper) as T)
			.Where(item => item is not null).Select(item => item!)
			.ToArray()) 
	{}
	internal DepthOneTag(params T[] content) : base (EntityToDepthOneTagName[typeof(T)]) 
	{
		lst = new(content);
	}
}

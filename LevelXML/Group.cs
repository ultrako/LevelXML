using System.Collections;
using System.Xml.Linq;
namespace HappyWheels;
// A group contains Shapes and non interactive Specials
public abstract class Group : Entity, IList<Entity>
{
	// So there's no such thing as a "make new empty group" button in the editor,
	// but it would be useful to have an empty group with the default properties
	// you get when you select some objects and press "make group out of objects"
	public static string EditorDefault =
	@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />";
	private List<Entity> lst;
	public void Add(Entity entity) { lst.Add(entity); }
	public bool Remove(Entity entity) { return lst.Remove(entity); }
	public int IndexOf(Entity entity) { return lst.IndexOf(entity); }
	public void Insert(int i, Entity entity) { lst.Insert(i, entity); }
	public void RemoveAt(int i) { lst.RemoveAt(i); }
	public void Clear() { lst.Clear(); }
	public bool Contains(Entity entity) { return lst.Contains(entity); }
	public void CopyTo(Entity[] entities, int i) { lst.CopyTo(entities, i); }
	public bool IsReadOnly { get { return false; } }
	IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() { return lst.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return lst.GetEnumerator();}
	public int Count => lst.Count;
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		elt.RemoveNodes();
		foreach (Entity entity in lst)
		{
			entity.PlaceInLevel(mapper);
			elt.Add(entity.elt);
		}
	}
	public Entity this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	public Group(string xml) : this(StrToXElement(xml)) {}
	internal Group(XElement? e, Func<XElement, Entity> ReverseMapper=default!) : 
		this(content: (e ?? new XElement("empty")).Elements()
			.Select(element => Entity.FromXElement(element, ReverseMapper))
			.ToArray()) 
	{}
	protected Group(params Entity[]? content) : base ("g") 
	{
		if (content is not null) {
			lst = new(content);
		} else {
			lst = new();
		}
	}
}

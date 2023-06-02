
using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;

internal class Vertices : LevelXMLTag, IList<Vertex>
{
	public void Add( Vertex v ) { verts.Add(v); }
	public int IndexOf(Vertex v) { return verts.IndexOf(v); }
	public void Insert(int index, Vertex v) { verts.Insert(index, v); }
	public void RemoveAt(int index) { verts.RemoveAt(index); }
	public Vertex this[int index] { get {return verts[index]; } set { verts[index] = value; } }
	public void Clear() { verts.Clear(); }
	public bool Contains(Vertex v) { return verts.Contains(v); }
	public void CopyTo(Vertex[] array, int index) { verts.CopyTo(array, index); }
	public bool Remove(Vertex v) { return verts.Remove(v); }
	public int Count => verts.Count;
	public bool IsReadOnly => false;
	IEnumerator<Vertex> IEnumerable<Vertex>.GetEnumerator() { return verts.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return verts.GetEnumerator(); }


	public HWBool? Connected
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? true;
			Elt.SetAttributeValue("f", val);
		}
	}
	private List<Vertex> verts;
	internal void PlaceInLevel(Entity parent, Func<Entity, int> mapper)
	{
		Elt.SetAttributeValue("id", mapper(parent));
		Elt.SetAttributeValue("n", verts.Count);
		int index = 0;
		foreach (Vertex v in verts)
		{
			string coord_full = $"{v.position.X}_{v.position.Y}";
			if (v.handle0 is not null) { coord_full += $"{v.handle0.X}_{v.handle0.Y}"; }
			if (v.handle1 is not null) { coord_full += $"{v.handle1.X}_{v.handle1.Y}"; }
			Elt.SetAttributeValue($"v{index}", coord_full);
			index += 1;
		}
	}
	// Todo: constructor from levelXML tag string
	public Vertices() : base("v")
	{
		verts = new();
		Connected = true;
	}
}

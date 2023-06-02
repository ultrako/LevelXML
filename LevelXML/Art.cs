using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;
///<summary>
/// A point in a 2D plane
///</summary>
public record Point(double X, double Y);
///<summary>
/// A vertex represents a corner in an art shape or polygon
///</summary>
///<param name="position"> 
/// The coordinates of the vertex, relative to the center of the shape it is contained in
///</param>
///<param name="handle0"> The coordinates of the first bezier handle </param>
///<param name="handle1"> The coordinates of the second bezier handle </param>
public record Vertex(Point position, Point? handle0=null, Point? handle1=null);
///<summary>
/// Art shapes have no collision, and their edges are defined with a list of Vertices
///</summary>
public class Art : Shape, IList<Vertex>
{
	private Vertices verts;
	internal override uint Type => 4;
	public static string EditorDefault =
        @"<sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";
	public override double? Width
	{
		get { return GetDoubleOrNull("p2"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new Exception("This would make the art shape disappear!");
			} 
			Elt.SetAttributeValue("p2", val);
		}
	}
	public override double? Height
	{
		get { return GetDoubleOrNull("p3"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new Exception("This would make the art shape disappear!");
			} 
			Elt.SetAttributeValue("p3", val);
		}
	}
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
	IEnumerator<Vertex> IEnumerable<Vertex>.GetEnumerator() { return (verts as IEnumerable<Vertex>).GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return (verts as IEnumerable).GetEnumerator(); }
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		Elt.Add(verts.Elt);
		verts.PlaceInLevel(this, mapper);
	}
	public Art() : this(EditorDefault) {}
	public Art(string xml) : this(StrToXElement(xml)) {}
	internal Art(XElement e)
	{
		if (e.Name.ToString() != "sh" || GetDoubleOrNull(e, "t") != 4)
		{
			throw new Exception("Did not give an art shape to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
		// Replace this with the constructor that takes an XElement when you make it
		verts = new();
	}
}

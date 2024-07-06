using System.Linq;
using System.Xml.Linq;
using System.Collections;
namespace LevelXML;

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

internal class Vertices : LevelXMLTag
{
	internal List<Vertex> verts = new();
	internal bool isEmpty;
	internal int originalIndex;
	private int id;
	private Entity? parent;
	internal Func<Entity, int>? vertMapper;

	public HWBool? Connected
	{
		// Perhaps expose this in CustomShape?
		//get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? true;
			Elt.SetAttributeValue("f", val);
		}
	}

	internal void FinishConstruction() { id = vertMapper!(parent!); }

	internal void PlaceInLevel()
	{
		FinishConstruction();
		Elt.SetAttributeValue("id", id);
		int index = 0;
		foreach (Vertex v in verts)
		{
			string coord_full = $"{v.position.X}_{v.position.Y}";
			if (v.handle0 is not null) { coord_full += $"_{v.handle0.X}_{v.handle0.Y}"; }
			if (v.handle1 is not null) { coord_full += $"_{v.handle1.X}_{v.handle1.Y}"; }
			Elt.SetAttributeValue($"v{index}", coord_full);
			index += 1;
		}
		if (verts.Count > 0)
		{
			Elt.SetAttributeValue("n", verts.Count);
		}
	}
	// Todo: constructor from levelXML tag string
	internal Vertices(Entity parent) : base("v")
	{
		this.parent = parent;
		Connected = true;
	}

	internal Vertices(XElement e, Entity parent, Func<Entity, int> mapper) : base("v")
	{
		// We are setting these two to use when Level.cs has finalized making its Entity lists
		this.parent = parent;
		this.vertMapper = mapper;
		List<XAttribute> vertices = e.Attributes().Where(attr => attr.Name.ToString()[0] == 'v').ToList();
		Connected = GetBoolOrNull(e, "f");
		isEmpty = vertices.Count <= 0;
		double? id = GetDoubleOrNull(e, "id");
		if (id is not null)
		{
			originalIndex = (int)id;
		}
		else
		{
			throw new LevelXMLException("<v> tag did not have an id!");
		}
		foreach (XAttribute vertex in vertices)
		{
			string raw = vertex.Value;
			List<double> coords = raw.Split("_").Select(str => Double.Parse(str)).ToList();
			if (coords.Count < 2)
			{
				throw new LevelXMLException("Failed to process vertex or vertex had fewer than 2 coordinates!");
			}
			Point position = new(coords[0], coords[1]);
			Point? handle1 = null;
			Point? handle2 = null;
			if (coords.Count >= 4)
			{
				handle1 = new(coords[2], coords[3]);
			}
			if (coords.Count >= 6)
			{
				handle2 = new(coords[4], coords[5]);
			}
			verts.Add(new(position, handle1, handle2));
		}
		
	}
}

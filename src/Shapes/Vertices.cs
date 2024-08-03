using System.Linq;
using System.Xml.Linq;
using System.Collections;
namespace LevelXML;

///<summary>
/// A point in a 2D plane
///</summary>
public record struct Point(double X, double Y);
///<summary>
/// A vertex represents a corner in an art shape or polygon
///</summary>
///<param name="Position"> 
/// The coordinates of the vertex, relative to the center of the shape it is contained in
///</param>
///<param name="HandleOne"> The coordinates of the first bezier handle </param>
///<param name="HandleTwo"> The coordinates of the second bezier handle </param>
public record Vertex(Point Position, Point? HandleOne=null, Point? HandleTwo=null);

internal class Vertices : LevelXMLTag
{
	internal List<Vertex> verts = new();
	internal bool isEmpty;
	internal int originalIndex;
	private int id;
	// The shape tag that contains this vertices tag
	private Entity? parent;
	internal Func<Entity, int>? vertMapper;

	public HWBool? Connected
	{
		set
		{
			HWBool val = value ?? true;
			Elt.SetAttributeValue("f", val);
		}
	}

	internal void PlaceInLevel()
	{
		id = vertMapper!(parent!);
		if (id < 0)
		{
			((CustomShape)parent!).ShallowCopy();
			id = vertMapper!(parent!);
		}
		Elt.SetAttributeValue("id", id);
		int index = 0;
		foreach (Vertex v in verts)
		{
			string coord_full = $"{v.Position.X}_{v.Position.Y}";
			if (v.HandleOne is Point handleOne) { coord_full += $"_{handleOne.X}_{handleOne.Y}"; }
			if (v.HandleTwo is Point handleTwo) { coord_full += $"_{handleTwo.X}_{handleTwo.Y}"; }
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
			List<double> coords = new();
			try
			{
				coords = raw.Split("_").Select(str => Double.Parse(str)).ToList();
			} catch (FormatException) {}
			// This can cause unexpected/hard to debug behavior when you don't mean to dot-split your coordinates,
			// or have decimals in your coordinates but only have a single coordinate,
			// but Happy Wheels supports this format so I have to too.
			if (coords.Count < 2)
			{
				try
				{
					coords = raw.Split(".").Select(str => Double.Parse(str)).ToList();
				} catch (FormatException) {}
			}
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

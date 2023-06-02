
using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// Polygons and Art shapes have a list of Vertices, defined by quadratic bezier curves.
///</summary>

public class Vertices : LevelXMLTag 
{ 
	public HWBool? Connected
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? true;
			Elt.SetAttributeValue("f", val);
		}
	}
	private List<(double, double, double?, double?, double?, double?)> verts;
	///<summary>
	/// Add an (x,y) point to a list of vertices
	///</summary>
	public void Add( (double, double) coord ) 
	{ 
		verts.Add((coord.Item1, coord.Item2, null, null, null, null)); 
	}
	public void Add( (double, double, double?, double?, double?, double?) coord ) { verts.Add(coord); }
	internal void PlaceInLevel(Entity parent, Func<Entity, int> mapper)
	{
		Elt.SetAttributeValue("id", mapper(parent));
		Elt.SetAttributeValue("n", verts.Count);
		int index = 0;
		foreach ((double x, double y, double? dx0, double? dy0, double? dx1, double? dy1) in verts)
		{
			string coord_full = $"{x}_{y}";
			if (dx0 is not null) { coord_full += $"_{dx0}"; }
			if (dy0 is not null) { coord_full += $"_{dy0}"; }
			if (dx1 is not null) { coord_full += $"_{dx1}"; }
			if (dy1 is not null) { coord_full += $"_{dy1}"; }
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

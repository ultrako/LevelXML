
using System.Xml.Linq;
namespace HappyWheels;


public class Vertices : LevelXMLTag 
{ 
	public HWBool? Connected
	{
		get { return GetBoolOrNull("f"); }
		set
		{
			HWBool val = value ?? true;
			elt.SetAttributeValue("f", val);
		}
	}
	private List<(double, double, double?, double?)> verts;
	public void Add( (double, double) coord ) 
	{ 
		verts.Add((coord.Item1, coord.Item2, null, null)); 
	}
	public void Add( (double, double, double?, double?) coord ) { verts.Add(coord); }
	internal void PlaceInLevel(Entity parent, Func<Entity, int> mapper)
	{
		elt.SetAttributeValue("id", mapper(parent));
		elt.SetAttributeValue("n", verts.Count);
		int index = 0;
		foreach ((double x1, double y1, double? x2, double? y2) in verts)
		{
			string coord_full = $"{x1}_{y1}";
			if (x2 is not null) { coord_full += $"_{x2}"; }
			if (y2 is not null) { coord_full += $"_{y2}"; }
			elt.SetAttributeValue($"v{index}", coord_full);
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

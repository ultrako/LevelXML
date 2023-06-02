using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// Polygons are shapes defined by a list of points.
///</summary>
public class Polygon : Shape
{
	private Vertices verts;
	internal override uint Type => 3;
	public static string EditorDefault =
        @"<sh t=""4"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";
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
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		verts.PlaceInLevel(this, mapper);
	}
	public Polygon() : this(EditorDefault) {}
	public Polygon(string xml) : this(StrToXElement(xml)) {}
	internal Polygon(XElement e)
	{
		if (e.Name.ToString() != "sh" || GetDoubleOrNull(e, "t") != 3)
		{
			//Console.WriteLine($"Name was {elt.Name.ToString()}, and type number was {GetDoubleOrNull(e, "t")}");
			throw new Exception("Did not give a polygon to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
		// Replace this with the constructor that takes an XElement when you make it
		verts = new();
	}
}

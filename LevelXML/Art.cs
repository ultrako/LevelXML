using System.Xml.Linq;
namespace HappyWheels;

public class Art : Shape
{
	public Vertices Vertices { get; private set; }
	public override uint Type => 4;
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
			elt.SetAttributeValue("p2", val);
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
			elt.SetAttributeValue("p3", val);
		}
	}
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		elt.RemoveNodes();
		elt.Add(Vertices.elt);
		Vertices.PlaceInLevel(this, mapper);
	}
	public Art() : this(EditorDefault) {}
	public Art(string xml) : this(StrToXElement(xml)) {}
	internal Art(XElement e)
	{
		if (e.Name.ToString() != "sh" || GetDoubleOrNull(e, "t") != 4)
		{
			throw new Exception("Did not give an art shape to the constructor!");
		}
		elt = new XElement(e.Name.ToString());
		setParams(e);
		// Replace this with the constructor that takes an XElement when you make it
		Vertices = new();
	}
}

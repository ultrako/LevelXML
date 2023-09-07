using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;
///<summary>
/// This kind of shape is defined by a list of vertices (Polygons and Art)
///</summary>
public abstract class CustomShape : Shape
{
	private Vertices vertices;
	internal bool isEmpty;
	internal bool hasVertices;
	internal int originalIndex;
	public IList<Vertex> Vertices
	{
		get { return vertices;}
	}
	public override double? Width
	{
		get { return GetDoubleOrNull("p2"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new LevelXMLException("This would make the shape disappear!");
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
				throw new LevelXMLException("This would make the shape disappear!");
			} 
			Elt.SetAttributeValue("p3", val);
		}
	}

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		Elt.Add(vertices.Elt);
		vertices.PlaceInLevel(this, mapper);
	}
	protected CustomShape(string xml) : this(StrToXElement(xml)) {}
	protected CustomShape(XElement e)
	{
		if (e.Name.ToString() != "sh")
		{
			throw new ArgumentException("Did not give a shape to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
		XElement? vTag = e.Element("v");
		if (vTag is not null)
		{
			vertices = new(vTag);
		}
		else
		{
			vertices = new();
		}
		isEmpty = vertices.isEmpty;
		originalIndex = vertices.originalIndex;
	}
}

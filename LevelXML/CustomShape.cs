using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;
///<summary>
/// This kind of shape is defined by a list of vertices (Polygons and Art)
///</summary>
public abstract class CustomShape : Shape
{
	private Vertices verticesTag;
	internal bool isEmpty;
	internal int originalIndex;
	public IList<Vertex> Vertices
	{
		get { return verticesTag.verts;}
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

	internal override void PlaceInLevel(Func<Entity, int> vertMapper)
	{
		verticesTag.vertMapper = vertMapper;
		Elt.RemoveNodes();
		Elt.Add(verticesTag.Elt);
		verticesTag.PlaceInLevel();
	}

	internal override void FinishConstruction()
	{
		verticesTag.FinishConstruction();
	}

	protected CustomShape(XElement e, Func<Entity, int> vertMapper, params Vertex[] vertices) : base(e)
	{
		SetParams(e);
		XElement? vTag = e.Element("v");
		if (vTag is not null)
		{
			this.verticesTag = new(vTag, this, vertMapper);
		}
		else
		{
			this.verticesTag = new(this);
		}
		foreach (Vertex v in vertices)
		{
			Vertices.Add(v);
		}
		isEmpty = verticesTag.isEmpty;
		originalIndex = verticesTag.originalIndex;
	}
}

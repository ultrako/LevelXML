using System.Xml.Linq;
using System.Collections;
namespace LevelXML;
/// <summary>
/// This kind of shape is defined by a list of vertices (Polygons and Art)
/// </summary>
public abstract class CustomShape : Shape
{
	private Vertices verticesTag;
	internal bool isEmpty;
	internal int originalIndex;
	public IList<Vertex> Vertices
	{
		get { return verticesTag.verts;}
	}
	public override double Width
	{
		// Either figure out a way to get the width from the vertices,
		// or make this field nullable
		get 
		{
			return GetDouble("p2");
		}
		set
		{ 
			SetDouble("p2", value);
		}
	}
	public override double Height
	{
		get 
		{
			return GetDouble("p3");
		}
		set
		{
			SetDouble("p3", value);
		}
	}

	internal void ShallowCopy(Func<int, IList<Vertex>> parentLocator)
	{
		verticesTag.verts = parentLocator(verticesTag.originalIndex).ToList();
		isEmpty = false;
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

	/// <summary>
	/// Call this method to set the Width and Height to the default values implied by the list of Vertices
	/// </summary>
	public void DefaultDimensions()
	{
		IEnumerable<double> xPositions = (new Vertex[]{ new(new(0,0))}).Concat(Vertices).Select(vertex => vertex.Position.X);
		if (xPositions.Count() > 2)
		{
			Width = GetDoubleOrNull("p2") ?? xPositions.DefaultIfEmpty().Max() - xPositions.DefaultIfEmpty().Min();
		} else
		{
			Width = double.NaN;
		}
		IEnumerable<double> yPositions = (new Vertex[]{ new(new(0,0))}).Concat(Vertices).Select(vertex => vertex.Position.Y);
		if (yPositions.Count() > 2)
		{
        	Height = GetDoubleOrNull("p3") ?? (yPositions.DefaultIfEmpty().Max() - yPositions.DefaultIfEmpty().Min());
		} else
		{
			Height = double.NaN;
		}
	}

	protected void SetParams(XElement e)
    {
        SetFirstParams(e);
		DefaultDimensions();
		if (GetDoubleOrNull(e, "p2") is double width)
		{
			Width = width;
		}
		if (GetDoubleOrNull(e, "p3") is double height)
		{
			Height = height;
		}
		SetLastParams(e);
    }

	protected CustomShape(XElement e, Func<Entity, int> vertMapper, params Vertex[] vertices) : base(e)
	{
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
		SetParams(e);
	}
}

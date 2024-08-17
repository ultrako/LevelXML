using System.Xml.Linq;
namespace LevelXML;
/// <summary>
/// This kind of shape is defined by a list of vertices (Polygons and Art)
/// </summary>
public abstract class CustomShape : Shape
{
	private readonly Vertices verticesTag;
	internal CustomShape CopiedShape;
	internal bool isEmpty;
	internal int originalIndex;
	public IList<Vertex> Vertices
	{
		get { return verticesTag.verts;}
	}
	public override double Width
	{
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

	internal void LocateParent(Func<int, CustomShape> parentLocator)
	{
		try
		{
			CopiedShape = parentLocator(verticesTag.originalIndex);
		} catch (InvalidOperationException)
		{
			throw new InvalidImportException("Empty custom shape with invalid ID!", string.Empty);
		}
	}

	internal void ShallowCopy()
	{
		verticesTag.verts = CopiedShape.Vertices.ToList();
		isEmpty = false;
	}

	internal override void PlaceInLevel(Func<Entity, int> vertMapper)
	{
		verticesTag.vertMapper = vertMapper;
		Elt.RemoveNodes();
		Elt.Add(verticesTag.Elt);
		verticesTag.PlaceInLevel();
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
		CopiedShape = this;
		XElement? vTag = e.Element("v");
		if (vTag is not null)
		{
			verticesTag = new(vTag, this, vertMapper);
		}
		else
		{
			verticesTag = new(this);
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

using System.Collections;
using System.Xml.Linq;
namespace LevelXML;
/// <summary>
/// A group is a collection of Shapes and non interactive Specials that all move together.
/// </summary>
public class Group : Entity
{
	// So there's no such thing as a "make new empty group" button in the editor,
	// but it would be useful to have an empty group with the default properties
	// you get when you select some objects and press "make group out of objects"
	public const string EditorDefault =
	@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />";
	private List<Entity> items;
	public IList<Entity> Items { get { return items;}}
	
	public override double X
	{
		get { return GetDoubleOrNull("x") ?? 0; }
		set
		{
			SetDouble("x", value);
		}
	}

	public override double Y
	{
		get { return GetDouble("y"); }
		set
		{
			SetDouble("y", value);
		}
	}

	public double Rotation
	{
		get { return GetDouble("r"); }
		set 
		{ 
			SetDouble("r", value);
		}
	}

	public double OriginX
	{
		get { return GetDouble("ox");}
		set
		{
			SetDouble("ox", value);
		}
	}

	public double OriginY
	{
		get { return GetDouble("oy");}
		set
		{
			SetDouble("oy", value);
		}
	}

	public HWBool Sleeping
	{
		get { return GetBool("s");}
		set
		{
			Elt.SetAttributeValue("s", value);
		}
	}

	public HWBool Foreground
	{
		get { return GetBool("f");}
		set
		{
			Elt.SetAttributeValue("f", value);
		}
	}

	public double Opacity
	{
		get { return GetDouble("o"); }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			// If the opacity is set to NaN, the import box sets it to 0
			Elt.SetAttributeValue("o", Math.Clamp(value, 0, 100));
		}
	}

	public HWBool Fixed
	{
		get { return GetBool("im");}
		set
		{
			Elt.SetAttributeValue("im", value);
		}
	}

	public HWBool FixedRotation
	{
		get { return GetBool("fr");}
		set
		{
			Elt.SetAttributeValue("fr", value);
		}
	}

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Entity entity in items)
		{
			entity.PlaceInLevel(mapper);
			Elt.Add(entity.Elt);
		}
	}

	protected virtual void setParams(XElement e)
	{
		X = GetDoubleOrNull(e, "x") ?? double.NaN;
		Y = GetDoubleOrNull(e, "y") ?? double.NaN;
		Rotation = GetDoubleOrNull(e, "r") ?? double.NaN;
		OriginX = GetDoubleOrNull(e, "ox") ?? double.NaN;
		OriginY = GetDoubleOrNull(e, "oy") ?? double.NaN;
		Sleeping = GetBoolOrNull(e, "s") ?? false;
		Foreground = GetBoolOrNull(e, "f") ?? false;
		Opacity = GetDoubleOrNull(e, "o") ?? 100;
		Fixed = GetBoolOrNull(e, "im") ?? false;
		FixedRotation = GetBoolOrNull(e, "fr") ?? false;
	}

	internal override void FinishConstruction()
	{
		foreach (Entity e in items)
		{
			e.FinishConstruction();
		}
	}

	public Group(params Entity[] content) : this(EditorDefault, content) {}
	
	internal Group(string xml, params Entity[]? content) : this(StrToXElement(xml), content:content, vertMapper:default!) {}

	internal Group(XElement e, Func<Entity, int> vertMapper, params Entity[]? content) :
	base("g")
	{
		IEnumerable<Entity> groupedEntities = 
			e.Elements()
			.Select(element => FromXElement(element, vertMapper: vertMapper))
			.Concat(content ?? Array.Empty<Entity>());
		items = new(groupedEntities);
		setParams(e);
	}
}

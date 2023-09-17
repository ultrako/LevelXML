using System.Collections;
using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// A group is a list of Shapes and non interactive Specials that all move together.
///</summary>
public class Group : Entity
{
	// So there's no such thing as a "make new empty group" button in the editor,
	// but it would be useful to have an empty group with the default properties
	// you get when you select some objects and press "make group out of objects"
	public static string EditorDefault =
	@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />";
	private List<Entity> items;
	public IList<Entity> Items { get { return items;}}
	
	// We need to make sure only non fixed shapes, or non interactive specials are grouped.
	// This function will throw an exception if that isn't the case.
	private void checkGroupableEntity(Entity entity)
	{
		if (entity is Shape shape)
		{}
		else if (entity is Special special)
		{
			// if (entity is Character character)
			if (entity is TextBox)
			{} else
			{
				throw new LevelXMLException($"{nameof(entity)} are not allowed in groups.");
			}
		} else
		{
			throw new LevelXMLException($"{nameof(entity)} are not allowed in groups.");
		}
	}
	
	public override double? X
	{
		get { return GetDoubleOrNull("x"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("x", (double)value); }
		}
	}

	public override double? Y
	{
		get { return GetDoubleOrNull("y"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("y", (double)value); }
		}
	}

	public double? Rotation
	{
		get { return GetDoubleOrNull("r"); }
		set 
		{ 
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("r", (double)value); }
		}
	}

	public double? OriginX
	{
		get { return GetDoubleOrNull("ox");}
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("ox", (double)value); }
		}
	}

	public double? OriginY
	{
		get { return GetDoubleOrNull("oy");}
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("oy", (double)value); }
		}
	}

	public HWBool? Sleeping
	{
		get { return GetBoolOrNull("s");}
		set
		{
			HWBool val = value ?? false;
			Elt.SetAttributeValue("s", val);
		}
	}

	public HWBool? Foreground
	{
		get { return GetBoolOrNull("f");}
		set
		{
			HWBool val = value ?? false;
			Elt.SetAttributeValue("f", val);
		}
	}

	public double? Opacity
	{
		get { return GetDoubleOrNull("o"); }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			double val = value ?? 100;
			// If the opacity is set to NaN, the import box sets it to 0
			Elt.SetAttributeValue("o", Math.Clamp(val, 0, 100));
		}
	}

	public HWBool? Fixed
	{
		get { return GetBoolOrNull("im");}
		set
		{
			HWBool val = value ?? false;
			Elt.SetAttributeValue("im", val);
		}
	}

	public HWBool? FixedRotation
	{
		get { return GetBoolOrNull("fr");}
		set
		{
			HWBool val = value ?? false;
			Elt.SetAttributeValue("fr", val);
		}
	}

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Entity entity in items)
		{
			checkGroupableEntity(entity);
			entity.PlaceInLevel(mapper);
			Elt.Add(entity.Elt);
		}
	}

	private void setParams(XElement e)
	{
		X = GetDoubleOrNull(e, "x");
		Y = GetDoubleOrNull(e, "y");
		Rotation = GetDoubleOrNull(e, "r");
		OriginX = GetDoubleOrNull(e, "ox");
		OriginY = GetDoubleOrNull(e, "oy");
		Sleeping = GetBoolOrNull(e, "s");
		Foreground = GetBoolOrNull(e, "f");
		Opacity = GetDoubleOrNull(e, "o");
		Fixed = GetBoolOrNull(e, "im");
		FixedRotation = GetBoolOrNull(e, "fr");
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
			.Select(element => Entity.FromXElement(element, vertMapper: vertMapper))
			.Concat(content ?? new Entity[0]);
		items = new(groupedEntities);
		setParams(e);
	}
}

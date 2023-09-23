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
	public const string EditorDefault =
	@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />";
	private List<Entity> items;
	public IList<Entity> Items { get { return items;}}
	
	// We need to make sure only non fixed shapes, or certain specials are grouped.
	// This function will throw an exception if that isn't the case.
	private void checkGroupableEntity(Entity entity)
	{
		if (entity is Shape ||
			entity is Van ||
			entity is DinnerTable ||
			entity is IBeam ||
			entity is SpikeSet ||
			entity is TextBox ||
			entity is NonPlayerCharacter ||
			entity is Chair ||
			entity is Bottle ||
			entity is Television ||
			entity is Boombox ||
			entity is Sign ||
			entity is Toilet ||
			entity is TrashCan ||
			entity is ArrowGun ||
			entity is Food ||
			entity is BladeWeapon) 
		{
			return;
		}
		throw new LevelXMLException("{nameof(entity)} are not allowed in groups.");
	}
	
	public override double X
	{
		get { return GetDoubleOrNull("x") ?? 0; }
		set
		{
			if (double.IsNaN(value))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("x", value); }
		}
	}

	public override double Y
	{
		get { return GetDoubleOrNull("y") ?? 0; }
		set
		{
			if (double.IsNaN(value))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("y", value); }
		}
	}

	public double Rotation
	{
		get { return GetDoubleOrNull("r") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("r", value); }
		}
	}

	public double OriginX
	{
		get { return GetDoubleOrNull("ox") ?? 0;}
		set
		{
			if (double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("ox", value); }
		}
	}

	public double OriginY
	{
		get { return GetDoubleOrNull("oy") ?? 0;}
		set
		{
			if (double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the group disappear!");
			}
			else { SetDouble("oy", value); }
		}
	}

	public HWBool Sleeping
	{
		get { return GetBoolOrNull("s") ?? false;}
		set
		{
			Elt.SetAttributeValue("s", value);
		}
	}

	public HWBool Foreground
	{
		get { return GetBoolOrNull("f") ?? false;}
		set
		{
			Elt.SetAttributeValue("f", value);
		}
	}

	public double Opacity
	{
		get { return GetDoubleOrNull("o") ?? 100; }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			// If the opacity is set to NaN, the import box sets it to 0
			Elt.SetAttributeValue("o", Math.Clamp(value, 0, 100));
		}
	}

	public HWBool Fixed
	{
		get { return GetBoolOrNull("im") ?? false;}
		set
		{
			Elt.SetAttributeValue("im", value);
		}
	}

	public HWBool FixedRotation
	{
		get { return GetBoolOrNull("fr") ?? false;}
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
			checkGroupableEntity(entity);
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
			.Select(element => Entity.FromXElement(element, vertMapper: vertMapper))
			.Concat(content ?? new Entity[0]);
		items = new(groupedEntities);
		setParams(e);
	}
}

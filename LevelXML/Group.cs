using System.Collections;
using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// A group is a list of Shapes and non interactive Specials that all move together.
///</summary>
public class Group : Entity, IList<Entity>
{
	// So there's no such thing as a "make new empty group" button in the editor,
	// but it would be useful to have an empty group with the default properties
	// you get when you select some objects and press "make group out of objects"
	public static string EditorDefault =
	@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />";
	private List<Entity> lst;
	public void Add(Entity entity) 
	{
		checkGroupableEntity(entity); 
		lst.Add(entity); 
	}
	public bool Remove(Entity entity) { return lst.Remove(entity); }
	public int IndexOf(Entity entity) { return lst.IndexOf(entity); }
	public void Insert(int i, Entity entity) 
	{
		checkGroupableEntity(entity); 
		lst.Insert(i, entity); 
	}
	public void RemoveAt(int i) { lst.RemoveAt(i); }
	public void Clear() { lst.Clear(); }
	public bool Contains(Entity entity) { return lst.Contains(entity); }
	public void CopyTo(Entity[] entities, int i) { lst.CopyTo(entities, i); }
	public bool IsReadOnly { get { return false; } }
	IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() { return lst.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return lst.GetEnumerator();}
	public int Count => lst.Count;
	// We need to make sure only non fixed shapes, or non interactive specials are grouped.
	// This function will throw an exception if that isn't the case.
	private void checkGroupableEntity(Entity entity)
	{
		if (entity is Shape shape)
		{
			if (shape.Fixed == true)
			{
				throw new Exception("Only non fixed shapes can go into groups.");
			}
		} else if (entity is Special special)
		{
			// if (entity is Character character)
			if (entity is TextBox)
			{} else
			{
				throw new Exception($"{nameof(entity)} are not allowed in groups.");
			}
		} else
		{
			throw new Exception($"{nameof(entity)} are not allowed in groups.");
		}
	}
	public Entity this[int index] 
	{ 
		get { return lst[index]; } 
		set 
		{
			checkGroupableEntity(value); 
			lst[index] = value; 
		} 
	}
	public override double? X
	{
		get { return GetDoubleOrNull("x"); }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("x", val);
		}
	}
	public override double? Y
	{
		get { return GetDoubleOrNull("y"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("y", val);
		}
	}
	public double? Rotation
	{
		get { return GetDoubleOrNull("r"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new Exception("That would make the group disappear!");
			}
			Elt.SetAttributeValue("r", Math.Clamp(val,-180,180)); 
		}
	}
	public double? OriginX
	{
		get { return GetDoubleOrNull("ox");}
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("ox", val);
		}
	}
	public double? OriginY
	{
		get { return GetDoubleOrNull("oy");}
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("oy", val);
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
		foreach (Entity entity in lst)
		{
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
	public Group(params Entity[] content) : this(EditorDefault, content) {}
	public Group(string xml, params Entity[]? content) : this(StrToXElement(xml), content:content) {}
	internal Group(XElement e, Func<XElement, Entity> ReverseMapper=default!, params Entity[]? content) :
	base("g")
	{
		IEnumerable<Entity> groupedEntities = 
			e.Elements()
			.Select(element => Entity.FromXElement(element, ReverseMapper))
			.Concat(content ?? new Entity[0]);
		lst = new(groupedEntities);
		setParams(e);
	}
}

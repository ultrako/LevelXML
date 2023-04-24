using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace HappyWheels;
// Shapes are either rectangles, triangles, circles, polygons, or art.
public abstract class Shape : Entity
{
	public override string Name => "sh";
	public abstract uint Type {get;}
	
	// The pattern of nullable properties that usually set their
	// backing field to non null (or throw),
	// is so that constructors of derived classes can pass their
	// XAttribute values to these properties and get import box default behavior.
	// They must _always_ set all these properties.
	private bool _interactive;
	public bool? Interactive
	{
		get { return _interactive; }
		set { _interactive = value ?? true; }
	}
	// All shapes have Width and Height, just that some are set differently
	public abstract float? Width {get; set;}
	public abstract float? Height {get; set;}
	private float _rotation;
	public float? Rotation
	{
		get { return _rotation; }
		set 
		{ 
			if (value is null || float.IsNaN((float)value!))
			{
				_rotation = 0;
			} else 
			{
				_rotation = Math.Clamp((float)value,-180,180); 
			}
		}
	}
	private bool _fixed;
	public bool? Fixed
	{
		get { return _fixed; }
		set { _fixed = value ?? true; }
	}
	private bool _sleeping;
	public bool? Sleeping
	{
		get { return _sleeping; }
		set { _sleeping = value ?? false; }
	}
	private float _density;
	public float? Density
	{
		get { return _density; }
		set
		{
			float val = value ?? 1;
			// I am hoping clamp doesn't mess with NaN as we do sometimes want
			// NaN density entities
			val = (float)Math.Clamp(val, 0.1, 100.0);
			_density = val;
		}
	}
	private int _fillColor;
	public int? FillColor
	{
		get { return _fillColor; }
		set { _fillColor = value ?? 0x000000; }
	}
	private int _outlineColor;
	public int? OutlineColor
	{
		get { return _outlineColor; }
		set { _outlineColor = value ?? -1; }
	}
	private uint _opacity;
	public float? Opacity
	{
		get { return _opacity; }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			float val = value ?? 100;
			// If the opacity is set to NaN, the import box sets it to 0
			if (float.IsNaN(val)) { val = 0; }
			_opacity = (uint)Math.Clamp(val, 0, 100);
		}
	}
	private uint _collision;
	public int? Collision 
	{
		get { return (int?)_collision; }
		set
		{
			// If this isn't set, it defaults to 1
			int val = value ?? 1;
			// Technically if this is set to NaN this ends up being 0,
			// but collision 0 has the exact same behavior as collision 1
			_collision = (uint)Math.Clamp(val, 1, 7);
		}
	}
	// Only circles actually have this, but we're like one thing away from having commonality
	// in every single Shape
	private uint _cutout;
	public int? Cutout {
		get { return (int?) _cutout; }
		set
		{
			int val = value ?? 0;
			_cutout = (uint)Math.Clamp(val, 0, 100);
		}
	}
	protected void setParams(XElement elt)
	{
		Interactive = getBoolOrNull(elt, "i");
        x = getFloatOrNull(elt, "p0");
        y = getFloatOrNull(elt, "p1");
        Width = getFloatOrNull(elt, "p2");
        Height = getFloatOrNull(elt, "p3");
        Rotation = getFloatOrNull(elt, "p4");
        Fixed = getBoolOrNull(elt, "p5");
        Sleeping = getBoolOrNull(elt, "p6");
        Density = getFloatOrNull(elt, "p7");
        FillColor = (int?)getFloatOrNull(elt, "p8");
        OutlineColor = (int?)getFloatOrNull(elt, "p9");
        Opacity = getFloatOrNull(elt, "p10");
        Collision = (int?)getFloatOrNull(elt, "p11");
	}
	protected override List<XAttribute> getAttributes(int? _) 
	{
		// None of these are ever missing from a <sh> tag
		// So I don't need to worry about doing a .Where on them
		return new List<XAttribute>()
		{
			new XAttribute("t", Type!),
			new XAttribute("i", FormatBool(Interactive!)),
			new XAttribute("p0", x!),
			new XAttribute("p1", y!),
			new XAttribute("p2", Width!),
			new XAttribute("p3", Height!),
			new XAttribute("p4", Rotation!),
			new XAttribute("p5", FormatBool(Fixed!)),
			new XAttribute("p6", FormatBool(Sleeping!)),
			new XAttribute("p7", Density!),
			new XAttribute("p8", FillColor!),
			new XAttribute("p9", OutlineColor!),
			new XAttribute("p10", Opacity!),
			new XAttribute("p11", Collision!)
		};
	}
	protected override List<XElement> getChildren() 
	{
		return new List<XElement> ();
	}
}

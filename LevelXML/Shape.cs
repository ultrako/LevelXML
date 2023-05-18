using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace HappyWheels;
// Shapes are either rectangles, triangles, circles, polygons, or art.
public abstract class Shape : Entity
{
	public abstract uint Type {get;}
	
	// The pattern of nullable properties that usually set their
	// backing field to non null (or throw),
	// is so that constructors of derived classes can pass their
	// XAttribute values to these properties and get import box default behavior.
	// They must _always_ set all these properties.
	public HWBool? Interactive
	{
		// If Interactive is true, then the XAttribute isn't set
		get { return GetBoolOrNull("i"); }
		set { if (value == false) { elt.SetAttributeValue("i", value); }; }
	}
	// For shapes, x and y are p0 and p1
	public override float? x
	{
		get { return GetFloatOrNull("p0"); }
		set
		{
			if (value is null || float.IsNaN((float)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { elt.SetAttributeValue("p0", value); }
		}
	}
	public override float? y
	{
		get { return GetFloatOrNull("p1"); }
		set
		{
			if (value is null || float.IsNaN((float)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { elt.SetAttributeValue("p1", value); }
		}
	}
	// All shapes have Width and Height, just that some are set differently
	public abstract float? Width {get; set;}
	public abstract float? Height {get; set;}
	public float? Rotation
	{
		get { return GetFloatOrNull("p4"); }
		set 
		{ 
			float val = value ?? 0;
			if (float.IsNaN(val)) 
			{
				throw new Exception("That would make the shape disappear!");
			}
			elt.SetAttributeValue("p4", Math.Clamp(val,-180,180)); 
		}
	}
	public HWBool? Fixed
	{
		get { return GetBoolOrNull("p5"); }
		set { elt.SetAttributeValue("p5", value ?? HWBool.True); }
	}
	public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p6"); }
		set { elt.SetAttributeValue("p6", value ?? HWBool.False); }
	}
	public float? Density
	{
		get { return GetFloatOrNull("p7"); }
		set
		{
			float val = value ?? 1;
			// I am hoping clamp doesn't mess with NaN as we do sometimes want
			// NaN density entities
			val = (float)Math.Clamp(val, 0.1, 100.0);
			elt.SetAttributeValue("p7", val);
		}
	}
	// Representing a RGB color with a float is weird, yes
	// It's just that in happy wheels anything can be NaN
	// But in c#, NaN is only a value in floats or doubles
	public float? FillColor
	{
		get { return GetFloatOrNull("p8"); }
		set 
		{
			float val = value ?? 4032711;
			elt.SetAttributeValue("p8", val); 
		}
	}
	public float? OutlineColor
	{
		get { return GetFloatOrNull("p9"); }
		set 
		{ 
			float val = value ?? -1;
			elt.SetAttributeValue("p9", val); 
		}
	}
	public float? Opacity
	{
		get { return GetFloatOrNull("p10"); }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			float val = value ?? 100;
			// If the opacity is set to NaN, the import box sets it to 0
			elt.SetAttributeValue("p10", Math.Clamp(val, 0, 100));
		}
	}
	public float? Collision 
	{
		get { return GetFloatOrNull("p11"); }
		set
		{
			// If this isn't set, it defaults to 1
			float val = value ?? 1;
			// Technically if this is set to NaN this ends up being 0,
			// but collision 0 has the exact same behavior as collision 1
			elt.SetAttributeValue("p11", Math.Clamp(val, 1, 7));
		}
	}
	// Only circles actually have this, but we're like one thing away from having commonality
	// in every single Shape
	public int? Cutout {
		get { return (int?)GetFloatOrNull("p12"); }
		set
		{
			int val = value ?? 0;
			elt.SetAttributeValue("p12", (uint)Math.Clamp(val, 0, 100));
		}
	}
	protected void setParams(XElement e)
	{
		elt.SetAttributeValue("t", Type);
		Interactive = GetBoolOrNull(e, "i");
        x = GetFloatOrNull(e, "p0");
        y = GetFloatOrNull(e, "p1");
        Width = GetFloatOrNull(e, "p2");
        Height = GetFloatOrNull(e, "p3");
        Rotation = GetFloatOrNull(e, "p4");
        Fixed = GetBoolOrNull(e, "p5");
        Sleeping = GetBoolOrNull(e, "p6");
        Density = GetFloatOrNull(e, "p7");
        FillColor = GetFloatOrNull(e, "p8");
        OutlineColor = GetFloatOrNull(e, "p9");
        Opacity = GetFloatOrNull(e, "p10");
        Collision = GetFloatOrNull(e, "p11");
	}
	internal Shape(params object?[] contents) : base("sh", contents) {}
}

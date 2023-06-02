using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace HappyWheels;
///<summary>
/// Shapes are simple entities that can have collision.
/// Shapes are either rectangles, triangles, circles, polygons, or art.
///</summary>
public abstract class Shape : Entity
{
	internal abstract uint Type {get;}
	
	// The pattern of nullable properties that usually set their
	// backing field to non null (or throw),
	// is so that constructors of derived classes can pass their
	// XAttribute values to these properties and get import box default behavior.
	// They must _always_ set all these properties.
	public HWBool? Interactive
	{
		// If Interactive is true, then the XAttribute isn't set
		get { return GetBoolOrNull("i"); }
		set { if (value == false) { Elt.SetAttributeValue("i", value); }; }
	}
	// For shapes, x and y are p0 and p1
	public override double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { Elt.SetAttributeValue("p0", value); }
		}
	}
	public override double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { Elt.SetAttributeValue("p1", value); }
		}
	}
	// All shapes have Width and Height, just that some are set differently
	public abstract double? Width {get; set;}
	public abstract double? Height {get; set;}
	public double? Rotation
	{
		get { return GetDoubleOrNull("p4"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new Exception("That would make the shape disappear!");
			}
			Elt.SetAttributeValue("p4", Math.Clamp(val,-180,180)); 
		}
	}
	public HWBool? Fixed
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.True); }
	}
	public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p6"); }
		set { Elt.SetAttributeValue("p6", value ?? HWBool.False); }
	}
	public double? Density
	{
		get { return GetDoubleOrNull("p7"); }
		set
		{
			double val = value ?? 1;
			// I am hoping clamp doesn't mess with NaN as we do sometimes want
			// NaN density entities
			val = (double)Math.Clamp(val, 0.1, 100.0);
			Elt.SetAttributeValue("p7", val);
		}
	}
	// Representing a RGB color with a double is weird, yes
	// It's just that in happy wheels anything can be NaN
	// But in c#, NaN is only a value in doubles or doubles
	public double? FillColor
	{
		get { return GetDoubleOrNull("p8"); }
		set 
		{
			double val = value ?? 4032711;
			Elt.SetAttributeValue("p8", val); 
		}
	}
	public double? OutlineColor
	{
		get { return GetDoubleOrNull("p9"); }
		set 
		{ 
			double val = value ?? -1;
			Elt.SetAttributeValue("p9", val); 
		}
	}
	public double? Opacity
	{
		get { return GetDoubleOrNull("p10"); }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			double val = value ?? 100;
			// If the opacity is set to NaN, the import box sets it to 0
			Elt.SetAttributeValue("p10", Math.Clamp(val, 0, 100));
		}
	}
	public double? Collision 
	{
		get { return GetDoubleOrNull("p11"); }
		set
		{
			// If this isn't set, it defaults to 1
			double val = value ?? 1;
			// Technically if this is set to NaN this ends up being 0,
			// but collision 0 has the exact same behavior as collision 1
			Elt.SetAttributeValue("p11", Math.Clamp(val, 1, 7));
		}
	}
	// Only circles actually have this, but we're like one thing away from having commonality
	// in every single Shape
	public int? Cutout {
		get { return (int?)GetDoubleOrNull("p12"); }
		set
		{
			int val = value ?? 0;
			Elt.SetAttributeValue("p12", (uint)Math.Clamp(val, 0, 100));
		}
	}
	protected void SetParams(XElement e)
	{
		Elt.SetAttributeValue("t", Type);
		Interactive = GetBoolOrNull(e, "i");
        X = GetDoubleOrNull(e, "p0");
        Y = GetDoubleOrNull(e, "p1");
        Width = GetDoubleOrNull(e, "p2");
        Height = GetDoubleOrNull(e, "p3");
        Rotation = GetDoubleOrNull(e, "p4");
        Fixed = GetBoolOrNull(e, "p5");
        Sleeping = GetBoolOrNull(e, "p6");
        Density = GetDoubleOrNull(e, "p7");
        FillColor = GetDoubleOrNull(e, "p8");
        OutlineColor = GetDoubleOrNull(e, "p9");
        Opacity = GetDoubleOrNull(e, "p10");
        Collision = GetDoubleOrNull(e, "p11");
	}
	internal Shape(params object?[] contents) : base("sh", contents) {}
}

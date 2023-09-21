using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace HappyWheels;
///<summary>
/// Shapes are simple entities that can have collision and they can be a part of groups.
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

	/// <summary>
	///  Setting interactive to false will treat the shape like flat artwork.
	///  The shape will only move if part of a group, and will not take away
	///  from the total available shapecount allowed in your level.
	/// </summary>
	public HWBool? Interactive
	{
		// If Interactive is true, then the XAttribute isn't set
		get { return GetBoolOrNull("i") ?? true; }
		set { if (value == false) { Elt.SetAttributeValue("i", value); }; }
	}
	
	public override double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the shape disappear!");
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
				throw new LevelXMLException("This would make the shape disappear!");
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
				throw new LevelXMLException("That would make the shape disappear!");
			}
			Elt.SetAttributeValue("p4", Math.Clamp(val,-180,180)); 
		}
	}

	/// <summary>
	///  Whether or not the shape moves
	/// </summary>
	public HWBool? Fixed
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.True); }
	}

	/// <summary>
	///  Whether or not the shape starts in an unmoving state until it is first collided with
	/// </summary>
	public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p6"); }
		set { Elt.SetAttributeValue("p6", value ?? HWBool.False); }
	}

	/// <summary>
	///  The density of the shape - how much force gravity applies to the shape per how large it is.
	/// </summary>
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

	/// <summary>
	/// The color of the shape inbetween its edges
	/// </summary>
	public double? FillColor
	{
		get { return GetDoubleOrNull("p8"); }
		set 
		{
			double val = value ?? 4032711;
			Elt.SetAttributeValue("p8", val); 
		}
	}

	// The color of the edges of the shape
	public double? OutlineColor
	{
		get { return GetDoubleOrNull("p9"); }
		set 
		{ 
			double val = value ?? -1;
			Elt.SetAttributeValue("p9", val); 
		}
	}

	/// <summary>
	/// How visible the shape is
	/// </summary>
	/// <remarks>
	/// At 0, a shape is invisible,
	/// At 1-99, a shape is translucent,
	/// And at 100, a shape is fully visible.
	/// </remarks>
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

	/// <summary>
	/// What kind of objects this shape can collide with
	/// </summary>
	public Collision Collision
	{
		get { return (Collision)GetDoubleOrNull(Elt, "p11")!;}
		set { Elt.SetAttributeValue("p11", value);}
	}
	
	protected virtual void SetParams(XElement e)
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
        Collision = (Collision?)GetDoubleOrNull(e, "p11") ?? Collision.Everything;
	}
	internal Shape(XElement e) : base("sh") 
	{
		Elt = new XElement(e.Name.ToString());
	}
}

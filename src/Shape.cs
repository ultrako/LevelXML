using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace LevelXML;
/// <summary>
/// Shapes are simple entities that can have collision and they can be a part of groups.
/// Shapes are either rectangles, triangles, circles, polygons, or art.
/// </summary>
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
	
	public override double X
	{
		get { return GetDouble("p0"); }
		set
		{
			Elt.SetAttributeValue("p0", value);
		}
	}
	public override double Y
	{
		get { return GetDouble("p1"); }
		set
		{
			Elt.SetAttributeValue("p1", value);
		}
	}

	// All shapes have Width and Height, just that some are set differently
	public abstract double Width {get; set;}

	public abstract double Height {get; set;}

	public double Rotation
	{
		get { return GetDouble("p4"); }
		set 
		{ 
			Elt.SetAttributeValue("p4", Math.Clamp(value,-180,180)); 
		}
	}

	/// <summary>
	///  Whether or not the shape moves
	/// </summary>
	public HWBool Fixed
	{
		get { return GetBool("p5"); }
		set { Elt.SetAttributeValue("p5", value); }
	}

	/// <summary>
	///  Whether or not the shape starts in an unmoving state until it is first collided with
	/// </summary>
	public HWBool Sleeping
	{
		get { return GetBool("p6"); }
		set { Elt.SetAttributeValue("p6", value); }
	}

	/// <summary>
	///  The density of the shape - how much force gravity applies to the shape per how large it is.
	/// </summary>
	public double Density
	{
		get { return GetDouble("p7"); }
		set
		{
			Elt.SetAttributeValue("p7", Math.Clamp(value, 0.1, 100.0));
		}
	}

	// Representing a RGB color with a double is weird, yes
	// It's just that in happy wheels anything can be NaN
	// But in c#, NaN is only a value in doubles

	/// <summary>
	/// The color of the shape inbetween its edges in RGB hex format.
	/// </summary>
	public double FillColor
	{
		get { return GetDouble("p8"); }
		set 
		{
			Elt.SetAttributeValue("p8", value); 
		}
	}

	// The color of the edges of the shape
	public double OutlineColor
	{
		get { return GetDouble("p9"); }
		set 
		{ 
			Elt.SetAttributeValue("p9", value); 
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
	public double Opacity
	{
		get { return GetDouble("p10"); }
		set
		{
			Elt.SetAttributeValue("p10", Math.Clamp(value, 0, 100));
		}
	}

	/// <summary>
	/// What kind of objects this shape can collide with
	/// </summary>
	public Collision Collision
	{
		get { return (Collision)GetDouble("p11");}
		set { Elt.SetAttributeValue("p11", value);}
	}
	
	// Had to split this out into two because the order of setting these properties matters
	protected void SetFirstParams(XElement e)
	{
		Elt.SetAttributeValue("t", Type);
		Interactive = GetBoolOrNull(e, "i");
        X = GetDoubleOrNull(e, "p0") ?? double.NaN;
        Y = GetDoubleOrNull(e, "p1") ?? double.NaN;
	}
	protected void SetLastParams(XElement e)
	{
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
        Fixed = GetBoolOrNull(e, "p5") ?? true;
        Sleeping = GetBoolOrNull(e, "p6") ?? false;
        Density = GetDoubleOrNull(e, "p7") ?? 1;
        FillColor = GetDoubleOrNull(e, "p8") ?? 4032711;
        OutlineColor = GetDoubleOrNull(e, "p9") ?? -1;
        Opacity = GetDoubleOrNull(e, "p10") ?? 100;
        Collision = (Collision?)GetDoubleOrNull(e, "p11") ?? Collision.Everything;
	}
	internal Shape(XElement e) : base("sh") 
	{
		Elt = new XElement(e.Name.ToString());
	}
}

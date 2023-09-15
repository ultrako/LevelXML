using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// A trigger is an entity that under certain circumstances can be activated,
/// and when it does, it does its particular Action.
///</summary>
public abstract class Trigger : Entity
{
	internal abstract uint Type {get;}
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
	public double? Width
	{
		get { return GetDoubleOrNull("w"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("w", Math.Clamp(val, 5, 5000));
		}
	}
	public double? Height
	{
		get { return GetDoubleOrNull("h"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("h", Math.Clamp(val, 5, 5000));
		}
	}
	public double? Rotation
	{
		get { return GetDoubleOrNull("a"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("a", val);
		}
	}
	public double? TriggeredBy
	{
		get { return GetDoubleOrNull("b"); }
		set
		{
			// Having NaN in triggeredBy is fine, 
			// it just works like "triggered only by other triggers"
			double val = value ?? double.NaN;
			if (double.IsNaN(val)) 
			{ 
				Elt.SetAttributeValue("b", val); 
			}
			else 
			{
				Elt.SetAttributeValue("b", (int)Math.Clamp(val, 1, 6));
			}
		}
	}
	public double? RepeatType
	{
		get { return GetDoubleOrNull("r"); }
		set
		{
			// If this is null or NaN or 0, then the trigger can't ever be activated
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This trigger could not ever get activated!");
			}
			int val = (int)value!;
			Elt.SetAttributeValue("r", Math.Clamp(val, 1, 4));
		}
	}
	public HWBool? StartDisabled
	{
		get { return GetBoolOrNull("sd") ?? HWBool.False; }
		set
		{
			if (value == true)
			{
				Elt.SetAttributeValue("sd", HWBool.True);
			}
			else
			{
				Elt.SetAttributeValue("sd", HWBool.False);
			}
		}
	}
	public double? Interval
	{
		get { return GetDoubleOrNull("i"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("i", val);
		}
	}
	public double? Delay
	{
		get { return GetDoubleOrNull("d"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("d", Math.Clamp(val, 0, 30));
		}
	}

	protected virtual void SetParams(XElement e)
	{
		X = GetDoubleOrNull(e, "x");
		Y = GetDoubleOrNull(e, "y");
		Width = GetDoubleOrNull(e, "w");
		Height = GetDoubleOrNull(e, "h");
		Rotation = GetDoubleOrNull(e, "a");
		TriggeredBy = GetDoubleOrNull(e, "b");
		Elt.SetAttributeValue("t", Type);
		RepeatType = GetDoubleOrNull(e, "r");
		StartDisabled = GetBoolOrNull(e, "sd");
		if (RepeatType == 4) 
		{
			Interval = GetDoubleOrNull(e, "i");
		}
	}
	// See if you can get rid of the param here
	internal Trigger(XElement e) : base("t")
	{
		if (e.Name.ToString() != "t")
		{
			throw new LevelXMLException("Did not give a trigger to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
	}
}

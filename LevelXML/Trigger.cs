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

	/// <summary>
	/// Which things can trigger this trigger
	/// </summary>
	public TriggeredBy? TriggeredBy
	{
		get { return (TriggeredBy?)GetDoubleOrNull("b") ?? HappyWheels.TriggeredBy.Nothing; }
		set
		{
			Elt.SetAttributeValue("b", value ?? HappyWheels.TriggeredBy.Nothing);
		}
	}

	/// <summary>
	/// What kind of repetition the trigger will do when activated once or more
	/// </summary>
	public RepeatType? RepeatType
	{
		get { return (RepeatType?)GetDoubleOrNull("r") ?? HappyWheels.RepeatType.Never; }
		set
		{
			Elt.SetAttributeValue("r", (RepeatType?)value ?? HappyWheels.RepeatType.Never);
		}
	}

	/// <summary>
	/// Whether or not the trigger starts disabled (and would need to be enabled by other triggers to activate)
	/// </summary>
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

	/// <summary>
	/// If the trigger repeats continuously, this is the rate in seconds at which the trigger repeats.
	/// </summary>
	public double? Interval
	{
		get { return GetDoubleOrNull("i"); }
		set
		{
			double val = value ?? double.NaN;
			Elt.SetAttributeValue("i", val);
		}
	}

	/// <summary>
	/// This is the amount of time in seconds after the trigger gets activated, before its actions fire.
	/// </summary>
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
	
	internal Trigger(XElement e) : base("t")
	{
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
	}
}

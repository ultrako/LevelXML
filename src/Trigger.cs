using System.Xml.Linq;
using System.Collections;

namespace LevelXML;
/// <summary>
/// A trigger is an entity that under certain circumstances can be activated,
/// and when it does, it does its particular Action.
/// </summary>
public abstract class Trigger : Entity
{
	internal abstract uint Type {get;}

	public override double X
	{
		get { return GetDoubleOrNull("x") ?? 0; }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			SetDouble("x", value);
		}
	}

	public override double Y
	{
		get { return GetDoubleOrNull("y") ?? 0; }
		set
		{
			SetDouble("y", value);
		}
	}

	public double Width
	{
		get { return GetDoubleOrNull("w") ?? 100; }
		set
		{
			SetDouble("w", Math.Clamp(value, 5, 5000));
		}
	}

	public double Height
	{
		get { return GetDoubleOrNull("h") ?? 100; }
		set
		{
			SetDouble("h", Math.Clamp(value, 5, 5000));
		}
	}

	public double Rotation
	{
		get { return GetDoubleOrNull("a") ?? 0; }
		set
		{
			SetDouble("a", value);
		}
	}

	/// <summary>
	/// Which things can trigger this trigger
	/// </summary>
	public TriggeredBy TriggeredBy
	{
		get { return (TriggeredBy?)GetDoubleOrNull("b") ?? LevelXML.TriggeredBy.Nothing; }
		set
		{
			Elt.SetAttributeValue("b", value);
		}
	}

	/// <summary>
	/// What kind of repetition the trigger will do when activated once or more
	/// </summary>
	public RepeatType RepeatType
	{
		get { return (RepeatType?)GetDoubleOrNull("r") ?? LevelXML.RepeatType.Never; }
		set
		{
			Elt.SetAttributeValue("r", value);
		}
	}

	/// <summary>
	/// Whether or not the trigger starts disabled (and would need to be enabled by other triggers to activate)
	/// </summary>
	public HWBool StartDisabled
	{
		get { return GetBoolOrNull("sd") ?? HWBool.False; }
		set
		{
			HWBool val = value;
			if (val == HWBool.NaN) { val = HWBool.False; }
			Elt.SetAttributeValue("sd", val);
		}
	}

	/// <summary>
	/// If the trigger repeats continuously, this is the rate in seconds at which the trigger repeats.
	/// </summary>
	public double Interval
	{
		get { return GetDoubleOrNull("i") ?? double.NaN; }
		set
		{
			Elt.SetAttributeValue("i", value);
		}
	}

	/// <summary>
	/// This is the amount of time in seconds after the trigger gets activated, before its actions fire.
	/// </summary>
	public double Delay
	{
		get { return GetDoubleOrNull("d") ?? double.NaN; }
		set
		{
			SetDouble("d", Math.Clamp(value, 0, 30));
		}
	}

	protected virtual void SetParams(XElement e)
	{
		X = GetDoubleOrNull(e, "x") ?? double.NaN;
		Y = GetDoubleOrNull(e, "y") ?? double.NaN;
		Width = GetDoubleOrNull(e, "w") ?? double.NaN;
		Height = GetDoubleOrNull(e, "h") ?? double.NaN;
		Rotation = GetDoubleOrNull(e, "a") ?? double.NaN;
		TriggeredBy = GetDoubleOrNull(e, "b") ?? LevelXML.TriggeredBy.Nothing;
		Elt.SetAttributeValue("t", Type);
		RepeatType = GetDoubleOrNull(e, "r") ?? LevelXML.RepeatType.Never;
		StartDisabled = GetBoolOrNull(e, "sd") ?? false;
		if (RepeatType == RepeatType.Continuous || RepeatType == RepeatType.Permanent) 
		{
			Interval = GetDoubleOrNull(e, "i") ?? double.NaN;
		}
	}
	
	internal Trigger(XElement e) : base("t")
	{
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
	}
}

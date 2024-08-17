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
		get { return GetDouble("x"); }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			SetDouble("x", value);
		}
	}

	public override double Y
	{
		get { return GetDouble("y"); }
		set
		{
			SetDouble("y", value);
		}
	}

	public double Width
	{
		get { return GetDouble("w"); }
		set
		{
			SetDouble("w", Math.Clamp(value, 5, 5000));
		}
	}

	public double Height
	{
		get { return GetDouble("h"); }
		set
		{
			SetDouble("h", Math.Clamp(value, 5, 5000));
		}
	}

	public double Rotation
	{
		get { return GetDouble("a"); }
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
		get { return (TriggeredBy)GetDouble("b"); }
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
		get { return (RepeatType)GetDouble("r"); }
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
		get { return GetBool("sd"); }
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
	public double? Interval
	{
		get { return GetDoubleOrNull("i"); }
		set
		{
			if (value is not null)
			{
				Elt.SetAttributeValue("i", value);
			}
		}
	}

	/// <summary>
	/// This is the amount of time in seconds after the trigger gets activated, before its actions fire.
	/// </summary>
	public double Delay
	{
		get { return GetDouble("d"); }
		set
		{
			SetDouble("d", Math.Clamp(value, 0, 30));
		}
	}

	private List<Target> lst;
	public IReadOnlyList<Target> Targets => lst;
	/// <summary>
	/// Append a target to this trigger's list of targets
	/// </summary>
	/// <param name="target"></param>
	public void AddTarget(Target target) { InsertTarget(lst.Count, target); }
	/// <summary>
	/// Insert a target into the list of targets at a particular index.
	/// The position indicates the order in which the actions fire ingame.
	/// </summary>
	/// <param name="index"></param>
	/// <param name="target"></param>
	public void InsertTarget(int index, Target target) 
	{  
		Target? sameTarget = lst
			.Where(other => other.Targeted == target.Targeted)
			.FirstOrDefault();
		if (sameTarget is not null)
		{
			foreach (TriggerAction action in target.Actions)
			{
				sameTarget.AddAction(action);
			}
		} else
		{
			lst.Insert(index, target); 
		}
	}
	public bool RemoveTarget(Target target) { return lst.Remove(target); }
	public void RemoveTargetAt(int index) { lst.RemoveAt(index);}
	public void ClearTargets() { lst.Clear(); }
	public bool ContainsTarget(Target target) { return lst.Contains(target); }
	public int IndexOfTarget(Target target) { return lst.IndexOf(target); }
	public int TargetCount => lst.Count;

	// Override this in Sound and Victory trigger to only call base functionality if TriggeredBy.Targets
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Target target in lst)
		{
			target.PlaceInLevel(mapper);
			Elt.Add(target.Elt);
		}
	}

	internal override void FinishConstruction()
	{
		lst.ForEach(target => target.FinishConstruction());
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
	
	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	internal Trigger(XElement e, Func<XElement, Entity> reverseMapper=default!, params Target[] targets) : base("t")
	{
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
		// If your trigger has elements, you need to pass a ReverseMapper to parse them
		lst = new(targets.Concat(e.Elements().Select(targetTag => Target.FromXElement(targetTag, reverseMapper))).ToArray());
	}
}

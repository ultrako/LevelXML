using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// A trigger is an entity that under certain circumstances can be activated,
/// and when it does, it goes through and applies its list of Targets.
///</summary>
public class Trigger : Entity, IList<Target>
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
	private List<Target> lst;
	public void Add(Target target) {
		Target? sameTarget = lst
			.Where(other => other.Targeted == target.Targeted)
			.FirstOrDefault();
		if (sameTarget is not null)
		{
			foreach (TriggerAction action in target)
			{
				sameTarget.Add(action);
			}
		} else
		{
			lst.Add(target); 
		}
	}
	public void Insert(int index, Target target) { lst.Insert(index, target); }
	public bool Remove(Target target) { return lst.Remove(target); }
	public void RemoveAt(int index) { lst.RemoveAt(index);}
	public void Clear() { lst.Clear(); }
	public bool Contains(Target target) { return lst.Contains(target); }
	public void CopyTo(Target[] targets, int size) { CopyTo(targets, size); }
	public int IndexOf(Target target) { return lst.IndexOf(target); }
	public int Count => lst.Count;
	public bool IsReadOnly => false;
	public Target this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	IEnumerator<Target> IEnumerable<Target>.GetEnumerator() { return lst.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return lst.GetEnumerator();}
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
	public double? Action
	{
		get { return GetDoubleOrNull("t"); }
		set
		{
			// If it's not there, or set to NaN, or set out of range, the level freezes on start
			// So we'll throw an exception for bad values of t
			if (value is null || double.IsNaN((double)value!) || (value < 1) || (value > 3) )
			{
				throw new Exception("This trigger would cause the level to freeze on start!");
			}
			double val = (double)value!;
			Elt.SetAttributeValue("t", (int)Math.Clamp(val, 1, 3));
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
	public double? Sound
	{
		get { return GetDoubleOrNull("s"); }
		set
		{
			// I forget the exact number, it's not 341, check this later
			if (value < 0 || value > 341)
			{
				throw new Exception("Sound number is invalid!");
			}
			if (value is not null)
			{
				Elt.SetAttributeValue("s", value);
			}
		}
	}
	public double? SoundLocation
	{
		get { return GetDoubleOrNull("l"); }
		set
		{
			if (value is double val)
			{
				Elt.SetAttributeValue("l", (int)Math.Clamp(val, 1.0, 2.0));
			}
		}
	}
	public double? Volume
	{
		get { return GetDoubleOrNull("v"); }
		set
		{
			if (value is double val)
			{
				Elt.SetAttributeValue("v", Math.Clamp(val, 0.0, 1.0));
			}
		}
	}
	protected void SetParams(XElement e)
	{
		X = GetDoubleOrNull(e, "x");
		Y = GetDoubleOrNull(e, "y");
		Width = GetDoubleOrNull(e, "w");
		Height = GetDoubleOrNull(e, "h");
		Rotation = GetDoubleOrNull(e, "a");
		TriggeredBy = GetDoubleOrNull(e, "b");
		Action = GetDoubleOrNull(e, "t");
		RepeatType = GetDoubleOrNull(e, "r");
		StartDisabled = GetBoolOrNull(e, "sd");
		if (RepeatType == 4) 
		{
			Interval = GetDoubleOrNull(e, "i");
		}
		Delay = GetDoubleOrNull(e, "d");
	}
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Target target in lst)
		{
			target.PlaceInLevel(mapper);
			Elt.Add(target.Elt);
		}
	}
	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	public Trigger(string xml=EditorDefault, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : this(StrToXElement(xml), ReverseMapper, targets: targets) {}
	internal Trigger(XElement e, Func<XElement, Entity> reverseMapper=default!, params Target[] targets) : base("t", e.Elements(), targets)
	{
		if (e.Name.ToString() != "t")
		{
			throw new ArgumentException("Did not give a trigger to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
		// If your trigger has elements, you need to pass a ReverseMapper to parse them
		lst = new(targets.Concat(e.Elements().Select(targetTag => Target.FromXElement(targetTag, reverseMapper))).ToArray());
	}
	internal override void FinishConstruction()
	{
		lst.ForEach(target => target.finishConstruction());
	}
}

using System.Xml.Linq;

namespace HappyWheels;
public class Trigger : Entity
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
	private List<Target> lst;
	public void Add(Target target) { lst.Add(target); }
	public void Insert(int index, Target target) { lst.Insert(index, target); }
	public void Remove(Target target) { lst.Remove(target); }
	public void IndexOf(Target target) { lst.IndexOf(target); }
	public Target this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	public override double? x
	{
		get { return GetDoubleOrNull("x"); }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			double val = value ?? double.NaN;
			elt.SetAttributeValue("x", val);
		}
	}
	public override double? y
	{
		get { return GetDoubleOrNull("y"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("y", val);
		}
	}
	public double? Width
	{
		get { return GetDoubleOrNull("w"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("w", Math.Clamp(val, 5, 5000));
		}
	}
	public double? Height
	{
		get { return GetDoubleOrNull("h"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("h", Math.Clamp(val, 5, 5000));
		}
	}
	public double? Rotation
	{
		get { return GetDoubleOrNull("a"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("a", val);
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
				elt.SetAttributeValue("b", val); 
			}
			else 
			{
				elt.SetAttributeValue("b", (int)Math.Clamp(val, 1, 6));
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
			elt.SetAttributeValue("t", (int)Math.Clamp(val, 1, 3));
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
			elt.SetAttributeValue("r", Math.Clamp(val, 1, 4));
		}
	}
	public HWBool? StartDisabled
	{
		get { return GetBoolOrNull("sd") ?? HWBool.False; }
		set
		{
			if (value == true)
			{
				elt.SetAttributeValue("sd", HWBool.True);
			}
			else
			{
				elt.SetAttributeValue("sd", HWBool.False);
			}
		}
	}
	public double? Interval
	{
		get { return GetDoubleOrNull("i"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("i", val);
		}
	}
	public double? Delay
	{
		get { return GetDoubleOrNull("d"); }
		set
		{
			double val = value ?? double.NaN;
			elt.SetAttributeValue("d", Math.Clamp(val, 0, 30));
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
				elt.SetAttributeValue("s", value);
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
				elt.SetAttributeValue("l", (int)Math.Clamp(val, 1.0, 2.0));
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
				elt.SetAttributeValue("v", Math.Clamp(val, 0.0, 1.0));
			}
		}
	}
	protected void setParams(XElement e)
	{
		x = GetDoubleOrNull(e, "x");
		y = GetDoubleOrNull(e, "y");
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
		elt.RemoveNodes();
		foreach (Target target in lst)
		{
			target.PlaceInLevel(mapper);
			elt.Add(target.elt);
		}
	}
	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	public Trigger(string xml=EditorDefault, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : this(StrToXElement(xml), ReverseMapper, targets: targets) {}
	internal Trigger(XElement e, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : base("t", e.Elements(), targets)
	{
		if (e.Name.ToString() != "t")
		{
			throw new Exception("Did not give a trigger to the constructor!");
		}
		elt = new XElement(e.Name.ToString());
		setParams(e);
		// If your trigger has elements, you need to pass a ReverseMapper to parse them
		lst = new(targets.Concat(e.Elements().Select(targetTag => Target.FromXElement(targetTag, ReverseMapper))).ToArray());
	}
	internal override void finishConstruction()
	{
		lst.ForEach(target => target.finishConstruction());
	}
}

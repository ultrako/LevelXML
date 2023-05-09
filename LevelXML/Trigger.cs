using System.Xml.Linq;

namespace HappyWheels;
public class Trigger : Entity
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
	private List<Target> lst;
	public void Add(Target target) { lst.Add(target); }
	public void Remove(Target target) { lst.Remove(target); }
	public void IndexOf(Target target) { lst.IndexOf(target); }
	public Target this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	public override float? x
	{
		get { return GetFloatOrNull("x"); }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			float val = value ?? float.NaN;
			elt.SetAttributeValue("x", val);
		}
	}
	public override float? y
	{
		get { return GetFloatOrNull("y"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("y", val);
		}
	}
	public float? Width
	{
		get { return GetFloatOrNull("w"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("w", Math.Clamp(val, 5, 5000));
		}
	}
	public float? Height
	{
		get { return GetFloatOrNull("h"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("h", Math.Clamp(val, 5, 5000));
		}
	}
	public float? Rotation
	{
		get { return GetFloatOrNull("a"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("a", val);
		}
	}
	public float? TriggeredBy
	{
		get { return GetFloatOrNull("b"); }
		set
		{
			// Having NaN in triggeredBy is fine, 
			// it just works like "triggered only by other triggers"
			float val = value ?? float.NaN;
			if (float.IsNaN(val)) 
			{ 
				elt.SetAttributeValue("b", val); 
			}
			else 
			{
				elt.SetAttributeValue("b", (int)Math.Clamp(val, 1, 6));
			}
		}
	}
	public float? Action
	{
		get { return GetFloatOrNull("t"); }
		set
		{
			// If it's not there, or set to NaN, or set out of range, the level freezes on start
			// So we'll throw an exception for bad values of t
			if (value is null || float.IsNaN((float)value!) || (value < 1) || (value > 3) )
			{
				throw new Exception("This trigger would cause the level to freeze on start!");
			}
			float val = (float)value!;
			elt.SetAttributeValue("t", (int)Math.Clamp(val, 1, 3));
		}
	}
	public float? RepeatType
	{
		get { return GetFloatOrNull("r"); }
		set
		{
			// If this is null or NaN or 0, then the trigger can't ever be activated
			if (value is null || float.IsNaN((float)value!))
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
			if (value is HWBool.True)
			{
				elt.SetAttributeValue("sd", FormatBool(HWBool.True));
			}
			else
			{
				elt.SetAttributeValue("sd", FormatBool(HWBool.False));
			}
		}
	}
	public float? Interval
	{
		get { return GetFloatOrNull("i"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("i", val);
		}
	}
	public float? Delay
	{
		get { return GetFloatOrNull("d"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("d", Math.Clamp(val, 0, 30));
		}
	}
	protected void setParams(XElement e)
	{
		x = GetFloatOrNull(e, "x");
		y = GetFloatOrNull(e, "y");
		Width = GetFloatOrNull(e, "w");
		Height = GetFloatOrNull(e, "h");
		Rotation = GetFloatOrNull(e, "a");
		TriggeredBy = GetFloatOrNull(e, "b");
		Action = GetFloatOrNull(e, "t");
		RepeatType = GetFloatOrNull(e, "r");
		StartDisabled = GetBoolOrNull(e, "sd");
		if (RepeatType == 4) 
		{
			Interval = GetFloatOrNull(e, "i");
		}
		Delay = GetFloatOrNull(e, "d");
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
}

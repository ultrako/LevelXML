using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// An ActivateTrigger is an entity that when activated,
/// goes through and applies its list of Targets.
///</summary>
public class ActivateTrigger : Trigger
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
    internal override uint Type => 1;
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

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.RemoveNodes();
		foreach (Target target in lst)
		{
			target.PlaceInLevel(mapper);
			Elt.Add(target.Elt);
		}
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Delay = GetDoubleOrNull(e, "d") ?? double.NaN;
    }
	public ActivateTrigger(params Target[] targets) : this(EditorDefault, default!, targets) {}
	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	internal ActivateTrigger(string xml=EditorDefault, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : this(StrToXElement(xml), ReverseMapper, targets: targets) {}
	internal ActivateTrigger(XElement e, Func<XElement, Entity> reverseMapper=default!, params Target[] targets) : base(e)
	{
		// If your trigger has elements, you need to pass a ReverseMapper to parse them
		lst = new(targets.Concat(e.Elements().Select(targetTag => Target.FromXElement(targetTag, reverseMapper))).ToArray());
	}
	internal override void FinishConstruction()
	{
		lst.ForEach(target => target.finishConstruction());
	}
}

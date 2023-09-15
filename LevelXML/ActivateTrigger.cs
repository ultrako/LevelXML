using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// An ActivateTrigger is an entity that when activated,
/// goes through and applies its list of Targets.
///</summary>
public class ActivateTrigger : Trigger, IList<Target>
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
    internal override uint Type => 1;
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
	public void CopyTo(Target[] targets, int index) { lst.CopyTo(targets, index); }
	public int IndexOf(Target target) { return lst.IndexOf(target); }
	public int Count => lst.Count;
	public bool IsReadOnly => false;
	public Target this[int index] { get { return lst[index]; } set { lst[index] = value; } }
	IEnumerator<Target> IEnumerable<Target>.GetEnumerator() { return lst.GetEnumerator(); }
	IEnumerator IEnumerable.GetEnumerator() { return lst.GetEnumerator();}

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
        Delay = GetDoubleOrNull(e, "d");
    }

	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	public ActivateTrigger(string xml=EditorDefault, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : this(StrToXElement(xml), ReverseMapper, targets: targets) {}
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

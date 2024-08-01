using System.Xml.Linq;
using System.Collections;

namespace LevelXML;
/// <summary>
/// An ActivateTrigger is an entity that when activated,
/// goes through and applies its list of Targets.
/// </summary>
public class ActivateTrigger : Trigger
{
	public const string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
    internal override uint Type => 1;

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
	internal ActivateTrigger(XElement e, Func<XElement, Entity> reverseMapper=default!, params Target[] targets) : base(e, reverseMapper, targets) {}
}

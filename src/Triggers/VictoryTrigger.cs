using System.Xml.Linq;

namespace LevelXML;

/// <summary>
/// A VictoryTrigger will cause the level victory if activated.
/// </summary>
public class VictoryTrigger : Trigger, IConvertibleToXML
{
    public const string EditorDefault =
    @"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""3"" r=""1"" sd=""f""/>";
    internal override uint Type => 3;

    internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		if (TriggeredBy == TriggeredBy.Targets)
		{
			base.PlaceInLevel(mapper);
		}
	}

    public string ToXML() { return ToXML(mapper:default!); }

    internal VictoryTrigger(XElement e) : base(e) {}
	
	public VictoryTrigger(params Target[] targets) : this(EditorDefault, default!, targets) {}
	internal VictoryTrigger(XElement e, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : base(e, ReverseMapper, targets) {}
	internal VictoryTrigger(string xml=EditorDefault, Func<XElement, Entity> ReverseMapper=default!, params Target[] targets) : this(StrToXElement(xml), ReverseMapper, targets: targets) {}
}
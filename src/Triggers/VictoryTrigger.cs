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

    public string ToXML() { return ToXML(mapper:default!); }

    internal VictoryTrigger(XElement e) : base(e) {}
	
	public VictoryTrigger(string xml=EditorDefault) : this(StrToXElement(xml)) {}
}
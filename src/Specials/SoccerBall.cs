using System.Xml.Linq;

namespace LevelXML;

public class SoccerBall : Special
{  
    public const string EditorDefault =
    @"<sp t=""10"" p0=""0"" p1=""0"" />";
    internal override uint Type => 10;

    public SoccerBall(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal SoccerBall(XElement e) : base(e)
    {
        SetParams(e);
    }
}
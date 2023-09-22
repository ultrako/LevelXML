using System.Xml.Linq;

namespace HappyWheels;

public class BuildingOne : Building
{
    internal override uint Type => 13;
    public const string EditorDefault =
    @"<sp t=""13"" p0=""0"" p1=""0"" p2=""1"" p3=""3""/>";

    public BuildingOne(string xml=EditorDefault) : base(xml) {}

    internal BuildingOne(XElement e) : base(e) {}
}
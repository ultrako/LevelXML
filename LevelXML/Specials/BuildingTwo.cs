using System.Xml.Linq;

namespace HappyWheels;

public class BuildingTwo : Building
{
    internal override uint Type => 14;
    public const string EditorDefault =
    @"<sp t=""14"" p0=""0"" p1=""0"" p2=""1"" p3=""3""/>";

    public BuildingTwo(string xml=EditorDefault) : base(xml) {}

    internal BuildingTwo(XElement e) : base(e) {}
}
using System.Xml.Linq;
namespace LevelXML;
/// <summary>
/// Polygons are Shapes defined by a list of Vertices.
/// </summary>
public class Polygon : CustomShape
{
	internal override uint Type => 3;
	public const string EditorDefault =
        @"<sh t=""3"" p0=""0"" p1=""0"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />";

	public Polygon(params Vertex[] vertices) : this(EditorDefault, vertices) {}
	internal Polygon(string xml, params Vertex[] vertices) : this(StrToXElement(xml), vertMapper: default!, vertices) {}
	internal Polygon(XElement e, Func<Entity, int> vertMapper, params Vertex[] vertices) : base(e, vertMapper, vertices) {}
}

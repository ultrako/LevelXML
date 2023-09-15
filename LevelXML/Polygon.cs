using System.Xml.Linq;
namespace HappyWheels;
///<summary>
/// Polygons are shapes defined by a list of points.
///</summary>
public class Polygon : CustomShape
{
	internal override uint Type => 3;
	public static string EditorDefault =
        @"<sh t=""3"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />";

	public Polygon() : this(EditorDefault) {}
	public Polygon(string xml) : this(StrToXElement(xml)) {}
	internal Polygon(XElement e) : base(e) 
	{
		if (GetDoubleOrNull(e, "t") != Type)
		{
			throw new LevelXMLException("Did not give a polygon to the constructor!");
		}
	}
}

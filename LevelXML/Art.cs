using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;
///<summary>
/// Art shapes have no collision, and their edges are defined with a list of Vertices
///</summary>
public class Art : CustomShape
{
	internal override uint Type => 4;
	public static string EditorDefault =
        @"<sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";
	
	public Art() : this(EditorDefault) {}
	public Art(string xml) : this(StrToXElement(xml)) {}
	internal Art(XElement e) : base(e) 
	{
		if (GetDoubleOrNull(e, "t") != Type)
		{
			throw new LevelXMLException("Did not give an art shape to the constructor!");
		}
	}
}

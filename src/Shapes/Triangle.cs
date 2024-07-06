using System.Xml.Linq;

namespace LevelXML;

/// <summary>
/// A triangle shape
/// </summary>
public class Triangle : Shape, IConvertibleToXML
{
	internal override uint Type => 2;
	public static string EditorDefault =
        @"<sh t=""2"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";

	public string ToXML() { return ToXML(mapper: default!); }

	public override double Width
	{
		get { return GetDoubleOrNull("p2") ?? 100; }
		set
		{
			if (double.IsNaN(value)) 
			{
                throw new LevelXMLException("This would make the triangle disappear!");
			} 
			SetDouble("p2", Math.Clamp(value, 5, 5000));
		}
	}

	public override double Height
	{
		get { return GetDoubleOrNull("p3") ?? 300; }
		set
		{
			if (double.IsNaN(value)) {
				throw new LevelXMLException("This would make the triangle disappear!");
			} 
			SetDouble("p3", Math.Clamp(value, 15, 5000));
		}
	}

	public Triangle() : this(EditorDefault) {}

	public Triangle(string xml) : this(StrToXElement(xml)) {}
	
	internal Triangle(XElement e) : base(e)
	{
		SetParams(e);
	}
}

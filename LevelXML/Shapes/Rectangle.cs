using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// A rectangle shape
/// </summary>
public class Rectangle : Shape
{
	internal override uint Type => 0;
	public static string EditorDefault =
        @"<sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";

	public string ToXML() { return ToXML(mapper: default!); }

	public override double Width
	{
		get { return GetDoubleOrNull("p2") ?? 100; }
		set
		{
			if (double.IsNaN(value)) 
			{
                throw new LevelXMLException("This would make the rectangle disappear!");
			} 
			SetDouble("p2", Math.Clamp(value, 5, 5000));
		}
	}

	public override double Height
	{
		get { return GetDoubleOrNull("p3") ?? 100; }
		set
		{
			if (double.IsNaN(value)) {
				throw new LevelXMLException("This would make the rectangle disappear!");
			} 
			SetDouble("p3", Math.Clamp(value, 5, 5000));
		}
	}

	public Rectangle() : this(EditorDefault) {}

	public Rectangle(string xml) : this(StrToXElement(xml)) {}
	
	internal Rectangle(XElement e) : base(e)
	{
		SetParams(e);
	}
}

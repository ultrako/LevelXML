using System.Xml.Linq;

namespace LevelXML;

/// <summary>
/// A triangle shape
/// </summary>
public class Triangle : Shape, IConvertibleToXML
{
	internal override uint Type => 2;
	public const string EditorDefault =
        @"<sh t=""2"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";

	public override double Width
	{
		get { return GetDouble("p2"); }
		set
		{

			SetDouble("p2", Math.Clamp(value, 5, 5000));
		}
	}

	// Should I consider clamping height based on Width?
	// Or should I just let the game do that for me?
	public override double Height
	{
		get { return GetDouble("p3"); }
		set
		{
			SetDouble("p3", Math.Clamp(value, 15, 5000));
		}
	}

	public Triangle() : this(EditorDefault) {}

	public Triangle(string xml) : this(StrToXElement(xml)) {}

	protected void SetParams(XElement e)
    {
        SetFirstParams(e);
		Width = GetDoubleOrNull(e, "p2") ?? 100;
        Height = GetDoubleOrNull(e, "p3") ?? 100;
		SetLastParams(e);
    }
	
	internal Triangle(XElement e) : base(e)
	{
		SetParams(e);
	}
}

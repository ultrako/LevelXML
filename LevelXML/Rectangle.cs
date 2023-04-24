using System.Xml.Linq;
namespace HappyWheels;

public class Rectangle : Shape
{
	public override uint Type => 0;
	public static string EditorDefault =
        @"<sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";
	private uint _width;
	private uint _height;
	public override float? Width
	{
		get { return _width; }
		set
		{
			float val = value ?? 100;
			if (float.IsNaN(val)) {
				throw new Exception("This would make the rectangle disappear!");
			} 
			_width = (uint)Math.Clamp(val, 5, 5000);
		}
	}
	public override float? Height
	{
		get { return _height; }
		set
		{
			float val = value ?? 100;
			if (float.IsNaN(val)) {
				throw new Exception("This would make the rectangle disappear!");
			} 
			_height = (uint)Math.Clamp(val, 5, 5000);
		}
	}
	// If you call new without any args (no xml tag, no XElement),
	// then this will just give you the same shape that
	// you would have gotten in the level editor by default.
	public Rectangle() : this(EditorDefault) {}
	public Rectangle(string xml) : this(StrToXElement(xml)) {}
	Rectangle(XElement elt)
	{
		if (elt.Name.ToString() != "sh" || (uint?)getFloatOrNull(elt, "t") != 0)
		{
			throw new Exception("Did not give a rectangle to the constructor!");
		}
		setParams(elt);
	}
}

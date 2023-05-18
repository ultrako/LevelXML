using System.Xml.Linq;
namespace HappyWheels;

public class Rectangle : Shape
{
	public override uint Type => 0;
	public static string EditorDefault =
        @"<sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>";
	public override double? Width
	{
		get { return GetDoubleOrNull("p2"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new Exception("This would make the rectangle disappear!");
			} 
			elt.SetAttributeValue("p2", Math.Clamp(val, 5, 5000));
		}
	}
	public override double? Height
	{
		get { return GetDoubleOrNull("p3"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new Exception("This would make the rectangle disappear!");
			} 
			elt.SetAttributeValue("p3", Math.Clamp(val, 5, 5000));
		}
	}
	// If you call new without any args (no xml tag, no XElement),
	// then this will just give you the same shape that
	// you would have gotten in the level editor by default.
	public Rectangle() : this(EditorDefault) {}
	public Rectangle(string xml) : this(StrToXElement(xml)) {}
	internal Rectangle(XElement e)
	{
		if (e.Name.ToString() != "sh" || GetDoubleOrNull(e, "t") != 0)
		{
			//Console.WriteLine($"Name was {elt.Name.ToString()}, and type number was {GetDoubleOrNull(e, "t")}");
			throw new Exception("Did not give a rectangle to the constructor!");
		}
		elt = new XElement(e.Name.ToString());
		setParams(e);
	}
}

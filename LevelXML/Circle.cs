using System.Xml.Linq;
namespace HappyWheels;

public class Circle : Shape
{
	internal override uint Type => 1;
    public static string EditorDefault =
        @"<sh t=""1"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>";
	public string ToXML() { return ToXML(mapper: default!); }
	public override double? Width
	{
		get { return GetDoubleOrNull("p2"); }
		set
		{
			double val = value ?? 100;
			if (double.IsNaN(val)) {
				throw new LevelXMLException("This would make the circle disappear!");
			}
            double clamped = Math.Clamp(val, 5, 5000);
			Elt.SetAttributeValue("p2", clamped);
            Elt.SetAttributeValue("p3", clamped);
		}
	}
	public override double? Height
	{
		get { return Width; }
		set { Width = value; }
	}

    public double? Cutout {
		get { return (double?)GetDoubleOrNull("p12"); }
		set
		{
			double val = value ?? 0;
			Elt.SetAttributeValue("p12", Math.Clamp(val, 0, 100));
		}
	}

	public Circle() : this(EditorDefault) {}
	public Circle(string xml) : this(StrToXElement(xml)) {}
	internal Circle(XElement e)
	{
		if (e.Name.ToString() != "sh" || GetDoubleOrNull(e, "t") != Type)
		{
			throw new LevelXMLException("Did not give a circle to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
		SetParams(e);
        Cutout = GetDoubleOrNull(e, "p12");
	}
}

using System.Xml.Linq;
namespace HappyWheels;

/// <summary>
/// A circle shape
/// </summary>
public class Circle : Shape, IConvertibleToXML
{
	internal override uint Type => 1;
    public static string EditorDefault =
        @"<sh t=""1"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>";
	public string ToXML() { return ToXML(mapper: default!); }
	public override double Width
	{
		get { return GetDoubleOrNull("p2") ?? 100; }
		set
		{
			if (double.IsNaN(value)) {
				throw new LevelXMLException("This would make the circle disappear!");
			}
            double clamped = Math.Clamp(value, 5, 5000);
			Elt.SetAttributeValue("p2", clamped);
            Elt.SetAttributeValue("p3", clamped);
		}
	}
	public override double Height
	{
		get { return Width; }
		set { Width = value; }
	}

    public double Cutout {
		get { return (double?)GetDoubleOrNull("p12") ?? 0; }
		set
		{
			SetDouble("p12", Math.Clamp(value, 0, 100));
		}
	}

	public Circle() : this(EditorDefault) {}
	public Circle(string xml) : this(StrToXElement(xml)) {}
	protected override void SetParams(XElement e)
	{
		base.SetParams(e);
		Cutout = GetDoubleOrNull(e, "p12") ?? 0;
	}
	internal Circle(XElement e) : base(e)
	{
		SetParams(e);
	}
}

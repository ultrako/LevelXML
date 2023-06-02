using System.Xml.Linq;
namespace HappyWheels;

public class TextBox : Special
{
    public override uint Type => 16;
    public static string EditorDefault =
        @"<sp t=""16"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[HERE'S SOME TEXT]]></p7>
        </sp>";
    private XElement contentElement;
    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new Exception("That would make the text box disappear!");
			}
			Elt.SetAttributeValue("p2", Math.Clamp(val,-180,180)); 
		}
	}
	public double? Color
	{
		get { return GetDoubleOrNull("p3");}
		set
		{
			Elt.SetAttributeValue("p3", value ?? 0);
		}
	}
    public double? Font
    {
        get { return GetDoubleOrNull("p4");}
        set
        {
            Elt.SetAttributeValue("p4", Math.Clamp((value ?? 1), 1, 5));
        }
    }
    public double? FontSize
    {
        get { return GetDoubleOrNull("p5");}
        set
        {
            Elt.SetAttributeValue("p5", Math.Clamp((value ?? 10), 10, 100));
        }
    }
    public double? Alignment
    {
        get { return GetDoubleOrNull("p6");}
        set
        {
            Elt.SetAttributeValue("p6", Math.Clamp((value ?? 1), 1, 3));
        }
    }
    public double? Opacity
	{
		get { return GetDoubleOrNull("p8"); }
		set
		{
			// If the opacity isn't set, the import box sets it to 100
			double val = value ?? 100;
			// If the opacity is set to NaN, the import box sets it to 0
			Elt.SetAttributeValue("p8", Math.Clamp(val, 0, 100));
		}
	}
    public string Content
    {
        get
        {
            return (contentElement.FirstNode as XCData)!.Value;
        }
        set
        {
            (contentElement.FirstNode as XCData)!.Value = value;
        }
    }
    private void SetParams(XElement e)
    {
        Elt.SetAttributeValue("t", Type);
        X = GetDoubleOrNull(e, "p0");
        Y = GetDoubleOrNull(e, "p1");
        Rotation = GetDoubleOrNull(e, "p2");
        Color = GetDoubleOrNull(e, "p3");
        Font = GetDoubleOrNull(e, "p4");
        FontSize = GetDoubleOrNull(e, "p5");
        Alignment = GetDoubleOrNull(e, "p6");
        Opacity = GetDoubleOrNull(e, "p8");
        Content = (e.Element("p7")!.FirstNode as XCData)!.Value;
    }
    internal TextBox(XElement e)
	{
		if (e.Name.ToString() != "sp" || GetDoubleOrNull(e, "t") != 16)
		{
			//Console.WriteLine($"Name was {elt.Name.ToString()}, and type number was {GetDoubleOrNull(e, "t")}");
			throw new Exception("Did not give a textbox to the constructor!");
		}
		Elt = new XElement(e.Name.ToString());
        contentElement = new("p7", new XCData(""));
        Elt.Add(contentElement);
		SetParams(e);
	}
    public TextBox() : this(EditorDefault) {}
	public TextBox(string xml) : this(StrToXElement(xml)) {}
}
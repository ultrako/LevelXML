using System.Xml.Linq;
namespace LevelXML;

/// <summary>
/// This is a non interactive Special that displays text.
/// </summary>
public class TextBox : Special
{
    internal override uint Type => 16;
    public static string EditorDefault =
        @"<sp t=""16"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[HERE'S SOME TEXT]]></p7>
        </sp>";
    private XElement contentElement;

    public double Rotation
	{
		get { return GetDoubleOrNull("p2") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("That would make the text box disappear!");
			}
			SetDouble("p2", value); 
		}
	}

	public double Color
	{
		get { return GetDoubleOrNull("p3") ?? 0;}
		set
		{
			SetDouble("p3", value);
		}
	}

    public double Font
    {
        get { return GetDoubleOrNull("p4") ?? 1;}
        set { SetDouble("p4", Math.Clamp(value, 1, 5)); }
    }

    public double FontSize
    {
        get { return GetDoubleOrNull("p5") ?? 10;}
        set { SetDouble("p5", Math.Clamp(value, 10, 100)); }
    }

    /// <summary>
    /// Whether the text is left, center, or right aligned, corresponding to 1, 2, and 3 respectively.
    /// </summary>
    public double Alignment
    {
        get { return GetDoubleOrNull("p6") ?? 1;}
        set { SetDouble("p6", Math.Clamp(value, 1, 3)); }
    }

    public double Opacity
	{
		get { return GetDoubleOrNull("p8") ?? 100; }
		set { SetDouble("p8", Math.Clamp(value, 0, 100)); }
	}

    /// <summary>
    ///  The text that this textbox holds
    /// </summary>
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

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Color = GetDoubleOrNull(e, "p3") ?? 0;
        Font = GetDoubleOrNull(e, "p4") ?? 1;
        FontSize = GetDoubleOrNull(e, "p5") ?? 10;
        Alignment = GetDoubleOrNull(e, "p6") ?? 1;
        Opacity = GetDoubleOrNull(e, "p8") ?? 100;
        Content = ((e.Element("p7") ?? new XElement("p7"))!.FirstNode as XCData)!.Value;
    }

    internal TextBox(XElement e) : base(e)
	{
        contentElement = new("p7", new XCData(""));
        Elt.Add(contentElement);
		SetParams(e);
	}

    public TextBox() : this(EditorDefault) {}
    
	public TextBox(string xml) : this(StrToXElement(xml)) {}
}
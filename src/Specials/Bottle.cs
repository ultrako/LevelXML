using System.Xml.Linq;

namespace LevelXML;

public class Bottle : Special
{
    internal override uint Type => 20;
    public const string EditorDefault = 
    @"<sp t=""20"" p0=""0"" p1=""0"" p2=""0"" p3=""1"" p4=""f"" p5=""t""/>";

    public double Rotation
	{
		get { return GetDoubleOrNull("p2") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", value); 
		}
	}

    public BottleType BottleType
    {
        get { return (BottleType?)GetDoubleOrNull("p3") ?? LevelXML.BottleType.Green; }
        set { Elt.SetAttributeValue("p3", value); }
    }

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p4") ?? false; }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public HWBool Interactive
	{
		get { return GetBoolOrNull("p5") ?? true; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        BottleType = GetDoubleOrNull(e, "p3") ?? LevelXML.BottleType.Green;
        Sleeping = GetBoolOrNull(e, "p4") ?? false;
        Interactive = GetBoolOrNull(e, "p5") ?? true;
    }

    public Bottle(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Bottle(XElement e) : base(e)
    {
        SetParams(e);
    }
}
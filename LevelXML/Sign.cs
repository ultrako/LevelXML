using System.Xml.Linq;

namespace HappyWheels;

public class Sign : Special
{
    internal override uint Type => 23;
    public const string EditorDefault = 
    @"<sp t=""23"" p0=""0"" p1=""0"" p2=""0"" p3=""1"" p4=""t""/>";

    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", val); 
		}
	}

    public SignType? SignType
    {
        get { return (SignType?)GetDoubleOrNull("p3"); }
        set { Elt.SetAttributeValue("p3", (SignType?)value ?? HappyWheels.SignType.RightArrow); }
    }

    public HWBool? ShowSignPost
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.True); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        SignType = GetDoubleOrNull(e, "p3");
        ShowSignPost = GetBoolOrNull(e, "p4");
    }

    public Sign(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Sign(XElement e) : base(e)
    {
        SetParams(e);
    }
}
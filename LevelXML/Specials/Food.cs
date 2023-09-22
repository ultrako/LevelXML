using System.Xml.Linq;

namespace HappyWheels;

public class Food : SimpleSpecial
{
    internal override uint Type => 32;
    public const string EditorDefault = 
    @"<sp t=""32"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t"" p5=""1""/>";

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

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", value ?? HWBool.False); }
	}

    public HWBool? Interactive
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.True); }
	}

    public FoodType? FoodType
    {
        get { return (FoodType?)GetDoubleOrNull("p5"); }
        set { Elt.SetAttributeValue("p5", (FoodType?)value ?? HappyWheels.FoodType.Watermelon); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Sleeping = GetBoolOrNull(e, "p3");
        Interactive = GetBoolOrNull(e, "p4");
        FoodType = GetDoubleOrNull(e, "p5");
    }

    public Food(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Food(XElement e) : base(e)
    {
        SetParams(e);
    }
}
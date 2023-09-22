using System.Xml.Linq;

namespace HappyWheels;

public class BladeWeapon : SimpleSpecial
{
    internal override uint Type => 34;
    public const string EditorDefault = 
    @"<sp t=""34"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""f"" p5=""t"" p6=""1""/>";

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

    public HWBool? Reverse
    {
        get { return GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", value ?? HWBool.False); }
    }

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p4"); }
		set { Elt.SetAttributeValue("p4", value ?? HWBool.False); }
	}

    public HWBool? Interactive
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.True); }
	}

    public WeaponType? WeaponType
    {
        get { return (WeaponType?)GetDoubleOrNull("p6"); }
        set { Elt.SetAttributeValue("p6", (WeaponType?)value ?? HappyWheels.WeaponType.Axe); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Reverse = GetBoolOrNull(e, "p3");
        Sleeping = GetBoolOrNull(e, "p4");
        Interactive = GetBoolOrNull(e, "p5");
        WeaponType = GetDoubleOrNull(e, "p6");
    }

    public BladeWeapon(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal BladeWeapon(XElement e) : base(e)
    {
        SetParams(e);
    }
}
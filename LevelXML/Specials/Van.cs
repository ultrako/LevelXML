using System.Xml.Linq;

namespace HappyWheels;

public class Van : SimpleSpecial
{
    internal override uint Type => 0;
    public const string EditorDefault = 
    @"<sp t=""0"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t""/>";

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

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p3") ?? false; }
		set { Elt.SetAttributeValue("p3", value); }
	}

    public HWBool Interactive
	{
		get { return GetBoolOrNull("p4") ?? true; }
		set { Elt.SetAttributeValue("p4", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Sleeping = GetBoolOrNull(e, "p3") ?? false;
        Interactive = GetBoolOrNull(e, "p4") ?? true;
    }

    public Van(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Van(XElement e) : base(e)
    {
        SetParams(e);
    }
}
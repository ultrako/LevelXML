using System.Xml.Linq;

namespace LevelXML;

public class TrashCan : Special, IRotatable, IGroupable
{
    internal override uint Type => 26;
    public const string EditorDefault = 
    @"<sp t=""26"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t"" p5=""t""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{
			SetDouble("p2", value); 
		}
	}

    public HWBool Sleeping
	{
		get { return GetBool("p3"); }
		set { Elt.SetAttributeValue("p3", value); }
	}

    public HWBool Interactive
	{
		get { return GetBool("p4"); }
		set { Elt.SetAttributeValue("p4", value); }
	}

    public HWBool HasTrash
    {
        get { return GetBool("p5"); }
		set { Elt.SetAttributeValue("p5", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Sleeping = GetBoolOrNull(e, "p3") ?? false;
        Interactive = GetBoolOrNull(e, "p4") ?? true;
        HasTrash = GetBoolOrNull(e, "p5") ?? true;
    }

    public TrashCan(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal TrashCan(XElement e) : base(e)
    {
        SetParams(e);
    }
}
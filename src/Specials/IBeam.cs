using System.Xml.Linq;

namespace LevelXML;

public class IBeam : Special
{
    internal override uint Type => 3;
    public const string EditorDefault = 
    @"<sp t=""3"" p0=""0"" p1=""0"" p2=""400"" p3=""32"" p4=""0"" p5=""f"" p6=""f"" />";

    public double Width
    {
        get { return GetDouble("p2"); }
        set
        {
            SetDouble("p2", Math.Clamp(value, 200, 1600));
        }
    }

    public double Height
    {
        get { return GetDouble("p3"); }
        set
        {
            SetDouble("p3", Math.Clamp(value, 32, 64));
        }
    }

    public double Rotation
	{
		get { return GetDouble("p4"); }
		set 
		{
			SetDouble("p4", value); 
		}
	}

     public HWBool Fixed
	{
		get { return GetBool("p5"); }
		set { Elt.SetAttributeValue("p5", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBool("p6"); }
		set { Elt.SetAttributeValue("p6", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? 400;
        Height = GetDoubleOrNull(e, "p3") ?? 32;
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
        Fixed = GetBoolOrNull(e, "p5") ?? false;
        Sleeping = GetBoolOrNull(e, "p6") ?? false;
    }

    public IBeam(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal IBeam(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace LevelXML;

public class Chain : Special
{
    internal override uint Type => 30;
    public const string EditorDefault = 
    @"<sp t=""30"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t"" p5=""20"" p6=""1"" p7=""0""/>";

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

    /// <summary>
    ///  This is the amount of links the chain has.
    /// </summary>
    public double LinkCount
    {
        get { return GetDouble("p5"); }
        set { SetDouble("p5", Math.Clamp(value, 2, 40)); }
    }

    /// <summary>
    ///  This is how large the links of the chain are.
    /// </summary>
    public double LinkScale
    {
        get { return GetDouble("p6"); }
        set
        {
            SetDouble("p6", Math.Clamp(value, 1, 10));
        }
    }

    /// <summary>
    /// This is the angle that each link intersects its neighbors
    /// </summary>
    public double Curve
    {
        get { return GetDouble("p7"); }
        set
        {
            SetDouble("p7", Math.Clamp(value, -10, 10));
        }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Sleeping = GetBoolOrNull(e, "p3") ?? false;
        Interactive = GetBoolOrNull(e, "p4") ?? true;
        LinkCount = GetDoubleOrNull(e, "p5") ?? 20;
        LinkScale = GetDoubleOrNull(e, "p6") ?? 1;
        Curve = GetDoubleOrNull(e, "p7") ?? 0;
    }

    public Chain(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Chain(XElement e) : base(e)
    {
        SetParams(e);
    }
}
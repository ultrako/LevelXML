using System.Xml.Linq;

namespace LevelXML;

public class Log : Special, IRotatable, IScaleable
{
    internal override uint Type => 4;
    public const string EditorDefault = 
    @"<sp t=""4"" p0=""0"" p1=""0"" p2=""36"" p3=""400"" p4=""0"" p5=""f"" p6=""f""/>";

    public double Width
    {
        get { return GetDouble("p2"); }
        set
        {
            SetDouble("p2", Math.Clamp(value, 36, 54));
        }
    }

    public double Height
    {
        get { return GetDouble("p3"); }
        set
        {
            SetDouble("p3", Math.Clamp(value, 200, 600));
        }
    }

    public double Rotation
	{
		get { return GetDoubleOrNull("p4") ?? 0; }
		set 
		{
			SetDouble("p4", value); 
		}
	}

     public HWBool Fixed
	{
		get { return GetBoolOrNull("p5") ?? false; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p6") ?? false; }
		set { Elt.SetAttributeValue("p6", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? 36;
        Height = GetDoubleOrNull(e, "p3") ?? 400;
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
        Fixed = GetBoolOrNull(e, "p5") ?? false;
        Sleeping = GetBoolOrNull(e, "p6") ?? false;
    }

    public Log(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Log(XElement e) : base(e)
    {
        SetParams(e);
    }
}
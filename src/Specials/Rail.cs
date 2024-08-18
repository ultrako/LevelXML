using System.Xml.Linq;

namespace LevelXML;

public class Rail : Special, IRotatable, IScaleable
{
    internal override uint Type => 4;
    public const string EditorDefault = 
    @"<sp t=""27"" p0=""0"" p1=""0"" p2=""250"" p3=""18"" p4=""0""/>";

    public double Width
    {
        get { return GetDouble("p2"); }
        set
        {
            SetDouble("p2", Math.Clamp(value, 100, 2000));
        }
    }

    public double Height
    {
        get { return GetDouble("p3"); }
        set
        {
            SetDouble("p3", Math.Clamp(value, 18, 18));
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

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? 250;
        Height = GetDoubleOrNull(e, "p3") ?? 18;
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
    }

    public Rail(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Rail(XElement e) : base(e)
    {
        SetParams(e);
    }
}
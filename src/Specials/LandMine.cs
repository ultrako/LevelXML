using System.Xml.Linq;

namespace LevelXML;

public class Landmine : Special, IRotatable
{
    internal override uint Type => 2;
    public const string EditorDefault = 
    @"<sp t=""2"" p0=""0"" p1=""0"" p2=""0"" />";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{
			SetDouble("p2", value); 
		}
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
    }

    public Landmine(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Landmine(XElement e) : base(e)
    {
        SetParams(e);
    }
}


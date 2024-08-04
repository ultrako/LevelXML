using System.Xml.Linq;

namespace LevelXML;

public class Fan : Special
{
    internal override uint Type => 8;
    public const string EditorDefault = 
    @"<sp t=""8"" p0=""0"" p1=""0"" p2=""0""/>";

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

    public Fan(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Fan(XElement e) : base(e)
    {
        SetParams(e);
    }
}
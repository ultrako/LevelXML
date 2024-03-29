using System.Xml.Linq;

namespace HappyWheels;

public class HomingMine : Special
{
    internal override uint Type => 25;
    public const string EditorDefault = 
    @"<sp t=""25"" p0=""0"" p1=""0"" p2=""1"" p3=""0""/>";

    public double Speed
    {
        get { return GetDoubleOrNull("p2") ?? 1; }
        set { SetDouble("p2", Math.Clamp(value, 1, 10)); }
    }

    public double Delay
    {
        get { return GetDoubleOrNull("p3") ?? 0; }
        set { SetDouble("p3", Math.Clamp(value, 0, 5)); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Speed = GetDoubleOrNull(e, "p2") ?? 1;
        Delay = GetDoubleOrNull(e, "p3") ?? 0;
    }

    public HomingMine(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal HomingMine(XElement e) : base(e)
    {
        SetParams(e);
    }
}
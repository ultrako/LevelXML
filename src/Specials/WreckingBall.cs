using System.Xml.Linq;

namespace LevelXML;

public class WreckingBall : Special
{
    internal override uint Type => 7;
    public const string EditorDefault = 
    @"<sp t=""7"" p0=""0"" p1=""0"" p2=""350"" />";

    /// <summary>
    /// The length of the wrecking ball's rope
    /// </summary>
    public double RopeLength
    {
        get { return GetDoubleOrNull("p2") ?? 350; }
        set { SetDouble("p2", Math.Clamp(value, 200, 1000)); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        RopeLength = GetDoubleOrNull(e, "p2") ?? 350;
    }

    public WreckingBall(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal WreckingBall(XElement e) : base(e)
    {
        SetParams(e);
    }
}
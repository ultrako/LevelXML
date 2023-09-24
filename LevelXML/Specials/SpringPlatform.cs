using System.Xml.Linq;

namespace HappyWheels;

public class SpringPlatform : Special
{
    internal override uint Type => 5;
    public const string EditorDefault = 
    @"<sp t=""5"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" />";

    public double Rotation
    {
        get { return GetDoubleOrNull("p2") ?? 0; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
            }
            SetDouble("p2", value);
        }
    }

    public double Delay
    {
        get { return GetDoubleOrNull("p3") ?? 0; }
        set
        {
            SetDouble("p3", Math.Clamp(value, 0, 2));
        }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Delay = GetDoubleOrNull(e, "p3") ?? 0;
    }

    public SpringPlatform(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal SpringPlatform(XElement e) : base(e)
    {
        SetParams(e);
    }
}
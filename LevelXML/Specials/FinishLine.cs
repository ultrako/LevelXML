using System.Xml.Linq;

namespace HappyWheels;

public class FinishLine : Special
{
    internal override uint Type => 9;
    public const string EditorDefault = 
    @"<sp t=""9"" p0=""0"" p1=""0"" />";

    public FinishLine(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal FinishLine(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace LevelXML;

public class Token : Special
{
    internal override uint Type => 31;
    public const string EditorDefault = 
    @"<sp t=""31"" p0=""0"" p1=""0"" p2=""1""/>";

    public TokenType TokenType
    {
        get { return (TokenType?)GetDoubleOrNull("p2") ?? LevelXML.TokenType.Skull; }
        set { Elt.SetAttributeValue("p2", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        TokenType = GetDoubleOrNull(e, "p2") ?? LevelXML.TokenType.Skull;
    }

    public Token(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Token(XElement e) : base(e)
    {
        SetParams(e);
    }
}
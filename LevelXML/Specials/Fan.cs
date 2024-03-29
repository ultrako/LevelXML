using System.Xml.Linq;

namespace HappyWheels;

public class Fan : Special
{
    internal override uint Type => 8;
    public const string EditorDefault = 
    @"<sp t=""8"" p0=""0"" p1=""0"" p2=""0""/>";

    public double Rotation
	{
		get { return GetDoubleOrNull("p2") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
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
using System.Xml.Linq;

namespace LevelXML;

public class Rail : Special
{
    internal override uint Type => 4;
    public const string EditorDefault = 
    @"<sp t=""27"" p0=""0"" p1=""0"" p2=""250"" p3=""18"" p4=""0""/>";

    public double Width
    {
        get { return GetDoubleOrNull("p2") ?? 250; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the width to NaN would make the special disappear!");
            }
            SetDouble("p2", Math.Clamp(value, 100, 2000));
        }
    }

    public double Height
    {
        get { return GetDoubleOrNull("p3") ?? 18; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the height to NaN would make the special disappear!");
            }
            SetDouble("p3", Math.Clamp(value, 18, 18));
        }
    }

    public double Rotation
	{
		get { return GetDoubleOrNull("p4") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
			}
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
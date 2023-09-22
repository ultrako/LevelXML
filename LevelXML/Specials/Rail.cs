using System.Xml.Linq;

namespace HappyWheels;

public class Rail : SimpleSpecial
{
    internal override uint Type => 4;
    public const string EditorDefault = 
    @"<sp t=""27"" p0=""0"" p1=""0"" p2=""250"" p3=""18"" p4=""0""/>";

    public double? Width
    {
        get { return GetDoubleOrNull("p2"); }
        set
        {
            double val = value ?? 250;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the width to NaN would make the special disappear!");
            }
            SetDouble("p2", Math.Clamp(val, 100, 2000));
        }
    }

    public double? Height
    {
        get { return GetDoubleOrNull("p3"); }
        set
        {
            double val = value ?? 18;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the height to NaN would make the special disappear!");
            }
            SetDouble("p3", Math.Clamp(val, 18, 18));
        }
    }

    public double? Rotation
	{
		get { return GetDoubleOrNull("p4"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
			}
			SetDouble("p4", val); 
		}
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2");
        Height = GetDoubleOrNull(e, "p3");
        Rotation = GetDoubleOrNull(e, "p4");
    }

    public Rail(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Rail(XElement e) : base(e)
    {
        SetParams(e);
    }
}
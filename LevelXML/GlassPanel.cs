using System.Xml.Linq;

namespace HappyWheels;

public class GlassPanel : Special
{
    internal override uint Type => 18;
    public const string EditorDefault = 
    @"<sp t=""18"" p0=""0"" p1=""0"" p2=""10"" p3=""100"" p4=""0"" p5=""f"" p6=""10"" p7=""t""/>";

    public double? Width
    {
        get { return GetDoubleOrNull("p2"); }
        set
        {
            double val = value ?? 10;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the width to NaN would make this glass panel disappear!");
            }
            SetDouble("p2", Math.Clamp(val, 5, 50));
        }
    }

    public double? Height
    {
        get { return GetDoubleOrNull("p3"); }
        set
        {
            double val = value ?? 100;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the height to NaN would make this glass panel disappear!");
            }
            SetDouble("p3", Math.Clamp(val, 50, 500));
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
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p4", val); 
		}
	}

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.False); }
	}

    /// <summary>
    /// Determines how much force is required to shatter the initial glass pane.
    /// </summary>
    public double? Strength
    {
        get { return GetDoubleOrNull("p6"); }
        set
        {
            SetDouble("p6", Math.Clamp(value ?? 10, 1, 10));
        }
    }

    /// <summary>
    /// Determines if broken glass can stab the character.
    /// </summary>
    public HWBool? Stabbing
    {
        get { return GetBoolOrNull("p7"); }
		set { Elt.SetAttributeValue("p7", value ?? HWBool.True); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2");
        Height = GetDoubleOrNull(e, "p3");
        Rotation = GetDoubleOrNull(e, "p4");
        Sleeping = GetBoolOrNull(e, "p5");
        Strength = GetDoubleOrNull(e, "p6");
        Stabbing = GetBoolOrNull(e, "p7");
    }

    public GlassPanel(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal GlassPanel(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace LevelXML;

public class GlassPanel : Special
{
    internal override uint Type => 18;
    public const string EditorDefault = 
    @"<sp t=""18"" p0=""0"" p1=""0"" p2=""10"" p3=""100"" p4=""0"" p5=""f"" p6=""10"" p7=""t""/>";

    public double Width
    {
        get { return GetDouble("p2"); }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the width to NaN would make this glass panel disappear!");
            }
            SetDouble("p2", Math.Clamp(value, 5, 50));
        }
    }

    public double Height
    {
        get { return GetDouble("p3"); }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the height to NaN would make this glass panel disappear!");
            }
            SetDouble("p3", Math.Clamp(value, 50, 500));
        }
    }

    public double Rotation
	{
		get { return GetDouble("p4"); }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p4", value); 
		}
	}

    public HWBool Sleeping
	{
		get { return GetBool("p5"); }
		set { Elt.SetAttributeValue("p5", value); }
	}

    /// <summary>
    /// Determines how much force is required to shatter the initial glass pane.
    /// </summary>
    public double Strength
    {
        get { return GetDouble("p6"); }
        set
        {
            SetDouble("p6", Math.Clamp(value, 1, 10));
        }
    }

    /// <summary>
    /// Determines if broken glass can stab the character.
    /// </summary>
    public HWBool Stabbing
    {
        get { return GetBool("p7"); }
		set { Elt.SetAttributeValue("p7", value); }
    }

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? 10;
        Height = GetDoubleOrNull(e, "p3") ?? 100;
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
        Sleeping = GetBoolOrNull(e, "p5") ?? false;
        Strength = GetDoubleOrNull(e, "p6") ?? 10;
        Stabbing = GetBoolOrNull(e, "p7") ?? true;
    }

    public GlassPanel(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal GlassPanel(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace HappyWheels;

public class SpikeSet : SimpleSpecial
{
    internal override uint Type => 6;
    public const string EditorDefault = 
    @"<sp t=""6"" p0=""0"" p1=""0"" p2=""0"" p3=""t"" p4=""20"" p5=""f""/>";


    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
			}
			SetDouble("p2", val); 
		}
	}

    public HWBool? Fixed
	{
		get { return GetBoolOrNull("p3"); }
		set { Elt.SetAttributeValue("p3", value ?? HWBool.True); }
	}

    /// <summary>
    /// This is the amount of spikes the spike set has.
    /// </summary>
    public double? Spikes
    {
        get { return GetDoubleOrNull("p4"); }
        set
        {
            double val = value ?? 20;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("Setting the number of spikes to NaN would cause the spike set to disappear!");
            }
            SetDouble("p4", val);
        }
    }

    public HWBool? Sleeping
	{
		get { return GetBoolOrNull("p5"); }
		set { Elt.SetAttributeValue("p5", value ?? HWBool.False); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Fixed = GetBoolOrNull(e, "p3");
        Spikes = GetDoubleOrNull(e, "p4");
        Sleeping = GetBoolOrNull(e, "p5");
    }

    public SpikeSet(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal SpikeSet(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace HappyWheels;

public class SpikeSet : SimpleSpecial
{
    internal override uint Type => 6;
    public const string EditorDefault = 
    @"<sp t=""6"" p0=""0"" p1=""0"" p2=""0"" p3=""t"" p4=""20"" p5=""f""/>";


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

    public HWBool Fixed
	{
		get { return GetBoolOrNull("p3") ?? true; }
		set { Elt.SetAttributeValue("p3", value); }
	}

    /// <summary>
    /// This is the amount of spikes the spike set has.
    /// </summary>
    public double Spikes
    {
        get { return GetDoubleOrNull("p4") ?? 20; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the number of spikes to NaN would cause the spike set to disappear!");
            }
            SetDouble("p4", value);
        }
    }

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p5") ?? false; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Fixed = GetBoolOrNull(e, "p3") ?? true;
        Spikes = GetDoubleOrNull(e, "p4") ?? 20;
        Sleeping = GetBoolOrNull(e, "p5") ?? false;
    }

    public SpikeSet(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal SpikeSet(XElement e) : base(e)
    {
        SetParams(e);
    }
}
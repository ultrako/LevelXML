using System.Xml.Linq;

namespace HappyWheels;

public class Boost : SimpleSpecial
{
    internal override uint Type => 12;
    public const string EditorDefault = 
    @"<sp t=""12"" p0=""0"" p1=""0"" p2=""0"" p3=""2"" p4=""20""/>";

    public double? Rotation
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 0;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", val); 
		}
	}

    /// <summary>
    /// This is the number of panels the boost has.
    /// This is directly related to how wide the boost is.
    /// </summary>
    public double? Panels
	{
		get { return GetDoubleOrNull("p3"); }
		set 
        { 
            double val = value ?? 2;
            if (double.IsNaN(val))
            {
                throw new LevelXMLException("That would make the boost disappear!");
            }
            Elt.SetAttributeValue("p3", Math.Clamp(val, 1, 6)); 
        }
	}

    public double? Speed
	{
		get { return GetDoubleOrNull("p4"); }
		set 
        {
            double val = value ?? 20;
            Elt.SetAttributeValue("p4", Math.Clamp(val, 10, 100)); 
        }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2");
        Panels = GetDoubleOrNull(e, "p3");
        Speed = GetDoubleOrNull(e, "p4");
    }

    public Boost(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Boost(XElement e) : base(e)
    {
        SetParams(e);
    }
}
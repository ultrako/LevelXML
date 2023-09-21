using System.Xml.Linq;

namespace HappyWheels;

public abstract class Building : Special
{
    /// <summary>
    /// How wide the building is (how many times the texture loops).
    /// </summary>
    public double? FloorWidth
	{
		get { return GetDoubleOrNull("p2"); }
		set 
		{ 
			double val = value ?? 1;
			if (double.IsNaN(val)) 
			{
				throw new LevelXMLException("That would make the special disappear!");
			}
			SetDouble("p2", Math.Clamp(val, 1, 10)); 
		}
	}

    /// <summary>
    /// How many floors tall the building is.
    /// </summary>
    public double? Floors
	{
		get { return GetDoubleOrNull("p3"); }
		set 
        {
            double val = value ?? 3; 
            SetDouble("p3", Math.Clamp(val, 3, 50)); 
        }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        FloorWidth = GetDoubleOrNull(e, "p2");
        Floors = GetDoubleOrNull(e, "p3");
    }

    public Building(string xml) : this(StrToXElement(xml)) {}

    internal Building(XElement e) : base(e)
    {
        SetParams(e);
    }
}
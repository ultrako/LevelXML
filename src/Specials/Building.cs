using System.Xml.Linq;

namespace LevelXML;

public abstract class Building : Special
{
    /// <summary>
    /// How wide the building is (how many times the texture loops).
    /// </summary>
    public double FloorWidth
	{
		get { return GetDouble("p2"); }
		set 
		{
			SetDouble("p2", Math.Clamp(value, 1, 10)); 
		}
	}

    /// <summary>
    /// How many floors tall the building is.
    /// </summary>
    public double Floors
	{
		get { return GetDouble("p3"); }
		set 
        {
            SetDouble("p3", Math.Clamp(value, 3, 50)); 
        }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        FloorWidth = GetDoubleOrNull(e, "p2") ?? 1;
        Floors = GetDoubleOrNull(e, "p3") ?? 3;
    }

    public Building(string xml) : this(StrToXElement(xml)) {}

    internal Building(XElement e) : base(e)
    {
        SetParams(e);
    }
}
using System.Xml.Linq;

namespace HappyWheels;

public class Boost : Special
{
    internal override uint Type => 12;
    public const string EditorDefault = 
    @"<sp t=""12"" p0=""0"" p1=""0"" p2=""0"" p3=""2"" p4=""20""/>";

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

    /// <summary>
    /// This is the number of panels the boost has.
    /// This is directly related to how wide the boost is.
    /// </summary>
    public double Panels
	{
		get { return GetDoubleOrNull("p3") ?? 2; }
		set 
        { 
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("That would make the boost disappear!");
            }
            Elt.SetAttributeValue("p3", Math.Clamp(value, 1, 6)); 
        }
	}

    public double Speed
	{
		get { return GetDoubleOrNull("p4") ?? 20; }
		set { Elt.SetAttributeValue("p4", Math.Clamp(value, 10, 100)); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
        Panels = GetDoubleOrNull(e, "p3") ?? 2;
        Speed = GetDoubleOrNull(e, "p4") ?? 20;
    }

    public Boost(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Boost(XElement e) : base(e)
    {
        SetParams(e);
    }
}
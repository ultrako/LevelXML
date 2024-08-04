using System.Xml.Linq;

namespace LevelXML;

public class Boost : Special
{
    internal override uint Type => 12;
    public const string EditorDefault = 
    @"<sp t=""12"" p0=""0"" p1=""0"" p2=""0"" p3=""2"" p4=""20""/>";

    public double Rotation
	{
		get { return GetDouble("p2"); }
		set 
		{ 
			SetDouble("p2", value); 
		}
	}

    /// <summary>
    /// This is the number of panels the boost has.
    /// This is directly related to how wide the boost is.
    /// </summary>
    public double Panels
	{
		get { return GetDouble("p3"); }
		set 
        {
            Elt.SetAttributeValue("p3", Math.Clamp(value, 1, 6)); 
        }
	}

    public double Speed
	{
		get { return GetDouble("p4"); }
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
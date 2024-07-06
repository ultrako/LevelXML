using System.Xml.Linq;

namespace LevelXML;

public class IBeam : Special
{
    internal override uint Type => 3;
    public const string EditorDefault = 
    @"<sp t=""3"" p0=""0"" p1=""0"" p2=""400"" p3=""32"" p4=""0"" p5=""f"" p6=""f"" />";

    public double Width
    {
        get { return GetDoubleOrNull("p2") ?? 400; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the width to NaN would make the special disappear!");
            }
            SetDouble("p2", Math.Clamp(value, 200, 1600));
        }
    }

    public double Height
    {
        get { return GetDoubleOrNull("p3") ?? 32; }
        set
        {
            if (double.IsNaN(value))
            {
                throw new LevelXMLException("Setting the height to NaN would make the special disappear!");
            }
            SetDouble("p3", Math.Clamp(value, 32, 64));
        }
    }

    public double Rotation
	{
		get { return GetDoubleOrNull("p4") ?? 0; }
		set 
		{ 
			if (double.IsNaN(value)) 
			{
				throw new LevelXMLException("Setting the rotation to NaN would make the special disappear!");
			}
			SetDouble("p4", value); 
		}
	}

     public HWBool Fixed
	{
		get { return GetBoolOrNull("p5") ?? false; }
		set { Elt.SetAttributeValue("p5", value); }
	}

    public HWBool Sleeping
	{
		get { return GetBoolOrNull("p6") ?? false; }
		set { Elt.SetAttributeValue("p6", value); }
	}

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Width = GetDoubleOrNull(e, "p2") ?? 400;
        Height = GetDoubleOrNull(e, "p3") ?? 32;
        Rotation = GetDoubleOrNull(e, "p4") ?? 0;
        Fixed = GetBoolOrNull(e, "p5") ?? false;
        Sleeping = GetBoolOrNull(e, "p6") ?? false;
    }

    public IBeam(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal IBeam(XElement e) : base(e)
    {
        SetParams(e);
    }
}
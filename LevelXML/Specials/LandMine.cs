using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// A landmine object.
/// As a reminder, this special has no trigger actions.
/// Pointing a trigger to it with no actions will cause
/// the landmine to detonate when the trigger is activated.
/// </summary>
public class Landmine : Special
{
    internal override uint Type => 2;
    public const string EditorDefault = 
    @"<sp t=""2"" p0=""0"" p1=""0"" p2=""0"" />";

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

    protected override void SetParams(XElement e)
    {
        base.SetParams(e);
        Rotation = GetDoubleOrNull(e, "p2") ?? 0;
    }

    public Landmine(string xml=EditorDefault) : this(StrToXElement(xml)) {}

    internal Landmine(XElement e) : base(e)
    {
        SetParams(e);
    }
}


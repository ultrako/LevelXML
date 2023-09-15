using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// A SoundTrigger plays a sound when activated.
///</summary>
public class SoundTrigger : Trigger
{
    public const string EditorDefault =
    @"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""2"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""1"" p=""0"" v=""1""/>";
	internal override uint Type => 2;
	// Potentially make a struct for this (though that'd be a lot of work)
	public double? Sound
	{
		get { return GetDoubleOrNull("s"); }
		set
		{
			// I forget the exact number, it's not 341, check this later
			if (value < 0 || value > 341)
			{
				throw new Exception("Sound number is invalid!");
			}
			if (value is not null)
			{
				Elt.SetAttributeValue("s", value);
			}
		}
	}
	// Make a struct for this
	public double? SoundLocation
	{
		get { return GetDoubleOrNull("l"); }
		set
		{
			if (value is double val)
			{
				Elt.SetAttributeValue("l", (int)Math.Clamp(val, 1.0, 2.0));
			}
		}
	}
    public double? Panning
    {
        get { return GetDoubleOrNull("p"); }
        // Test this behavior later
        set
        {
            Elt.SetAttributeValue("p", Math.Clamp(value ?? 0, -1.0, 1.0));
        }
    }
	public double? Volume
	{
		get { return GetDoubleOrNull("v"); }
		set
		{
			if (value is double val)
			{
				Elt.SetAttributeValue("v", Math.Clamp(val, 0.0, 1.0));
			}
		}
	}
	protected override void SetParams(XElement e)
	{
		base.SetParams(e);
        Sound = GetDoubleOrNull(e, "s");
		Delay = GetDoubleOrNull(e, "d");
        SoundLocation = GetDoubleOrNull(e, "l");
        Panning = GetDoubleOrNull(e, "p");
        Volume = GetDoubleOrNull(e, "v");
	}
    internal SoundTrigger(XElement e) : base(e) {}
	public SoundTrigger(string xml=EditorDefault) : this(StrToXElement(xml)) {}
	
}

using System.Xml.Linq;
using System.Collections;

namespace HappyWheels;
///<summary>
/// A SoundTrigger plays a sound when activated.
///</summary>
public class SoundTrigger : Trigger, IConvertibleToXML
{
    public const string EditorDefault =
    @"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""2"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""1"" p=""0"" v=""1""/>";
	internal override uint Type => 2;
	// Potentially make a struct for this (though that'd be a lot of work)

	/// <summary>
	/// The ID of the sound to be played
	/// I'd make a struct for this but there are 326 sounds in the game...
	/// </summary>
	public double Sound
	{
		get { return GetDoubleOrNull("s") ?? 0; }
		set
		{
			if (value < 0 || value > 325)
			{
				throw new LevelXMLException("Sound number is invalid! This trigger would freeze the level!");
			}
			SetDouble("s", value);
		}
	}
	
	/// <summary>
	///  Whether or not the sound is played as if coming from the location of the trigger
	/// </summary>
	public HWBool IsLocal
	{
		get 
		{
			double value = GetDoubleOrNull("l") ?? 1;
			return value > 1;
		}
		set
		{
			if (value == HWBool.True)
			{
				SetDouble("l", 2);
			}
			else
			{
				SetDouble("l", 1);
			}
		}
	}

	/// <summary>
	/// This makes the sound move from the left to the right if positive,
	/// vice versa if negative.
	/// </summary>
    public double Panning
    {
        get { return GetDoubleOrNull("p") ?? 0; }
        set
        {
            SetDouble("p", Math.Clamp(value, -1.0, 1.0));
        }
    }

	/// <summary>
	///  How loud the sound is, from 0 being silent, to 1 being full volume.
	/// </summary>
	public double Volume
	{
		get { return GetDoubleOrNull("v") ?? 1; }
		set
		{
			SetDouble("v", Math.Clamp(value, 0.0, 1.0));
		}
	}

	protected override void SetParams(XElement e)
	{
		base.SetParams(e);
        Sound = GetDoubleOrNull(e, "s") ?? 0;
		Delay = GetDoubleOrNull(e, "d") ?? double.NaN;
        IsLocal = (GetDoubleOrNull(e, "l") ?? 1) > 1;
        Panning = GetDoubleOrNull(e, "p") ?? 0;
        Volume = GetDoubleOrNull(e, "v") ?? 1;
	}

	public string ToXML() { return ToXML(mapper:default!); }

    internal SoundTrigger(XElement e) : base(e) {}
	
	public SoundTrigger(string xml=EditorDefault) : this(StrToXElement(xml)) {}
	
}

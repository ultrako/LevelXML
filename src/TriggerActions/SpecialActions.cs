using System.Xml.Linq;

namespace LevelXML;

/// <summary>
/// This action causes the targeted Harpoon to fire.
/// </summary>
public class FireHarpoon : TriggerAction, ITriggerAction<Harpoon>
{
	public FireHarpoon()
	{
		SetDouble("i", 0);
	}
}

/// <summary>
/// This action prevents a Harpoon from firing until activated by another trigger.
/// </summary>
public class DeactivateHarpoon : TriggerAction, ITriggerAction<Harpoon>
{
	public DeactivateHarpoon()
	{
		SetDouble("i", 1);
	}
}

/// <summary>
/// This action will allow a deactivated Harpoon to start firing again.
/// </summary>
public class ActivateHarpoon : TriggerAction, ITriggerAction<Harpoon>
{
	public ActivateHarpoon()
	{
		SetDouble("i", 2);
	}
}

/// <summary>
/// This trigger action slides a TextBox to a particular x and y position over time.
/// </summary>
public class Slide : TriggerAction, ITriggerAction<TextBox>
{
	public const string EditorDefault =
	@"<a i=""1"" p0=""1"" p1=""0"" p2=""0""/>";

	/// <summary>
	/// How long it will take the TextBox to slide to the specified position
	/// </summary>
	public double? Duration
	{
		get { return GetDoubleOrNull("p0"); }
		set { SetDouble("p0", value ?? 1); }
	}

	/// <summary>
	/// The X coordinate the TextBox will slide to
	/// </summary>
	public double? X
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 0); }
	}

	/// <summary>
	/// The Y coordinate the TextBox will slide to
	/// </summary>
	public double? Y
	{
		get { return GetDoubleOrNull("p2"); }
		set { SetDouble("p2", value ?? 0); }
	}

	public Slide(double duration, double x, double y)
	{
		Elt.SetAttributeValue("i", 1);
		Duration = duration;
		X = x;
		Y = y;
	}
	public Slide(string xml=EditorDefault) : this(StrToXElement(xml)) {}

	internal Slide(XElement e)
	{
		Elt.SetAttributeValue("i", 1);
		Duration = GetDoubleOrNull(e, "p0");
		X = GetDoubleOrNull(e, "p1");
		Y = GetDoubleOrNull(e, "p2");
	}
}

/// <summary>
/// This trigger action makes the NPC return to the pose set at the start of the level.
/// </summary>
public class HoldPose : TriggerAction, ITriggerAction<NonPlayerCharacter>
{
	public HoldPose()
	{
		SetDouble("i", 2);
	}
}

/// <summary>
/// This trigger action makes the NPC limp.
/// </summary>
public class ReleasePose : TriggerAction, ITriggerAction<NonPlayerCharacter>
{
	public ReleasePose()
	{
		SetDouble("i", 3);
	}
}

/// <summary>
/// This trigger action shatters a GlassPanel.
/// </summary>
public class Shatter : TriggerAction, ITriggerAction<GlassPanel>
{
	public Shatter()
	{
		SetDouble("i", 0);
	}
}
namespace LevelXML;

// None of the actions concerning triggers have any parameters

/// <summary>
/// This causes the target Trigger to be activated
/// </summary>
public class Activate : TriggerAction, ITriggerAction<Trigger>
{
	public Activate()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

/// <summary>
/// This causes the target Trigger to be disabled, 
/// requiring that another Trigger enable it before it can be activated again.
/// </summary>
public class Disable : TriggerAction, ITriggerAction<Trigger>
{
	public Disable()
	{
		Elt.SetAttributeValue("i", 1);
	}
}

/// <summary>
/// This causes the target Trigger to be enabled, so it can be activated in the future.
/// </summary>
public class Enable : TriggerAction, ITriggerAction<Trigger>
{
	public Enable()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

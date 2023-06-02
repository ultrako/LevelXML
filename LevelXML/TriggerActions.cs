namespace HappyWheels;

// None of the actions concerning triggers have any parameters

public class Activate : TriggerAction<Trigger>
{
	public Activate()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

public class Disable : TriggerAction<Trigger>
{
	public Disable()
	{
		Elt.SetAttributeValue("i", 1);
	}
}

public class Enable : TriggerAction<Trigger>
{
	public Enable()
	{
		Elt.SetAttributeValue("i", 2);
	}
}

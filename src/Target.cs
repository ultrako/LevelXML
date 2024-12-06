using System.Xml.Linq;
using System.Linq;
using System.Collections;
namespace LevelXML;

// Weird rules about covariance make it so I can't have a list of Target<T> with varying T,
// so I'll just make an abstract class Target that Target<T> derives from
/// <summary>
/// A Target is an Entity and a list of actions that a trigger does to that Entity.
/// Triggers cannot interleave actions between two different entities.
/// </summary>

public abstract class Target : LevelXMLTag 
{
	/// <summary>
	/// Which Entity the list of actions in this target will apply to.
	/// </summary>
			 
	public Entity Targeted {get; set;} = default!;

	public abstract void AddAction(ITriggerAction action);

	public abstract bool RemoveAction(ITriggerAction action);

	public abstract int IndexOfAction(ITriggerAction action);

	/// <summary>
	/// The actions that will happen to the Target Entity.
	/// </summary>
	public abstract IReadOnlyList<ITriggerAction> Actions { get; }

	protected Target(Entity e) : base(e.Elt.Name) { Targeted = e; }

	private readonly Task? setTargeted;

	protected Target(XElement e, Func<XElement, Entity> ReverseTargetMapper) : base(e.Name)
	{
		setTargeted = Task.Run( () => Targeted = ReverseTargetMapper(e));
	}

	// This following function is only called if this object is constructed with a string
	// In LevelXML, targets are set via indexes - but we can only go from index to entity
	// in the context of an entire level.
	internal void FinishConstruction() { setTargeted!.Wait(); }
	
	internal static Target FromXElement(XElement e, Func<XElement, Entity> ReverseTargetMapper)
	{
		return e.Name.ToString() switch
		{
			"sh" => new Target<Shape>(e, ReverseTargetMapper),
			"sp" => ReverseTargetMapper(e) switch 
			{
				SpikeSet => new Target<SpikeSet>(e, ReverseTargetMapper),
				Rail => new Target<Rail>(e, ReverseTargetMapper),
				Television => new Target<Television>(e, ReverseTargetMapper),
				Toilet => new Target<Toilet>(e, ReverseTargetMapper),
				IBeam => new Target<IBeam>(e, ReverseTargetMapper),
				Chain => new Target<Chain>(e, ReverseTargetMapper),
				TrashCan => new Target<TrashCan>(e, ReverseTargetMapper),
				Van => new Target<Van>(e, ReverseTargetMapper),
				Landmine => new Target<Landmine>(e, ReverseTargetMapper),
				WreckingBall => new Target<WreckingBall>(e, ReverseTargetMapper),
				BladeWeapon => new Target<BladeWeapon>(e, ReverseTargetMapper),
				Fan => new Target<Fan>(e, ReverseTargetMapper),
				Boost => new Target<Boost>(e, ReverseTargetMapper),
				Harpoon => new Target<Harpoon>(e, ReverseTargetMapper),
				TextBox => new Target<TextBox>(e, ReverseTargetMapper),
				NonPlayerCharacter => new Target<NonPlayerCharacter>(e, ReverseTargetMapper),
				GlassPanel => new Target<GlassPanel>(e, ReverseTargetMapper),
				HomingMine => new Target<HomingMine>(e, ReverseTargetMapper),
				Jet => new Target<Jet>(e, ReverseTargetMapper),
				Chair => new Target<Chair>(e, ReverseTargetMapper),
				Boombox => new Target<Boombox>(e, ReverseTargetMapper),
				DinnerTable => new Target<DinnerTable>(e, ReverseTargetMapper),
				Log => new Target<Log>(e, ReverseTargetMapper),
				Food => new Target<Food>(e, ReverseTargetMapper),
				Bottle => new Target<Bottle>(e, ReverseTargetMapper),
				Meteor => new Target<Meteor>(e, ReverseTargetMapper),
				Special => new Target<Special>(e, ReverseTargetMapper),
				_ => throw new InvalidImportException("Invalid special type pointed to by trigger!", e.ToString())
			},
			"g" => new Target<Group>(e, ReverseTargetMapper),
			"j" => new Target<Joint>(e, ReverseTargetMapper),
			"t" => new Target<Trigger>(e, ReverseTargetMapper),
			_ => throw new InvalidImportException("Invalid name for a trigger target!", e.ToString()),
		};
	}
}

public class Target<T> : Target where T : Entity
{
	private List<ITriggerAction<T>> lst;

	public override IReadOnlyList<ITriggerAction<T>> Actions => lst;

	public override void AddAction(ITriggerAction action) {
		// A trigger can only do one thing to another trigger
		if (typeof(T) == typeof(Trigger))
		{
			if (lst.Count > 0)
			{
				throw new LevelInvalidException("Tried to add a second action to a trigger!", this);
			}
		}
		if (action is ITriggerAction<T> act)
		{
			lst.Add(act); 
		} else
		{
			throw new LevelInvalidException("Tried to add an action of the wrong Entity type " + nameof(T), this);
		}
	}
	public override bool RemoveAction(ITriggerAction action) 
	{
		if (action is ITriggerAction<T> act)
		{
			return lst.Remove(act); 
		}
		return false;
	}
	public override int IndexOfAction(ITriggerAction action) 
	{ 
		if (action is ITriggerAction<T> act)
		{
			return lst.IndexOf(act);
		}
		return -1;
	}

	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.SetAttributeValue("i", mapper(Targeted));
		Elt.RemoveNodes();
		foreach (ITriggerAction<T> action in lst)
		{
			Elt.Add(StrToXElement(action.ToXML()));
		}
	}
	///<summary>
	/// You can construct a Target[T] out of an Entity of type T and a list of Action[T]
	///</summary>

	public Target(T targeted, params ITriggerAction<T>[] actions) : 
	base(targeted) 
	{
		if (typeof(T) == typeof(Trigger) && actions.Length > 1)
		{
			// The import box allows this, but it makes a really confusing double action,
			// and you can only change them together and it only activates once so it's
			// the same as just setting one action
			List<LevelXMLTag> faultyParties = new()
            {
                this
            };
			foreach (ITriggerAction action in actions)
			{
				faultyParties.Add((LevelXMLTag)action);
			}
			throw new LevelInvalidException("Triggers can only have one action applied to them per source trigger!", faultyParties.ToArray());
		}
		lst = new(actions);
	}
	// Mapper usually gives indices from Entities.
	// ReverseTargetMapper gives Entities from XElements with a name and a index
	internal Target(XElement e, Func<XElement, Entity> ReverseTargetMapper) :
		base(e, ReverseTargetMapper)
	{
		ITriggerAction<T>[] actions = e.Elements()
			.Select(tag => ITriggerAction<T>.FromXElement(tag))
			.ToArray();
		lst = new(actions);
	}
}

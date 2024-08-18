namespace LevelXML;

public static class LevelDiagnostics
{
    public static IEnumerable<LevelXMLException> GetExceptions(params Entity[] entities)
    {
        List<LevelXMLException> exceptions = new();
        GetExceptionsBase(entities, exceptions);
        return exceptions;
    }

    public static IEnumerable<LevelXMLException> GetExceptions(Level level)
    {
        List<LevelXMLException> exceptions = new();
        if (level.Character == Character.None)
        {
            exceptions.Add(new LevelWouldFreezeOnStartException(
                "Level cannot have a NaN character type.", 
                level));
        }
        GetExceptionsBase(level.Entities, exceptions);
        return exceptions;
    }

    private static void GetExceptionsBase(IEnumerable<Entity> entities, IList<LevelXMLException> exceptions)
    {
        foreach (Shape shape in entities.OfType<Shape>()
            .Concat(entities.OfType<Group>().SelectMany(group => group.Items).OfType<Shape>()))
        {
            CheckNaNDensityNonFixedShape(shape, exceptions);
        }
        foreach (Group group in entities.OfType<Group>())
        {
            CheckGroupHasOnlyGroupableEntities(group, exceptions);
        }
        foreach (Trigger trigger in entities.OfType<Trigger>())
        {
            foreach(Target target in trigger.Targets)
            {
                CheckTargetable(target.Targeted, target, exceptions);
            }
        }
        foreach (Special special in 
            entities.OfType<Special>()
            .Concat(entities.OfType<Group>().SelectMany(group => group.Items).OfType<Special>()))
        {
            CheckIfIsBottleWithInvalidType(special, exceptions);
            CheckIfIsFoodWithInvalidType(special, exceptions);
            CheckIfIsJetWithNaNPower(special, exceptions);
            CheckIfNonfixedNanLengthSpikeSet(special, exceptions);
        }
        foreach (Joint joint in entities.OfType<Joint>())
        {
            CheckIfNaNAngleSlidingJoint(joint, exceptions);
        }
    }

    private static void CheckGroupHasOnlyGroupableEntities(Group group, IList<LevelXMLException> exceptions)
    {
        foreach (Entity entity in group.Items)
        {
            CheckGroupableEntity(entity, exceptions);
        }
    }

    private static void CheckGroupableEntity(Entity entity, IList<LevelXMLException> exceptions)
	{
        bool levelFreeze = false;
        bool invisSpecialNotInGroup = false;
		if (entity is Shape) { return; }
        else if (entity is Van van)
        {
            if (van.Interactive) { levelFreeze = true; }
        }
		else if (entity is DinnerTable dinnerTable)
        {
            if (dinnerTable.Interactive) { levelFreeze = true; }
        }
		else if(entity is IBeam iBeam)
        {
            return;
        }
		else if(entity is SpikeSet)
        {
            // Setting it to fixed makes it not interactive in the group
            return;
        }
		else if(entity is TextBox)
        {
            return;
        }
		else if(entity is NonPlayerCharacter npc)
        {
            if (npc.Interactive) { levelFreeze = true; }
        }
		else if (entity is Chair chair)
        {
            if (chair.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if	(entity is Bottle bottle)
        {
            if (bottle.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if (entity is Television television)
        {
            if (television.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if (entity is Boombox boombox)
        {
            if (boombox.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if (entity is Sign)
        {
            return;
        }
		else if (entity is Toilet toilet)
        {
            // Invis also, but when broken the parts are visible
            if (toilet.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if (entity is TrashCan trashCan)
        {
            // Same as toilet
            if (trashCan.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if	(entity is ArrowGun)
        {
            // If this is fixed and in a group,
            // it's not collidable and it doesn't point towards anything,
            // it just always points up.
            return;
        }
		else if (entity is Food food)
        {
            if (food.Interactive) { invisSpecialNotInGroup = true; }
        }
		else if(entity is BladeWeapon)
		{
			return;
		}
        else
        {
            exceptions.Add(new LevelWouldFreezeOnStartException($"{entity.GetType().Name} are not allowed in groups.", entity));
        }
		if (levelFreeze)
        {
            exceptions.Add(new LevelWouldFreezeOnStartException($"{entity.GetType().Name} cannot be interactive and in groups.", entity));
        }
        if (invisSpecialNotInGroup)
        {
            exceptions.Add(new SpecialInvisibleAndNotInGroupException($"{entity.GetType().Name} cannot be interactive and in groups.", (Special)entity));
        }
	}

    private static void CheckTargetable(Entity entity, Target target, IList<LevelXMLException> exceptions)
    {
        if (entity is Special special)
        {
            CheckTargetableSpecial(special, target, exceptions);
        }
    }

    private static void CheckTargetableSpecial(Special special, Target target, IList<LevelXMLException> exceptions)
    {
        bool canBeTargeted = special switch
        {
            SpikeSet => true,
			Rail => true,
			Television => true,
			Toilet => true,
			IBeam => true,
			Chain => true,
			TrashCan => true,
			Van => true,
			Landmine => true,
			WreckingBall => true,
			BladeWeapon => true,
			Fan => true,
			Boost => true,
			Harpoon => true,
			TextBox => true,
			NonPlayerCharacter => true,
			GlassPanel => true,
			HomingMine => true,
			Jet => true,
			Chair => true,
			Boombox => true,
			DinnerTable => true,
			Log => true,
			Food => true,
			Bottle => true,
			Meteor => true,
            _ => false,
        };
        if (!canBeTargeted)
        {
            exceptions.Add(new LevelWouldFreezeOnStartException(
                $"{special.GetType().Name} cannot be pointed to by triggers.",
                target));
        }
    }

    private static void CheckIfIsBottleWithInvalidType(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Bottle bottle)
        {
            if (bottle.BottleType == BottleType.None)
            {
                exceptions.Add(new LevelWouldFreezeOnStartException("Bottle cannot have a NaN type.", bottle));
            }
        }
    }

    private static void CheckIfIsFoodWithInvalidType(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Food food)
        {
            if (food.FoodType == FoodType.None)
            {
                exceptions.Add(new LevelWouldFreezeOnStartException("Food cannot have a NaN type.", food));
            }
        }
    }

    private static void CheckIfIsJetWithNaNPower(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Jet jet)
        {
            if (double.IsNaN(jet.Power))
            {
                exceptions.Add(new EntityWouldBeBlackHoleException("Jet cannot have NaN power.", jet));
            }
        }
    }

    private static void CheckIfNonfixedNanLengthSpikeSet(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is SpikeSet spikes)
        {
            if (double.IsNaN(spikes.Spikes) && !spikes.Fixed)
            {
                exceptions.Add(new EntityWouldBeBlackHoleException("Nonfixed spike sets cannot have NaN spikes.", spikes));
            }
        }
    }

    private static void CheckIfNaNAngleSlidingJoint(Joint joint, IList<LevelXMLException> exceptions)
    {
        if (joint is SlidingJoint slidingJoint)
        {
            if (double.IsNaN(slidingJoint.Angle))
            {
                exceptions.Add(new EntityWouldBeBlackHoleException("A NaN angle in a sliding joint would black hole the attached entities.", slidingJoint));
            }
        }
    }

    private static void CheckNaNDensityNonFixedShape(Shape shape, IList<LevelXMLException> exceptions)
    {
        if (!shape.Fixed)
        {
            if (double.IsNaN(shape.Density))
            {
                exceptions.Add(new EntityWouldBeBlackHoleException("A NaN density in a non fixed shape makes it a black hole.", shape));
            }
        }
    }
}
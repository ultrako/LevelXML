namespace LevelXML;

/// <summary>
/// A class that helps with finding anything wrong about your level.
/// </summary>
public static class LevelDiagnostics
{
    /// <summary>
    /// Checks if there are any oddities (level would not start, entity would disappear or be a black hole)
    /// in the list of entities and returns them.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public static IEnumerable<LevelXMLException> GetExceptions(params Entity[] entities)
    {
        List<LevelXMLException> exceptions = new();
        GetExceptionsBase(entities, exceptions);
        return exceptions;
    }

    /// <summary>
    /// This checks if there are any oddities in the level,
    /// including its list of entities, and returns them
    /// If possible, it's preferred to use the other public method as the Level constructor
    /// may mess with your list of entities before this method could diagnose it.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
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
            CheckIfBoostWithNaNPanels(special, exceptions);
            CheckIfBuildingWithNaNFloorWidth(special, exceptions);
            CheckChainNaNProperties(special, exceptions);
            CheckIsNPCWithNaNAngles(special, exceptions);
        }
        foreach (Joint joint in entities.OfType<Joint>())
        {
            CheckIfNaNAngleSlidingJoint(joint, exceptions);
        }
        foreach (Entity entity in entities.Concat(entities.OfType<Group>().SelectMany(group => group.Items)))
        {
            CheckIfNaNCoordinates(entity, exceptions);
            CheckNaNRotation(entity, exceptions);
            CheckNaNDimensions(entity, exceptions);
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
        if (entity is not IGroupable)
        {
            exceptions.Add(new LevelWouldFreezeOnStartException($"{entity.GetType().Name} are not allowed in groups.", entity));
            return;
        }
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
		else if(entity is SpikeSet)
        {
            // Setting it to fixed makes it not interactive in the group
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

    private static void CheckIfNaNCoordinates(Entity entity, IList<LevelXMLException> exceptions)
    {
        if (double.IsNaN(entity.X) )
        {
            exceptions.Add(new TagWouldHaveNoEffectException($"{entity.GetType().Name} has a NaN X coordinate.", entity));
        }
        if (double.IsNaN(entity.Y))
        {
            exceptions.Add(new TagWouldHaveNoEffectException($"{entity.GetType().Name} has a NaN Y coordinate.", entity));
        }
    }

    private static void CheckNaNDimensions(Entity entity, IList<LevelXMLException> exceptions)
    {
        if (entity is IScaleable scaleable)
        {
            if (double.IsNaN(scaleable.Width))
            {
                exceptions.Add(new TagWouldHaveNoEffectException($"{entity.GetType().Name} has NaN width.", entity));
            }
            if (double.IsNaN(scaleable.Height))
            {
                exceptions.Add(new TagWouldHaveNoEffectException($"{entity.GetType().Name} has NaN height.", entity));
            }
        }
    }

    private static void CheckNaNRotation(Entity entity, IList<LevelXMLException> exceptions)
    {
        if (entity is IRotatable rotatable)
        {
            if (double.IsNaN(rotatable.Rotation))
            {
                exceptions.Add(new TagWouldHaveNoEffectException($"{entity.GetType().Name} has NaN rotation.", entity));
            }
        }
    }

    private static void CheckIfBoostWithNaNPanels(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Boost boost)
        {
            if (double.IsNaN(boost.Panels))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("Boost has NaN panels.", boost));
            }
        }
    }

    private static void CheckIfBuildingWithNaNFloorWidth(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Building building)
        {
            if (double.IsNaN(building.FloorWidth))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("Building has a NaN floor width.", building));
            }
        }
    }

    private static void CheckChainNaNProperties(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is Chain chain)
        {
            if (double.IsNaN(chain.LinkScale))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("Chain has a NaN link scale.", chain));
            }
            if (double.IsNaN(chain.Curve))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("Chain has a NaN curve.", chain));
            }
            if (double.IsNaN(chain.LinkCount))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("Chain has a NaN link count.", chain));
            }
        }
    }

    private static void CheckIsNPCWithNaNAngles(Special special, IList<LevelXMLException> exceptions)
    {
        if (special is NonPlayerCharacter npc)
        {
            if (double.IsNaN(npc.NeckAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN neck angle.", npc));
            }
            if (double.IsNaN(npc.FrontArmAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN front arm angle.", npc));
            }
            if (double.IsNaN(npc.BackArmAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN back arm angle.", npc));
            }
            if (double.IsNaN(npc.FrontElbowAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN front elbow angle.", npc));
            }
            if (double.IsNaN(npc.BackElbowAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN back elbow angle.", npc));
            }
            if (double.IsNaN(npc.FrontLegAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN front leg angle.", npc));
            }
            if (double.IsNaN(npc.BackLegAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN back leg angle.", npc));
            }
            if (double.IsNaN(npc.FrontKneeAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN front knee angle.", npc));
            }
            if (double.IsNaN(npc.BackKneeAngle))
            {
                exceptions.Add(new TagWouldHaveNoEffectException("NPC has a NaN back knee angle.", npc));
            }
        }
    }
}
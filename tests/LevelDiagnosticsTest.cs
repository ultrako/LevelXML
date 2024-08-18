using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LevelXML.Test;

public class LevelDiagnosticsTest
{
    [Fact]
    public void NoExceptions()
    {
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(new Rectangle()).ToList();
        Assert.Empty(exceptions);
    }

    [Fact]
    public void TestJointInGroup()
    {
        PinJoint joint = new();
        Group group = new(joint);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("PinJoint are not allowed in groups.", exception.Message);
        Assert.Equal(joint, exception.FaultyTag);
    }

    [Fact]
    public void TestSoccerBallPointedToByTrigger()
    {
        SoccerBall ball = new();
        Target<Special> target = new(ball);
        ActivateTrigger trigger = new(target);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(trigger, ball).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("SoccerBall cannot be pointed to by triggers.", exception.Message);
        Assert.Equal(target, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNCharacterType()
    {
        Level level = new()
        {
            Character = Character.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(level).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Level cannot have a NaN character type.", exception.Message);
        Assert.Equal(level, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNBottleType()
    {
        Bottle bottle = new()
        {
            BottleType = BottleType.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(bottle).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Bottle cannot have a NaN type.", exception.Message);
        Assert.Equal(bottle, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNBottleTypeInGroup()
    {
        Bottle bottle = new()
        {
            BottleType = BottleType.None,
            Interactive = false,
        };
        Group group = new(bottle);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Bottle cannot have a NaN type.", exception.Message);
        Assert.Equal(bottle, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNFoodType()
    {
        Food food = new()
        {
            FoodType = FoodType.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(food).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Food cannot have a NaN type.", exception.Message);
        Assert.Equal(food, exception.FaultyTag);
    }

    [Fact]
    public void TestInteractiveNPCInGroup()
    {
        NonPlayerCharacter npc = new()
        {
            Interactive = true
        };
        Group group = new(npc);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("NonPlayerCharacter cannot be interactive and in groups.", exception.Message);
        Assert.Equal(npc, exception.FaultyTag);
    }

    [Fact]
    public void TestInteractiveBoomboxInGroup()
    {
        Boombox boombox = new()
        {
            Interactive = true
        };
        Group group = new(boombox);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (SpecialInvisibleAndNotInGroupException)exceptions.First();
        Assert.Equal("Boombox cannot be interactive and in groups.", exception.Message);
        Assert.Equal(boombox, exception.FaultySpecial);
    }

    [Fact]
    public void TestJetWithNaNPower()
    {
        Jet jet = new()
        {
            Power = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(jet).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("Jet cannot have NaN power.", exception.Message);
        Assert.Equal(jet, exception.FaultyEntity);
    }

    [Fact]
    public void TestNaNLengthSpikes()
    {
        SpikeSet spikes = new()
        {
            Spikes = double.NaN,
            Fixed = false
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(spikes).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("Nonfixed spike sets cannot have NaN spikes.", exception.Message);
        Assert.Equal(spikes, exception.FaultyEntity);
    }

    [Fact]
    public void TestNaNSlidingJointAngle()
    {
        Rectangle rect = new()
        {
            Fixed = false
        };
        SlidingJoint joint = new(rect)
        {
            Angle = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(rect, joint).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("A NaN angle in a sliding joint would black hole the attached entities.", exception.Message);
        Assert.Equal(joint, exception.FaultyEntity);
    }

    [Fact]
    public void TestNonFixedNaNDensityShape()
    {
        Triangle triangle = new()
        {
            Fixed = false,
            Density = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(triangle).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        // People can ignore this if they're making a black hole on purpose
        Assert.Equal("A NaN density in a non fixed shape makes it a black hole.", exception.Message);
        Assert.Equal(triangle, exception.FaultyEntity);
    }

    [Fact]
    public void TestXCoordinateIsNaN()
    {
        Fan fan = new()
        {
            X = double.NaN,
            Y = 0,
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(fan).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Fan has a NaN X coordinate.", exception.Message);
        Assert.Equal(fan, exception.FaultyTag);
    }

    [Fact]
    public void TestYCoordinateIsNaNInsideGroup()
    {
        Circle circle = new()
        {
            X = 0,
            Y = double.NaN,
            Fixed = false
        };
        Group group = new(circle);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Circle has a NaN Y coordinate.", exception.Message);
        Assert.Equal(circle, exception.FaultyTag);
    }

    [Fact]
    public void TestShapeWidthIsNaN()
    {
        Rectangle rectangle = new()
        {
            Width = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(rectangle).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Rectangle has NaN width.", exception.Message);
        Assert.Equal(rectangle, exception.FaultyTag);
    }

    [Fact]
    public void TestShapeHeightIsNaN()
    {
        Rectangle rectangle = new()
        {
            Width = 0,
            Height = double.NaN
        };
        Group group = new(rectangle);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Rectangle has NaN height.", exception.Message);
        Assert.Equal(rectangle, exception.FaultyTag);
    }

    [Fact]
    public void TestShapeRotationIsNaN()
    {
        Triangle triangle = new()
        {
            Rotation = double.NaN,
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(triangle).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Triangle has NaN rotation.", exception.Message);
        Assert.Equal(triangle, exception.FaultyTag);
    }

    [Fact]
    public void TestSpecialsRotationsAreNaN()
    {
        ArrowGun arrowGun = new();
        BladeWeapon bladeWeapon = new();
        Boombox boombox = new();
        Boost boost = new();
        Bottle bottle = new();
        Cannon cannon = new();
        Chain chain = new();
        Chair chair = new();
        DinnerTable table = new();
        Fan fan = new();
        Food food = new();
        GlassPanel panel = new();
        Harpoon harpoon = new();
        IBeam iBeam = new();
        Jet jet = new();
        Landmine landmine = new();
        Log log = new();
        NonPlayerCharacter npc = new();
        PaddlePlatform paddlePlatform = new();
        Rail rail = new();
        Sign sign = new();
        SpikeSet spikes = new();
        SpringPlatform springPlatform = new();
        Television tv = new();
        TextBox text = new();
        Toilet toilet = new();
        TrashCan trashCan = new();
        Van van = new();
        List<Special> rotatables = new()
        {
            arrowGun, bladeWeapon, boombox, boost, bottle,
            cannon, chain, chair, table, fan, food, panel,
            harpoon, iBeam, jet, landmine, log, npc, paddlePlatform,
            rail, sign, spikes, springPlatform, tv, text, toilet,
            trashCan, van
        };
        foreach (IRotatable special in rotatables.Cast<IRotatable>())
        {
            special.Rotation = double.NaN;
        }
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(rotatables.ToArray()).ToList();
        Assert.Equal(rotatables.Count, exceptions.Count);
        foreach (LevelXMLException exception in exceptions)
        {
            Assert.IsType<TagWouldHaveNoEffectException>(exception);
        }
        List<LevelXMLTag> causes = exceptions
            .OfType<TagWouldHaveNoEffectException>()
            .Select(exception => exception.FaultyTag)
            .ToList();
        foreach (Special special in rotatables)
        {
            Assert.Contains(special, causes);
        }
    }

    [Fact]
    public void TestSpecialsWidthAreNaN()
    {
        GlassPanel glassPanel = new();
        IBeam iBeam = new();
        Log log = new();
        Meteor meteor = new();
        Rail rail = new();
        List<Special> scaleables = new()
        {
            glassPanel, iBeam, log, meteor, rail
        };
        foreach (IScaleable scaleable in scaleables.Cast<IScaleable>())
        {
            scaleable.Width = double.NaN;
        }
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(scaleables.ToArray()).ToList();
        // +1 because meteor gives two exceptions
        Assert.Equal(scaleables.Count+1, exceptions.Count);
        foreach (LevelXMLException exception in exceptions)
        {
            Assert.IsType<TagWouldHaveNoEffectException>(exception);
        }
        List<LevelXMLTag> causes = exceptions
            .OfType<TagWouldHaveNoEffectException>()
            .Select(exception => exception.FaultyTag)
            .ToList();
        foreach (Special special in scaleables)
        {
            Assert.Contains(special, causes);
        }
    }

    [Fact]
    public void TestSpecialsHeightAreNaN()
    {
        GlassPanel glassPanel = new();
        IBeam iBeam = new();
        Log log = new();
        Meteor meteor = new();
        Rail rail = new();
        List<Special> scaleables = new()
        {
            glassPanel, iBeam, log, meteor, rail
        };
        foreach (IScaleable scaleable in scaleables.Cast<IScaleable>())
        {
            scaleable.Height = double.NaN;
        }
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(scaleables.ToArray()).ToList();
        // +1 because meteor gives two exceptions
        Assert.Equal(scaleables.Count+1, exceptions.Count);
        foreach (LevelXMLException exception in exceptions)
        {
            Assert.IsType<TagWouldHaveNoEffectException>(exception);
        }
        List<LevelXMLTag> causes = exceptions
            .OfType<TagWouldHaveNoEffectException>()
            .Select(exception => exception.FaultyTag)
            .ToList();
        foreach (Special special in scaleables)
        {
            Assert.Contains(special, causes);
        }
    }

    [Fact]
    public void TestNaNPanelsInBoost()
    {
        Boost boost = new()
        {
            Panels = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(boost).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Boost has NaN panels.", exception.Message);
        Assert.Equal(boost, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNFloorWidth()
    {
        BuildingOne building = new()
        {
            FloorWidth = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(building).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Building has a NaN floor width.", exception.Message);
        Assert.Equal(building, exception.FaultyTag);
    }

    [Fact]
    public void TestChainNaNLinkScale()
    {
        Chain chain = new()
        {
            LinkScale = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(chain).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Chain has a NaN link scale.", exception.Message);
        Assert.Equal(chain, exception.FaultyTag);
    }

    [Fact]
    public void TestChainNaNCurve()
    {
        Chain chain = new()
        {
            Curve = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(chain).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Chain has a NaN curve.", exception.Message);
        Assert.Equal(chain, exception.FaultyTag);
    }

    [Fact]
    public void TestChainLinkCount()
    {
        Chain chain = new()
        {
            LinkCount = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(chain).ToList();
        var exception = (TagWouldHaveNoEffectException)exceptions.First();
        Assert.Equal("Chain has a NaN link count.", exception.Message);
        Assert.Equal(chain, exception.FaultyTag);
    }

    [Fact]
    public void TestNPCWithNaNAngles()
    {
        NonPlayerCharacter npc = new()
        {
            NeckAngle = double.NaN,
            FrontArmAngle = double.NaN,
            BackArmAngle = double.NaN,
            FrontElbowAngle = double.NaN,
            BackElbowAngle = double.NaN,
            FrontLegAngle = double.NaN,
            BackLegAngle = double.NaN,
            FrontKneeAngle = double.NaN,
            BackKneeAngle = double.NaN,
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(npc).ToList();
        Assert.Equal(9, exceptions.Count);
        foreach(TagWouldHaveNoEffectException exception in exceptions.Cast<TagWouldHaveNoEffectException>())
        {
            Assert.Contains("NPC has a NaN", exception.Message);
            Assert.Contains("angle.", exception.Message);
            Assert.Equal(npc, exception.FaultyTag);
        }
    }
}
using Xunit;

namespace HappyWheels.Test;

public class FoodTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Food food = new();
        Assert.Equal(0, food.Rotation);
        Assert.False(food.Sleeping);
        Assert.True(food.Interactive);
        Assert.Equal(FoodType.Watermelon, food.FoodType);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Food food = new();
        Assert.Throws<LevelXMLException>(() => food.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingFoodTypeAsNaN()
    {
        Food food = new();
        Assert.Throws<LevelXMLException>(() => food.FoodType = double.NaN);
    }
}
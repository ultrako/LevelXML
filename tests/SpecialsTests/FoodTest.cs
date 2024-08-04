using Xunit;

namespace LevelXML.Test;

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
        food.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingFoodTypeAsNaN()
    {
        Food food = new();
        food.FoodType = double.NaN;
    }
}
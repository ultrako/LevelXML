using Xunit;
using System;

namespace HappyWheels.Test;

public class FoodTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(FoodType.Watermelon != FoodType.Pineapple);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(FoodType.Pineapple.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((2.0).GetHashCode(), FoodType.Pumpkin.GetHashCode());
    }
}
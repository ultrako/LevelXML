using Xunit;
using System;

namespace HappyWheels.Test;

public class InfoTest
{
	[Fact]
	public void TestMinimal()
	{
		Info info = new(@"<info c=""1"" e=""1""/>");
		Assert.Equal(@"<info v=""" + Info.HappyWheelsVersion + @""" x=""NaN"" y=""NaN"" c=""1"" f=""f"" h=""f"" bg=""NaN"" bgc=""16777215"" e=""1"" />",
			info.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TestNaN()
	{
		Info info = new(@"<info v=""NaN"" x=""NaN"" y=""NaN"" c=""1"" f=""NaN"" h=""NaN"" bg=""NaN"" bgc=""NaN"" e=""1""/>");
		Assert.Equal(@"<info v=""" + Info.HappyWheelsVersion + @""" x=""NaN"" y=""NaN"" c=""1"" f=""f"" h=""f"" bg=""NaN"" bgc=""NaN"" e=""1"" />",
			info.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TestLowValues()
	{
		Info info = new(@"<info v=""-99999"" x=""0"" y=""0"" c=""-99999"" f=""f"" h=""f"" bg=""-99999"" bgc=""0"" e=""1""/>");
		Assert.Equal(@"<info v=""" + Info.HappyWheelsVersion + @""" x=""0"" y=""0"" c=""1"" f=""f"" h=""f"" bg=""-99999"" bgc=""0"" e=""1"" />",
			info.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TestHighValues()
	{
		Info info = new(@"<info v=""99999"" x=""0"" y=""0"" c=""99999"" f=""f"" h=""f"" bg=""99999"" bgc=""0"" e=""1""/>");
		Assert.Equal(@"<info v=""" + Info.HappyWheelsVersion + @""" x=""0"" y=""0"" c=""11"" f=""f"" h=""f"" bg=""99999"" bgc=""0"" e=""1"" />",
			info.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TestDefaultValues()
	{
		Info info = new();
		Assert.Equal(Info.HappyWheelsVersion, info.Version);
		Assert.Equal(300, info.X);
		Assert.Equal(5100, info.Y);
	}
}

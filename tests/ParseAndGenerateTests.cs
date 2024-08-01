using Xunit;
using System.IO;
using System.Text.RegularExpressions;

namespace LevelXML.Test;

public class ParseAndGenerateTests
{
    const string xmlDirectory = "./TestLevelXMLs";
    

    private static string ReadLevelHelper(string inputFilePath)
    {
        string levelXML = File.ReadAllText(inputFilePath);

        string pattern = @"v=""[^""]*""";
        string replacement = $"v=\"{Info.LevelXMLVersion}\"";
        return Regex.Replace(levelXML, pattern, replacement);
    }

    public static TheoryData<string, string> InputAndExpectedTestData()
    {
        TheoryData<string, string> testData = new();
        string[] inputFiles = Directory.GetFiles(xmlDirectory, "*_Input");
        foreach (string inputFile in inputFiles)
        {
            string expectedFile = inputFile.Replace("_Input", "_Expected");
            testData.Add(
                ReadLevelHelper(inputFile),
                ReadLevelHelper(expectedFile)
            );
        }
        return testData;
    }

    [Theory]
    [MemberData(nameof(InputAndExpectedTestData))]
    public void InputAndExpectedTests(string input, string expected)
    {
        Level level = new(input);
        Assert.Equal(expected, level.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
}
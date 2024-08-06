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

        string versionPattern = @"info v=""[^""]*""";
        string versionReplacement = $"info v=\"{Info.LevelXMLVersion}\"";
        string selfClosingTagPattern = "\"/>";
        string selfClosingTagReplacement = "\" />";
        return Regex.Replace(Regex.Replace(levelXML, versionPattern, versionReplacement), 
            selfClosingTagPattern, selfClosingTagReplacement);
    }

    public static TheoryData<string, string> InputAndExpectedTestData()
    {
        TheoryData<string, string> testData = new();
        string[] inputFiles = Directory.GetFiles(xmlDirectory, "*_Input");
        foreach (string inputFile in inputFiles)
        {
            string expectedFile = inputFile.Replace("_Input", "_Expected");
            testData.Add(
                inputFile,
                expectedFile
            );
        }
        return testData;
    }

    [Theory]
    [MemberData(nameof(InputAndExpectedTestData))]
    public void InputAndExpectedTests(string input, string expected)
    {
        Level level = new(ReadLevelHelper(input));
        Assert.Equal(ReadLevelHelper(expected), level.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    public static TheoryData<string> InvariantTestData()
    {
        TheoryData<string> testData = new();
        string[] inputFiles = Directory.GetFiles(xmlDirectory, "*_Invariant");
        foreach (string inputFile in inputFiles)
        {
            testData.Add(inputFile);
        }
        return testData;
    }

    [Theory]
    [MemberData(nameof(InvariantTestData))]
    public void InvariantTests(string levelXML)
    {
        string levelData = ReadLevelHelper(levelXML);
        Level level = new(levelData);
        Assert.Equal(levelData, level.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    public static TheoryData<string> ImportThrowsTestData()
    {
        TheoryData<string> testData = new();
        string[] inputFiles = Directory.GetFiles(xmlDirectory, "*_Throws");
        foreach (string inputFile in inputFiles)
        {
            testData.Add(inputFile);
        }
        return testData;
    }

    [Theory]
    [MemberData(nameof(ImportThrowsTestData))]
    public void ImportThrowsTests(string levelXML)
    {
        Assert.Throws<LevelXMLException>(() => new Level(ReadLevelHelper(levelXML)));
    }
}
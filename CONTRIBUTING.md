# Contributing to the LevelXML Library
Firstly, thank you for helping with this library.
If you have a suggestion or have noticed a bug, please open a new issue in the Issue tab.
Feel free to make a branch, solve the issue, and then open a pull request. 
By contributing, you are expected to uphold the library's Code of Conduct. It's short. https://github.com/ultrako/LevelXML/CODE_OF_CONDUCT.md

# Scope of the library
The very first rule of thumb for considering whether to add a functionality to this library is the question:
## Does Happy Wheels do that?
There's no point in adding a feature into this library if the level importing function in the Happy Wheels game doesn't support it.
On the other hand, if there _is_ something that you can do by editing the LevelXML, and this library doesn't have it, then end developers would have to limit themselves just to use this library.
Of course, there is still a lot of leeway between those two extremes - we tend towards the latter, and strive for a library that can minimally do everything that the game can, but rely on dependent code to implement anything else. 
For example, we don't offer a method that analyzes a level's trigger graph and optimizes it to have the fewest amount of triggers, because you don't need to rely on the library internals to do that, it's possible just by importing it in a separate project.

Exceptions to this rule include:
1. Diagnostic features: Providing insight into why a level or part of a level fails to import
2. Parsing and formatting: We can deviate from what the import box does as long as functionally (meaning, in regard to in-game visuals and physics) the level ends up the same.
For example, in self-closing tags, Happy Wheels has a space between the last attribute and the close of the tag, but we don't (<sh t="1" /> as opposed to <sh t="1"/>). That's just a consequence of the XML library we use, and doesn't really matter.

The scope rule also brings up a very important test for this library:
## If I take a level, import it into my library, and then export it from my library, will it behave and look exactly the same as it did before?
If you can find a level for which the answer is "No", please submit a bug report with the related LevelXML.
# Testing
There are a few patterns for the tests in this library:
1. Invoke an empty constructor, assign to properties, assert a string being equal to ToXML().
2. Invoke an empty constructor, assign to properties, assert numeric or boolean values being equal to properties (to test clamping/NaN behavior).
3. Pass strings to constructors (especially to Level), assert numeric or boolean values being equal to properties.
4. Pass strings to constructors, assert a string being equal to ToXML().
Generally every value of an LevelXMLTag needs to be tested this way. Happy Wheels will clamp, not let you import the level, do something to NaNs, make an object disappear, or do nothing, to a variety of inputs, all in a different way for every single XML attribute. We need to be able to parse and understand that attribute, mimic Happy Wheels behavior when LevelXMLTags are made and modified, and we need to output LevelXML that Happy Wheels can parse.

# Style Guidelines/General Conventions
This project adheres to the Microsoft C# coding conventions. 
If something is not explicitly mentioned by the coding guidelines, try to be consistent with the rest of the code. If you can't find anything in either the coding conventions, nor in the rest of the code, just make an assumption, and if it doesn't get caught by a linter or a code review it wasn't important enough.
If you find code that isn't consistent with the Microsoft coding conventions, please submit a bug report.

When making a branch, make sure its name starts with 'feature/' and has a descriptive title matching the GitHub issue it's solving
We use trunk-based branching, so branch off of main and then make Pull Requests back to main (make sure to increment the version as according to https://semver.org)

## Documentation
Every single public member should be documented with ///<docstrings>
We use Doxygen to generate our docs. Currently, you need to manually re-run it whenever you make a breaking change or addition to the public members of the library.

# Help
The Happy Wheels wiki page on LevelXML has been very useful in creating this library. Of course, manual testing in the game is what we use as the source of truth, but this page is still a good reference and is seldom wrong: https://happywheels.fandom.com/wiki/LevelXML
If an issue with this library needs more involved discussion than is appropriate for a GitHub issue, or if you need help with developing/using the library, feel free to contact me on Discord. My username is Ultrako.
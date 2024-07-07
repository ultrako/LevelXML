This library has a class hierarchy representing every feature in a Happy Wheels level.
It allows you to import the LevelXML format (the format that the game's import and export boxes use in its level editor) into objects in the class hierarchy,
and it allows you to export to the LevelXML format, so that you can paste the levels back into the game again.
To use it:
1. Add the package to your project (a "dotnet add package" command)
2. Add a "using LevelXML;" line to the file that you want to use the libary in
3. Use any prexisting LevelXML as segments of the level you want to construct, using Level's constructor.<br />
Example:
<code>
Level controlsVehicleLevel = new Level("./controlsVehicle.xml");
</code>
4. Construct new Entities and modify them using the library. <br />
Example:
<code>
Vehicle controlsVehicle = (Vehicle)controlsVehicleLevel.Groups[0];
controlsVehicle.GrabbingPose = GrabbingPose.Hold;
Rectangle floor = new()
{
   Y = 5100,
   X = 0,
   Width = 5000,
   Height = 50
}
</code>
5. Combine all your entities into one array, and construct a new Level with them. Then, use Level's ToXML() method to get the LevelXML representation of your entire level.
It is important that no objects in this list reference any objects outside of the list, or otherwise the library will throw an exception when calling ToXML().<br />
Example:
<code>
Level level = new(controlsVehicleLevel.Entities.Concat(new[] {floor}).ToArray());
Console.WriteLine(level.ToXML());
</code>

# UnitySaveableEditor
Save custom data into json file inside Unity project folder, one editor can have multiple section as your desire by giving it a name.
Save data will be locate in Application.dataPath+"../SavableEditorData"

# Installing
Download [package](https://github.com/sumatakso/UnitySaveableEditor/blob/master/SaveableEditor.unitypackage) here then import to your project or download [this](https://github.com/sumatakso/UnitySaveableEditor/blob/master/Assets/SaveableEditor/Editor/SaveableEditor.cs) class and place wherever you want.

# Example
Before you'll be able to save data you need to make a struct or class of it first.
### Create structure
```c#
// Create save struct or class
public struct SaveEditorData
{
    public string someString;
    public bool isToggle;
}
// Init value
SaveEditorData saveEditor = new SaveEditorData
{
    someString = "Some string to save",
    isToggle = false
};
```
### Save
```c#
// Save without section, result in a "MAIN" section.
SaveableEditor.Save("ExampleEditor", this.saveEditor);
...
// Save with section name.
SaveableEditor.Save("ExampleEditor", "MonsterConfigProperty", this.saveEditor);
```

### Load
```c#
// Load without giving section, result in a "MAIN" section. and provide default value if it's not there.
this.saveEditor = SaveableEditor.Load("ExampleEditor", this.saveEditor);
...
// Load with section name.
this.saveEditor = SaveableEditor.Load("ExampleEditor", "MonsterConfigProperty", this.saveEditor);
```
### Remove
```c#
// Remove all save of "ExampleEditor"
SaveableEditor.Load("ExampleEditor");
...
// Remove some section of "ExampleEditor"
SaveableEditor.Load("ExampleEditor", "MonsterConfigProperty");
```

### Full example
Clone the project and open editor window from Saveable Example > Window
or have a look a [this](https://github.com/sumatakso/UnitySaveableEditor/blob/master/Assets/SaveableEditor/Editor/TestEditorSaveWindow.cs) as an editor window

# Versioning
This project use [Semantic](https://semver.org/) as version control.

# Author
 - [sumatakso](https://github.com/sumatakso)

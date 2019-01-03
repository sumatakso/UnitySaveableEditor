using UnityEngine;
using UnityEditor;

public class TestEditorSaveWindow : EditorWindow
{
    public struct SaveEditorData
    {
        public string someString;
        public bool isToggle;
    }

    SaveEditorData saveEditor = new SaveEditorData
    {
        someString = "Some string to save",
        isToggle = false
    };

    string saveSection = "MAIN";

    [MenuItem("Saveable Example/Window")]
    static void OpenWindow()
    {
        GetWindow<TestEditorSaveWindow>();
    }
    private void OnEnable()
    {
        saveEditor = SaveableEditor.Load(this.GetType().Name, saveSection, saveEditor);
    }
    private void DrawPropertyArea()
    {
        EditorGUILayout.LabelField("Save properties", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");
        saveEditor.someString = EditorGUILayout.TextField("Some string", saveEditor.someString);
        saveEditor.isToggle = EditorGUILayout.Toggle("Toggle", saveEditor.isToggle);
        EditorGUILayout.EndHorizontal();
    }
    private void DrawFunctionArea()
    {
        EditorGUILayout.LabelField("Function", EditorStyles.boldLabel);
        saveSection = EditorGUILayout.TextField("Save section", saveSection);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            SaveableEditor.Save(this.GetType().Name, saveSection, this.saveEditor);
        }
        if (GUILayout.Button("Load"))
        {
            this.saveEditor = SaveableEditor.Load(this.GetType().Name, saveSection, this.saveEditor);
        }
        if (GUILayout.Button("Remove"))
        {
            SaveableEditor.Remove(this.GetType().Name, saveSection);
        }
        if (GUILayout.Button("Remove All"))
        {
            SaveableEditor.Remove(this.GetType().Name);
        }
        EditorGUILayout.EndHorizontal();
    }
    private void OnGUI()
    {
        this.DrawPropertyArea();
        EditorGUILayout.Space();
        this.DrawFunctionArea();
    }
}

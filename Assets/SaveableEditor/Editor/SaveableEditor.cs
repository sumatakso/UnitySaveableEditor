using System.IO;
using UnityEngine;

public class SaveableEditor
{
    #region Private var and method
    public static string SaveFolder
    {
        get { return Path.Combine(Application.dataPath, ".." + Path.DirectorySeparatorChar + "SaveableEditorData"); }
    }
    const string DEFAULT_SECTION = "MAIN";
    const string FILENAME_FORMAT = "{0}-{1}.json";
    public static string ToFilePath(string editorName, string valueSection)
    {
        return Path.Combine(SaveFolder,string.Format(FILENAME_FORMAT,editorName, valueSection));
    }
    public static void SaveTo(string editorName, string section, string jsonString)
    {
        string useSection = string.IsNullOrEmpty(section) ? DEFAULT_SECTION : section;
        string filePath = ToFilePath(editorName, useSection);
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
        File.WriteAllText(filePath, jsonString);
    }
    #endregion

    #region Public method
    /// <summary>
    /// Save editor property class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="editorName"></param>
    /// <param name="saveValue"></param>
    public static void Save<T>(string editorName, T saveValue)
    {
        Save(editorName, DEFAULT_SECTION, saveValue);
    }
    /// <summary>
    /// Save editor property class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="editorName"></param>
    /// <param name="section"></param>
    /// <param name="saveValue"></param>
    public static void Save<T>(string editorName, string section, T saveValue)
    {
        SaveTo(editorName, section, JsonUtility.ToJson(saveValue, true));
    }
    /// <summary>
    /// Load Editor property class by provide default if it's not there.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="editorName"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T Load<T>(string editorName, T defaultValue)
    {
        return Load(editorName, DEFAULT_SECTION, defaultValue);
    }
    /// <summary>
    /// Load Editor property class by provide default if it's not there.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="editorName"></param>
    /// <param name="section"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T Load<T>(string editorName, string section, T defaultValue)
    {
        string filePath = ToFilePath(editorName, section);
        if (File.Exists(filePath))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        }
        return defaultValue;
    }
    /// <summary>
    /// Remove save for editor, send none section will result in all save will be remove.
    /// </summary>
    /// <param name="editorName"></param>
    /// <param name="sections"></param>
    public static void Remove(string editorName, params string[] sections)
    {
        if (!Directory.Exists(SaveFolder))
        {
            return;
        }
        if (sections.Length == 0)
        {
            foreach(string path in Directory.EnumerateFiles(SaveFolder))
            {
                if (Path.GetFileName(path).StartsWith(editorName))
                {
                    File.Delete(path);
                }
            }
        }
        else
        {
            foreach (string section in sections)
            {
                string path = ToFilePath(editorName, section);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
        string[] files = Directory.GetFiles(SaveFolder);
        if (files.Length == 0)
        {
            Directory.Delete(SaveFolder);
        }
    }
    #endregion
}

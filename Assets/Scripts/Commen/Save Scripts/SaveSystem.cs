using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static int CurrentSave = 1;

    #region Example

    public static void SaveExample(SaveExample ex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + CurrentSave.ToString("D2") + "_save.example";
        FileStream stream = new FileStream(path, FileMode.Create);

        ExampleData data = new ExampleData(ex);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ExampleData LoadExample()
    {
        string path = Application.persistentDataPath + "/" + CurrentSave.ToString("D2") + "_save.example";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ExampleData data = (ExampleData) formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path + ", Creating new save...");
            return null;
        }
    }

    public static void DeleteExample()
    {
        string path = Application.persistentDataPath + "/" + CurrentSave.ToString("D2") + "_save.example";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    #endregion

    #region Options

    public static void SaveOptions(OptionsData options)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Options.prefs";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, options);
        stream.Close();
    }

    public static OptionsData LoadOptions()
    {
        string path = Application.persistentDataPath + "/Options.prefs";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OptionsData data = (OptionsData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path + ", Create new save...");
            return null;
        }
    }

    #endregion

}
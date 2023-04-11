using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(SaveData saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }

        string path = Application.persistentDataPath + "yeetthebunnies.save";
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        //return true;
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "yeetthebunnies.save";

        if (!File.Exists(path)) return null;

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            SaveData save = formatter.Deserialize(file) as SaveData;
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static SaveData Load(SaveData saveData)
    {
        string path = Application.persistentDataPath + "yeetthebunnies.save";

        if (!File.Exists(path)) Save(saveData);

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            SaveData save = formatter.Deserialize(file) as SaveData;
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}

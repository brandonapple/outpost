using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData();
        formatter.Serialize(stream, data);
        stream.Close();
      
    }
    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("not file found");
            return null;
        }
    }

    public static void SaveOptionsData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/options.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        OptionsData data = new OptionsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static OptionsData LoadOptionsData()
    {
        string path = Application.persistentDataPath + "/options.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            OptionsData data = formatter.Deserialize(stream) as OptionsData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveHistoryData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/history.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        HistroryData data = new HistroryData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static HistroryData LoadHistoryData()
    {
        string path = Application.persistentDataPath + "/history.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            HistroryData data = formatter.Deserialize(stream) as HistroryData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void SaveGameThroughData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamePlaythrough.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        GamePlaythroughData data = new GamePlaythroughData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GamePlaythroughData LoadGamePlaythroughData()
    {
        string path = Application.persistentDataPath + "/gamePlaythrough.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GamePlaythroughData data = formatter.Deserialize(stream) as GamePlaythroughData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }

    }

    public static void SaveWeaponData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/weaponData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        WeaponData data = new WeaponData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static WeaponData LoadWeaponData()
    {
        string path = Application.persistentDataPath + "/weaponData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            WeaponData data = formatter.Deserialize(stream) as WeaponData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }

    }

}

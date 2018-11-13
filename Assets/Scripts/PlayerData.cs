using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Data
{
    public DateTime RecordedRealTime;
    public DateTime RecordedGameTime;
    public bool IsInitialized;
}

public class PlayerData : MonoBehaviour
{
    private Data data;

    public void Init()
    {
        data = new Data();
        data.RecordedGameTime = DateTime.Now.Date;
        
        Load();
    }

    public Data GetData()
    {
        return data;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");

        bf.Serialize(file, data);
        file.Close();
        
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            data = (Data)bf.Deserialize(file);

            file.Close();
        }
        else
            Debug.Log("File doesn't exists");

    }
}

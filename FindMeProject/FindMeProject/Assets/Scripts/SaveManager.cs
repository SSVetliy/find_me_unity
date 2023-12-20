using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerProperty player;


    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "save.dat");
        FileStream file = File.Create(path);
        SaveData data = new SaveData();

        data.myList = JsonUtility.ToJson(player.myList);

        data.curShirtId = player.curShirtId;
        data.curBackgroundId = player.curBackgroundId;

        data.countFlipCard = player.countFlipCard;
        data.countAddStep = player.countAddStep;
        data.countAddTime = player.countAddTime;
        data.countViewAll = player.countViewAll;

        data.countCoin = player.countCoin;
        data.countKey = player.countKey;

        data.openLevels = player.openLevels;

        formatter.Serialize(file, data);
        file.Close();
    }

    public SaveData Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "save.dat");
        FileStream file = File.Open(path, FileMode.Open);
        SaveData data = new SaveData();
        if (file.Length != 0)
            data = (SaveData)formatter.Deserialize(file);

        file.Close();

        return data;
    }

    public void ResetSaveProgress()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.dat");
        File.Delete(path);
        File.Create(path).Dispose();
    }

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.dat");
        if (!File.Exists(path))
        {
            File.Create(path);
        }
    }
}

[Serializable]
public class SaveData
{
    public string myList;

    public int curShirtId;
    public int curBackgroundId;

    public int countFlipCard;
    public int countAddStep;
    public int countAddTime;
    public int countViewAll;

    public int countCoin;
    public int countKey;

    public int openLevels;

    public SaveData()
    {
        curShirtId = -1;
        curBackgroundId = -1;
    }
}

using UnityEngine;
using System.IO;
using System.Xml.Serialization;
public class SaveData
{
    public int unlockedLevel;
    public float timer;
    public int deathCount;

    public SaveData(int unlockedLevel = 1, float timer = 0, int deathCount = 0)
    {
        this.unlockedLevel = unlockedLevel;
        this.timer = timer;
        this.deathCount = deathCount;
    }

    public bool TrySetUnlockedLevel(int level)
    {
        if (level > unlockedLevel)
        {
            unlockedLevel = level;
            return true;
        }
        return false;
    }
}

public class SaveSystem : MonoBehaviour
{
    public SaveData saveData;
    const string fileName = "/PlayerSave.json";
    string filePath;

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
        }
        else
        {
            saveData = new SaveData();
        }
    }

    public void SaveData()
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData));
    }
    public void DeleteData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    void Awake()
    {
        filePath = Application.persistentDataPath + fileName;
        LoadData();
    }
    void OnDisable()
    {
        SaveData();
    }

}

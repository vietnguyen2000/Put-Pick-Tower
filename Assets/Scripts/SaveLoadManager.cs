using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public class PlayerData
    {
        public List<string> skins = new List<string>();
        public List<string> towers = new List<string>();
        public int coins;
        public int passedLevel;
    }
    // Start is called before the first frame update
    public PlayerData SavedData { get; set; }

    string path;
    void Start()
    {

        path = "Assets/Resources/player_data.json"; // Change later
        SavedData = new PlayerData();
        if (File.Exists(path))
            ReadSavedData();
        else
            WriteDefaultData();

    }
    public void WriteDefaultData()
    {
        SavedData.skins = new List<string>
        {
            "Owlet"
        };
        SavedData.towers = new List<string>
        {
            "Tower_Blue"
        };
        SavedData.coins = 0;
        SavedData.passedLevel = 0;
        
        File.WriteAllText(path, JsonUtility.ToJson(SavedData));
        //TempJsonReader reader = JsonUtility.FromJson<TempJsonReader>(readfile);
    }
    public void WriteNewPlayerData()
    {
        File.WriteAllText(path, JsonUtility.ToJson(SavedData));
    }
    public void ReadSavedData()
    {
        SavedData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
    }
    public void AddNewlyUnlockedSkin(string skin)
    {
        SavedData.skins.Add(skin);
    }
    public void AddNewlyUnlockedTower(string tower)
    {
        SavedData.towers.Add(tower);
    }
    public void AddCoins(int coin)
    {
        SavedData.coins += coin;
    }
    public void PassNewLevel()
    {
        SavedData.passedLevel += 1;
    }
}

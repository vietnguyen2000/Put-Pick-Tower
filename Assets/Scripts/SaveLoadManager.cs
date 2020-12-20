using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
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
    private SaveLoadManager(){
        path = Application.persistentDataPath + "player_data.json";
        SavedData = new PlayerData();
        if (File.Exists(path))
            ReadSavedData();
        else
            WriteDefaultData();
    }
    private static SaveLoadManager instance = null;
    public static SaveLoadManager Instance{
        get{
            if (instance == null)  
            {  
                instance = new SaveLoadManager();  
            }  
            return instance;  
        }
    }
    string path;
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
        WriteNewPlayerData();
    }
    public void AddNewlyUnlockedTower(string tower)
    {
        SavedData.towers.Add(tower);
        WriteNewPlayerData();
    }
    public void AddCoins(int coin)
    {
        SavedData.coins += coin;
        WriteNewPlayerData();
    }
    public void PassNewLevel()
    {
        SavedData.passedLevel = Mathf.Max(GameData.levelChosen+1,SavedData.passedLevel);
        WriteNewPlayerData();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Tower[] listOfTower;
    public Tower currentTower;
    public Player player;
    public SpawnMonsterManager spawnMonsterManager;
    public UpgradeStage UpgradeStage;
    public SaveLoadManager saveLoadManager;
    public int numOfCoins;
    public int scaleOfCoinPoint;
    public int scaleOfMonsterPoint;
    public int scaleOfTimePoint;

    private int totalPoints;
    private int totalCoinsCollected;
    private int totalTimePlayed;

    private int totalMonsterPoints;
    [System.Serializable]
    public class GUIStats{
        public Text PlayerHP;
        public Text PlayerSpeed;
        public Text PlayerPutpickSpeed;
        public Text TowerDamage;
        public Text TowerAttackSpeed;
        public Text TowerRange;
        public Text Coin;
        public Text Round;
        public Image PlayerIcon;
        public Image TowerIcon;

        public void UpdatePlayerStats(float HP, float speed, float PutpickSpeed){
            PlayerHP.text = HP.ToString("F1");
            PlayerSpeed.text = speed.ToString("F1");
            PlayerPutpickSpeed.text = PutpickSpeed.ToString("F1");
        }
        public void UpdateTowerStats(Sprite towerIcon,float damage, float attackSpeed, float range){
            TowerDamage.text = damage.ToString("F1");
            TowerAttackSpeed.text = attackSpeed.ToString("F1");
            TowerRange.text = range.ToString("F1");
            TowerIcon.sprite = towerIcon;
        }
        public void UpdateCoin(int coins, int require){
            Coin.text = coins.ToString() + "/" + require.ToString();
        }
    }
    [System.Serializable]
    public class GameOverMenu{
        public GameObject Canvas;
        public Text title;
        public Text totalTime;
        public Text totalCoins;
        public Text totalCoinsExtra;
        public Text totalpoints;
    }
    public GUIStats guiStats;
    public GameOverMenu gameOverMenu;
    void Awake()
    {
        GameObject p = GameObject.Instantiate(Resources.Load(GameData.skinChosen),Vector3.zero,new Quaternion()) as GameObject;
        player = p.GetComponent<Player>();
        listOfTower = new Tower[GameData.numofTower];
        for (var i = 0; i < GameData.numofTower; i++){
            p= GameObject.Instantiate(Resources.Load(GameData.towerChosen[i]),new Vector3(0,0.5f*(i+1),0),new Quaternion()) as GameObject;
            listOfTower[i] = p.GetComponent<Tower>();
        }
        UpgradeStage= FindObjectOfType<UpgradeStage>();
        spawnMonsterManager = FindObjectOfType<SpawnMonsterManager>();
        saveLoadManager = SaveLoadManager.Instance;
        currentTower = listOfTower[0];
        player.CollectCoin = CollectCoin;
    }
    public void changeSFX(Toggle toggle){
        bool isOn = toggle.isOn;

    }
    public void changeSound(Toggle toggle){
        bool isOn = toggle.isOn;

    }
    public void exit(){
        SceneManager.LoadScene("Menu Start",LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void win() {
        gameOverMenu.title.text="WIN";
        gameOverMenu.totalCoins.text = totalCoinsCollected.ToString();
        gameOverMenu.totalCoinsExtra.text = numOfCoins.ToString();
        totalPoints = totalCoinsCollected * scaleOfCoinPoint + totalMonsterPoints * scaleOfMonsterPoint + 
                      totalTimePlayed * scaleOfTimePoint;
        gameOverMenu.totalpoints.text = totalPoints.ToString();
        gameOverMenu.Canvas.SetActive(true);
        saveLoadManager.AddCoins(numOfCoins);
        saveLoadManager.PassNewLevel();
        // saveLoadManager.WriteNewPlayerData();
    }
    public void lose(){
        gameOverMenu.title.text="LOSE";
        gameOverMenu.totalCoins.text = totalCoinsCollected.ToString();
        gameOverMenu.totalCoinsExtra.text = numOfCoins.ToString();
        totalPoints = totalCoinsCollected * scaleOfCoinPoint + totalMonsterPoints * scaleOfMonsterPoint + 
                      totalTimePlayed * scaleOfTimePoint;
        gameOverMenu.totalpoints.text = totalPoints.ToString();
        gameOverMenu.Canvas.SetActive(true);
    }

    void CollectCoin(){
        numOfCoins +=1;
        totalCoinsCollected += 1;
    }

    public void MonsterKillScore(int score)
    {
        totalMonsterPoints += score;
    }
    // Update is called once per frame
    // public void BackDoor()
    // {
    //     if (Input.GetKeyDown(KeyCode.F12))
    //     {
    //         win();
    //         saveLoadManager.AddCoins(15);
    //         saveLoadManager.PassNewLevel();
    //         saveLoadManager.AddNewlyUnlockedSkin("Green");
    //         saveLoadManager.AddNewlyUnlockedTower("Shjt");
    //         saveLoadManager.WriteNewPlayerData();
    //     }
    // }
    void Update()
    {
        guiStats.UpdatePlayerStats(player.Hp,player.Speed,player.TimePutPick);
        guiStats.UpdateTowerStats(currentTower.towerIcon,currentTower.Damage,currentTower.AttackSpeed,currentTower.AttackRange);
        guiStats.UpdateCoin(numOfCoins,UpgradeStage.CoinRequire);
        // BackDoor();
    }
}

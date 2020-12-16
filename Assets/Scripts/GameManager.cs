using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Tower[] listOfTower;
    public Tower currentTower;
    public Player player;
    public SpawnMonsterManager spawnMonsterManager;
    public UpgradeStage UpgradeStage;
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
    public class WinMenu{
        public GameObject Canvas;
        public Text totalTime;
        public Text totalCoins;
        public Text totalCoinsExtra;
        public Text totalpoints;
    }
    public GUIStats guiStats;
    public WinMenu winMenu;
    void Awake()
    {
        player = (Player)FindObjectOfType<Player>();
        listOfTower = FindObjectsOfType<Tower>();
        UpgradeStage= FindObjectOfType<UpgradeStage>();
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

    }
    public void win() {
        winMenu.totalCoins.text = totalCoinsCollected.ToString();
        winMenu.totalCoinsExtra.text = numOfCoins.ToString();
        totalPoints = totalCoinsCollected * scaleOfCoinPoint + totalMonsterPoints * scaleOfMonsterPoint + 
                      totalTimePlayed * scaleOfTimePoint;
        winMenu.totalpoints.text = totalPoints.ToString();
        winMenu.Canvas.SetActive(true);
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
    void Update()
    {
        guiStats.UpdatePlayerStats(player.Hp,player.Speed,player.TimePutPick);
        guiStats.UpdateTowerStats(currentTower.towerIcon,currentTower.Damage,currentTower.AttackSpeed,currentTower.AttackRange);
        guiStats.UpdateCoin(numOfCoins,UpgradeStage.CoinRequire);
    }
}

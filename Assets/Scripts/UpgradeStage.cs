using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeStage : MyObject
{
    public Sprite enable;
    public Sprite disnable;
    [HideInInspector]public Tower CurrentTower;
    [HideInInspector]public Player CurrentPlayer;
    public int CoinRequire{
        get => coinRequireLevel[coinRequireIndex];
    }
    public int[] coinRequireLevel;
    private int coinRequireIndex;
    
    public Canvas UpgradeCanvas;
    public bool isActive;

    [Header("Button")]
    public Button   buttonHP;
    public Button   buttonSpeed;
    public Button   buttonPutpickSpeed;
    public Button   buttonDamage;
    public Button   buttonAttackRate;
    public Button   buttonAttackRange;

    private float currentTimeScale;
    protected override void Update() {
        if (gameManager.numOfCoins>=CoinRequire) spriteRenderer.sprite = enable;
        else spriteRenderer.sprite = disnable;
    }
    public void Activate(Player player, Tower tower){
        if (gameManager.numOfCoins>=CoinRequire && isActive == false){
            CurrentPlayer = player;
            CurrentTower = tower;
            buttonHP.interactable = true;
            buttonSpeed.interactable = true;
            buttonPutpickSpeed.interactable = true;
            buttonDamage.interactable = true;
            buttonAttackRange.interactable = true;
            if (CurrentPlayer.currentHPLevel +1 == CurrentPlayer.HPLevel.Length) buttonHP.interactable = false;
            if (CurrentPlayer.currentSpeedLevel +1 == CurrentPlayer.SpeedLevel.Length) buttonSpeed.interactable = false;
            if (CurrentPlayer.currentPutpickSpeedLevel +1 == CurrentPlayer.PutpickSpeedLevel.Length) buttonPutpickSpeed.interactable = false;
            if (CurrentTower.currentDamageLevel +1 == CurrentTower.DamageLevel.Length) buttonDamage.interactable = false;
            if (CurrentTower.currentAttackSpeedLevel +1 == CurrentTower.AttackSpeedLevel.Length) buttonAttackRate.interactable = false;
            if (CurrentTower.currentAttackRangeLevel +1 == CurrentTower.AttackRangeLevel.Length) buttonAttackRange.interactable = false;
            if (!(buttonHP.interactable || buttonSpeed.interactable || buttonPutpickSpeed.interactable || buttonDamage.interactable || buttonAttackRate.interactable || buttonAttackRange.interactable))
                return;
            UpgradeCanvas.gameObject.SetActive(true);
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            Debug.Log("set time scale");
            isActive = true;
        }
    }
    private void updateLevel(){
        gameManager.numOfCoins -= CoinRequire;
        if (coinRequireIndex< coinRequireLevel.Length-1)
            coinRequireIndex +=1;
    }
    public void Deactivate(){
        UpgradeCanvas.gameObject.SetActive(false);
        Debug.Log("reset time scale");
        isActive = false;
        Time.timeScale = currentTimeScale;
        CurrentPlayer.PickObjectUp(CurrentTower);
    }
    public void UpgradeHP(){
        CurrentPlayer.UpgradeHP();
        updateLevel();
        Deactivate();
    }
    public void UpgradeSpeed(){
        CurrentPlayer.UpgradeSpeed();
        updateLevel();
        Deactivate();
    }
    public void UpgradePutpickSpeed(){
        CurrentPlayer.UpgradePutpickSpeed();
        updateLevel();
        Deactivate();
    }
    public void UpgradeDamage(){
        CurrentTower.UpgradeDamage();
        updateLevel();
        Deactivate();
    }
    public void UpgradeAttackRate(){
        CurrentTower.UpgradeAttackRate();
        updateLevel();
        Deactivate();
    }
    public void UpgradeAttackRange(){
        CurrentTower.UpgradeAttackRange();
        updateLevel();
        Deactivate();
    }
}

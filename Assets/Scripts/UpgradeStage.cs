using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeStage : MyObject
{
    public Tower CurrentTower;
    public Player CurrentPlayer;
    public int CoinRequire;
    public Canvas UpgradeCanvas;
    public bool isActive;
    [Header("Maximum level")]
    public int  maxHP = 5;
    public int  maxSpeed = 5;
    public int  maxPutpickSpeed = 5;
    public int  maxDamage = 5;
    public int  maxAttackRate = 5;
    public int  maxAttackRange = 5;
    [Header("Button")]
    public Button   buttonHP;
    public Button   buttonSpeed;
    public Button   buttonPutpickSpeed;
    public Button   buttonDamage;
    public Button   buttonAttackRate;
    public Button   buttonAttackRange;
    private int _HPlevel=0,
                _Speedlevel=0,
                _PutpickSpeedlevel=0,
                _Damagelevel=0,
                _AttackRatelevel=0,
                _AttackRangelevel=0;

    private float currentTimeScale;
    // Start is called before the first frame update
    public void Activate(Player player, Tower tower){
        if (gameManager.numOfCoins>=CoinRequire && isActive == false){
            UpgradeCanvas.gameObject.SetActive(true);
            CurrentPlayer = player;
            CurrentTower = tower;
            if (_HPlevel==maxHP) buttonHP.interactable = false;
            if (_Speedlevel==maxSpeed) buttonSpeed.interactable = false;
            if (_PutpickSpeedlevel==maxPutpickSpeed) buttonPutpickSpeed.interactable = false;
            if (_Damagelevel==maxDamage) buttonDamage.interactable = false;
            if (_AttackRatelevel==maxAttackRate) buttonAttackRate.interactable = false;
            if (_AttackRangelevel==maxAttackRange) buttonAttackRange.interactable = false;
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isActive = true;
        }
    }
    public void Deactivate(){
        UpgradeCanvas.gameObject.SetActive(false);
        isActive = false;
        Time.timeScale = currentTimeScale;
        CurrentPlayer.PickObjectUp(CurrentTower);
    }
    public void UpdateHP(){
        CurrentPlayer.MaxHp +=1;
        _HPlevel+=1;
        Deactivate();
    }
    public void UpdateSpeed(){
        CurrentPlayer.CarrySpeed+=0.4f;
        CurrentPlayer.NormalSpeed+=0.3f;
        _Speedlevel+=1;
        Deactivate();
    }
    public void UpdatePutpickSpeed(){
        CurrentPlayer.TimePutPick-=0.1f;
        _PutpickSpeedlevel+=1;
        Deactivate();
    }
    public void UpdateDamage(){
        _Damagelevel+=1;
        Deactivate();
    }
    public void UpdateAttackRate(){
        _AttackRatelevel+=1;
        Deactivate();
    }
    public void UpdateAttackRange(){
        _AttackRangelevel+=1;
        Deactivate();
    }
}

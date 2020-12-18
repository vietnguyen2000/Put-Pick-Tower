using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BuyMenu : MonoBehaviour{
    
    
    public Button buttonBuy;
    public Text currentCoinsText;
    public Text costText;
    public Text remainingCoinsText;
    public Action buyAction;
    [HideInInspector]public Button buttonRequired;
    [HideInInspector]public int cost;

    private void Start() {
        currentCoinsText.text = SaveLoadManager.Instance.SavedData.coins.ToString();
        costText.text = cost.ToString();
        remainingCoinsText.text = (SaveLoadManager.Instance.SavedData.coins - cost).ToString();
        if (SaveLoadManager.Instance.SavedData.coins >= cost){
            buttonBuy.interactable=true;
        }
        else{
            buttonBuy.interactable= false;
        }
    }
    public void buy(){
        ButtonControl.enableAllActionOnClick(buttonRequired);
        buttonRequired.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
        buyAction();
    }
}
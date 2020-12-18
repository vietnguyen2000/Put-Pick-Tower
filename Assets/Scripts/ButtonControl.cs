using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ButtonControl{
    static GameObject buyMenuGameObject;
    public static void disableAllActionOnClick(Button button){
        for (int index = 0 ; index < button.onClick.GetPersistentEventCount(); index++){
            button.onClick.SetPersistentListenerState(index,UnityEngine.Events.UnityEventCallState.Off);
            Image[] images = button.GetComponentsInChildren<Image>();
            images[images.Length-1].color=Color.black;
        }
    }
    public static void enableAllActionOnClick(Button button){
        for (int index = 0 ; index < button.onClick.GetPersistentEventCount(); index++){
            button.onClick.SetPersistentListenerState(index,UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            Image[] images = button.GetComponentsInChildren<Image>();
            images[images.Length-1].color=Color.white;
        }
    }
    public static void addBuyRequireButton(Button button, int cost, Action buyAction){
        button.onClick.AddListener(()=>openBuyMenu(button,cost,buyAction));
    }
    public static void openBuyMenu(Button button, int cost, Action buyAction){
        if (buyMenuGameObject == null){
            buyMenuGameObject = GameObject.Instantiate(Resources.Load<GameObject>("BuyMenu"));
            buyMenuGameObject.transform.SetParent(button.transform.parent.parent,false);
            buyMenuGameObject.transform.localPosition = Vector3.zero;
        }
        buyMenuGameObject.SetActive(true);
        BuyMenu buyMenu = buyMenuGameObject.GetComponent<BuyMenu>();
        buyMenu.cost = cost;
        buyMenu.buttonRequired = button;
        buyMenu.buyAction = buyAction;
        
    }
}
[System.Serializable]
public class ButtonNeedToBuy{
    public Button button;
    public int cost;
}
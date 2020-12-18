using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChooseSkin : MonoBehaviour
{
    public ButtonNeedToBuy[] skins;
    private Button.ButtonClickedEvent[] defaultClicked;

    private void OnEnable() {

        for( int i= 0 ; i < skins.Length; i++)
        {
            var index = i;
            if (!SaveLoadManager.Instance.SavedData.skins.Contains(skins[index].button.gameObject.name)){
                ButtonControl.disableAllActionOnClick(skins[index].button);
                ButtonControl.addBuyRequireButton(skins[index].button,skins[index].cost,()=>SaveLoadManager.Instance.AddNewlyUnlockedSkin(skins[index].button.gameObject.name));
            }
            else{
                ButtonControl.enableAllActionOnClick(skins[index].button);
            }
        }
    }
    public void chooseSkin(string name){
        GameData.skinChosen = name;
    }
}
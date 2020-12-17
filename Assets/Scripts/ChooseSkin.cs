using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChooseSkin : MonoBehaviour
{
    public Button[] skins;
    private void OnEnable() {
        foreach (var skin in skins)
        {
            Image[] images = skin.GetComponentsInChildren<Image>();
            if (!SaveLoadManager.Instance.SavedData.skins.Contains(skin.gameObject.name)){
                images[images.Length-1].color = Color.black;
                skin.interactable = false;
            }
            else{
                images[images.Length-1].color = Color.white;
                skin.interactable = true;
            }
        }
    }
    public void chooseSkin(string name){
        GameData.skinChosen = name;
    }
}
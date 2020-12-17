using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChooseSkin : MonoBehaviour
{
    public Button[] skins;
    private void Start() {
    
    }
    public void chooseSkin(string name){
        GameData.skinChosen = name;
    }
}
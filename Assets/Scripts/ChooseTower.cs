using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class ChooseTower : MonoBehaviour
{
    public Button[] towers;
    public Text countText;
    private int currentCount = 0;
    private int numofTower = 1;
    private void OnEnable() {
        currentCount = 0;
        numofTower = GameData.numofTower;
        countText.text = "0/"+numofTower.ToString();
        GameData.towerChosen = new string[numofTower];
    }
    public void chooseTower(string name){
        GameData.towerChosen[currentCount] = name;
        currentCount += 1;
        countText.text = currentCount.ToString()+"/"+numofTower.ToString();
        if (currentCount == numofTower){
            SceneManager.LoadScene("Level"+GameData.levelChosen.ToString(),LoadSceneMode.Single);
        }
    }
}
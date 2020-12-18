using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class ChooseTower : MonoBehaviour
{
    public ButtonNeedToBuy[] towers;
    public Text countText;
    private int currentCount = 0;
    private int numofTower = 1;
    private void OnEnable() {
        currentCount = 0;
        numofTower = GameData.numofTower;
        countText.text = "0/"+numofTower.ToString();
        GameData.towerChosen = new string[numofTower];
        for( int i= 0 ; i < towers.Length; i++)
        {
            var index = i;
            if (!SaveLoadManager.Instance.SavedData.towers.Contains(towers[index].button.gameObject.name)){
                ButtonControl.disableAllActionOnClick(towers[index].button);
                ButtonControl.addBuyRequireButton(towers[index].button,towers[index].cost,()=>SaveLoadManager.Instance.AddNewlyUnlockedTower(towers[index].button.gameObject.name));
            }
            else{
                ButtonControl.enableAllActionOnClick(towers[index].button);
            }
        }
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
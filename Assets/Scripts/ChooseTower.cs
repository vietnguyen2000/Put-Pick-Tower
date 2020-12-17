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
        foreach (var tower in towers)
        {
            Image[] images = tower.GetComponentsInChildren<Image>();
            if (!SaveLoadManager.Instance.SavedData.towers.Contains(tower.gameObject.name)){
                images[images.Length-1].color = Color.black;
                tower.interactable = false;
            }
            else{
                images[images.Length-1].color = Color.white;
                tower.interactable = true;
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
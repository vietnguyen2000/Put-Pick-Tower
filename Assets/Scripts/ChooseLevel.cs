using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChooseLevel : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform skinpanel;
    [System.Serializable]
    public class Map{
        public int numOfTower;
    }
    public GameObject button;
    public Map[] maps;
    public int paddingBetweenButton;
    public int pady;

    private int levelChosen;
    private GameObject skinChosen;
    private GameObject[] towerChosen;

    // Start is called before the first frame update
    void Start()
    {
        int numOfElementOneLine = Mathf.FloorToInt(panel.sizeDelta.x / (button.GetComponent<RectTransform>().sizeDelta.x+paddingBetweenButton*2));
        Vector3 firstPos = new Vector3(-panel.sizeDelta.x/2 + paddingBetweenButton + button.GetComponent<RectTransform>().sizeDelta.x , pady,0);
        GameObject lastButton = GameObject.Instantiate(button,Vector3.zero,new Quaternion(),panel.transform);
        lastButton.transform.localPosition = firstPos;
        lastButton.GetComponent<Button>().onClick.AddListener(()=>ButtonLevelClicked(1));
        for (int i = 2; i < maps.Length+1;i++){
            Vector3 position = lastButton.transform.localPosition + new Vector3(button.GetComponent<RectTransform>().sizeDelta.x+paddingBetweenButton,0,0);
            if(i%numOfElementOneLine==1){
                position = firstPos - new Vector3(0,(button.GetComponent<RectTransform>().sizeDelta.y+paddingBetweenButton)*((int)i/numOfElementOneLine));
            }
            GameObject curButton =  GameObject.Instantiate(button,Vector3.zero,new Quaternion(),panel.transform);
            curButton.transform.localPosition = position;
            int num = i;
            curButton.GetComponent<Button>().onClick.AddListener(()=>ButtonLevelClicked(num));
            curButton.GetComponentInChildren<Text>().text = i.ToString();
            lastButton = curButton;
        }
    }
    public void ButtonLevelClicked(int level){
        Debug.Log(level);
        GameData.levelChosen = level;
        GameData.numofTower = maps[level-1].numOfTower;
        skinpanel.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }



    // Update is called once per frame
    
}

public class GameData{
    public static int levelChosen;
    public static int numofTower;
    public static string skinChosen;
    public static string[] towerChosen;

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnCoinManager : MonoBehaviour 
{

    // Start is called before the first frame update
    [HideInInspector]public List<Vector3> availablePlaces;

    public GameObject coinObject;
    public GameManager gameManager;
    public int originalAmount;
    private Coin[] coins;
    int count;
    void Start()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>();
        availablePlaces = gameManager.availablePlaces;
        coins = new Coin[originalAmount];
        for (int i = 0 ; i < originalAmount; i++){
            coins[i] = GameObject.Instantiate(coinObject).GetComponent<Coin>();
            coins[i].availablePlace = availablePlaces.ToArray();
        }
    }
}
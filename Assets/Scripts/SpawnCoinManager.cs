using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnCoinManager : MonoBehaviour 
{

    // Start is called before the first frame update
    public Tilemap tileMap;

    public List<Vector3> availablePlaces;

    public List<Vector3> CoinPos;

    public GameObject coinObject;

    public int originalAmount;
    private Coin[] coins;
    int count;
    void Start()
    {
        //Get tileMap -> parent or playerGround?
        Tilemap[] tileMaps = (Tilemap[])FindObjectsOfType<Tilemap>();
        foreach( var x in tileMaps){
            if (x.gameObject.layer == LayerMask.NameToLayer("Player Ground")){
                tileMap = x;
                break;
            }
        }
        ///Buggy script -> Bi lap vo han o dau do
        
        //tileMap = transform.GetComponent<Tilemap>(); //-> Code khong lay duoc tileMap -> PlayerGround
        if (tileMap == null)
            Debug.Log("tileMap is null");
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
            }
        }
        coins = new Coin[originalAmount];
        for (int i = 0 ; i < originalAmount; i++){
            coins[i] = GameObject.Instantiate(coinObject).GetComponent<Coin>();
            coins[i].availablePlace = availablePlaces.ToArray();
        }

        CoinPos = new List<Vector3>();

    }

    // Update is called once per frame
    void Update()
    {
        if (count < originalAmount)
        {

        }
    }
}
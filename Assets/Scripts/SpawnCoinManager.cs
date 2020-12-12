using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnCoinManager : MonoBehaviour 
{

    // Start is called before the first frame update
    [HideInInspector]public List<Vector3> availablePlaces;

    public GameObject coinObject;

    public int originalAmount;
    private Coin[] coins;
    int count;
    void Start()
    {
        availablePlaces = new List<Vector3>();
        //Get tileMap -> parent or playerGround?
        Tilemap[] tileMaps = (Tilemap[])FindObjectsOfType<Tilemap>();
        foreach( var tileMap in tileMaps){
            if (tileMap.gameObject.layer == LayerMask.NameToLayer("Player Ground")){
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
            }
        }
        coins = new Coin[originalAmount];
        for (int i = 0 ; i < originalAmount; i++){
            coins[i] = GameObject.Instantiate(coinObject).GetComponent<Coin>();
            coins[i].availablePlace = availablePlaces.ToArray();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count < originalAmount)
        {

        }
    }
}
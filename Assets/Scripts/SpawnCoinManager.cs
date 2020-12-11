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
    
    int count;


    void Start()
    {
        //Get tileMap -> parent or playerGround?

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
                else
                {
                    //No tile at "place"
                }
            }
        }

        ///

        CoinPos = new List<Vector3>();
        GetCoinPosition();

        ObjectPooler.Pool coinPool = new ObjectPooler.Pool(coinObject.name, coinObject, originalAmount);
        ObjectPooler.SharedInstance.pools.Add(coinPool);

        for (int i = 0; i < originalAmount;i++)
        {
            SpawnCoin(CoinPos[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count < originalAmount)
        {

        }
    }

    //Random vi tri cua tung coin luc khoi tao
    void GetCoinPosition()
    {
        int i = 0;
        while (i < originalAmount)
        {
            int index = Random.Range(0,availablePlaces.Count);
            if (CheckLayer(availablePlaces[index]))
            {
                CoinPos.Add(availablePlaces[index]);
                i++;
            }
        }
    }
    
    //Raycast xuong de check xem co dung cai layermask ko?
    bool CheckLayer(Vector3 pos)
    {
        LayerMask playerground = LayerMask.GetMask("PlayerGround");
        if (Physics.Raycast(pos, new Vector3(0, 0, 1), 20.0f, playerground))
        {
            return true;
        }

        return false;
    }

    //SpawnCoin tu ObjectPooler
    void SpawnCoin(Vector3 position)
    {
        Debug.Log("Spawning Coins");
        //Spawning coin from ObjectPooler
        GameObject coins = ObjectPooler.SharedInstance.SpawnFromPool(coinObject.name, position, new Quaternion());
        //
    }
}
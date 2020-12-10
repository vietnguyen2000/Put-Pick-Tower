using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterManager : MonoBehaviour 
{
    public enum Spawn_State {Waiting, Counting, Spawning};
    public enum Option {MonsterDead, AfterFirstSpawn, AfterLastSpawn }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform monster;
        public GameObject monsterGameObject;
        public int count;
        public float rate;
        public Transform[] path;
        public Option option = Option.MonsterDead;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public string enemyTag = "Enemy";

    private float searchCountDown = 1f;

    private Spawn_State state = Spawn_State.Counting;



    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        switch(waves[nextWave].option)
        {
            case Option.MonsterDead:
                SpawnAfterMonsterDead();
                break;
            case Option.AfterFirstSpawn:
                SpawnAfterFirstSpawn();
                break;
            case Option.AfterLastSpawn:
                SpawnAfterLastSpawn();
                break;
        }

    }

    //Spawn monster after they are killed.
    private void SpawnAfterMonsterDead()
    {
        if (state == Spawn_State.Waiting)
        {
            if (!MonsterIsAlive(waves))
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != Spawn_State.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    //Spawn monster after x seconds from the first monster spawned
    void SpawnAfterFirstSpawn()
    {
        if (waveCountDown <= 0)
        {
            if (state != Spawn_State.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                WaveCompleted();
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    //Spawn monster after x seconds from the last monster spawned
    void SpawnAfterLastSpawn()
    {
        if (state == Spawn_State.Waiting)
            WaveCompleted();
        if (waveCountDown <= 0)
        {
            if (state != Spawn_State.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                waveCountDown = timeBetweenWaves;
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);

        state = Spawn_State.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnMonster(_wave.monsterGameObject, _wave.path[0], _wave.path);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = Spawn_State.Waiting;

        yield break;
    }

    void SpawnMonster (GameObject _monster, Transform position, Transform[] path)
    {
        Debug.Log("Spawning Enemy: " + _monster.name);
        //Need to be fixed to use the ObjectPooler
        GameObject _mons = GameObject.Instantiate(_monster, position.position, new Quaternion());
        //
        Monster m = _mons.GetComponent<Monster>();
        m.path = path;
    
    }

    bool MonsterIsAlive(Wave[] waves)
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            //Find by tag or by gameobjects? If GameObjects then have to use a dictionary
            if (GameObject.FindGameObjectWithTag(enemyTag) == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave " + nextWave + " Completed");

        state = Spawn_State.Counting;
        waveCountDown = timeBetweenWaves;
        
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves completed");
        }
        else
            nextWave++;

    }

    //Code for ObjectPool

    public ObjectPooler ObjectPool;

    //ObjectPool.pools[i] -> i = 1 type of GameObject
    private void Awake()
    {
        Debug.Log("Awake is called");
        //List<GameObject> temp = new List<GameObject>();
        //ObjectPool = new ObjectPooler();
        //ObjectPool.pools.Capacity = EnemyTypeSize(waves,temp);
        //int[] sized = EnemySize(waves, temp);
        //for (int i = 0; i < ObjectPool.pools.Capacity; i++)
        //{
        //    ObjectPool.pools[i].tag = temp[i].name;
        //    Debug.Log(temp[i].name);
        //    ObjectPool.pools[i].prefab = temp[i];
        //    ObjectPool.pools[i].size = sized[i];
        //}

    }

    //Get the size of List<ObjectPool> ObjectPool.pools
    //Count each type of GameObject
    private int EnemyTypeSize(Wave[] waves, List<GameObject> temp)
    {
        if (waves[0].monsterGameObject != null)
        {
            for (int i = 0; i < waves.Length;i++)
            {
                if (!temp.Contains(waves[i].monsterGameObject))
                {
                    temp.Add(waves[i].monsterGameObject);
                }
            }
            return temp.Count;
        }
        return 0;
    }

    //Get amount for each GameObject
    private int[] EnemySize(Wave[] waves, List<GameObject> temp)
    {
        int[] sized = new int[temp.Count];
        for (int i = 0; i< temp.Count;i++)
        {
            for (int j = 0; j < waves.Length; j++)
            {
                if (temp[i] == waves[j].monsterGameObject) 
                    sized[i] += waves[j].count;
            }
        }
        return sized;
    }

}
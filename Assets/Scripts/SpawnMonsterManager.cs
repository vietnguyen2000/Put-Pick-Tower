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
        public float spawnAfterTime = 3f;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float waveCountDown;
    public string enemyTag = "Enemy";

    private float searchCountDown = 1f;

    private Spawn_State state = Spawn_State.Counting;
    protected GameManager gameManager;
    public GameObject Portal;

    private void Awake()
    {
        Debug.Log("Awake SpawnMonsterManager is called");
        List<GameObject> temp = new List<GameObject>();
        //Debug.Log(ObjectPooler.SharedInstance.pools.Capacity);
        // ObjectPooler.SharedInstance.pools.Capacity = EnemyTypeSize(waves, temp);
        //Debug.Log(ObjectPooler.SharedInstance.pools.Capacity);
        int[] sized = EnemySize(waves, temp);
        for (int i = 0; i < ObjectPooler.SharedInstance.pools.Capacity; i++)
        {
            ObjectPooler.Pool newPool = new ObjectPooler.Pool(temp[i].name, temp[i], sized[i]);
            ObjectPooler.SharedInstance.pools.Add(newPool);
            Debug.Log("Count: " + ObjectPooler.SharedInstance.pools.Count);
            Debug.Log("Enemy pooled: " + temp[i].name);
        }
        ObjectPooler.Pool portalPool = new ObjectPooler.Pool(Portal.name,Portal,10);
        ObjectPooler.SharedInstance.pools.Add(portalPool);
    }
    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = waves[0].spawnAfterTime;
        gameManager = (GameManager)FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextWave != -1){
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

        if (nextWave == -1) //win the game
            return;

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
                if (nextWave == -1) //win the game
                    return;
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
        if (nextWave == -1) //win the game
            return;
        if (state != Spawn_State.Spawning)
        {
            StartCoroutine(SpawnWave(waves[nextWave]));
            waveCountDown = waves[nextWave].spawnAfterTime;
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        GameObject portal = ObjectPooler.SharedInstance.SpawnFromPool(Portal.name, _wave.path[0].position, new Quaternion());
        state = Spawn_State.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnMonster(_wave.monsterGameObject, _wave.path[0], _wave.path);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = Spawn_State.Waiting;
        portal.gameObject.SetActive(false);
        yield break;
    }

    void SpawnMonster (GameObject _monster, Transform position, Transform[] path)
    {
        Debug.Log("Spawning Enemy: " + _monster.name);
        //Need to be fixed to use the ObjectPooler
        GameObject _mons = ObjectPooler.SharedInstance.SpawnFromPool(_monster.name, position.position, new Quaternion());
        //
        Monster m = _mons.GetComponent<Monster>();
        m.path = path;
        m.LivingStatus = LiveObject.Status.Alive;
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
        
        if (nextWave != -1){
            if (nextWave + 1 > waves.Length - 1)
            {
                Debug.Log("All waves completed");
                gameManager.win();
                nextWave = -1;
                this.enabled = false;
            }
            else{
                nextWave++;
                waveCountDown = waves[nextWave].spawnAfterTime;
            }
        }

    }

    //ObjectPool.pools[i] -> i = 1 type of GameObject

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
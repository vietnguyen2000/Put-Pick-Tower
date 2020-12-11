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
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public string enemyTag = "Enemy";

    private float searchCountDown = 1f;

    private Spawn_State state;
    public Option option = Option.MonsterDead;

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
        switch(option)
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
            if (!MonsterIsAlive())
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
        
        GameObject _mons = GameObject.Instantiate(_monster, position.position, new Quaternion());
        _mons.tag = enemyTag;
        //_mons.layer = LayerMask.NameToLayer("Monster");
        Monster m = _mons.GetComponent<Monster>();
        m.path = path;
        m.LivingStatus = LiveObject.Status.Alive;
    }

    bool MonsterIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            //Use tag Enemy, if it is other tag then change this part in the code
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
}
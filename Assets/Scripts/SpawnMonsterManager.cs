using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterManager : MonoBehaviour 
{
    public enum Spawn_State {Waiting, Counting, Spawning};
    public enum Option {MonsterDead, MonsterOut, AfterFirstSpawn, AfterLastSpawn }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform monster;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public string enemyTag = "Enemy";

    private float searchCountDown = 1f;

    private Spawn_State state;

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

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);

        state = Spawn_State.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnMonster(_wave.monster);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = Spawn_State.Waiting;

        yield break;
    }

    void SpawnMonster (Transform _monster)
    {
        Debug.Log("Spawning Enemy: " + _monster.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_monster, _sp.position, _sp.rotation);
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
        Debug.Log("Wave Completed");

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING};

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;




    }
    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    private SpawnState state = SpawnState.COUNTING;


    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));

            }

        }
        else {
            waveCountDown -= Time.deltaTime;
        
        }
    }
    IEnumerator SpawnWave(Wave _wave) {

        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.count; ++i) 
        {

            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);//this can be changed
        }

        state = SpawnState.WAITING;

        yield break;
    
    }
    void SpawnEnemy(Transform _enemy) {

        //spawn Enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}

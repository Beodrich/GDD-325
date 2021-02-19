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

    private float searchCountDown=1f;
    private SpawnState state = SpawnState.COUNTING;


    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (state == SpawnState.WAITING) {
            //check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //begin a new round
                Debug.Log("Wave Completed!");
                return;


            }
            else {
                return;
            }
        }

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
    bool EnemyIsAlive() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("enemy") == null)
            {
                return false;

            }
        }
        return true;
    
    }
    IEnumerator SpawnWave(Wave _wave) {

        state = SpawnState.SPAWNING;
        Debug.Log("Spawning Wave: " + _wave.name);
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
        Instantiate(_enemy, transform.position, transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}

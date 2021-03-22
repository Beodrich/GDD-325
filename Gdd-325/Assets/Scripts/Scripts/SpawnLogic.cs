using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnLogic : MonoBehaviour
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING};

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;




    }
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountDown;

    private float searchCountDown=1f;
    private SpawnState state = SpawnState.COUNTING;
    public static float countOfGolems;

    private bool inBetweenRounds = false;
   [SerializeField] private Text waveNameText;
    [SerializeField] private Text numOfGolemText;

    private Shop shop;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");

        }
        waveCountDown = timeBetweenWaves;
        countOfGolems = this.waves[nextWave].count;

        shop = GameObject.Find("Canvas").GetComponent<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        //FOR DEBUG ONLY- GET RID OF THIS LINE IN FINAL BUILD
       // Debug.Log("There are " + GameObject.FindGameObjectsWithTag("enemy").Length + " golems left in the current wave ");
        waveNameText.text = "Current Wave: "+ this.waves[nextWave].name;
        numOfGolemText.text =  countOfGolems + " Golems";
        if (inBetweenRounds) {
            //do shop system here 
            shop.activateShopUI();
        }
        
        
        if (state == SpawnState.WAITING) {
            //check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();


            }
            else {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING && !inBetweenRounds)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));

            }

        }
        else {
            waveCountDown -= Time.deltaTime;
        
        }
    }
    void WaveCompleted() {
        //begin a new round
        //if we want to add a shop system we might want to do it in this function
        if (!Shop.hasBoughItem)
        {
            //Debug.Log("In between rounds");
            inBetweenRounds = true;
            return;
        }
        else {
            inBetweenRounds = false;
        }
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! LOOPING");
            countOfGolems = this.waves[nextWave].count;
        }
        else
        {
            nextWave++;
            countOfGolems = this.waves[nextWave].count;
        }
    }
    bool EnemyIsAlive() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("enemy").Length==0)
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
            Transform enemy = _wave.enemy[Random.Range(0, _wave.enemy.Length)];
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1f / _wave.rate);//this can be changed
        }

        state = SpawnState.WAITING;

        yield break;
    
    }
    void SpawnEnemy(Transform _enemy) {

        //spawn Enemy
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var newGolem=Instantiate(_enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
        newGolem.tag = "enemy";
        //Debug.Log("Spawning Enemy: " + newGolem.name + " At spawn point " + randomSpawnPoint.name);
        newGolem.gameObject.SetActive(true);
    }
}

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
        if (waveCountDown <= 0) {
            
        
        }
    }
}

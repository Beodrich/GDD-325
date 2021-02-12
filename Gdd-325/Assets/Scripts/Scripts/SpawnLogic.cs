using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    public Transform[] spawnLocation;
    public Golem golem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //choose a random location
        int spawnPoint = Random.Range(0, 1);
    }
}

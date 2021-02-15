using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    [SerializeField] private Golem golem;
    [SerializeField] private int enemiesSpawned;
    [SerializeField] private int maxEnemies;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Golem golemInst = Instantiate(golem, golem.getRandomSpawnPoint(), Quaternion.identity);
        golemInst.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log(golemInst);
    }
}

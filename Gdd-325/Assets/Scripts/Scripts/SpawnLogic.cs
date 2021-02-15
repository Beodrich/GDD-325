using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    [SerializeField] private Golem golem;
    // Start is called before the first frame update
    void Start()
    {

        //Golem golemInst = Instantiate(golem, golem.getRandomSpawnPoint(), Quaternion.identity);
        //golemInst.GetComponent<SpriteRenderer>().enabled = true;
        //Debug.Log(golemInst);
    }

    // Update is called once per frame
    void Update()
    {
        int ranGolem = Random.Range(0, 1);
        Golem golemInst = Instantiate(golem, golem.getRandomSpawnPoint(), Quaternion.identity);
        golemInst.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log(golemInst);
    }
}

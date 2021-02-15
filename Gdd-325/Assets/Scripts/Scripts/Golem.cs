using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health = 10.0f;
    [SerializeField] private Sprite[] golemState;
    [SerializeField] private Vector3[] spawnPoints;
    public Spells spells;

    public Vector3 getRandomSpawnPoint() {
        int random = Random.Range(0, 4);
        return this.spawnPoints[random];
    }
    public Sprite getGolemState()
    {

        return golemState[0];
    }
    void Update()
    {
        Debug.Log(health);
    }

    public void TakeDamage()
    {
        if (spells.fire == true)
        {
            health -= 5;
        }
        /*
        if (spells.ice == true)
        {
            health -= 5;
        }
        if (spells.earth == true)
        {
            health -= 5;
        }
        if (spells.air == true)
        {
            health -= 5;
        }
        if (spells.base == true)
        {
            health -= 5;
        }*/

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDamaged()
    {
        CopyController.health -= 10;
    }
}

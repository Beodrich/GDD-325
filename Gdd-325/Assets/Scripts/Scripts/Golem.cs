using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health;
    [SerializeField] private Sprite[] golemState;
    [SerializeField] private Vector3[] spawnPoints;

    public Vector3 getRandomSpawnPoint() {
        int random = Random.Range(0, 4);
        return this.spawnPoints[random];
    }
    public Sprite getGolemState()
    {

        return golemState[0];
    }
}

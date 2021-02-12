using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Golem", menuName = "ScriptableObjects/Golem")]
public class Golem : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health;
    [SerializeField] private Sprite[] golemState;
    [SerializeField] private Vector2[] spawnPoints;

}

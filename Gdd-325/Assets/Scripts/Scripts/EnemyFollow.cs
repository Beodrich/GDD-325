using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 5f;
    private Transform playerPos;
    private Rigidbody2D rb;
    List<Rigidbody2D> enemyRb;
    private float repealRange = 1f;
    public float moveRange = 0.25f;
    private void Awake()
    {
        playerPos = GameObject.Find("MonkE").transform;
        rb = GetComponent<Rigidbody2D>();

        if (enemyRb == null) {
            enemyRb = new List<Rigidbody2D>();
        
        }
        enemyRb.Add(rb);
    }
    private void OnDestroy()
    {
        enemyRb.Remove(rb);
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > moveRange) {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        
        }
    }
    private void FixedUpdate()
    {
        Vector2 repealForce = Vector2.zero;
        foreach (Rigidbody2D enemy in enemyRb) {

            if (enemy == rb) {

                continue;
            }
            if (Vector2.Distance(enemy.position, rb.position) <= repealRange) {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repealForce += repelDir;
            }
        
        }
    }
}

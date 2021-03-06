using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    private ShootingEnemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MonkE").transform;
        target = new Vector2(player.position.x, player.position.y);
        enemy = GameObject.Find("shooting enemy 1(Clone)").GetComponent<ShootingEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //can eaither be targer or player pos
       
        
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        


        
        if (transform.position.x == target.x && transform.position.y == target.y) {
            DestroyProjectile();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            DestroyProjectile();
        }
    }
    void DestroyProjectile() {

        Destroy(gameObject);
    }
}

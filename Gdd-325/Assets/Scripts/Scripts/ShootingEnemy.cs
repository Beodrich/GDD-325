using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ShootingEnemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public float time;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    private  bool isClose = false;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        //if in a certain range stop moving and shoot for a certian amount of seconds
        float distance = Vector2.Distance(this.transform.position, player.position);
        //Debug.Log(distance);
        if (distance < stoppingDistance) {


            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<AIPath>().enabled = false;
            isClose = false;
            startTimeBtwShots = 0.5f;

        }
        else {
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<AIPath>().enabled = true;
            isClose = true;
            startTimeBtwShots = 2f;

        }



        ////move towards postion
        //if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        //}
        //stop moving
        // if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance) {
        //    transform.position = this.transform.position;

        //}
        //else if (Vector2.Distance(transform.position, player.position) < retreatDistance){
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);



        //}
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position ,Quaternion.identity);
          timeBtwShots = startTimeBtwShots;
        }
        else {
            timeBtwShots -= Time.deltaTime;

        }
    }
    public bool getIsClose() {

        return isClose;
    }

}

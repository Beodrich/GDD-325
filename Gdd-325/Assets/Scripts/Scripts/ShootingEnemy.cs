using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ShootingEnemy : MonoBehaviour
{
    //public float speed;
    //public float stoppingDistance;
   // public float retreatDistance;
    public Transform player;
    public bool isShoot = false;
    public float time;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    private  bool isClose = false;
    IAstarAI ai;
    public float radius;
    private Vector2 point;
    private bool canShoot = true;
    private bool canMove = true;
   [SerializeField] private Vector2 min;
   [SerializeField]private Vector2 max;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        ai = GetComponent<IAstarAI>();
        ai.destination = PickRandomPoint();
        ai.SearchPath();

    }

    // Update is called once per frame

    Vector2 PickRandomPoint() {
        point= new Vector2 (Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        
        //point.y = 0;
        //point += (Vector2)ai.position;
        return point;
    }
    void ChangeAmin()
    {

    }
    void Update()
    {
        /* //if in a certain range stop moving and shoot for a certian amount of seconds
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

         }*/



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

        //if ai has reached its path stop it from moving and fire
        //else choose another path
        /* if (!ai.pathPending &&  !ai.hasPath && !canShoot )
         {
             ai.destination = PickRandomPoint();
             ai.SearchPath();
             canShoot = true;
             canMove = true;
         }*/
        /*  if (timeBtwShots <= 0 && canShoot )
          {

              StartCoroutine(ShootTime());
              timeBtwShots = startTimeBtwShots;

          }
          else {
              timeBtwShots -= Time.deltaTime;

          }*/
        //Debug.Log(timeBtwShots);
        if (ai.reachedDestination) {

            Shoot();
           
           
            ai.SearchPath();
        }
        if (timeBtwShots > 0) {
            timeBtwShots -= Time.deltaTime;
        
        }


    }
    IEnumerator ShootTime() {

        if (timeBtwShots <= 0)
        {


            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else {
           
        
        
        }
            //timeBtwShots -= Time.deltaTime;
        
       
        yield return new WaitForSeconds(2f);
        isShoot = false;
        ai.destination = PickRandomPoint();




    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point, radius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("enemy")) {
            Debug.Log("collison");
            ai.destination = PickRandomPoint();
            ai.SearchPath();

        }
    

    }

    //shoot
    public void Shoot() {

        isShoot = true;
        StartCoroutine(ShootTime());
    
    }
}

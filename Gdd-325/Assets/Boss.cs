using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
enum BossStates { 
    spawning,
    rolling,
    waiting,
    none


}
[RequireComponent(typeof(AnimatorLogic))]
[RequireComponent(typeof(Rigidbody2D))]

public class Boss : MonoBehaviour
{
    public float attackDistance = 1.5f;
    private Transform target;
   
    private int heath = 12;
    public float speed = 5f;
    private bool isAttack = false;
    public Transform player;
    public Transform[] golemsToSpawn;
    public float rate = 1f;
    private BossStates state = BossStates.none;
    private SpawnLogic spawn;
    private IAstarAI ai;
    private AnimatorLogic animator;
    private bool canMove=true;
    private Rigidbody2D rb;
    private Vector2 direction;
    private PlayerController monkE;
    //animations states
    private const string golem_Bowling_State = "BowlingState";
    private const string golem_Up_State = "Boss_Up";
    private const string golem_Right_State = "Boss_Right";
    private const string golem_Left_State = "Boss_Left";
    private const string golem_Down_State = "Boss_Move";
    // Start is called before the first frame update
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.Find("SpawnControl").GetComponent<SpawnLogic>();
        ai = GetComponent<IAstarAI>();
        animator = GetComponent<AnimatorLogic>();
        rb = GetComponent<Rigidbody2D>();
        monkE = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {

       

        if (canMove)
        {
            direction = target.position - transform.position;
            direction = direction.normalized;
            //1.spawns in stuff
            //2. rolls towards the players next know location until it hits a wall
            //3 stunned for x seconds
            //4. repeate 1-4 till dead
            //Debug.Log("has finished spawning " + spawn.getIsSpawning());
            BowlingAttack();
            
        }


        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")) {

            //stun state
            animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            StartCoroutine(StunTime());
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // if the golem hits the player, it damages the player and starts the new wave of enemies
            animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            // damage player
            monkE.TakeDamage(5);
            // start new wave
        }
    }

    public void BowlingAttack() {
        rb.velocity = direction * speed;
        canMove = false;
        animator.ChangeAnimationState(golem_Bowling_State);




    }
    IEnumerator StunTime() {

        yield return new WaitForSeconds(2f);
        canMove = true;
    
    }




}

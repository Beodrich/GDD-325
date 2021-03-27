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

    private bool canSpawn = false;
    public float speed = 5f;
    private bool isAttack = false;
    public Transform player;
    public Transform[] golemsToSpawn;
    public float rate = 1f;
    private BossStates state = BossStates.none;
    private SpawnLogic spawn;
    private AnimatorLogic animator;
    private bool canMove=true;
    private Rigidbody2D rb;
    private Vector2 direction;
    private PlayerController monkE;
    private bool canDamage = false;
    private float health = 50f;
    //animations states
    private const string golem_Bowling_State = "BowlingState";
    private const string golem_Up_State = "Boss_Up";
    private const string golem_Right_State = "Boss_Right";
    private const string golem_Left_State = "Boss_Left";
    private const string golem_Down_State = "Boss_Move";
    // Player Attack  Damage
    public float golemInitFireDamage = 2, golemInitIceDamage = 3, golemInitWindDamage = 4, golemInitEarthDamage = 3;
    public float fireDamage = 1, fireDuration = 2;
    public bool startIce;
    public float initialIceTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.Find("SpawnControl").GetComponent<SpawnLogic>();
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
        if (!canSpawn) {
            canMove = true;
        
        }
        if (canDamage)
        {

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
            canSpawn = true;

        }
    }

    public void BowlingAttack() {
        rb.velocity = direction * speed;
        canMove = false;
        animator.ChangeAnimationState(golem_Bowling_State);

    }
    IEnumerator StunTime() {

        yield return new WaitForSeconds(4f);
        canMove = true;
    
    }
    public float getHP() {
        return health;
    
    }

    public void TakeDamage()
    {
        if (monkE.isFire())
        {
            health -= golemInitFireDamage;
            DamageOverTime(fireDamage, fireDuration);
        }
        if (monkE.isIce())
        {
            health -= golemInitIceDamage;
            startIce = true;
        }
        if (monkE.isWind())
        {
            health -= golemInitWindDamage;
        }
        if (monkE.isEarth())
        {
            health -= golemInitEarthDamage;
        }
    }

    // FIRE
    // Calling Damage over time and assigning amount of damage and how long it burns for
    public void DamageOverTime(float damage, float damageTime)
    {
        StartCoroutine(DamageOverTimeCoroutine(damage, damageTime));
    }
    //FIRE
    // Damage Over Time
    IEnumerator DamageOverTimeCoroutine(float damageAmount, float time)
    {
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / time;
        while (amountDamaged < damageAmount)
        {
            health -= damagePerLoop;


            Debug.Log(health.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    //ICE
    // Calling Slow Golem and assigning time the golem is slowed for
    public void SlowGolem(float slowAmount, float maxTime)
    {
        //Debug.Log(initialIceTime);

        if (initialIceTime >= maxTime)
        {
            initialIceTime = 0f;
            speed = 5;
            startIce = false;
        }
        else
        {
            initialIceTime += Time.deltaTime;
            speed *= slowAmount;
            if (speed < 2.5f)
            {
                speed = 2.5f;
            }
        }
    }
    public bool getIsSpawn() {

        return canSpawn;
    }
    public void setIsSpawn(bool value) {

        this.canSpawn = value;
    }




}

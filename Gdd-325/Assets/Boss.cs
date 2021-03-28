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
    //spawn logic 
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform golem;
    [SerializeField] private float count = 5f;
    [SerializeField] private float rate;
    [SerializeField] private float stunAmount = 2f;
    private float searchCountDown = 1f;
    public float attackDistance = 1.5f;
    private Transform target;
    [SerializeField] AudioSource audio;
    private bool canSpawn = false;
    public float speed = 5f;
    private bool isAttack = false;
    private bool shitMonkE=false;
    private bool isCurrentlySpawning = false;
    public Transform player;
    public Transform[] golemsToSpawn;
    private BossStates state = BossStates.none;
    private SpawnLogic spawn;
    [SerializeField]private AnimatorLogic animator;
    private bool canMove=false;
    [SerializeField]private Rigidbody2D rb;
    private Vector2 direction;
    [SerializeField] private PlayerController monkE;
    private bool canDamage = false;
    [SerializeField] private float health = 50f;
    [SerializeField] private float meleeDamage = 10f;
    //animations states
    private const string golem_Bowling_State = "BowlingState";
    private const string golem_Up_State = "Boss_Up";
    private const string golem_Right_State = "Boss_Right";
    private const string golem_Left_State = "Boss_Left";
    private const string golem_Down_State = "Boss_Move";
    private Vector2 bossDirection;
    // Player Attack  Damage
    public float golemInitFireDamage = 2, golemInitIceDamage = 3, golemInitWindDamage = 4, golemInitEarthDamage = 3;
    public float fireDamage = 1, fireDuration = 2;
    public bool startIce;
    public float initialIceTime = 0;
    private  float Max_Health;
    //ui stuff
    [SerializeField] private GameObject bossText;
    [SerializeField] private GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.Find("SpawnControl").GetComponent<SpawnLogic>();
        spawn.enabled = false;
        animator = GetComponent<AnimatorLogic>();
       // rb = GetComponent<Rigidbody2D>();
        monkE = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Max_Health = health;
        bossText.SetActive(true);
        bar.SetActive(true);
        StartCoroutine(BossIntro());

    }
    IEnumerator BossIntro() {
        yield return new WaitForSeconds(5f);
        canMove = true;
    
    
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

        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && isCurrentlySpawning) {
            canMove = true;
            canSpawn = false;
            isCurrentlySpawning = false;
        }

        if (canSpawn) {
            canSpawn = false;
            canMove = false;
            Invoke("WaitForSpawn", 2f);
        }
        ChangeAnimation();
        BossState();

        
       

    }
    private void ChangeAnimation()
    {
        bossDirection = monkE.transform.position - this.transform.position;
        bossDirection = bossDirection.normalized;

        if (shitMonkE)
        {
            animator.ChangeAnimationState(golem_Bowling_State);
        }
        else
        {

            if (bossDirection.x <= -0.9f)
            {
                animator.ChangeAnimationState(golem_Left_State);

            }
            else if (bossDirection.x >= 0.9f)
            {
                animator.ChangeAnimationState(golem_Right_State);

            }
            else if (bossDirection.y >= 0.9f)
            {
                animator.ChangeAnimationState(golem_Up_State);
            }
            else if (bossDirection.y <= -0.9f)
            {
                animator.ChangeAnimationState(golem_Down_State);
            }
        }
    }

        private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")) {

            //stun state
            animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            StartCoroutine(StunTime());
            shitMonkE = false;
            audio.Play();
            canDamage = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // if the golem hits the player, it damages the player and starts the new wave of enemies
            //animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            // damage player
            monkE.TakeDamage(5);
            monkE.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // start new wave
            canSpawn = true;
            shitMonkE = false;
            audio.Play();
            canDamage = false;
        }
    }
    void WaitForSpawn() {

        StartCoroutine(SpawnWave());
    }
    public void BowlingAttack() {
        rb.velocity = direction * speed;
        canMove = false;
        shitMonkE = true;
        animator.ChangeAnimationState(golem_Bowling_State);

    }
    IEnumerator StunTime() {

        yield return new WaitForSeconds(stunAmount);
        canMove = true;
        canDamage = false;
    
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
            else if (monkE.isIce())
            {
                health -= golemInitIceDamage;
                startIce = true;
            }
            else if (monkE.isWind())
            {
                health -= golemInitWindDamage;
            }
            else if (monkE.isEarth())
            {
                health -= golemInitEarthDamage;
            }
        CheckForDeath();
    }
    public void MeleeDamge() {
        if (canDamage) { 
        health -= meleeDamage;
        CheckForDeath();
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
    IEnumerator SpawnWave()
    {
        isCurrentlySpawning = true;

        //Debug.Log("Spawning Wave: " + _wave.name);
        //Spawn
        for (int i = 0; i <count; ++i)
        {
            
            SpawnEnemy(golem);
            yield return new WaitForSeconds(1f /rate);//this can be changed
        }


        yield break;

    }
    void SpawnEnemy(Transform _enemy)
    {
        //spawn Enemy
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var newGolem = Instantiate(_enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
        newGolem.tag = "enemy";
        //Debug.Log("Spawning Enemy: " + newGolem.name + " At spawn point " + randomSpawnPoint.name);
        newGolem.gameObject.SetActive(true);
    }

    bool isGolemDead() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
            {
                Debug.Log("all golems are not");

                return true;

            }
        }
        return false;

    }
    void CheckForDeath() {

        if (health <= 0) {
            //play death animation
            Destroy(this.gameObject);
        
        
        }
    
    }

    public bool isCanDamage()
    {
        return canDamage;
    }
    void BossState() { 
        if(health<41 && health > 31)
            {
                speed = 12;
                count = 3;
                rate = 2;
            }
        else if(health<31 && health > 21)
        {
            speed = 14;
            count = 5;
            rate = 3;
        }
        else if (health < 21 && health > 11)
        {
            speed = 16;
            count = 8;
            rate = 4;
        }
        else if(health < 11 && health > 0)
        {
                speed = 20;
                count = 15;
                rate = 5;
        }


    }
    public float getMaxHP() {

        return Max_Health;
    }

}



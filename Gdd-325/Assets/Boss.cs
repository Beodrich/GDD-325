using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    [SerializeField] AudioSource damageSound;
    private bool canSpawn = false;
    public float speed = 5f;
    private bool isAttack = false;
    private bool isBowling=false;
    private bool isCurrentlySpawning = false;
    public Transform player;
   
    
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
    private const string golem_Bowling_Down = "BowlingDown"; 
    private const string golem_Bowling_Up = "BowlingUp";
    private const string golem_Bowling_Left = "BowlingLeft";
    private const string golem_Bowling_Right = "BowlingRight";
    private const string golem_Up_State = "Boss_Up";
    private const string golem_Right_State = "Boss_Right";
    private const string golem_Left_State = "Boss_Left";
    private const string golem_Down_State = "Boss_Move";
    //animator change var
    [Range(0.1f, 1)]
    [SerializeField] private float animationRange=0.1f;
    private Vector2 bossDirection;
    // Player Attack  Damage
    public float golemInitFireDamage = 2, golemInitIceDamage = 3, golemInitWindDamage = 4, golemInitEarthDamage = 3;
    public float fireDamage = 1, fireDuration = 2;
    public bool startIce;
    public float initialIceTime = 0;
    private  float Max_Health;
    Vector2 preVelocity=Vector2.zero;

    
    //ui stuff
    [SerializeField] private GameObject bossText;
    [SerializeField] private GameObject bar;
    [SerializeField] private Text currentWaveText;
    [SerializeField] private Text numOfGolemsLeft;
    // Start is called before the first frame update
    void Start()
    {
        damageSound = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.Find("SpawnControl").GetComponent<SpawnLogic>();
        spawn.enabled = false;
        animator = GetComponent<AnimatorLogic>();
       // rb = GetComponent<Rigidbody2D>();
        monkE = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Max_Health = health;
        currentWaveText.enabled = false;
        numOfGolemsLeft.enabled = false;
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
        //Debug.Log("is bowling -------> " + isBowling);
        CheckForDeath();
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
           // StartCoroutine(AfterGolemsDie());
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

       // Debug.Log("is bowling -----> " + isBowling);


        if (!isBowling)
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
        if (other.gameObject.CompareTag("wall"))
        {

            //stun state
            animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            StartCoroutine(StunTime());
            isBowling = false;
            canDamage = true;
        }
        //this fixes a bug where the boss stoped moving if a projectile hits it while in a bowling state
        else if (other.gameObject.CompareTag("projectile")&& isBowling) {
            //keep on moving
            rb.velocity = preVelocity;
            


        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // if the golem hits the player, it damages the player and starts the new wave of enemies
            //animator.ChangeAnimationState(golem_Up_State);
            rb.velocity = Vector2.zero;
            // damage player
            monkE.TakeDamage(3);
            monkE.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // start new wave
            canSpawn = true;
            isBowling = false;
            canDamage = false;

        }
       }
    void WaitForSpawn() {

        StartCoroutine(SpawnWave());
    }
    public void BowlingAttack() {
        rb.velocity = direction * speed;
        preVelocity = rb.velocity;
        canMove = false;
        isBowling = true;
        //animator.ChangeAnimationState(golem_Bowling_Down);
        Debug.Log(direction);
       // Debug.Log("is bowling ------> " + isBowling);
            //animator.ChangeAnimationState(golem_Bowling_Down);
            if (direction.x <= -animationRange)
            {
                animator.ChangeAnimationState(golem_Bowling_Left);

            }
             if (direction.x >= animationRange)
            {
                animator.ChangeAnimationState(golem_Bowling_Right);

            }
             if (direction.y >= animationRange)
            {
                animator.ChangeAnimationState(golem_Bowling_Up);
            }
             if (direction.y <= -animationRange)
            {
                animator.ChangeAnimationState(golem_Bowling_Down);
            }
         if (direction.y >= animationRange && direction.x <= -animationRange)
        {
            animator.ChangeAnimationState(golem_Bowling_Up);

        }

         if (direction.y >= animationRange && direction.x >= animationRange)
        {
            animator.ChangeAnimationState(golem_Bowling_Right);


        }
         if (direction.y <= -animationRange && direction.x <= -animationRange)
        {
            animator.ChangeAnimationState(golem_Bowling_Down);

        }
         if (direction.y <= animationRange && direction.x >= animationRange)
        {
            animator.ChangeAnimationState(golem_Bowling_Right);

        }
      



    }
    IEnumerator StunTime() {

        yield return new WaitForSeconds(stunAmount);
        canMove = true;
        canDamage = false;
    
    }
    IEnumerator AfterGolemsDie() { 
    yield  return new WaitForSeconds(3f);
        canMove = true;
        canSpawn = false;
        isCurrentlySpawning = false;


    }
    public float getHP() {
        return health;
    
    }
    public void setHP(float hp)
    {
        this.health = hp;

    }
    /// <summary>
    /// take damamge function 
    /// </summary>
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
        damageSound.Play();
        
    }
    public void MeleeDamge() {
        if (canDamage) { 
        health -= meleeDamage;
        
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
        newGolem.GetComponent<Golem>().ReScanPath();
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

            canMove = false;
            StartCoroutine(DeadBoss());
        
        }
    
    }
    IEnumerator DeadBoss()
    {
        //TODO: play death animation

        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        SceneManager.LoadScene("YouWin");
    }

    public bool isCanDamage()
    {
        return canDamage;
    }
    void BossState() { 
        if(health<91 && health > 71)
        {
            speed = 14;
            count = 5;
            rate = 3;
            stunAmount = 1.75f;
        }
        else if(health<71 && health > 51)
        {
            speed = 16;
            count = 8;
            rate = 4;
            stunAmount = 1.5f;
        }
        else if (health < 51 && health > 31)
        {
            speed = 20;
            count = 15;
            rate = 5;
            stunAmount = 1.25f;
        }
        else if(health < 31 && health > 0)
        {
            speed = 22;
            count = 18;
            rate = 6;
            stunAmount = 1f;
        }


    }
    public float getMaxHP() {

        return Max_Health;
    }

}



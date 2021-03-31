using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

[RequireComponent(typeof(AnimatorLogic))]
public class Golem : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private AIPath golemPath;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health = 10.0f;
    private float maxHeath;
    [SerializeField] private Transform[] nextGolemToTransform;
    private PlayerController player;
    public float fireDamage;
    public float fireDuration;
    public float golemInitFireDamage = 2f;
    public float golemInitIceDamage = 4f;
    public float golemInitWindDamage = 4f;
    public float golemInitEarthDamage = 4f;
    //public float iceDuration;
    public float iceSlowed = 0.5f;
    private float initialIceTime = 0;
    public float maxIceTime = 3;
    private bool startIce = false;
    private float topSpeed;
    private float currentSpeed;
    //for player melee
    private int length;

    private static float numOfGolems;
    [SerializeField] private Text numOfGolemText;
    //GOLEM ANIMATIONS
    private string golem_Right;
    private string golem_Left;
    private string golem_Up;
    private string golem_Down;
    [Range(0.0F, 1F)]
    [SerializeField] private float animationChangeRange = 0.9f;
    private AnimatorLogic anim;


    private float dazedTime;
    [SerializeField] private float startDazedTime;
    private void Start()
    {
       
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        //moveSpeed = GetComponent<AIPath>().maxSpeed;
        golemPath = GetComponent<AIPath>();
        topSpeed = golemPath.maxSpeed;
        currentSpeed = topSpeed;
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.name == "crawler(Clone)")
        {
            golem_Right = "Crawler_Right";
            golem_Down = "Crawler_Down";
            golem_Left = "Crawler_Left";
            golem_Up = "Crawler_Up";

        }
        else if (gameObject.name == "shooting enemy (Clone)") {
            golem_Right = "Golem_Shoot_Right";
            golem_Left = "Golem_Shoot_Left";
            golem_Down = "Golem_Shoot_Down";
            golem_Up = "Golem_Shoot_Up";
        
        }
        else if (gameObject.name == "Crouchie(Clone)")
        {
            golem_Right = "Golem_Crouch_Right";
            golem_Left = "Golem_Crouch_Left";
            golem_Down = "Golem_Crouch_Down";
            golem_Up = "Golem_Crouch_Up";

        }
        else if (gameObject.name == "Golem(Clone)")
        {
            golem_Right = "Golem_Right";
            golem_Left = "Golem_Left";
            golem_Down = "Golem_Down";
            golem_Up = "Golem_UP";

        }
        anim = GetComponent<AnimatorLogic>();
    }
    public Transform getGolemState()
    {



        return null;
        
        
    }
    private void changeGolem() {
      

        if (nextGolemToTransform != null )
        {

          var newGolem=  Instantiate(nextGolemToTransform[Random.Range(0, nextGolemToTransform.Length)], this.transform.position, Quaternion.identity);
          
            newGolem.gameObject.SetActive(true);

        }
        else {

            SpawnLogic.countOfGolems -= 1;
        }
        
    
    }
    void Update()
    {
        Debug.Log(health);
        //for melee
        if (dazedTime <= 0)
        {
            currentSpeed = topSpeed;
            golemPath.maxSpeed = topSpeed;


        }
        else
        {
            currentSpeed = 0;
            golemPath.maxSpeed = 0;
            dazedTime -= Time.deltaTime;

        }
        //Debug.Log(health);
        if (health <= 0)
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            
            string name = this.gameObject.name;
            Destroy(this.gameObject);
           
                changeGolem();
            
            
        }
        if (startIce)
        {
            SlowGolem(iceSlowed, maxIceTime);
        }
        //MAKE SURE ANIMATIONS NAMES ARE ALL THE SAME 
        Vector2 golemVector = golemPath.desiredVelocity.normalized;
        //Debug.Log(golemVector);
        if (golemVector.x <= -animationChangeRange)
        {
            anim.ChangeAnimationState(golem_Left);

        }
        else if (golemVector.x > animationChangeRange)
        {
            anim.ChangeAnimationState(golem_Right);

        }
         if (golemVector.y > animationChangeRange)
         {

            anim.ChangeAnimationState(golem_Up);

         }
        else if (golemVector.y <= -animationChangeRange)
        {
            anim.ChangeAnimationState(golem_Down);

        }
    }

        public Transform getPosition()
        {
        return this.transform;
        }

    public void TakeDamage()
    {
        if(player.isFire())
        {
            health -= golemInitFireDamage;
            DamageOverTime(fireDamage,fireDuration);
        }
        if (player.isIce())
        {
            health -= golemInitIceDamage;
            startIce = true; 
        }
        if(player.isWind())
        {
            health -= golemInitWindDamage;
        }
        if (player.isEarth())
        {
            health -= golemInitEarthDamage;
        }

    }
    public void TakeMeleeDamage(float amount) {
        health -= amount;
        dazedTime = startDazedTime;
    
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
        while(amountDamaged < damageAmount)
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
            golemPath.maxSpeed = 5;
            startIce = false;
        }
        else
        {
            initialIceTime += Time.deltaTime;
            golemPath.maxSpeed *= slowAmount;
            if (golemPath.maxSpeed < 2.5f)
            {
                golemPath.maxSpeed = 2.5f;
            }
        }
    }


/*    public void TakeDamage(SpellState spell)
    {
        switch (spell) {
            case SpellState.Fire:
                //code here
                health -= 5;
                break;
            case SpellState.Ice:
                //code here
                break;
            case SpellState.Earth:
                //code
                break;
            case SpellState.Air:
                //code
                break;
            default://none case
                    //code here
                break;
        }
    }*/

    public void PlayerDamaged()
    {
        player.TakeDamage(attackDamage);
    }
    public float getGolemHp() {
        return health;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //play golem attack animation here 
            PlayerDamaged();
        
        
        }
    }
    public void ReScanPath() {
        AstarPath.active = FindObjectOfType<AstarPath>(); AstarPath.active.Scan();


    }
}

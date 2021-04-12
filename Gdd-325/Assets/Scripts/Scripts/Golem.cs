using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

[RequireComponent(typeof(GolemAnimations))]
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
        // Debug.Log(health);
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
        
    }

        
    IEnumerator DOT()
    {
        float amountDamaged = 0;
        float damagePerLoop = fireDamage / fireDuration;
        Debug.Log(amountDamaged + "<- Amount Damaged");
        Debug.Log(damagePerLoop + "<- Damaged Per Loop");
        while (amountDamaged < fireDamage)
        {
            health -= damagePerLoop;


            Debug.Log(health.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    public void TakeDamage()
    {
        if(player.isFire())
        {
            health -= golemInitFireDamage;
            //DamageOverTime(fireDamage,fireDuration);
            //StartCoroutine(DamageOverTimeCoroutine(fireDamage, fireDuration));
            StartCoroutine(DOT());
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
        Debug.Log(amountDamaged + "<- Amount Damaged");
        Debug.Log(damagePerLoop + "<- Damaged Per Loop");
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
            if (golemPath.maxSpeed <= 1)
            {
                golemPath.maxSpeed = 1f;
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
//when not moving attack?
    public void PlayerDamaged()
    {
        player.TakeDamage(attackDamage);
    }
    public float getGolemHp() {
        return health;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    public void ReScanPath() {
        AstarPath.active = FindObjectOfType<AstarPath>(); AstarPath.active.Scan();


    }
}

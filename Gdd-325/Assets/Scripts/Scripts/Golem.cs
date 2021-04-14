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

    /// <summary>
    /// Does damage over time depending on how much damage wants to be done and for how long
    /// and then takes that health away from enemy
    /// </summary>
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

    /// <summary>
    /// Depending on the wand equiped, does that type of damage
    /// </summary>
    public void TakeDamage()
    {
        if(player.isFire())
        {
            health -= golemInitFireDamage;
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

    /// <summary>
    /// Calling Slow Golem and assigning time the golem is slowed for
    /// </summary>
    /// <param name="slowAmount"></param>
    /// <param name="maxTime"></param>
    public void SlowGolem(float slowAmount, float maxTime)
    {

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

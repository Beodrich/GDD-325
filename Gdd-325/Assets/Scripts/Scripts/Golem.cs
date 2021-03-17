using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Golem : MonoBehaviour
{
    private AIPath golemPath;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health = 10.0f;
    private float maxHeath;
    [SerializeField] private Sprite[] golemState;
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
   

    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        //moveSpeed = GetComponent<AIPath>().maxSpeed;
        golemPath = GetComponent<AIPath>();
        
       
    }
    public Sprite getGolemState()
    {
        return golemState[0];
    }
    
    void Update()
    {
        //Debug.Log(health);
        if (health <= 0)
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SpawnLogic.countOfGolems -= 1;
        }
        if (startIce)
        {
            SlowGolem(iceSlowed, maxIceTime);
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
            //health -= golemInitIceDamage;
        }
        if (player.isEarth())
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerDamaged();
        
        
        }
    }
}

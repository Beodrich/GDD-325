using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health = 10.0f;
    [SerializeField] private Sprite[] golemState;
    private PlayerController player;
    public float fireDamage;
    public float fireDuration;

    //public float iceDuration;
    public float iceSlowed;


    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
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
            health -= 2;
            DamageOverTime(fireDamage,fireDuration);
        }
        if (player.isIce())
        {
            health -= 3;
            SlowGolem(iceSlowed);
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
    public void SlowGolem(float slowAmount)
    {
        moveSpeed *= slowAmount;
        if (moveSpeed <= 2.5f)
        {
            moveSpeed = 2.5f;
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
        player.health -= 10;
    }
}

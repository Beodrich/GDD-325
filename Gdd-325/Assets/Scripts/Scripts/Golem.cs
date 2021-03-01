using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spells;

public class Golem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float health = 10.0f;
    [SerializeField] private Sprite[] golemState;
   
    //public Spells spells;

   
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
        if(fire)
        {
            this.health -= 5;
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
        PlayerController.health -= 10;
    }
}

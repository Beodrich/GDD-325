using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private PlayerController player;
    private WeaponWand wand;
    private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack=0.3f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange=1.4f;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private int damage;

    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        wand = GetComponent<WeaponWand>();
        
    }

    /// <summary>
    /// the update funcation handles player melee for the player 
    
    /// </summary>
    void Update()
    {
       
        if (timeBtwAttack <= 0)
        {
            //count down timer 
            timeBtwAttack = startTimeBtwAttack;
            //if the user presses the space bar
            if (Input.GetKey(KeyCode.Space)) {
                //figure out enemies that are overlaping 
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                //loop through the enemeis array
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    var currentGolem = enemiesToDamage[i];
                    if (currentGolem.name == "Boss")
                    {
                        currentGolem.GetComponent<Boss>().MeleeDamge();
                        

                    }
                    else
                    {
                        enemiesToDamage[i].GetComponent<Golem>().TakeMeleeDamage(damage);
                       



                    }
                }
            
            
            }
        }
        else {
            //decrase the count down timer
            timeBtwAttack -= Time.deltaTime;
        
        
        }



    
    
    
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
 
}
